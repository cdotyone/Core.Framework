using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Civic.Core.Data;
using Civic.T4.WebApi.OData;

namespace Civic.T4.WebApi
{
    public static class OData3FilterBuilder
    {
        /// <summary>
        /// This expands OData filter string into SQL compatible where and matching stored procedure parameters
        /// </summary>
        /// <param name="command">The command to add the parameters to</param>
        /// <param name="filter">odata3 - $filter filter expression</param>
        /// <param name="properties">the property name map</param>
        /// <returns>Where clause</returns>
        public static string ParseExpression(IDBCommand command, string filter, string[] properties)
        {
            var whereList = new List<string>();

            var parser = new Parser();
            var expression = parser.Parse(filter,new List<string>(properties));

            var where = expandExpression(command, expression, properties, whereList);
            return where;
        }

        /// <summary>
        /// This expands OData filter string into SQL compatible where and matching stored procedure parameters
        /// </summary>
        /// <param name="command">The command to add the parameters to</param>
        /// <param name="expression">The root expression</param>
        /// <param name="properties">the property name map</param>
        /// <returns>Where clause</returns>
        //public static string ExpandExpression(IDBCommand command, IExpression expression, string[] properties)
        //{
        //    var whereList = new List<string>();
        //    var where = expandExpression(command, expression, properties, whereList);
        //    return where;
        //}

        private static string expandExpression(IDBCommand command, IExpression expression, string[] properties, List<string> whereList)
        {
            var sb = new StringBuilder();

            if (expression.Type == ExpressionTypes.Criteria)
            {
                var ce = expression as CriteriaExpression;
                if (ce == null) throw new Exception("error using parsed query");
                int pos = 0;
                foreach (var entityProperty in properties)
                {
                    if (entityProperty.ToLower() == ce.Name.ToLower())
                    {
                        if (pos > 0) sb.Append(" and ");
                        pos++;
                        var name = addParameter(entityProperty, ce.Value, whereList, command);

                        switch (ce.Operator)
                        {
                            case FieldOperator.Like:
                                sb.AppendFormat("{0} like '%'+{1}+'%'", entityProperty, name);
                                break;
                            case FieldOperator.Equal:
                                sb.AppendFormat("{0} = {1}", entityProperty, name);
                                break;
                            case FieldOperator.NotEqual:
                                sb.AppendFormat("{0} <> {1}", entityProperty, name);
                                break;
                            case FieldOperator.GreaterThan:
                                sb.AppendFormat("{0} >= {1}", entityProperty, name);
                                break;
                            case FieldOperator.GreaterOrEqualThan:
                                sb.AppendFormat("{0} > {1}", entityProperty, name);
                                break;
                            case FieldOperator.LessThan:
                                sb.AppendFormat("{0} < {1}", entityProperty, name);
                                break;
                            case FieldOperator.LessOrEqualThan:
                                sb.AppendFormat("{0} <= {1}", entityProperty, name);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                }
            }

            if (expression.Type == ExpressionTypes.AndCriteria)
            {
                var andExpression = expression as AndCriteriaExpression;

                if (andExpression != null)
                {
                    sb.Append("(" + expandExpression(command, andExpression.LeftExpression, properties, whereList));
                    sb.Append(" and ");
                    sb.Append(expandExpression(command, andExpression.NestedExpression, properties, whereList) + ")");
                }
            }

            if (expression.Type == ExpressionTypes.OrCriteria)
            {
                var andExpression = expression as OrCriteriaExpression;

                if (andExpression != null)
                {
                    sb.Append("(" + expandExpression(command, andExpression.LeftExpression, properties, whereList));
                    sb.Append(" or ");
                    sb.Append(expandExpression(command, andExpression.NestedExpression, properties, whereList) + ")");
                }
            }

            return sb.ToString();
        }

        private static string addParameter(string name, string value, List<string> whereList, IDBCommand command)
        {
            name = name.ToLower();
            int pos = whereList.IndexOf(name);
            if (pos >= 0) return "@_val" + (pos + 1).ToString(CultureInfo.InvariantCulture);

            pos = whereList.Count;
            whereList.Add(name);

            name = "@_val" + (pos + 1).ToString(CultureInfo.InvariantCulture);
            command.AddInParameter(name, value);

            return name;
        }


    }
}

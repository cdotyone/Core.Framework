using System;
using System.Linq.Expressions;

namespace Core.Framework.OData.Parse
{
    public class FunctionExpression : CriteriaExpression
    {
        private readonly String _functionName;

        public FunctionExpression(String functionName, ParametersExpression parametersExpression)
        {
            _functionName = functionName;
            Parameters = parametersExpression;
            //criteria = FunctionBuilder.parseCriteria(this);
            //log.debug("Add " + criteria);
        }

        public override ExpressionTypes Type => ExpressionTypes.Function;

        public Expression Expression
        { 
            get; 
            private set; 
        }

        public override String Name 
        {
            get { return _functionName; }
        }

        public ParametersExpression Parameters { get; }
    }
}
using System;

namespace Core.Framework.OData.Parse
{

    public class CriteriaExpression : IExpression {

        public CriteriaExpression() {
        }

        public CriteriaExpression(String criteriaName, FieldOperatorNode fieldOperator) {
            Name = criteriaName;
            Operator = fieldOperator.FieldOperator;
            Value = fieldOperator.Value;
        }

        public virtual ExpressionTypes Type
        {
            get { return ExpressionTypes.Criteria; }
        }

        public IExpression NestedExpression { get; set; }

        public virtual string Name { get; set; }

        public FieldOperator Operator { get; set; }

        public string Value { get; set; }
    }

}

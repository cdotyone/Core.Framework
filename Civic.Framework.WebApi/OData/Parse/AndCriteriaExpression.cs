namespace Civic.Framework.WebApi.OData
{
    public class AndCriteriaExpression : IExpression
    {
        public AndCriteriaExpression(IExpression left,IExpression right)
        {
            LeftExpression = left;
            NestedExpression = right;
        }

        public AndCriteriaExpression(IExpression left)
        {
            LeftExpression = left;
        }

        public ExpressionTypes Type
        {
            get { return ExpressionTypes.AndCriteria; }
        }

        public IExpression NestedExpression { get; set; }

        public IExpression LeftExpression { get; set; }
    }
}

namespace Civic.Framework.WebApi.OData
{
    public class OrCriteriaExpression : IExpression
    {
        public OrCriteriaExpression(IExpression left,IExpression right)
        {
            LeftExpression = left;
            NestedExpression = right;
        }

        public OrCriteriaExpression(IExpression left)
        {
            LeftExpression = left;
        }

        public ExpressionTypes Type
        {
            get { return ExpressionTypes.OrCriteria; }
        }

        public IExpression NestedExpression { get; set; }

        public IExpression LeftExpression { get; set; }

    }
}

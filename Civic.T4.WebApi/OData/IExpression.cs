namespace Civic.T4.WebApi.OData
{
    public interface IExpression
    {
        ExpressionTypes Type { get; }

        IExpression NestedExpression { get; set; }
    }

}

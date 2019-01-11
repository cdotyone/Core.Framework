namespace Civic.Framework.WebApi.OData
{
    public interface IExpression
    {
        ExpressionTypes Type { get; }

        IExpression NestedExpression { get; set; }
    }

}

namespace Framework.Core.OData.Parse
{
    public interface IExpression
    {
        ExpressionTypes Type { get; }

        IExpression NestedExpression { get; set; }
    }

}

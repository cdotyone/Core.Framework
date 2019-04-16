namespace Stack.Core.Framework.OData.Parse
{
    public interface IExpression
    {
        ExpressionTypes Type { get; }

        IExpression NestedExpression { get; set; }
    }

}

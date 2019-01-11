﻿namespace Framework.Core.OData.Parse
{
    public enum TokenTypes
    {
        Criteria, 
        FieldOperator, 
        Value, 
        OpenBracket, 
        CloseBracket, 
        AndOperator, 
        OrOperator, 
        Epsilon, 
        String, 
        Function, 
        Separator
    }
}

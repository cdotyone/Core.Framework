﻿using System;
using System.Linq.Expressions;

namespace Civic.T4.WebApi.OData
{
    public class FunctionExpression : CriteriaExpression
    {
        private readonly String _functionName;
        private readonly ParametersExpression _parametersExpression;

        public FunctionExpression(String functionName, ParametersExpression parametersExpression)
        {
            _functionName = functionName;
            _parametersExpression = parametersExpression;
            //criteria = FunctionBuilder.parseCriteria(this);
            //log.debug("Add " + criteria);
        }

        public ExpressionTypes Type
        {
            get { return ExpressionTypes.Function; }
        }

        public Expression Expression
        { 
            get; 
            private set; 
        }

        public String Name 
        {
            get { return _functionName; }
        }

        public ParametersExpression Parameters
        {
            get { return _parametersExpression; }
        }
    }
}
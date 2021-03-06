﻿using System;

namespace Core.Framework.OData.Parse
{
    public class ParserException :Exception
    {
        public ParserException(string message) : base(message)
        {
            
        }

        public ParserException(Exception innerEx) : base("Error parsing string", innerEx)
        {

        }

        public ParserException(string message, Exception innerEx)
            : base(message, innerEx)
        {
        }
    }
}

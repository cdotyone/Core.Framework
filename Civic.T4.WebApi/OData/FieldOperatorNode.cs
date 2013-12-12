using System;

namespace Civic.T4.WebApi.OData
{
    public class FieldOperatorNode {

        private String fieldOperator;
        private String _value;

        public FieldOperatorNode(String fieldOperation, string value)
        {
            fieldOperator = fieldOperation.ToLower();
            Value = value;
        }
        
        public string Value {
            get { return _value; }
            set { _value = value; }
        }

        public FieldOperator FieldOperator {
            get
            {
                switch (fieldOperator)
                {
                    case "like":
                        return FieldOperator.Like;
                    case "eq":
                        return FieldOperator.Equal;
                    case "neq":
                        return FieldOperator.NotEqual;
                    case "gt":
                        return FieldOperator.GreaterThan;
                    case "ge":
                        return FieldOperator.GreaterOrEqualThan;
                    case "lt":
                        return FieldOperator.LessThan;
                    case "le":
                        return FieldOperator.LessOrEqualThan;
                }

                throw new ParserException("Invalid FieldOperator found " + fieldOperator);
            }
        }
        
        public override String ToString() {
                return string.Format("[{0},{1}]", FieldOperator, Value);
        }
    }
}

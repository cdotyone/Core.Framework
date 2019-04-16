using System;

namespace Stack.Core.Framework.OData.Parse
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
                    case "eq":
                        return FieldOperator.Equal;
                    case "ne":
                        return FieldOperator.NotEqual;
                    case "gt":
                        return FieldOperator.GreaterThan;
                    case "ge":
                        return FieldOperator.GreaterOrEqualThan;
                    case "lt":
                        return FieldOperator.LessThan;
                    case "le":
                        return FieldOperator.LessOrEqualThan;
                    case "like":
                        return FieldOperator.Like;
                    case "nk":
                        return FieldOperator.NotLike;
                    case "is":
                        return FieldOperator.Is;
                    case "in":
                        return FieldOperator.In;
                    case "nin":
                        return FieldOperator.NotIn;
                }

                throw new ParserException("Invalid FieldOperator found " + fieldOperator);
            }
        }
        
        public override String ToString() {
                return string.Format("[{0},{1}]", FieldOperator, Value);
        }
    }
}

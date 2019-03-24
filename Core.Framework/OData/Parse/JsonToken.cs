using System;

namespace Core.Framework.OData.Parse
{
    public class JsonToken
    {
        public TokenTypes TokenType { get; private set; }

        public string Sequence { get; private set; }

        public JsonToken(TokenTypes tokenType, String sequence)
        {
                TokenType = tokenType;
                Sequence = sequence;
        }

        public override String ToString() {
            return String.Format("[{0}, {1}]", TokenType, Sequence);
        }
    }
}

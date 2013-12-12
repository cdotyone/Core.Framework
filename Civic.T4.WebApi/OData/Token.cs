using System;

namespace Civic.T4.WebApi.OData
{
    public class Token
    {
        public TokenTypes TokenType { get; private set; }

        public string Sequence { get; private set; }

        public Token(TokenTypes tokenType, String sequence)
        {
                TokenType = tokenType;
                Sequence = sequence;
        }

        public override String ToString() {
            return String.Format("[{0}, {1}]", TokenType, Sequence);
        }
    }
}

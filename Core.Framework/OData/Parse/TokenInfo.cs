using System;
using System.Text.RegularExpressions;

namespace Stack.Core.Framework.OData.Parse
{
    internal class TokenInfo {

        public Regex Regex { get; private set; }
        
        public TokenTypes TokenType { get; private set; }
        
        public TokenInfo(Regex regex, TokenTypes tokenType) {
            Regex = regex;
            TokenType = tokenType;
        }

        public override String ToString() {
            return String.Format("[{0},{1}]", Regex, TokenType);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Civic.Framework.WebApi.OData
{
    public class Tokenizer
    {

        private readonly List<TokenInfo> _tokenInfos;
        private static readonly List<TokenInfo> _tokenLex;
        private readonly List<JsonToken> _tokens;

        static Tokenizer()
        {
            _tokenLex = new List<TokenInfo>();

            add(@"\(", TokenTypes.OpenBracket);
            add(@"\)", TokenTypes.CloseBracket);
            add(@"or\s", TokenTypes.OrOperator);
            add(@"and\s", TokenTypes.AndOperator);
            add(@"\s*,\s*", TokenTypes.Separator);
            add(@"(eq|neq|gt|ge|lt|le|like)", TokenTypes.FieldOperator);
            add(@"'.+?[']", TokenTypes.String);
            add(@"(-?[0-9]+(\.[0-9]+)?|true|false)", TokenTypes.Value);
        }
        
        public Tokenizer(IEnumerable<string> fields,IEnumerable<string> functions)
        {
            _tokenInfos = new List<TokenInfo>();
            _tokens = new List<JsonToken>();

            if (fields != null) Add(@"(" + string.Join("|", fields) + @")\s", TokenTypes.Criteria);
            if (functions != null) Add(@"(" + string.Join("|", functions) + @")\s", TokenTypes.Function);
            _tokenInfos.AddRange(_tokenLex);
        }

        private static void add(String regex, TokenTypes stringLiteral)
        {
            _tokenLex.Add(new TokenInfo(new Regex("^" + regex, RegexOptions.None), stringLiteral));
        }

        public void Add(String regex, TokenTypes stringLiteral)
        {
            if (stringLiteral == TokenTypes.Criteria)
                _tokenInfos.Add(new TokenInfo(new Regex("^" + regex, RegexOptions.IgnoreCase), stringLiteral));
            else
                _tokenInfos.Add(new TokenInfo(new Regex("^" + regex, RegexOptions.None), stringLiteral));
        }

        public void Tokenize(String str)
        {
            String s = str.Trim();
            _tokens.Clear();

            while (s != "")
            {
                bool match = false;
                foreach (TokenInfo info in _tokenInfos)
                {
                    Match m = info.Regex.Match(s+" ");

                    if (m.Success)
                    {
                        match = true;
                        TokenTypes tokenType;
                        String tok = m.Groups[0].Value.Trim();
                        if (info.TokenType == TokenTypes.String)
                        {
                            if (tok.StartsWith("'"))
                            {
                                tok = tok.Trim('\'');
                                
                            }
                            tokenType = TokenTypes.Value;
                        }
                        else
                        {
                            tokenType = info.TokenType;
                        }

                        s = info.Regex.Replace(s+" ", "").Trim();
                        _tokens.Add(new JsonToken(tokenType, tok));
                        break;
                    }
                }

                if (!match)
                {
                    throw new ParserException("Unexpected token \"" + s.Split(' ')[0] + "\" in input: " + str);
                }
            }
        }

        public List<JsonToken> Tokens
        {
            get { return _tokens; }
        }

    }
}

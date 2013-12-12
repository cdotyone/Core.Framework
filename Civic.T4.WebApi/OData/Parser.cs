using System;
using System.Collections.Generic;
using System.Linq;

namespace Civic.T4.WebApi.OData
{
    public class Parser
    {
        Queue<Token> _tokens;
        Token _lookahead;

        public IExpression Parse(String expression, List<string> fields, List<string> functions = null)
        {
            if(fields == null) throw new ArgumentNullException("fields");
            if(fields.Count<0) throw new ArgumentException("fields must contain at least one field name","fields");

            var tokenizer = new Tokenizer(fields,functions==null || functions.Count == 0 ? null : functions);
            tokenizer.Tokenize(expression);
            return Parse(tokenizer.Tokens);
        }

        public IExpression Parse(List<Token> tokens)
        {
            _tokens = new Queue<Token>(tokens);
            _lookahead = _tokens.First();
            return expression();
        }

        private void nextToken()
        {
            _tokens.Dequeue();
            if (_tokens.Count==0)
            {
                _lookahead = new Token(TokenTypes.Epsilon, "");
            }
            else
            {
                _lookahead = _tokens.First();
            }
        }

        // expression -> or_term or_op
        // or_op -> OR_OPERATOR or_term or_op
        // or_op -> EPSILON
        private IExpression expression()
        {
            // expression -> or_term or_op
            IExpression orTermExpression = orTerm();
            return orOp(orTermExpression);
        }

        // or_op -> OR_OPERATOR or_term or_op
        // or_op -> EPSILON
        private IExpression orOp(IExpression expression)
        {
            if (_lookahead.TokenType == TokenTypes.OrOperator)
            {
                // or_op -> OR_OPERATOR or_term or_op
                OrCriteriaExpression orCriteria;
                if (expression.Type == ExpressionTypes.OrCriteria)
                {
                    orCriteria = (OrCriteriaExpression)expression;
                }
                else
                {
                    orCriteria = new OrCriteriaExpression(expression);
                }
                nextToken();
                IExpression orTermExpression = orTerm();
                orCriteria.NestedExpression = orTermExpression;
                return orOp(orCriteria);
            }
            // or_op -> EPSILON
            return expression;
        }

        // or_term -> and_term and_op
        // and_op -> AND_OPERATOR and_term and_op
        // and_op -> EPSILON
        private IExpression orTerm()
        {
            // or_term -> and_term and_op
            IExpression andTermExpression = andTerm();
            return andOp(andTermExpression);
        }

        // and_op -> AND_OPERATOR and_term and_op
        // and_op -> EPSILON
        private IExpression andOp(IExpression expression)
        {
            if (_lookahead.TokenType == TokenTypes.AndOperator)
            {
                // and_op -> AND_OPERATOR and_term and_op
                AndCriteriaExpression andCriteria;
                if (expression.Type == ExpressionTypes.AndCriteria)
                {
                    andCriteria = (AndCriteriaExpression)expression;
                }
                else
                {
                    andCriteria = new AndCriteriaExpression(expression);
                }
                nextToken();
                IExpression andTermExpression = andTerm();
                andCriteria.NestedExpression = andTermExpression;
                return andOp(andCriteria);
            }
            // and_op -> EPSILON
            return expression;
        }

        // and_term -> CRITERIA field_op
        // and_term -> FUNCTION func_op
        // and_term -> argument
        private IExpression andTerm()
        {
            if (_lookahead.TokenType == TokenTypes.Criteria)
            {
                // and_term -> CRITERIA field_op
                String criteria = _lookahead.Sequence;
                nextToken();
                FieldOperatorNode fieldOperator = fieldOp();
                return new CriteriaExpression(criteria, fieldOperator);
            }
            
            if (_lookahead.TokenType == TokenTypes.Function)
            {
                // and_term -> FUNCTION func_op
                String function = _lookahead.Sequence;
                nextToken();
                ParametersExpression parametersExpression = funcOp();
                return new FunctionExpression(function, parametersExpression);
            }
            
            // and_term -> argument
            return argument();
        }

        // func_op -> OPEN_BRACKET parameter CLOSE_BRACKET
        private ParametersExpression funcOp()
        {
            if (_lookahead.TokenType == TokenTypes.OpenBracket)
            {
                // func_op -> OPEN_BRACKET parameter CLOSE_BRACKET
                nextToken();
                var parametersExpression = new ParametersExpression();
                parameter(parametersExpression);
                if (_lookahead.TokenType != TokenTypes.CloseBracket)
                {
                    throw new ParserException("Closing bracket expected and " + _lookahead.Sequence + " found instead");
                }
                nextToken();
                return parametersExpression;
            }
            
            throw new ParserException("Open bracket expected and " + _lookahead.Sequence + " found instead");
        }

        // parameter -> FUNCTION func_op param_op
        // parameter -> value param_op
        private void parameter(ParametersExpression parametersExpression)
        {
            if (_lookahead.TokenType == TokenTypes.Function)
            {
                // parameter -> FUNCTION func_op param_op
                String function = _lookahead.Sequence;
                nextToken();
                ParametersExpression nestedParameters = funcOp();
                var functionExpression = new FunctionExpression(function, nestedParameters);
                parametersExpression.NestedExpression = functionExpression.NestedExpression;
                paramOp(parametersExpression);
            }
            else
            {
                // parameter -> value param_op
                FieldOperatorNode fieldOperatorNode = paramOp(parametersExpression);
                fieldOperatorNode.Value = value();
                parametersExpression.AddParameter(fieldOperatorNode);
            }
        }

        // param_op -> SEPARATOR FIELD_OPERATOR param_op
        // param_op -> SEPARATOR parameter
        // param_op -> EPSILON
        private FieldOperatorNode paramOp(ParametersExpression parametersExpression)
        {
            if (_lookahead.TokenType == TokenTypes.Separator)
            {
                nextToken();
                if (_lookahead.TokenType == TokenTypes.FieldOperator)
                {
                    // param_op -> SEPARATOR FIELD_OPERATOR param_op
                    String fieldOperator = _lookahead.Sequence;
                    nextToken();
                    paramOp(parametersExpression);
                    return new FieldOperatorNode(fieldOperator, null);
                }
                
                // param_op -> SEPARATOR parameter
                parameter(parametersExpression);
            }

            // param_op -> EPSILON
            return new FieldOperatorNode("eq", null);
        }

        // field_op -> FIELD_OPERATOR value
        private FieldOperatorNode fieldOp()
        {
            if (_lookahead.TokenType == TokenTypes.FieldOperator)
            {
                // field_op -> FIELD_OPERATOR value
                String fieldOperator = _lookahead.Sequence;
                nextToken();
                return new FieldOperatorNode(fieldOperator, value());
            }
            
            throw new ParserException("FieldOperator expected and " + _lookahead.Sequence + " found instead");
        }

        // argument -> OPEN_BRACKET expression CLOSE_BRACKET
        private IExpression argument()
        {
            if (_lookahead.TokenType == TokenTypes.OpenBracket)
            {
                // argument -> OPEN_BRACKET expression CLOSE_BRACKET
                nextToken();
                IExpression result = expression();
                if (_lookahead.TokenType != TokenTypes.CloseBracket)
                {
                    throw new ParserException("Closing bracket expected and " + _lookahead.Sequence + " found instead");
                }
                nextToken();
                return result;
            }
            
/*            if (_lookahead.TokenType == TokenTypes.Name)
            {
                nextToken();
                IExpression result = expression();
                return result;
            }*/

            throw new ParserException("Open bracket expected and " + _lookahead.Sequence + " found instead");
        }

        // value -> VALUE
        private string value()
        {
            if (_lookahead.TokenType == TokenTypes.Value)
            {
                var value = _lookahead.Sequence;
                // value -> VALUE
                nextToken();
                return value;
            }
            
            throw new ParserException("String, number or date expected and " + _lookahead.Sequence + " found instead");
        }
    }
}

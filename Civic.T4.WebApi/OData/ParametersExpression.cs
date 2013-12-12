using System.Collections.Generic;

namespace Civic.T4.WebApi.OData
{
    public class ParametersExpression : CriteriaExpression {

        private readonly List<FieldOperatorNode> _fieldOperatorNodes;

        public ParametersExpression() {
            _fieldOperatorNodes = new List<FieldOperatorNode>();
        }

        public ParametersExpression(IExpression nestedCriteria, List<FieldOperatorNode> parameters)
        {
            NestedExpression = nestedCriteria;
            _fieldOperatorNodes = parameters;
        }

        public void AddParameter(FieldOperatorNode parameter) {
            _fieldOperatorNodes.Insert(0, parameter);
        }

        public List<FieldOperatorNode> FieldOperatorNodes {
         get {
            return _fieldOperatorNodes;
            }
        }
    }
}

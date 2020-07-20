using System;
using System.Collections.Generic;
using System.Text;

namespace SIM.Neo4j.Cypher
{
    public class NodeIdentifier : CypherComponent
    {
        private string _id;
        private Type _label;
        private CypherPattern _pattern;

        internal NodeIdentifier(CypherPattern pattern)
        {
            _pattern = pattern;
        }

        internal NodeIdentifier(CypherPattern pattern, string id) : this(pattern, id, null)
        {

        }

        internal NodeIdentifier(CypherPattern pattern, Type label) : this(pattern, null, label)
        {

        }

        internal NodeIdentifier(CypherPattern pattern, string id, Type label) : this(pattern)
        {
            _id = id;
            _label = label;
        }

        public CypherPattern All()
        {
            return _pattern;
        }

        internal override string AsPainCypher()
        {
            throw new NotImplementedException();
        }

        internal override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}

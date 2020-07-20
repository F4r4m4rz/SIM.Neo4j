using System;
using System.Collections.Generic;
using System.Text;
using SIM.Neo4j.Abstractions;

namespace SIM.Neo4j.Cypher
{
    public class NodeIdentifier : CypherPatternComponent
    {
        internal NodeIdentifier(CypherPattern pattern) : base(pattern)
        {
            _symbol = "(#id# #labels#)";
            _pattern.Nodes.Enqueue(this);
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

        internal override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}

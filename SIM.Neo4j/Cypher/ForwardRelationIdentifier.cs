using System;
using System.Collections.Generic;
using System.Text;

namespace SIM.Neo4j.Cypher
{
    public class ForwardRelationIdentifier : RelationIdentifier
    {
        internal ForwardRelationIdentifier(CypherPattern pattern) : base(pattern)
        {
            _symbol = $"{_symbol}>";
        }

        internal ForwardRelationIdentifier(CypherPattern pattern, string id) : base(pattern, id)
        {
            _symbol = $"{_symbol}>";
        }

        internal ForwardRelationIdentifier(CypherPattern pattern, Type label) : base(pattern, label)
        {
            _symbol = $"{_symbol}>";
        }

        internal ForwardRelationIdentifier(CypherPattern pattern, string id, Type label) : base(pattern, id, label)
        {
            _symbol = $"{_symbol}>";
        }
    }
}

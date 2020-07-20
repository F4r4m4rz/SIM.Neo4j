using System;
using System.Collections.Generic;
using System.Text;

namespace SIM.Neo4j.Cypher
{
    public class BackwardRelationIdentifier : RelationIdentifier
    {
        internal BackwardRelationIdentifier(CypherPattern pattern) : base(pattern)
        {
            _symbol = $"<{_symbol}";
        }

        internal BackwardRelationIdentifier(CypherPattern pattern, string id) : base(pattern, id)
        {
            _symbol = $"<{_symbol}";
        }

        internal BackwardRelationIdentifier(CypherPattern pattern, Type label) : base(pattern, label)
        {
            _symbol = $"<{_symbol}";
        }

        internal BackwardRelationIdentifier(CypherPattern pattern, string id, Type label) : base(pattern, id, label)
        {
            _symbol = $"<{_symbol}";
        }
    }
}

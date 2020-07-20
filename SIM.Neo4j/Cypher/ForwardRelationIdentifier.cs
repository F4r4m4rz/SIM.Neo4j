using System;
using System.Collections.Generic;
using System.Text;

namespace SIM.Neo4j.Cypher
{
    public class ForwardRelationIdentifier : RelationIdentifier
    {
        internal ForwardRelationIdentifier(CypherPattern pattern) : base(pattern)
        {
        }

        internal ForwardRelationIdentifier(CypherPattern pattern, string id) : base(pattern, id)
        {
        }

        internal ForwardRelationIdentifier(CypherPattern pattern, Type label) : base(pattern, label)
        {
        }

        internal ForwardRelationIdentifier(CypherPattern pattern, string id, Type label) : base(pattern, id, label)
        {
        }
    }
}

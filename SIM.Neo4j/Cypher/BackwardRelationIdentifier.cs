using System;
using System.Collections.Generic;
using System.Text;

namespace SIM.Neo4j.Cypher
{
    public class BackwardRelationIdentifier : RelationIdentifier
    {
        internal BackwardRelationIdentifier(CypherPattern pattern) : base(pattern)
        {

        }

        internal BackwardRelationIdentifier(CypherPattern pattern, string id) : base(pattern, id)
        {

        }

        internal BackwardRelationIdentifier(CypherPattern pattern, Type label) : base(pattern, label)
        {

        }

        internal BackwardRelationIdentifier(CypherPattern pattern, string id, Type label) : base(pattern, id, label)
        {

        }
    }
}

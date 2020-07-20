using System;
using System.Collections.Generic;
using System.Text;

namespace SIM.Neo4j.Cypher
{
    public class RelationIdentifier : CypherPatternComponent
    {
        internal RelationIdentifier(CypherPattern pattern) : base(pattern)
        {
            _symbol = "-[#id# #labels#]-";
            _pattern.Relations.Enqueue(this);
        }

        internal RelationIdentifier(CypherPattern pattern, string id) : this(pattern, id, null)
        {

        }

        internal RelationIdentifier(CypherPattern pattern, Type label) : this(pattern, null, label)
        {

        }

        internal RelationIdentifier(CypherPattern pattern, string id, Type label) : this(pattern)
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

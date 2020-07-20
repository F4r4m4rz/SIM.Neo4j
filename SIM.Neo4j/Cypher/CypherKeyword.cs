using SIM.Neo4j.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIM.Neo4j.Cypher
{
    public class CypherKeyword : CypherComponent
    {
        private CypherCommand _command;
        private Queue<CypherPattern> _patterns;

        internal CypherKeyword(CypherCommand command)
        {
            _command = command;
            _command.Keywords.Enqueue(this);
        }

        public CypherPattern Pattern()
        {
            return new CypherPattern(this);
        }

        internal override string AsPainCypher()
        {
            throw new NotImplementedException();
        }

        internal override void Validate()
        {
            throw new NotImplementedException();
        }

        public CypherCommand Close()
        {
            return _command;
        }

        internal Queue<CypherPattern> Patterns { get => _patterns; }
    }
}

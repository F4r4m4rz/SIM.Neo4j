using SIM.Neo4j.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIM.Neo4j.Cypher
{
    public class CypherKeyword : CypherComponent
    {
        protected CypherCommand _command;
        protected Queue<CypherPattern> _patterns;


        private CypherKeyword()
        {
            _patterns = new Queue<CypherPattern>();
        }

        internal CypherKeyword(CypherCommand command) : this()
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
            var _return = _symbol;
            var initCount = _patterns.Count;
            while (_patterns.Count > 0)
            {
                var format = _patterns.Count == initCount ? "{0} {1}" : "{0},{1}";
                var str = _patterns.Dequeue().AsPainCypher();
                _return = string.Format(format, _return, str);
            }
            return _return;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIM.Neo4j.Cypher
{
    public class ReturnCommand : CypherKeyword
    {
        private ICollection<string> _ids;

        internal ReturnCommand(CypherCommand command, params string[] ids) : base(command)
        {
            _symbol = "RETURN";
            _ids = ids;
        }

        internal override string AsPainCypher()
        {
            var _return = _symbol;
            for (int i = 0; i < _ids.Count; i++)
            {
                var format = i == 0 ? "{0} {1}" : "{0},{1}";
                _return = string.Format(format, _return, _ids.ElementAt(i));
            }
            _return = string.Concat(_return, "\n");
            return _return;
        }
    }
}

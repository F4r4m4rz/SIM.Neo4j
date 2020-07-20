using SIM.Neo4j.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Neo4j.Cypher
{
    public class CypherCommand : CypherComponent
    {
        private ICollection<string> _occupiedIds;
        private Queue<CypherKeyword> _keywords;

        internal CypherCommand()
        {
            _keywords = new Queue<CypherKeyword>();
        }

        public MatchCommand Match()
        {
            return new MatchCommand(this);
        }

        public ReturnCommand Return(params string[] ids)
        {
            return new ReturnCommand(this, ids);
        }

        internal override string AsPainCypher()
        {
            throw new NotImplementedException();
        }

        internal override void Validate()
        {
            throw new NotImplementedException();
        }

        internal Queue<CypherKeyword> Keywords { get => _keywords; }
    }
}

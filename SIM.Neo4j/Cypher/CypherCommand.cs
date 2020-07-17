using SIM.Neo4j.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Neo4j.Cypher
{
    public class CypherCommand
    {
        private readonly Delegate query;

        public CypherCommand(Func<SimEntity, SimGraph> query)
        {
            this.query = query;
        }
    }
}

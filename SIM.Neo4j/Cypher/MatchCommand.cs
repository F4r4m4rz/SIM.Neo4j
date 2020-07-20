using SIM.Neo4j.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIM.Neo4j.Cypher
{
    public class MatchCommand : CypherKeyword
    {
        internal MatchCommand(CypherCommand command) : base(command)
        {

        }
    }
}

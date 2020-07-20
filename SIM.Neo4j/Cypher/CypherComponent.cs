using System;
using System.Collections.Generic;
using System.Text;

namespace SIM.Neo4j.Cypher
{
    public abstract class CypherComponent
    {
        protected string _symbol;
        internal abstract string AsPainCypher();
        internal abstract void Validate();
    }
}

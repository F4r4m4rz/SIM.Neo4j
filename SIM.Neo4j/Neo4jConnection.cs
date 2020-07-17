using Neo4j.Driver;
using SIM.Neo4j.Cypher;
using SIM.Neo4j.Internal;
using System;
using System.Threading.Tasks;

namespace SIM.Neo4j.Connection
{
    /// <summary>
    /// Class to establish a connection to Neo4j Databse
    /// </summary>
    public class Neo4jConnection : IDisposable
    {
        private IDriver _driver;

        public Neo4jConnection(string uri, string username = null, string password = null)
        {
            if (Mediator.Ne4jConnection != null)
                throw new InvalidOperationException("There is an open Neo4j connection.\n Cannot establish parallel connections to Neo4j.");

            _driver = GraphDatabase.Driver(uri, AuthTokens.Basic(username, password));
            Mediator.Ne4jConnection = this;
        }

        internal async Task<SimGraph> Get(CypherCommand command)
        {

        }

        public void Dispose()
        {
            Mediator.Destroy();
        }
    }
}

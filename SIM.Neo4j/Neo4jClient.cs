using Neo4j.Driver;
using SIM.Neo4j.Cypher;
using SIM.Neo4j.Internal;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace SIM.Neo4j.Connection
{
    /// <summary>
    /// Class to establish a connection to Neo4j Databse
    /// </summary>
    public class Neo4jClient : IDisposable
    {
        private IDriver _driver;
        private SimGraph _graph;
        private ChangeTracker _changeTracker;

        public Neo4jClient(string uri, string username = null, string password = null)
        {
            if (Mediator.Ne4jConnection != null)
                throw new InvalidOperationException("There is an open Neo4j connection.\n Cannot establish parallel connections to Neo4j.");

            _driver = GraphDatabase.Driver(uri, AuthTokens.Basic(username, password));
            CheckConnectivity();
            //Mediator.Ne4jConnection = this;
            _graph = new SimGraph(this);
            _changeTracker = new ChangeTracker();
        }

        public CypherCommand NewCommand()
        {
            return new CypherCommand();
        }

        public SimGraph ExecuteQuery(CypherCommand command)
        {
            throw new NotImplementedException();
        }

        public void ExecuteNonQuery(CypherCommand command)
        {

        }

        private void CheckConnectivity()
        {
            var checkingConnectivity = _driver.VerifyConnectivityAsync();
            checkingConnectivity.Wait();
        }

        public void Dispose()
        {
            //Mediator.Destroy();
            _driver.CloseAsync();
        }

        public SimGraph Graph { get => _graph; }
        public ChangeTracker ChangeTracker { get => _changeTracker; }
    }
}

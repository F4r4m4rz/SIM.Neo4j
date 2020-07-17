using Neo4j.Driver;
using SIM.Neo4j.Connection;
using System;
namespace SIM.Neo4j.Internal
{
    internal static class Mediator
    {
        private static Neo4jConnection ne4jConnection;

        internal static bool IsConnected()
        {
            return Ne4jConnection != null;
        }

        internal static void Destroy()
        {
            Ne4jConnection = null;
            ChangeTracker = null;
        }

        internal static Neo4jConnection Ne4jConnection
        {
            get
            {
                return ne4jConnection;
            }

            set
            {
                ChangeTracker = new ChangeTracker();
                ne4jConnection = value;
            }
        }

        internal static ChangeTracker ChangeTracker { get; set; }
    }
}

using SIM.Neo4j.Abstractions;
using SIM.Neo4j.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Neo4j
{
    public partial class SimGraph
    {
        public static SimGraph Get(Func<SimEntity, SimGraph> query)
        {
            if (!Mediator.IsConnected())
                ThrowNoConnectionException(nameof(Get));
            return new SimGraph();
        }

        private static void ThrowNoConnectionException(string method)
        {
            throw new InvalidOperationException($"There is no connection to Neo4j\nCalled method {method}");
        }
    }
}

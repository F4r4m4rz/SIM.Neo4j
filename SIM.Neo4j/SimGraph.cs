using System;
using System.Collections.Generic;
using SIM.Neo4j.Abstractions;
using SIM.Neo4j.Internal;

namespace SIM.Neo4j
{
    /// <summary>
    /// Represents a graph
    /// </summary>
    public class SimGraph
    {
        /// <summary>
        /// List of available nodes in the graph
        /// </summary>
        public ICollection<SimNode> Nodes { get; set; }

        /// <summary>
        /// List of available relations in the graph
        /// </summary>
        public ICollection<SimRelation> Relations { get; set; }

        /// <summary>
        /// Current instance of the change tracker
        /// </summary>
        internal ChangeTracker ChangeTracker { get; set; }
    }
}

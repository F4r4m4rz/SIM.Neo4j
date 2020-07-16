using System;
namespace SIM.Neo4j.Abstractions
{
    /// <summary>
    /// Represents relations or edges in a graph
    /// </summary>
    public abstract class SimRelation : SimEntity
    {
        /// <summary>
        /// The Node which lays at the start of the relation arrow
        /// </summary>
        public SimNode Origin { get; set; }

        /// <summary>
        /// The Node which lays at the end of the relation arrow
        /// </summary>
        public SimNode Target { get; set; }
    }
}

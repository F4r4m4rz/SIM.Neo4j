using System;
using System.Collections.Generic;

namespace SIM.Neo4j.Abstractions
{
    /// <summary>
    /// Represents an abstraction of graph entities
    /// </summary>
    public abstract class SimEntity
    {
        /// <summary>
        /// id value of the entity in neo4j database
        /// </summary>
        public int Neo4jId { get; set; }

        /// <summary>
        /// A dictionary where all properties are stored
        /// <p>Key: parameter name</p>
        /// <p>Value: parameter value</p>
        /// </summary>
        public IDictionary<string, object> Properties { get; set; }
    }
}

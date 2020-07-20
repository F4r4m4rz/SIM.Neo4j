using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SIM.Neo4j.Abstractions;
using SIM.Neo4j.Connection;
using SIM.Neo4j.Internal;

namespace SIM.Neo4j
{
    /// <summary>
    /// Represents a graph
    /// </summary>
    public class SimGraph
    {
        private ICollection<SimNode> _nodes;
        private ICollection<SimRelation> _relations;
        private Neo4jClient _client;

        public SimGraph()
        {
            _nodes = new List<SimNode>();
            _relations = new List<SimRelation>();
        }

        /// <summary>
        /// Instansiate a connected graph
        /// </summary>
        /// <param name="neo4JClient"></param>
        public SimGraph(Neo4jClient neo4JClient) : this()
        {
            _client = neo4JClient;
        }

        /// <summary>
        /// Instansiate a connected graph with initial nodes and relations
        /// </summary>
        /// <param name="neo4JClient"></param>
        /// <param name="nodes"></param>
        /// <param name="relations"></param>
        public SimGraph(Neo4jClient neo4JClient, IEnumerable<SimNode> nodes, IEnumerable<SimRelation> relations) : this(neo4JClient)
        {
            _nodes = nodes.ToList();
            _relations = relations.ToList();

            // Subscribe to all nodes and relations
            _client.ChangeTracker.SubscribeToChangeStateEvent(_nodes);
            _client.ChangeTracker.SubscribeToChangeStateEvent(_relations);
        }

        /// <summary>
        /// Adds a new entity to the current graph
        /// </summary>
        /// <param name="entity">New entity (Node or relation)</param>
        public void Add(SimEntity entity)
        {
            switch (entity)
            {
                case SimNode node:
                    AddNode(node);
                    break;
                case SimRelation relation:
                    AddRelation(relation);
                    break;
                default:
                    throw new ArgumentException($"{entity} is not a valid Node or Relation");
            }
        }

        /// <summary>
        /// Add new entities (Nodes/Relations) into the current graph
        /// </summary>
        /// <param name="entities"></param>
        public void Add(IEnumerable<SimEntity> entities)
        {
            for (int i = 0; i < entities.Count(); i++)
            {
                Add(entities.ElementAt(i));
            }
        }

        /// <summary>
        /// Add content of the graph into the current graph
        /// </summary>
        /// <param name="graph">Graph to be added to the current graph</param>
        public void Add(SimGraph graph)
        {
            // Add nodes
            Add(graph.Nodes);

            // Add relations
            Add(graph.Relations);
        }

        private void AddRelation(SimRelation relation)
        {
            // Check if relation is already available on the graph
            if (_relations.Contains(relation)) return;

            // Add nodes
            AddNode(relation.Origin);
            AddNode(relation.Target);

            // Subscribe to this node
            _client?.ChangeTracker.SubscribeToChangeStateEvent(relation);

            // Change state of the relation to Added and add to graph
            relation.ChangeState = ChangeState.Added;
            _relations.Add(relation);
        }

        private void AddNode(SimNode node)
        {
            // Check if node is already available on the graph
            if (_nodes.Contains(node)) return;

            // Subscribe to this node
            _client?.ChangeTracker.SubscribeToChangeStateEvent(node);

            // Change state of the node to Added and add to graph
            node.ChangeState = ChangeState.Added;
            _nodes.Add(node);
        }

        /// <summary>
        /// Updates the corresponding entity on the current graph
        /// </summary>
        /// <param name="entity"></param>
        public void Update(SimEntity entity)
        {
            switch (entity)
            {
                case SimNode node:
                    UpdateNode(node);
                    break;
                case SimRelation relation:
                    UpdateRelation(relation);
                    break;
                default:
                    throw new ArgumentException($"{entity} is not a valid Node or Relation");
            }
        }

        /// <summary>
        /// Updates the corresponding entities on the current graph
        /// </summary>
        /// <param name="entities"></param>
        public void Update(IEnumerable<SimEntity> entities)
        {
            for (int i = 0; i < entities.Count(); i++)
            {
                Update(entities.ElementAt(i));
            }
        }

        /// <summary>
        /// Updates corresponding entites from passed in graph on the current graph
        /// </summary>
        /// <param name="graph"></param>
        public void Update(SimGraph graph)
        {
            // Update nodes
            Update(graph.Nodes);

            // Update relations
            Update(graph.Relations);
        }

        private void UpdateRelation(SimRelation relation)
        {
            // Remove old version
            var removed = _relations.Remove(relation) ? true : throw new ArgumentException($"{relation} could not be found on the current graph");

            // Add new version and change the state to Modified
            relation.ChangeState = ChangeState.Modified;
            _relations.Add(relation);
        }

        private void UpdateNode(SimNode node)
        {
            // Remove old version
            var removed = _nodes.Remove(node) ? true : throw new ArgumentException($"{node} could not be found on the current graph");

            // Add new version and change the state to Modified
            node.ChangeState = ChangeState.Modified;
            _nodes.Add(node);
        }

        /// <summary>
        /// Remove the entity from the current graph
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(SimEntity entity)
        {
            switch (entity)
            {
                case SimNode node:
                    RemoveNode(node);
                    break;
                case SimRelation relation:
                    RemoveRelation(relation);
                    break;
                default:
                    throw new ArgumentException($"{entity} is not a valid Node or Relation");
            }
        }

        /// <summary>
        /// Remove the entities from the current graph
        /// </summary>
        /// <param name="entities"></param>
        public void Remove(IEnumerable<SimEntity> entities)
        {
            for (int i = 0; i < entities.Count(); i++)
            {
                Remove(entities.ElementAt(i));
            }
        }

        public void Remove(SimGraph graph)
        {
            // Remove nodes
            Remove(graph.Nodes);

            // Remove relations
            Remove(graph.Relations);
        }

        private void RemoveRelation(SimRelation relation)
        {
            var r = _relations.FirstOrDefault(a => a.Equals(relation)) ?? throw new ArgumentException($"{relation} could not be found on the current graph");

            // Change the state to removed
            r.ChangeState = ChangeState.Removed;
        }

        private void RemoveNode(SimNode node)
        {
            var n = _nodes.FirstOrDefault(a => a.Equals(node)) ?? throw new ArgumentException($"{node} could not be found on the current graph");

            // Change the state to removed
            n.ChangeState = ChangeState.Removed;

            // Remove relations where "n" is either as origin or target
            RemoveCorruptedRelations(n);
        }

        private void RemoveCorruptedRelations(SimNode node)
        {
            var corruptedRelations = _relations.Where(a => a.Origin.Equals(node) || a.Target.Equals(node));
            Remove(corruptedRelations);
        }

        /// <summary>
        /// List of available nodes in the graph
        /// </summary>
        public IReadOnlyCollection<SimNode> Nodes { get => new ReadOnlyCollection<SimNode>(_nodes.Except(_nodes.Where(a=>a.ChangeState == ChangeState.Removed)).ToList()); }

        /// <summary>
        /// List of available relations in the graph
        /// </summary>
        public ICollection<SimRelation> Relations { get => new ReadOnlyCollection<SimRelation>(_relations.Except(_relations.Where(a=>a.ChangeState==ChangeState.Removed)).ToList()); }

        /// <summary>
        /// Current instance of the change tracker
        /// </summary>
        internal ChangeTracker ChangeTracker { get; set; }
    }
}

using SIM.Neo4j.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SIM.Neo4j.Cypher
{
    public class CypherPattern : CypherComponent
    {
        private Queue<NodeIdentifier> _nodes;
        private Queue<RelationIdentifier> _relations;
        private CypherKeyword _keyword;

        private CypherPattern()
        {
            _nodes = new Queue<NodeIdentifier>();
            _relations = new Queue<RelationIdentifier>();
        }

        internal CypherPattern(CypherKeyword keyword) : this()
        {
            _keyword = keyword;
            _keyword.Patterns.Enqueue(this);
        }

        public NodeIdentifier Node()
        {
            return new NodeIdentifier(this);
        }

        public NodeIdentifier Node(string id)
        {
            return new NodeIdentifier(this, id);
        }

        public NodeIdentifier Node(string id, Type label)
        {
            ValidateLabel(label, typeof(SimNode));
            return new NodeIdentifier(this, id, label);
        }

        public NodeIdentifier Node(Type label)
        {
            ValidateLabel(label, typeof(SimNode));
            return new NodeIdentifier(this, label);
        }

        public RelationIdentifier Relation()
        {
            return new RelationIdentifier(this);
        }

        public RelationIdentifier Relation(string id)
        {
            return new RelationIdentifier(this, id);
        }

        public RelationIdentifier Relation(string id, Type label)
        {
            ValidateLabel(label, typeof(SimRelation));
            return new RelationIdentifier(this, id, label);
        }

        public RelationIdentifier Relation(Type label)
        {
            ValidateLabel(label, typeof(SimRelation));
            return new RelationIdentifier(this, label);
        }

        public BackwardRelationIdentifier RelationFrom()
        {
            return new BackwardRelationIdentifier(this);
        }

        public BackwardRelationIdentifier RelationFrom(string id)
        {
            return new BackwardRelationIdentifier(this, id);
        }

        public BackwardRelationIdentifier RelationFrom(string id, Type label)
        {
            ValidateLabel(label, typeof(SimRelation));
            return new BackwardRelationIdentifier(this, id, label);
        }

        public BackwardRelationIdentifier RelationFrom(Type label)
        {
            ValidateLabel(label, typeof(SimRelation));
            return new BackwardRelationIdentifier(this, label);
        }

        public ForwardRelationIdentifier RelationTo()
        {
            return new ForwardRelationIdentifier(this);
        }

        public ForwardRelationIdentifier RelationTo(string id)
        {
            return new ForwardRelationIdentifier(this, id);
        }

        public ForwardRelationIdentifier RelationTo(string id, Type label)
        {
            ValidateLabel(label, typeof(SimRelation));
            return new ForwardRelationIdentifier(this, id, label);
        }

        public ForwardRelationIdentifier RelationTo(Type label)
        {
            ValidateLabel(label, typeof(SimRelation));
            return new ForwardRelationIdentifier(this, label);
        }

        private void ValidateLabel(Type label, Type parentType)
        {
            var isValid = false;
            var baseType = label.BaseType;
            while (baseType.BaseType != null && !isValid)
            {
                isValid = baseType == parentType;
                baseType = baseType.BaseType;
            }
            if (!isValid)
                throw new ArgumentException($"Label as type {label} is not accepted.\nExpected label of type {parentType}");
        }

        internal override string AsPainCypher()
        {
            var _return = string.Empty;
            while (_nodes.Count>0)
            {
                string node = _nodes.Dequeue().AsPainCypher();
                string relation = _relations.Count != 0 ? _relations.Dequeue().AsPainCypher() : string.Empty;
                _return = string.Format("{0}{1}{2}", _return, node, relation);
            }
            _return = string.Concat(_return, "\n");
            return _return;
        }

        internal override void Validate()
        {
            // Check if nodes and relations are defined correctly
            if (_nodes.Count < 1 ||
                _nodes.Count != _relations.Count + 1)
                throw new ValidationException("Nodes and relations are not defined correctly");
        }

        public CypherKeyword Close()
        {
            return _keyword;
        }

        internal Queue<NodeIdentifier> Nodes { get => _nodes; }
        internal Queue<RelationIdentifier> Relations { get => _relations; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SIM.Neo4j.Cypher
{
    internal class CypherHierarchy
    {
        private Queue<CypherComponent> _queue;

        internal CypherHierarchy()
        {
            _queue = new Queue<CypherComponent>();
        }

        internal void Push(CypherComponent component)
        {
            _queue.Enqueue(component);
        }

        internal CypherComponent Pull()
        {
            return _queue.Dequeue();
        }
    }
}

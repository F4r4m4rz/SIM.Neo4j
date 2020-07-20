using System;
using SIM.Neo4j.Abstractions;

namespace SIM.Neo4j.Cypher
{
    public abstract class CypherPatternComponent : CypherComponent
    {
        protected string _id;
        protected Type _label;
        protected CypherPattern _pattern;

        protected internal CypherPatternComponent(CypherPattern pattern)
        {
            _pattern = pattern;
        }

        internal override string AsPainCypher()
        {
            var _return = _symbol;
            _return = _return.Replace("#id#", _id);
            var labels = string.Empty;
            var baseType = _label;
            while (baseType != null && baseType != typeof(SimEntity))
            {
                var label = $":{baseType.Name}";
                labels = string.Format("{0} {1}", labels, label);
                baseType = baseType.BaseType;
            }
            _return = _return.Replace("#labels#", labels);
            return _return;
        }
    }
}

using System;
namespace SIM.Neo4j.Internal
{
    internal enum ChangeState
    {
        Added,
        Modified,
        Removed,
        NotChanged
    }
}

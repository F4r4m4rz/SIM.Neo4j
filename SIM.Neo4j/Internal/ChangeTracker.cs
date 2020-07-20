using SIM.Neo4j.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SIM.Neo4j.Internal
{
    /// <summary>
    /// This object will track all the changes which occurs on a graph
    /// </summary>
    public class ChangeTracker
    {
        private IList<SimEntity> _added, _updated, _removed, _notChanged = new List<SimEntity>();

        public void SubscribeToChangeStateEvent(SimEntity entity)
        {
            entity.StateHasChanged += EntityStateHasChanged;
        }

        public void SubscribeToChangeStateEvent(IEnumerable<SimEntity> entities)
        {
            for (int i = 0; i < entities.Count(); i++)
            {
                SubscribeToChangeStateEvent(entities.ElementAt(i));
            }
        }

        private void EntityStateHasChanged(object sender, ChangeState e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Entities with change state as "Added" in the current session
        /// </summary>
        public IReadOnlyCollection<SimEntity> AddedEntities { get => new ReadOnlyCollection<SimEntity>(_added); }

        /// <summary>
        /// Entities with change state as "Updated" in the current session
        /// </summary>
        public IReadOnlyCollection<SimEntity> UpdatedEntities { get => new ReadOnlyCollection<SimEntity>(_updated); }

        /// <summary>
        /// Entities with change state as "Removed" in the current session
        /// </summary>
        public IReadOnlyCollection<SimEntity> RemovedEntities { get => new ReadOnlyCollection<SimEntity>(_removed); }

        /// <summary>
        /// Entities with change state as "NotChanged" in the current session
        /// </summary>
        public IReadOnlyCollection<SimEntity> NotChangedEntities { get => new ReadOnlyCollection<SimEntity>(_notChanged); }
    }
}

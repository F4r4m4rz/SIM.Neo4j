using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using SIM.Neo4j.Internal;

namespace SIM.Neo4j.Abstractions
{
    /// <summary>
    /// Represents an abstraction of graph entities
    /// </summary>
    public abstract class SimEntity : INotifyPropertyChanged
    {
        private SimPropertyDictionary properties;
        private ChangeState _changeState = ChangeState.NotChanged;

        protected SimEntity()
        {
            properties = new SimPropertyDictionary();
            properties.EntriesChanged += new EventHandler<KeyValuePair<string, object>?>((sender, e) => ChangeState = ChangeState.Modified);
            PropertyChanged += new PropertyChangedEventHandler((sender, e) => ChangeState = ChangeState.Modified);
        }

        public void AddProperty(string propName, object value)
        {
            properties.Add(propName, value);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Properties)));
        }

        /// <summary>
        /// A dictionary where all properties are stored
        /// <p>Key: parameter name</p>
        /// <p>Value: parameter value</p>
        /// </summary>
        public IDictionary<string, object> Properties => properties;

        /// <summary>
        /// id value of the entity in neo4j database
        /// </summary>
        public int Neo4jId { get; set; }

        /// <summary>
        /// Status of the change which will be tracked by changed tracker
        /// </summary>
        public ChangeState ChangeState
        {
            get => _changeState;

            set
            {
                _changeState = value;
                StateHasChanged?.Invoke(this, value);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<ChangeState> StateHasChanged;
    }
}

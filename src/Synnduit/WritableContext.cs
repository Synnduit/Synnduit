using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Synnduit.Properties;

namespace Synnduit
{
    /// <summary>
    /// Provides read-write information relevant to the run segment that's currently being
    /// executed.
    /// </summary>
    [Export(typeof(IWritableContext))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    internal class WritableContext : IWritableContext
    {
        private IContext context;

        [ImportingConstructor]
        public WritableContext()
        {
            this.context = null;
        }

        /// <summary>
        /// Gets the type of the current run segment.
        /// </summary>
        public SegmentType SegmentType
        {
            get { return this.Context.SegmentType; }
        }

        /// <summary>
        /// Gets the one-based index of the current run segment.
        /// </summary>
        public int SegmentIndex
        {
            get { return this.Context.SegmentIndex; }
        }

        /// <summary>
        /// Gets the number of segments in the current run.
        /// </summary>
        public int SegmentCount
        {
            get { return this.Context.SegmentCount; }
        }

        /// <summary>
        /// Gets source system information.
        /// </summary>
        public IExternalSystem SourceSystem
        {
            get { return this.Context.SourceSystem; }
        }

        /// <summary>
        /// Gets destination system information.
        /// </summary>
        public IExternalSystem DestinationSystem
        {
            get { return this.Context.DestinationSystem; }
        }

        /// <summary>
        /// Gets entity type infromation.
        /// </summary>
        public IEntityType EntityType
        {
            get { return this.Context.EntityType; }
        }

        /// <summary>
        /// Gets the collection of all registered external systems.
        /// </summary>
        public IEnumerable<IExternalSystem> ExternalSystems
        {
            get { return this.Context.ExternalSystems; }
        }

        /// <summary>
        /// Gets the collection of all registered entity types.
        /// </summary>
        public IEnumerable<IEntityType> EntityTypes
        {
            get { return this.Context.EntityTypes; }
        }

        /// <summary>
        /// Gets the consolidated dictionary of parameters applicable to the current run
        /// segment.
        /// </summary>
        public IReadOnlyDictionary<string, string> Parameters
        {
            get { return this.Context.Parameters; }
        }

        /// <summary>
        /// Gets the run data; this dictionary allows individual feeds, sinks, and services
        /// to preserve arbitrary pieces of data between segments.
        /// </summary>
        public IDictionary<string, object> RunData
        {
            get { return this.Context.RunData; }
        }

        /// <summary>
        /// Sets the underlying <see cref="IContext" /> implementation instance; the
        /// context data will be retrieved from this instance.
        /// </summary>
        /// <param name="context">
        /// The underlying <see cref="IContext" /> implementation instance.
        /// </param>
        public void SetContext(IContext context)
        {
            this.context = context;
        }

        private IContext Context
        {
            get
            {
                if(this.context == null)
                {
                    throw new InvalidOperationException(Resources.ContextNotAvailable);
                }
                return this.context;
            }
        }
    }
}

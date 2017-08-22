using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace Synnduit
{
    /// <summary>
    /// Provides information relevant to the run segment that's currently being executed.
    /// </summary>
    [Export(typeof(IContext))]
    internal class Context : IContext
    {
        private readonly IWritableContext writableContext;

        [ImportingConstructor]
        public Context(IWritableContext writableContext)
        {
            this.writableContext = writableContext;
        }

        /// <summary>
        /// Gets the type of the current run segment.
        /// </summary>
        public SegmentType SegmentType
        {
            get { return this.writableContext.SegmentType; }
        }

        /// <summary>
        /// Gets the one-based index of the current run segment.
        /// </summary>
        public int SegmentIndex
        {
            get { return this.writableContext.SegmentIndex; }
        }

        /// <summary>
        /// Gets the number of segments in the current run.
        /// </summary>
        public int SegmentCount
        {
            get { return this.writableContext.SegmentCount; }
        }

        /// <summary>
        /// Gets source system information.
        /// </summary>
        public IExternalSystem SourceSystem
        {
            get { return this.writableContext.SourceSystem; }
        }

        /// <summary>
        /// Gets destination system information.
        /// </summary>
        public IExternalSystem DestinationSystem
        {
            get { return this.writableContext.DestinationSystem; }
        }

        /// <summary>
        /// Gets entity type infromation.
        /// </summary>
        public IEntityType EntityType
        {
            get { return this.writableContext.EntityType; }
        }

        /// <summary>
        /// Gets the collection of all registered external systems.
        /// </summary>
        public IEnumerable<IExternalSystem> ExternalSystems
        {
            get { return this.writableContext.ExternalSystems; }
        }

        /// <summary>
        /// Gets the collection of all registered entity types.
        /// </summary>
        public IEnumerable<IEntityType> EntityTypes
        {
            get { return this.writableContext.EntityTypes; }
        }

        /// <summary>
        /// Gets the consolidated dictionary of parameters applicable to the current run
        /// segment.
        /// </summary>
        public IReadOnlyDictionary<string, string> Parameters
        {
            get { return this.writableContext.Parameters; }
        }

        /// <summary>
        /// Gets the run data; this dictionary allows individual feeds, sinks, and services
        /// to preserve arbitrary pieces of data between segments.
        /// </summary>
        public IDictionary<string, object> RunData
        {
            get { return this.writableContext.RunData; }
        }
    }
}

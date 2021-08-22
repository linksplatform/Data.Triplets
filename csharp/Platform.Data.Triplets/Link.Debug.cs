using System;
using System.Diagnostics;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data.Triplets
{
    /// <summary>
    /// <para>
    /// The link.
    /// </para>
    /// <para></para>
    /// </summary>
    public partial struct Link
    {
        #region Properties

        // ReSharper disable InconsistentNaming
        // ReSharper disable UnusedMember.Local
#pragma warning disable IDE0051 // Remove unused private members

        /// <summary>
        /// <para>
        /// Gets the я a value.
        /// </para>
        /// <para></para>
        /// </summary>
        [DebuggerDisplay(null, Name = "Source")]
        private Link Я_A => this == null ? Itself : Source;

        /// <summary>
        /// <para>
        /// Gets the я b value.
        /// </para>
        /// <para></para>
        /// </summary>
        [DebuggerDisplay(null, Name = "Linker")]
        private Link Я_B => this == null ? Itself : Linker;

        /// <summary>
        /// <para>
        /// Gets the я c value.
        /// </para>
        /// <para></para>
        /// </summary>
        [DebuggerDisplay(null, Name = "Target")]
        private Link Я_C => this == null ? Itself : Target;

        /// <summary>
        /// <para>
        /// Gets the я d value.
        /// </para>
        /// <para></para>
        /// </summary>
        [DebuggerDisplay("Count = {Я_DC}", Name = "ReferersBySource")]
        private Link[] Я_D => this.GetArrayOfRererersBySource();

        /// <summary>
        /// <para>
        /// Gets the я e value.
        /// </para>
        /// <para></para>
        /// </summary>
        [DebuggerDisplay("Count = {Я_EC}", Name = "ReferersByLinker")]
        private Link[] Я_E => this.GetArrayOfRererersByLinker();

        /// <summary>
        /// <para>
        /// Gets the я f value.
        /// </para>
        /// <para></para>
        /// </summary>
        [DebuggerDisplay("Count = {Я_FC}", Name = "ReferersByTarget")]
        private Link[] Я_F => this.GetArrayOfRererersByTarget();

        /// <summary>
        /// <para>
        /// Gets the я dc value.
        /// </para>
        /// <para></para>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Int64 Я_DC => this == null ? 0 : ReferersBySourceCount;

        /// <summary>
        /// <para>
        /// Gets the я ec value.
        /// </para>
        /// <para></para>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Int64 Я_EC => this == null ? 0 : ReferersByLinkerCount;

        /// <summary>
        /// <para>
        /// Gets the я fc value.
        /// </para>
        /// <para></para>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Int64 Я_FC => this == null ? 0 : ReferersByTargetCount;

        /// <summary>
        /// <para>
        /// Gets the я h value.
        /// </para>
        /// <para></para>
        /// </summary>
        [DebuggerDisplay(null, Name = "Timestamp")]
        private DateTime Я_H => this == null ? DateTime.MinValue : Timestamp;

        // ReSharper restore UnusedMember.Local
        // ReSharper restore InconsistentNaming
#pragma warning restore IDE0051 // Remove unused private members

        #endregion

        /// <summary>
        /// <para>
        /// Returns the string.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <returns>
        /// <para>The string</para>
        /// <para></para>
        /// </returns>
        public override string ToString()
        {
            const string nullString = "null";
            if (this == null)
            {
                return nullString;
            }
            else
            {
                if (this.TryGetName(out string name))
                {
                    return name;
                }
                else
                {
                    return ((long)_link).ToString();
                }
            }
        }
    }
}

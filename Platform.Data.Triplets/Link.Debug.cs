﻿using System;
using System.Diagnostics;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data.Triplets
{
    public partial struct Link
    {
        #region Properties

        // ReSharper disable InconsistentNaming
        // ReSharper disable UnusedMember.Local
#pragma warning disable IDE0051 // Remove unused private members

        [DebuggerDisplay(null, Name = "Source")]
        private Link Я_A => this == null ? Itself : Source;

        [DebuggerDisplay(null, Name = "Linker")]
        private Link Я_B => this == null ? Itself : Linker;

        [DebuggerDisplay(null, Name = "Target")]
        private Link Я_C => this == null ? Itself : Target;

        [DebuggerDisplay("Count = {Я_DC}", Name = "ReferersBySource")]
        private Link[] Я_D => this.GetArrayOfRererersBySource();

        [DebuggerDisplay("Count = {Я_EC}", Name = "ReferersByLinker")]
        private Link[] Я_E => this.GetArrayOfRererersByLinker();

        [DebuggerDisplay("Count = {Я_FC}", Name = "ReferersByTarget")]
        private Link[] Я_F => this.GetArrayOfRererersByTarget();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Int64 Я_DC => this == null ? 0 : ReferersBySourceCount;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Int64 Я_EC => this == null ? 0 : ReferersByLinkerCount;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Int64 Я_FC => this == null ? 0 : ReferersByTargetCount;

        [DebuggerDisplay(null, Name = "Timestamp")]
        private DateTime Я_H => this == null ? DateTime.MinValue : Timestamp;

        // ReSharper restore UnusedMember.Local
        // ReSharper restore InconsistentNaming
#pragma warning restore IDE0051 // Remove unused private members

        #endregion

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

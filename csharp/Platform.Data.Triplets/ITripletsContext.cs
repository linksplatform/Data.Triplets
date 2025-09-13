using System;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data.Triplets
{
    /// <summary>
    /// <para>
    /// Defines a context for triplets operations that allows multiple instances of the native library.
    /// </para>
    /// <para></para>
    /// </summary>
    public interface ITripletsContext : IDisposable
    {
        /// <summary>
        /// <para>
        /// Gets the context identifier.
        /// </para>
        /// <para></para>
        /// </summary>
        IntPtr ContextId { get; }

        /// <summary>
        /// <para>
        /// Gets a value indicating whether this context is valid and active.
        /// </para>
        /// <para></para>
        /// </summary>
        bool IsValid { get; }
    }
}
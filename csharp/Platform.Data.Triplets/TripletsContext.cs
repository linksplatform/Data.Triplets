using System;
using System.Runtime.InteropServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data.Triplets
{
    /// <summary>
    /// <para>
    /// Represents a context for triplets operations that allows multiple instances of the native library.
    /// </para>
    /// <para></para>
    /// </summary>
    public class TripletsContext : ITripletsContext
    {
        private const string DllName = "Platform_Data_Triplets_Kernel";

        private IntPtr _contextId;
        private bool _disposed = false;

        /// <summary>
        /// <para>
        /// Gets the context identifier.
        /// </para>
        /// <para></para>
        /// </summary>
        public IntPtr ContextId => _contextId;

        /// <summary>
        /// <para>
        /// Gets a value indicating whether this context is valid and active.
        /// </para>
        /// <para></para>
        /// </summary>
        public bool IsValid => _contextId != IntPtr.Zero && !_disposed;

        /// <summary>
        /// <para>
        /// Initializes a new instance of the <see cref="TripletsContext"/> class with default settings.
        /// </para>
        /// <para></para>
        /// </summary>
        public TripletsContext() : this("db.links")
        {
        }

        /// <summary>
        /// <para>
        /// Initializes a new instance of the <see cref="TripletsContext"/> class with the specified database path.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="dbPath">
        /// <para>The path to the database file.</para>
        /// <para></para>
        /// </param>
        public TripletsContext(string dbPath)
        {
            _contextId = CreateContext(dbPath);
            if (_contextId == IntPtr.Zero)
            {
                throw new InvalidOperationException("Failed to create triplets context.");
            }
        }

        /// <summary>
        /// <para>
        /// Creates a new context with the specified database path.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="dbPath">
        /// <para>The path to the database file.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The context pointer, or IntPtr.Zero if creation failed.</para>
        /// <para></para>
        /// </returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern IntPtr CreateContext(string dbPath);

        /// <summary>
        /// <para>
        /// Destroys the specified context.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="context">
        /// <para>The context to destroy.</para>
        /// <para></para>
        /// </param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void DestroyContext(IntPtr context);

        /// <summary>
        /// <para>
        /// Releases all resources used by the <see cref="TripletsContext"/>.
        /// </para>
        /// <para></para>
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// <para>
        /// Releases unmanaged and - optionally - managed resources.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="disposing">
        /// <para><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</para>
        /// <para></para>
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (_contextId != IntPtr.Zero)
                {
                    DestroyContext(_contextId);
                    _contextId = IntPtr.Zero;
                }
                _disposed = true;
            }
        }

        /// <summary>
        /// <para>
        /// Finalizes an instance of the <see cref="TripletsContext"/> class.
        /// </para>
        /// <para></para>
        /// </summary>
        ~TripletsContext()
        {
            Dispose(false);
        }
    }
}
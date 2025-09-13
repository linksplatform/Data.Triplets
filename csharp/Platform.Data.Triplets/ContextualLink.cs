using System;
using System.Runtime.InteropServices;
using Int = System.Int64;
using LinkIndex = System.UInt64;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data.Triplets
{
    /// <summary>
    /// <para>
    /// Represents a link that operates within a specific context, allowing multiple instances of the native library.
    /// </para>
    /// <para></para>
    /// </summary>
    public partial struct ContextualLink : ILink<ContextualLink>, IEquatable<ContextualLink>
    {
        private const string DllName = "Platform_Data_Triplets_Kernel";

        private readonly LinkIndex _link;
        private readonly ITripletsContext _context;

        /// <summary>
        /// <para>
        /// Gets the context associated with this link.
        /// </para>
        /// <para></para>
        /// </summary>
        public ITripletsContext Context => _context;

        /// <summary>
        /// <para>
        /// Gets the source link.
        /// </para>
        /// <para></para>
        /// </summary>
        public ContextualLink Source => new ContextualLink(GetSourceIndex(_context.ContextId, _link), _context);

        /// <summary>
        /// <para>
        /// Gets the linker link.
        /// </para>
        /// <para></para>
        /// </summary>
        public ContextualLink Linker => new ContextualLink(GetLinkerIndex(_context.ContextId, _link), _context);

        /// <summary>
        /// <para>
        /// Gets the target link.
        /// </para>
        /// <para></para>
        /// </summary>
        public ContextualLink Target => new ContextualLink(GetTargetIndex(_context.ContextId, _link), _context);

        /// <summary>
        /// <para>
        /// Gets the first referer by source.
        /// </para>
        /// <para></para>
        /// </summary>
        public ContextualLink FirstRefererBySource => new ContextualLink(GetFirstRefererBySourceIndex(_context.ContextId, _link), _context);

        /// <summary>
        /// <para>
        /// Gets the first referer by linker.
        /// </para>
        /// <para></para>
        /// </summary>
        public ContextualLink FirstRefererByLinker => new ContextualLink(GetFirstRefererByLinkerIndex(_context.ContextId, _link), _context);

        /// <summary>
        /// <para>
        /// Gets the first referer by target.
        /// </para>
        /// <para></para>
        /// </summary>
        public ContextualLink FirstRefererByTarget => new ContextualLink(GetFirstRefererByTargetIndex(_context.ContextId, _link), _context);

        /// <summary>
        /// <para>
        /// Gets the count of referers by source.
        /// </para>
        /// <para></para>
        /// </summary>
        public Int ReferersBySourceCount => (Int)GetLinkNumberOfReferersBySource(_context.ContextId, _link);

        /// <summary>
        /// <para>
        /// Gets the count of referers by linker.
        /// </para>
        /// <para></para>
        /// </summary>
        public Int ReferersByLinkerCount => (Int)GetLinkNumberOfReferersByLinker(_context.ContextId, _link);

        /// <summary>
        /// <para>
        /// Gets the count of referers by target.
        /// </para>
        /// <para></para>
        /// </summary>
        public Int ReferersByTargetCount => (Int)GetLinkNumberOfReferersByTarget(_context.ContextId, _link);

        /// <summary>
        /// <para>
        /// Gets the total number of referers.
        /// </para>
        /// <para></para>
        /// </summary>
        public Int TotalReferers => ReferersBySourceCount + ReferersByLinkerCount + ReferersByTargetCount;

        /// <summary>
        /// <para>
        /// Gets the timestamp of this link.
        /// </para>
        /// <para></para>
        /// </summary>
        public DateTime Timestamp => DateTime.FromFileTimeUtc(GetTime(_context.ContextId, _link));

        /// <summary>
        /// <para>
        /// Initializes a new instance of the <see cref="ContextualLink"/> struct.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="link">
        /// <para>The link index.</para>
        /// <para></para>
        /// </param>
        /// <param name="context">
        /// <para>The context to operate within.</para>
        /// <para></para>
        /// </param>
        public ContextualLink(LinkIndex link, ITripletsContext context)
        {
            _link = link;
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #region Native Method Declarations with Context Support

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern LinkIndex GetSourceIndex(IntPtr context, LinkIndex link);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern LinkIndex GetLinkerIndex(IntPtr context, LinkIndex link);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern LinkIndex GetTargetIndex(IntPtr context, LinkIndex link);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern LinkIndex GetFirstRefererBySourceIndex(IntPtr context, LinkIndex link);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern LinkIndex GetFirstRefererByLinkerIndex(IntPtr context, LinkIndex link);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern LinkIndex GetFirstRefererByTargetIndex(IntPtr context, LinkIndex link);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern Int GetTime(IntPtr context, LinkIndex link);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern LinkIndex CreateLink(IntPtr context, LinkIndex source, LinkIndex linker, LinkIndex target);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern LinkIndex UpdateLink(IntPtr context, LinkIndex link, LinkIndex newSource, LinkIndex newLinker, LinkIndex newTarget);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void DeleteLink(IntPtr context, LinkIndex link);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern LinkIndex ReplaceLink(IntPtr context, LinkIndex link, LinkIndex replacement);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern LinkIndex SearchLink(IntPtr context, LinkIndex source, LinkIndex linker, LinkIndex target);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern LinkIndex GetLinkNumberOfReferersBySource(IntPtr context, LinkIndex link);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern LinkIndex GetLinkNumberOfReferersByLinker(IntPtr context, LinkIndex link);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern LinkIndex GetLinkNumberOfReferersByTarget(IntPtr context, LinkIndex link);

        private delegate void Visitor(LinkIndex link);
        private delegate Int StopableVisitor(LinkIndex link);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void WalkThroughAllReferersBySource(IntPtr context, LinkIndex root, Visitor action);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern int WalkThroughReferersBySource(IntPtr context, LinkIndex root, StopableVisitor func);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void WalkThroughAllReferersByLinker(IntPtr context, LinkIndex root, Visitor action);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern int WalkThroughReferersByLinker(IntPtr context, LinkIndex root, StopableVisitor func);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void WalkThroughAllReferersByTarget(IntPtr context, LinkIndex root, Visitor action);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern int WalkThroughReferersByTarget(IntPtr context, LinkIndex root, StopableVisitor func);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern Int WalkThroughLinks(IntPtr context, StopableVisitor func);

        #endregion

        #region Factory Methods

        /// <summary>
        /// <para>
        /// Creates a new link with the specified source, linker, and target within the given context.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="source">
        /// <para>The source link.</para>
        /// <para></para>
        /// </param>
        /// <param name="linker">
        /// <para>The linker link.</para>
        /// <para></para>
        /// </param>
        /// <param name="target">
        /// <para>The target link.</para>
        /// <para></para>
        /// </param>
        /// <param name="context">
        /// <para>The context to operate within.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The newly created contextual link.</para>
        /// <para></para>
        /// </returns>
        public static ContextualLink Create(ContextualLink source, ContextualLink linker, ContextualLink target, ITripletsContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (!context.IsValid) throw new InvalidOperationException("Context is not valid.");
            
            var newLinkIndex = CreateLink(context.ContextId, source._link, linker._link, target._link);
            return new ContextualLink(newLinkIndex, context);
        }

        /// <summary>
        /// <para>
        /// Creates a new link with the specified source, linker, and target using this link's context.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="source">
        /// <para>The source link.</para>
        /// <para></para>
        /// </param>
        /// <param name="linker">
        /// <para>The linker link.</para>
        /// <para></para>
        /// </param>
        /// <param name="target">
        /// <para>The target link.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The newly created contextual link.</para>
        /// <para></para>
        /// </returns>
        public ContextualLink Create(ContextualLink source, ContextualLink linker, ContextualLink target)
        {
            return Create(source, linker, target, _context);
        }

        /// <summary>
        /// <para>
        /// Searches for a link with the specified source, linker, and target within the given context.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="source">
        /// <para>The source link.</para>
        /// <para></para>
        /// </param>
        /// <param name="linker">
        /// <para>The linker link.</para>
        /// <para></para>
        /// </param>
        /// <param name="target">
        /// <para>The target link.</para>
        /// <para></para>
        /// </param>
        /// <param name="context">
        /// <para>The context to search within.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The found contextual link, or a default link if not found.</para>
        /// <para></para>
        /// </returns>
        public static ContextualLink Search(ContextualLink source, ContextualLink linker, ContextualLink target, ITripletsContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (!context.IsValid) throw new InvalidOperationException("Context is not valid.");
            
            var foundLinkIndex = SearchLink(context.ContextId, source._link, linker._link, target._link);
            return new ContextualLink(foundLinkIndex, context);
        }

        #endregion

        #region Basic Operations

        /// <summary>
        /// <para>
        /// Updates this link with new source, linker, and target values.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="newSource">
        /// <para>The new source link.</para>
        /// <para></para>
        /// </param>
        /// <param name="newLinker">
        /// <para>The new linker link.</para>
        /// <para></para>
        /// </param>
        /// <param name="newTarget">
        /// <para>The new target link.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The updated contextual link.</para>
        /// <para></para>
        /// </returns>
        public ContextualLink Update(ContextualLink newSource, ContextualLink newLinker, ContextualLink newTarget)
        {
            if (!_context.IsValid) throw new InvalidOperationException("Context is not valid.");
            
            var updatedLinkIndex = UpdateLink(_context.ContextId, _link, newSource._link, newLinker._link, newTarget._link);
            return new ContextualLink(updatedLinkIndex, _context);
        }

        /// <summary>
        /// <para>
        /// Deletes this link from the context.
        /// </para>
        /// <para></para>
        /// </summary>
        public void Delete()
        {
            if (!_context.IsValid) throw new InvalidOperationException("Context is not valid.");
            
            DeleteLink(_context.ContextId, _link);
        }

        /// <summary>
        /// <para>
        /// Replaces this link with another link.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="replacement">
        /// <para>The replacement link.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The replacement contextual link.</para>
        /// <para></para>
        /// </returns>
        public ContextualLink Replace(ContextualLink replacement)
        {
            if (!_context.IsValid) throw new InvalidOperationException("Context is not valid.");
            
            var replacedLinkIndex = ReplaceLink(_context.ContextId, _link, replacement._link);
            return new ContextualLink(replacedLinkIndex, _context);
        }

        #endregion

        #region Walking Operations

        /// <summary>
        /// <para>
        /// Walks through all referers by source.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="walker">
        /// <para>The action to perform on each referer.</para>
        /// <para></para>
        /// </param>
        public void WalkThroughReferersAsSource(Action<ContextualLink> walker)
        {
            if (!_context.IsValid) throw new InvalidOperationException("Context is not valid.");
            
            var context = _context;
            void wrapper(ulong x) => walker(new ContextualLink(x, context));
            WalkThroughAllReferersBySource(_context.ContextId, _link, wrapper);
        }

        /// <summary>
        /// <para>
        /// Walks through all referers by linker.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="walker">
        /// <para>The action to perform on each referer.</para>
        /// <para></para>
        /// </param>
        public void WalkThroughReferersAsLinker(Action<ContextualLink> walker)
        {
            if (!_context.IsValid) throw new InvalidOperationException("Context is not valid.");
            
            var context = _context;
            void wrapper(ulong x) => walker(new ContextualLink(x, context));
            WalkThroughAllReferersByLinker(_context.ContextId, _link, wrapper);
        }

        /// <summary>
        /// <para>
        /// Walks through all referers by target.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="walker">
        /// <para>The action to perform on each referer.</para>
        /// <para></para>
        /// </param>
        public void WalkThroughReferersAsTarget(Action<ContextualLink> walker)
        {
            if (!_context.IsValid) throw new InvalidOperationException("Context is not valid.");
            
            var context = _context;
            void wrapper(ulong x) => walker(new ContextualLink(x, context));
            WalkThroughAllReferersByTarget(_context.ContextId, _link, wrapper);
        }

        /// <summary>
        /// <para>
        /// Walks through all referers.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="walker">
        /// <para>The action to perform on each referer.</para>
        /// <para></para>
        /// </param>
        public void WalkThroughReferers(Action<ContextualLink> walker)
        {
            WalkThroughReferersAsSource(walker);
            WalkThroughReferersAsLinker(walker);
            WalkThroughReferersAsTarget(walker);
        }

        /// <summary>
        /// <para>
        /// Walks through referers by source with a stopable walker.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="walker">
        /// <para>The function that returns false to stop walking.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>True if walking completed without stopping, false if stopped early.</para>
        /// <para></para>
        /// </returns>
        public bool WalkThroughReferersAsSource(Func<ContextualLink, bool> walker)
        {
            if (!_context.IsValid) throw new InvalidOperationException("Context is not valid.");
            
            var context = _context;
            long wrapper(ulong x) => walker(new ContextualLink(x, context)) ? 1 : 0;
            return WalkThroughReferersBySource(_context.ContextId, _link, wrapper) != 0;
        }

        /// <summary>
        /// <para>
        /// Walks through referers by linker with a stopable walker.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="walker">
        /// <para>The function that returns false to stop walking.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>True if walking completed without stopping, false if stopped early.</para>
        /// <para></para>
        /// </returns>
        public bool WalkThroughReferersAsLinker(Func<ContextualLink, bool> walker)
        {
            if (!_context.IsValid) throw new InvalidOperationException("Context is not valid.");
            
            var context = _context;
            long wrapper(ulong x) => walker(new ContextualLink(x, context)) ? 1 : 0;
            return WalkThroughReferersByLinker(_context.ContextId, _link, wrapper) != 0;
        }

        /// <summary>
        /// <para>
        /// Walks through referers by target with a stopable walker.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="walker">
        /// <para>The function that returns false to stop walking.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>True if walking completed without stopping, false if stopped early.</para>
        /// <para></para>
        /// </returns>
        public bool WalkThroughReferersAsTarget(Func<ContextualLink, bool> walker)
        {
            if (!_context.IsValid) throw new InvalidOperationException("Context is not valid.");
            
            var context = _context;
            long wrapper(ulong x) => walker(new ContextualLink(x, context)) ? 1 : 0;
            return WalkThroughReferersByTarget(_context.ContextId, _link, wrapper) != 0;
        }

        /// <summary>
        /// <para>
        /// Walks through all referers with a stopable walker.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="walker">
        /// <para>The function that returns false to stop walking.</para>
        /// <para></para>
        /// </param>
        public void WalkThroughReferers(Func<ContextualLink, bool> walker)
        {
            WalkThroughReferersAsSource(walker);
            WalkThroughReferersAsLinker(walker);
            WalkThroughReferersAsTarget(walker);
        }

        #endregion

        #region Static Operations

        /// <summary>
        /// <para>
        /// Walks through all links in the given context.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="walker">
        /// <para>The action to perform on each link.</para>
        /// <para></para>
        /// </param>
        /// <param name="context">
        /// <para>The context to walk within.</para>
        /// <para></para>
        /// </param>
        public static void WalkThroughAllLinks(Action<ContextualLink> walker, ITripletsContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (!context.IsValid) throw new InvalidOperationException("Context is not valid.");
            
            WalkThroughAllLinks(x => { walker(new ContextualLink(x, context)); return true; }, context);
        }

        /// <summary>
        /// <para>
        /// Walks through all links in the given context with a stopable walker.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="walker">
        /// <para>The function that returns false to stop walking.</para>
        /// <para></para>
        /// </param>
        /// <param name="context">
        /// <para>The context to walk within.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>True if walking completed without stopping, false if stopped early.</para>
        /// <para></para>
        /// </returns>
        public static bool WalkThroughAllLinks(Func<ContextualLink, bool> walker, ITripletsContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (!context.IsValid) throw new InvalidOperationException("Context is not valid.");
            
            long wrapper(ulong x) => walker(new ContextualLink(x, context)) ? 1 : 0;
            return WalkThroughLinks(context.ContextId, wrapper) != 0;
        }

        #endregion

        #region Operators and Conversions

        /// <summary>
        /// <para>
        /// Performs an implicit conversion from <see cref="ContextualLink"/> to <see cref="LinkIndex"/>.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="link">
        /// <para>The link.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The result of the conversion.</para>
        /// <para></para>
        /// </returns>
        public static implicit operator LinkIndex(ContextualLink link) => link._link;

        /// <summary>
        /// <para>
        /// Performs an implicit conversion from <see cref="ContextualLink"/> to <see cref="Int"/>.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="link">
        /// <para>The link.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The result of the conversion.</para>
        /// <para></para>
        /// </returns>
        public static implicit operator Int(ContextualLink link) => (Int)link._link;

        /// <summary>
        /// <para>
        /// Performs an implicit conversion from <see cref="ContextualLink"/> to nullable <see cref="LinkIndex"/>.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="link">
        /// <para>The link.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The result of the conversion.</para>
        /// <para></para>
        /// </returns>
        public static implicit operator LinkIndex?(ContextualLink link) => link._link == 0 ? (LinkIndex?)null : link._link;

        /// <summary>
        /// <para>
        /// Implements the operator ==.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="left">
        /// <para>The left.</para>
        /// <para></para>
        /// </param>
        /// <param name="right">
        /// <para>The right.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The result of the operator.</para>
        /// <para></para>
        /// </returns>
        public static bool operator ==(ContextualLink left, ContextualLink right) => left.Equals(right);

        /// <summary>
        /// <para>
        /// Implements the operator !=.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="left">
        /// <para>The left.</para>
        /// <para></para>
        /// </param>
        /// <param name="right">
        /// <para>The right.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The result of the operator.</para>
        /// <para></para>
        /// </returns>
        public static bool operator !=(ContextualLink left, ContextualLink right) => !left.Equals(right);

        #endregion

        #region Equality Members

        /// <summary>
        /// <para>
        /// Determines whether this instance equals the specified other instance.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="other">
        /// <para>The other instance.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>True if the instances are equal, false otherwise.</para>
        /// <para></para>
        /// </returns>
        public bool Equals(ContextualLink other)
        {
            return _link == other._link && ReferenceEquals(_context, other._context);
        }

        /// <summary>
        /// <para>
        /// Determines whether the specified object is equal to this instance.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="obj">
        /// <para>The object to compare with this instance.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>True if the specified object is equal to this instance; otherwise, false.</para>
        /// <para></para>
        /// </returns>
        public override bool Equals(object obj)
        {
            return obj is ContextualLink other && Equals(other);
        }

        /// <summary>
        /// <para>
        /// Returns a hash code for this instance.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <returns>
        /// <para>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</para>
        /// <para></para>
        /// </returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(_link, _context);
        }

        #endregion

        #region Utility Methods

        /// <summary>
        /// <para>
        /// Returns the link index.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <returns>
        /// <para>The link index.</para>
        /// <para></para>
        /// </returns>
        public LinkIndex ToIndex() => _link;

        /// <summary>
        /// <para>
        /// Returns the link as an integer.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <returns>
        /// <para>The link as an integer.</para>
        /// <para></para>
        /// </returns>
        public Int ToInt() => (Int)_link;

        /// <summary>
        /// <para>
        /// Determines whether this link exists (is not null/zero).
        /// </para>
        /// <para></para>
        /// </summary>
        /// <returns>
        /// <para>True if the link exists, false otherwise.</para>
        /// <para></para>
        /// </returns>
        public bool Exists() => _link != 0 && _context?.IsValid == true;

        /// <summary>
        /// <para>
        /// Returns a string representation of this link.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <returns>
        /// <para>A string representation of this link.</para>
        /// <para></para>
        /// </returns>
        public override string ToString()
        {
            return $"ContextualLink({_link}, Context: {_context?.ContextId})";
        }

        #endregion
    }
}
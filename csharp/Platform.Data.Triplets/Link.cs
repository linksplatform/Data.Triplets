using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using Int = System.Int64;
using LinkIndex = System.UInt64;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data.Triplets
{
    /// <summary>
    /// <para>
    /// The link definition.
    /// </para>
    /// <para></para>
    /// </summary>
    public struct LinkDefinition : IEquatable<LinkDefinition>
    {
        /// <summary>
        /// <para>
        /// The source.
        /// </para>
        /// <para></para>
        /// </summary>
        public readonly Link Source;
        /// <summary>
        /// <para>
        /// The linker.
        /// </para>
        /// <para></para>
        /// </summary>
        public readonly Link Linker;
        /// <summary>
        /// <para>
        /// The target.
        /// </para>
        /// <para></para>
        /// </summary>
        public readonly Link Target;

        /// <summary>
        /// <para>
        /// Initializes a new <see cref="LinkDefinition"/> instance.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="source">
        /// <para>A source.</para>
        /// <para></para>
        /// </param>
        /// <param name="linker">
        /// <para>A linker.</para>
        /// <para></para>
        /// </param>
        /// <param name="target">
        /// <para>A target.</para>
        /// <para></para>
        /// </param>
        public LinkDefinition(Link source, Link linker, Link target)
        {
            Source = source;
            Linker = linker;
            Target = target;
        }

        /// <summary>
        /// <para>
        /// Initializes a new <see cref="LinkDefinition"/> instance.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="link">
        /// <para>A link.</para>
        /// <para></para>
        /// </param>
        public LinkDefinition(Link link) : this(link.Source, link.Linker, link.Target) { }

        /// <summary>
        /// <para>
        /// Determines whether this instance equals.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="other">
        /// <para>The other.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The bool</para>
        /// <para></para>
        /// </returns>
        public bool Equals(LinkDefinition other) => Source == other.Source && Linker == other.Linker && Target == other.Target;
    }

    /// <summary>
    /// <para>
    /// The link.
    /// </para>
    /// <para></para>
    /// </summary>
    public partial struct Link : ILink<Link>, IEquatable<Link>
    {
        private const string DllName = "Platform_Data_Triplets_Kernel";

        // TODO: Заменить на очередь событий, по примеру Node.js (+сделать выключаемым)
        /// <summary>
        /// <para>
        /// The created delegate.
        /// </para>
        /// <para></para>
        /// </summary>
        public delegate void CreatedDelegate(LinkDefinition createdLink);
        public static event CreatedDelegate CreatedEvent = createdLink => { };

        /// <summary>
        /// <para>
        /// The updated delegate.
        /// </para>
        /// <para></para>
        /// </summary>
        public delegate void UpdatedDelegate(LinkDefinition linkBeforeUpdate, LinkDefinition linkAfterUpdate);
        public static event UpdatedDelegate UpdatedEvent = (linkBeforeUpdate, linkAfterUpdate) => { };

        /// <summary>
        /// <para>
        /// The deleted delegate.
        /// </para>
        /// <para></para>
        /// </summary>
        public delegate void DeletedDelegate(LinkDefinition deletedLink);
        public static event DeletedDelegate DeletedEvent = deletedLink => { };

        #region Low Level

        #region Basic Operations

        /// <summary>
        /// <para>
        /// Gets the source index using the specified link.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="link">
        /// <para>The link.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The link index</para>
        /// <para></para>
        /// </returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern LinkIndex GetSourceIndex(LinkIndex link);

        /// <summary>
        /// <para>
        /// Gets the linker index using the specified link.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="link">
        /// <para>The link.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The link index</para>
        /// <para></para>
        /// </returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern LinkIndex GetLinkerIndex(LinkIndex link);

        /// <summary>
        /// <para>
        /// Gets the target index using the specified link.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="link">
        /// <para>The link.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The link index</para>
        /// <para></para>
        /// </returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern LinkIndex GetTargetIndex(LinkIndex link);

        /// <summary>
        /// <para>
        /// Gets the first referer by source index using the specified link.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="link">
        /// <para>The link.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The link index</para>
        /// <para></para>
        /// </returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern LinkIndex GetFirstRefererBySourceIndex(LinkIndex link);

        /// <summary>
        /// <para>
        /// Gets the first referer by linker index using the specified link.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="link">
        /// <para>The link.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The link index</para>
        /// <para></para>
        /// </returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern LinkIndex GetFirstRefererByLinkerIndex(LinkIndex link);

        /// <summary>
        /// <para>
        /// Gets the first referer by target index using the specified link.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="link">
        /// <para>The link.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The link index</para>
        /// <para></para>
        /// </returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern LinkIndex GetFirstRefererByTargetIndex(LinkIndex link);

        /// <summary>
        /// <para>
        /// Gets the time using the specified link.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="link">
        /// <para>The link.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The int</para>
        /// <para></para>
        /// </returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern Int GetTime(LinkIndex link);

        /// <summary>
        /// <para>
        /// Creates the link using the specified source.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="source">
        /// <para>The source.</para>
        /// <para></para>
        /// </param>
        /// <param name="linker">
        /// <para>The linker.</para>
        /// <para></para>
        /// </param>
        /// <param name="target">
        /// <para>The target.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The link index</para>
        /// <para></para>
        /// </returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern LinkIndex CreateLink(LinkIndex source, LinkIndex linker, LinkIndex target);

        /// <summary>
        /// <para>
        /// Updates the link using the specified link.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="link">
        /// <para>The link.</para>
        /// <para></para>
        /// </param>
        /// <param name="newSource">
        /// <para>The new source.</para>
        /// <para></para>
        /// </param>
        /// <param name="newLinker">
        /// <para>The new linker.</para>
        /// <para></para>
        /// </param>
        /// <param name="newTarget">
        /// <para>The new target.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The link index</para>
        /// <para></para>
        /// </returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern LinkIndex UpdateLink(LinkIndex link, LinkIndex newSource, LinkIndex newLinker, LinkIndex newTarget);

        /// <summary>
        /// <para>
        /// Deletes the link using the specified link.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="link">
        /// <para>The link.</para>
        /// <para></para>
        /// </param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void DeleteLink(LinkIndex link);

        /// <summary>
        /// <para>
        /// Replaces the link using the specified link.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="link">
        /// <para>The link.</para>
        /// <para></para>
        /// </param>
        /// <param name="replacement">
        /// <para>The replacement.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The link index</para>
        /// <para></para>
        /// </returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern LinkIndex ReplaceLink(LinkIndex link, LinkIndex replacement);

        /// <summary>
        /// <para>
        /// Searches the link using the specified source.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="source">
        /// <para>The source.</para>
        /// <para></para>
        /// </param>
        /// <param name="linker">
        /// <para>The linker.</para>
        /// <para></para>
        /// </param>
        /// <param name="target">
        /// <para>The target.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The link index</para>
        /// <para></para>
        /// </returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern LinkIndex SearchLink(LinkIndex source, LinkIndex linker, LinkIndex target);

        /// <summary>
        /// <para>
        /// Gets the mapped link using the specified mapped index.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="mappedIndex">
        /// <para>The mapped index.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The link index</para>
        /// <para></para>
        /// </returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern LinkIndex GetMappedLink(Int mappedIndex);

        /// <summary>
        /// <para>
        /// Sets the mapped link using the specified mapped index.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="mappedIndex">
        /// <para>The mapped index.</para>
        /// <para></para>
        /// </param>
        /// <param name="linkIndex">
        /// <para>The link index.</para>
        /// <para></para>
        /// </param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void SetMappedLink(Int mappedIndex, LinkIndex linkIndex);

        /// <summary>
        /// <para>
        /// Opens the links using the specified filename.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="filename">
        /// <para>The filename.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The int</para>
        /// <para></para>
        /// </returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern Int OpenLinks(string filename);

        /// <summary>
        /// <para>
        /// Closes the links.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <returns>
        /// <para>The int</para>
        /// <para></para>
        /// </returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern Int CloseLinks();

        #endregion

        #region Referers Count Selectors

        /// <summary>
        /// <para>
        /// Gets the link number of referers by source using the specified link.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="link">
        /// <para>The link.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The link index</para>
        /// <para></para>
        /// </returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern LinkIndex GetLinkNumberOfReferersBySource(LinkIndex link);

        /// <summary>
        /// <para>
        /// Gets the link number of referers by linker using the specified link.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="link">
        /// <para>The link.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The link index</para>
        /// <para></para>
        /// </returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern LinkIndex GetLinkNumberOfReferersByLinker(LinkIndex link);

        /// <summary>
        /// <para>
        /// Gets the link number of referers by target using the specified link.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="link">
        /// <para>The link.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The link index</para>
        /// <para></para>
        /// </returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern LinkIndex GetLinkNumberOfReferersByTarget(LinkIndex link);

        #endregion

        #region Referers Walkers
        private delegate void Visitor(LinkIndex link);
        private delegate Int StopableVisitor(LinkIndex link);

        /// <summary>
        /// <para>
        /// Walks the through all referers by source using the specified root.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="root">
        /// <para>The root.</para>
        /// <para></para>
        /// </param>
        /// <param name="action">
        /// <para>The action.</para>
        /// <para></para>
        /// </param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void WalkThroughAllReferersBySource(LinkIndex root, Visitor action);

        /// <summary>
        /// <para>
        /// Walks the through referers by source using the specified root.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="root">
        /// <para>The root.</para>
        /// <para></para>
        /// </param>
        /// <param name="func">
        /// <para>The func.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The int</para>
        /// <para></para>
        /// </returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern int WalkThroughReferersBySource(LinkIndex root, StopableVisitor func);

        /// <summary>
        /// <para>
        /// Walks the through all referers by linker using the specified root.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="root">
        /// <para>The root.</para>
        /// <para></para>
        /// </param>
        /// <param name="action">
        /// <para>The action.</para>
        /// <para></para>
        /// </param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void WalkThroughAllReferersByLinker(LinkIndex root, Visitor action);

        /// <summary>
        /// <para>
        /// Walks the through referers by linker using the specified root.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="root">
        /// <para>The root.</para>
        /// <para></para>
        /// </param>
        /// <param name="func">
        /// <para>The func.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The int</para>
        /// <para></para>
        /// </returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern int WalkThroughReferersByLinker(LinkIndex root, StopableVisitor func);

        /// <summary>
        /// <para>
        /// Walks the through all referers by target using the specified root.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="root">
        /// <para>The root.</para>
        /// <para></para>
        /// </param>
        /// <param name="action">
        /// <para>The action.</para>
        /// <para></para>
        /// </param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void WalkThroughAllReferersByTarget(LinkIndex root, Visitor action);

        /// <summary>
        /// <para>
        /// Walks the through referers by target using the specified root.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="root">
        /// <para>The root.</para>
        /// <para></para>
        /// </param>
        /// <param name="func">
        /// <para>The func.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The int</para>
        /// <para></para>
        /// </returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern int WalkThroughReferersByTarget(LinkIndex root, StopableVisitor func);

        /// <summary>
        /// <para>
        /// Walks the through all links using the specified action.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="action">
        /// <para>The action.</para>
        /// <para></para>
        /// </param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void WalkThroughAllLinks(Visitor action);

        /// <summary>
        /// <para>
        /// Walks the through links using the specified func.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="func">
        /// <para>The func.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The int</para>
        /// <para></para>
        /// </returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern int WalkThroughLinks(StopableVisitor func);

        #endregion

        #endregion

        #region Constains

        /// <summary>
        /// <para>
        /// The itself.
        /// </para>
        /// <para></para>
        /// </summary>
        public static readonly Link Itself = null;
        /// <summary>
        /// <para>
        /// The continue.
        /// </para>
        /// <para></para>
        /// </summary>
        public static readonly bool Continue = true;
        /// <summary>
        /// <para>
        /// The stop.
        /// </para>
        /// <para></para>
        /// </summary>
        public static readonly bool Stop;

        #endregion

        #region Static Fields
        private static readonly object _lockObject = new object();
        private static volatile int _memoryManagerIsReady;
        private static readonly Dictionary<ulong, long> _linkToMappingIndex = new Dictionary<ulong, long>();

        #endregion

        #region Fields

        /// <summary>
        /// <para>
        /// The link.
        /// </para>
        /// <para></para>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly LinkIndex _link;

        #endregion

        #region Properties

        /// <summary>
        /// <para>
        /// Gets the source value.
        /// </para>
        /// <para></para>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Link Source => GetSourceIndex(_link);

        /// <summary>
        /// <para>
        /// Gets the linker value.
        /// </para>
        /// <para></para>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Link Linker => GetLinkerIndex(_link);

        /// <summary>
        /// <para>
        /// Gets the target value.
        /// </para>
        /// <para></para>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Link Target => GetTargetIndex(_link);

        /// <summary>
        /// <para>
        /// Gets the first referer by source value.
        /// </para>
        /// <para></para>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Link FirstRefererBySource => GetFirstRefererBySourceIndex(_link);

        /// <summary>
        /// <para>
        /// Gets the first referer by linker value.
        /// </para>
        /// <para></para>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Link FirstRefererByLinker => GetFirstRefererByLinkerIndex(_link);

        /// <summary>
        /// <para>
        /// Gets the first referer by target value.
        /// </para>
        /// <para></para>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Link FirstRefererByTarget => GetFirstRefererByTargetIndex(_link);

        /// <summary>
        /// <para>
        /// Gets the referers by source count value.
        /// </para>
        /// <para></para>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Int ReferersBySourceCount => (Int)GetLinkNumberOfReferersBySource(_link);

        /// <summary>
        /// <para>
        /// Gets the referers by linker count value.
        /// </para>
        /// <para></para>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Int ReferersByLinkerCount => (Int)GetLinkNumberOfReferersByLinker(_link);

        /// <summary>
        /// <para>
        /// Gets the referers by target count value.
        /// </para>
        /// <para></para>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Int ReferersByTargetCount => (Int)GetLinkNumberOfReferersByTarget(_link);

        /// <summary>
        /// <para>
        /// Gets the total referers value.
        /// </para>
        /// <para></para>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Int TotalReferers => (Int)GetLinkNumberOfReferersBySource(_link) + (Int)GetLinkNumberOfReferersByLinker(_link) + (Int)GetLinkNumberOfReferersByTarget(_link);

        /// <summary>
        /// <para>
        /// Gets the timestamp value.
        /// </para>
        /// <para></para>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public DateTime Timestamp => DateTime.FromFileTimeUtc(GetTime(_link));

        #endregion

        #region Infrastructure

        /// <summary>
        /// <para>
        /// Initializes a new <see cref="Link"/> instance.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="link">
        /// <para>A link.</para>
        /// <para></para>
        /// </param>
        public Link(LinkIndex link) => _link = link;

        /// <summary>
        /// <para>
        /// Starts the memory manager using the specified storage filename.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="storageFilename">
        /// <para>The storage filename.</para>
        /// <para></para>
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// <para>Файл ({storageFilename}) хранилища не удалось открыть.</para>
        /// <para></para>
        /// </exception>
        public static void StartMemoryManager(string storageFilename)
        {
            lock (_lockObject)
            {
                if (_memoryManagerIsReady == default)
                {
                    if (OpenLinks(storageFilename) == 0)
                    {
                        throw new InvalidOperationException($"Файл ({storageFilename}) хранилища не удалось открыть.");
                    }
                    Interlocked.Exchange(ref _memoryManagerIsReady, 1);
                }
            }
        }

        /// <summary>
        /// <para>
        /// Stops the memory manager.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// <para>Файл хранилища не удалось закрыть. Возможно он был уже закрыт, или не открывался вовсе.</para>
        /// <para></para>
        /// </exception>
        public static void StopMemoryManager()
        {
            lock (_lockObject)
            {
                if (_memoryManagerIsReady != default)
                {
                    if (CloseLinks() == 0)
                    {
                        throw new InvalidOperationException("Файл хранилища не удалось закрыть. Возможно он был уже закрыт, или не открывался вовсе.");
                    }
                    Interlocked.Exchange(ref _memoryManagerIsReady, 0);
                }
            }
        }

        public static implicit operator LinkIndex?(Link link) => link._link == 0 ? (LinkIndex?)null : link._link;

        public static implicit operator Link(LinkIndex? link) => new Link(link ?? 0);

        public static implicit operator Int(Link link) => (Int)link._link;

        public static implicit operator Link(Int link) => new Link((LinkIndex)link);

        public static implicit operator LinkIndex(Link link) => link._link;

        public static implicit operator Link(LinkIndex link) => new Link(link);

        public static explicit operator Link(List<Link> links) => LinkConverter.FromList(links);

        public static explicit operator Link(Link[] links) => LinkConverter.FromList(links);

        public static explicit operator Link(string @string) => LinkConverter.FromString(@string);

        public static bool operator ==(Link first, Link second) => first.Equals(second);

        public static bool operator !=(Link first, Link second) => !first.Equals(second);

        public static Link operator &(Link first, Link second) => Create(first, Net.And, second);

        /// <summary>
        /// <para>
        /// Determines whether this instance equals.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="obj">
        /// <para>The obj.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The bool</para>
        /// <para></para>
        /// </returns>
        public override bool Equals(object obj) => obj is Link link ? Equals(link) : false;

        /// <summary>
        /// <para>
        /// Determines whether this instance equals.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="other">
        /// <para>The other.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The bool</para>
        /// <para></para>
        /// </returns>
        public bool Equals(Link other) => _link == other._link || (LinkDoesNotExist(_link) && LinkDoesNotExist(other._link));

        /// <summary>
        /// <para>
        /// Gets the hash code.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <returns>
        /// <para>The int</para>
        /// <para></para>
        /// </returns>
        public override int GetHashCode() => base.GetHashCode();
        private static bool LinkDoesNotExist(LinkIndex link) => link == 0 || GetLinkerIndex(link) == 0;
        private static bool LinkWasDeleted(LinkIndex link) => link != 0 && GetLinkerIndex(link) == 0;
        private bool IsMatchingTo(Link source, Link linker, Link target)
            => ((Source == this && source == null) || (Source == source))
            && ((Linker == this && linker == null) || (Linker == linker))
            && ((Target == this && target == null) || (Target == target));

        /// <summary>
        /// <para>
        /// Returns the index.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <returns>
        /// <para>The link index</para>
        /// <para></para>
        /// </returns>
        public LinkIndex ToIndex() => _link;

        /// <summary>
        /// <para>
        /// Returns the int.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <returns>
        /// <para>The int</para>
        /// <para></para>
        /// </returns>
        public Int ToInt() => (Int)_link;

        #endregion

        #region Basic Operations

        /// <summary>
        /// <para>
        /// Creates the source.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="source">
        /// <para>The source.</para>
        /// <para></para>
        /// </param>
        /// <param name="linker">
        /// <para>The linker.</para>
        /// <para></para>
        /// </param>
        /// <param name="target">
        /// <para>The target.</para>
        /// <para></para>
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// <para>Менеджер памяти ещё не готов.</para>
        /// <para></para>
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <para>Невозможно создать связь.</para>
        /// <para></para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <para>Удалённая связь не может использоваться в качестве значения. </para>
        /// <para></para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <para>Удалённая связь не может использоваться в качестве значения. </para>
        /// <para></para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <para>Удалённая связь не может использоваться в качестве значения. </para>
        /// <para></para>
        /// </exception>
        /// <returns>
        /// <para>The link.</para>
        /// <para></para>
        /// </returns>
        public static Link Create(Link source, Link linker, Link target)
        {
            if (_memoryManagerIsReady == default)
            {
                throw new InvalidOperationException("Менеджер памяти ещё не готов.");
            }
            if (LinkWasDeleted(source))
            {
                throw new ArgumentException("Удалённая связь не может использоваться в качестве значения.", nameof(source));
            }
            if (LinkWasDeleted(linker))
            {
                throw new ArgumentException("Удалённая связь не может использоваться в качестве значения.", nameof(linker));
            }
            if (LinkWasDeleted(target))
            {
                throw new ArgumentException("Удалённая связь не может использоваться в качестве значения.", nameof(target));
            }
            Link link = CreateLink(source, linker, target);
            if (link == null)
            {
                throw new InvalidOperationException("Невозможно создать связь.");
            }
            CreatedEvent.Invoke(new LinkDefinition(link));
            return link;
        }

        /// <summary>
        /// <para>
        /// Restores the index.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="index">
        /// <para>The index.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The link</para>
        /// <para></para>
        /// </returns>
        public static Link Restore(Int index) => Restore((LinkIndex)index);

        /// <summary>
        /// <para>
        /// Restores the index.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="index">
        /// <para>The index.</para>
        /// <para></para>
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// <para>Менеджер памяти ещё не готов.</para>
        /// <para></para>
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <para>Связь с указанным адресом удалена, либо не существовала.</para>
        /// <para></para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <para>У связи не может быть нулевого адреса.</para>
        /// <para></para>
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <para>Указатель не является корректным. </para>
        /// <para></para>
        /// </exception>
        /// <returns>
        /// <para>The link</para>
        /// <para></para>
        /// </returns>
        public static Link Restore(LinkIndex index)
        {
            if (_memoryManagerIsReady == default)
            {
                throw new InvalidOperationException("Менеджер памяти ещё не готов.");
            }
            if (index == 0)
            {
                throw new ArgumentException("У связи не может быть нулевого адреса.");
            }
            try
            {
                Link link = index;
                if (LinkDoesNotExist(link))
                {
                    throw new InvalidOperationException("Связь с указанным адресом удалена, либо не существовала.");
                }
                return link;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Указатель не является корректным.", ex);
            }
        }

        /// <summary>
        /// <para>
        /// Creates the mapped using the specified source.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="source">
        /// <para>The source.</para>
        /// <para></para>
        /// </param>
        /// <param name="linker">
        /// <para>The linker.</para>
        /// <para></para>
        /// </param>
        /// <param name="target">
        /// <para>The target.</para>
        /// <para></para>
        /// </param>
        /// <param name="mappingIndex">
        /// <para>The mapping index.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The link</para>
        /// <para></para>
        /// </returns>
        public static Link CreateMapped(Link source, Link linker, Link target, object mappingIndex) => CreateMapped(source, linker, target, Convert.ToInt64(mappingIndex));

        /// <summary>
        /// <para>
        /// Creates the mapped using the specified source.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="source">
        /// <para>The source.</para>
        /// <para></para>
        /// </param>
        /// <param name="linker">
        /// <para>The linker.</para>
        /// <para></para>
        /// </param>
        /// <param name="target">
        /// <para>The target.</para>
        /// <para></para>
        /// </param>
        /// <param name="mappingIndex">
        /// <para>The mapping index.</para>
        /// <para></para>
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// <para>Менеджер памяти ещё не готов.</para>
        /// <para></para>
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <para>Существующая привязанная связь не соответствует указанным Source, Linker и Target.</para>
        /// <para></para>
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <para>Установить привязанную связь не удалось.</para>
        /// <para></para>
        /// </exception>
        /// <returns>
        /// <para>The mapped link.</para>
        /// <para></para>
        /// </returns>
        public static Link CreateMapped(Link source, Link linker, Link target, Int mappingIndex)
        {
            if (_memoryManagerIsReady == default)
            {
                throw new InvalidOperationException("Менеджер памяти ещё не готов.");
            }
            Link mappedLink = GetMappedLink(mappingIndex);
            if (mappedLink == null)
            {
                mappedLink = Create(source, linker, target);
                SetMappedLink(mappingIndex, mappedLink);
                if (GetMappedLink(mappingIndex) != mappedLink)
                {
                    throw new InvalidOperationException("Установить привязанную связь не удалось.");
                }
            }
            else if (!mappedLink.IsMatchingTo(source, linker, target))
            {
                throw new InvalidOperationException("Существующая привязанная связь не соответствует указанным Source, Linker и Target.");
            }
            _linkToMappingIndex[mappedLink] = mappingIndex;
            return mappedLink;
        }

        /// <summary>
        /// <para>
        /// Determines whether try set mapped.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="link">
        /// <para>The link.</para>
        /// <para></para>
        /// </param>
        /// <param name="mappingIndex">
        /// <para>The mapping index.</para>
        /// <para></para>
        /// </param>
        /// <param name="rewrite">
        /// <para>The rewrite.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The bool</para>
        /// <para></para>
        /// </returns>
        public static bool TrySetMapped(Link link, Int mappingIndex, bool rewrite = false)
        {
            Link mappedLink = GetMappedLink(mappingIndex);

            if (mappedLink == null || rewrite)
            {
                mappedLink = link;
                SetMappedLink(mappingIndex, mappedLink);
                if (GetMappedLink(mappingIndex) != mappedLink)
                {
                    return false;
                }
            }
            else if (!mappedLink.IsMatchingTo(link.Source, link.Linker, link.Target))
            {
                return false;
            }
            _linkToMappingIndex[mappedLink] = mappingIndex;
            return true;
        }

        /// <summary>
        /// <para>
        /// Gets the mapped using the specified mapping index.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="mappingIndex">
        /// <para>The mapping index.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The link</para>
        /// <para></para>
        /// </returns>
        public static Link GetMapped(object mappingIndex) => GetMapped(Convert.ToInt64(mappingIndex));

        /// <summary>
        /// <para>
        /// Gets the mapped using the specified mapping index.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="mappingIndex">
        /// <para>The mapping index.</para>
        /// <para></para>
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// <para>Mapped link with index {mappingIndex} is not set.</para>
        /// <para></para>
        /// </exception>
        /// <returns>
        /// <para>The mapped link.</para>
        /// <para></para>
        /// </returns>
        public static Link GetMapped(Int mappingIndex)
        {
            if (!TryGetMapped(mappingIndex, out Link mappedLink))
            {
                throw new InvalidOperationException($"Mapped link with index {mappingIndex} is not set.");
            }
            return mappedLink;
        }

        /// <summary>
        /// <para>
        /// Gets the mapped or default using the specified mapping index.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="mappingIndex">
        /// <para>The mapping index.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The mapped link.</para>
        /// <para></para>
        /// </returns>
        public static Link GetMappedOrDefault(object mappingIndex)
        {
            TryGetMapped(mappingIndex, out Link mappedLink);
            return mappedLink;
        }

        /// <summary>
        /// <para>
        /// Gets the mapped or default using the specified mapping index.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="mappingIndex">
        /// <para>The mapping index.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The mapped link.</para>
        /// <para></para>
        /// </returns>
        public static Link GetMappedOrDefault(Int mappingIndex)
        {
            TryGetMapped(mappingIndex, out Link mappedLink);
            return mappedLink;
        }

        /// <summary>
        /// <para>
        /// Determines whether try get mapped.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="mappingIndex">
        /// <para>The mapping index.</para>
        /// <para></para>
        /// </param>
        /// <param name="mappedLink">
        /// <para>The mapped link.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The bool</para>
        /// <para></para>
        /// </returns>
        public static bool TryGetMapped(object mappingIndex, out Link mappedLink) => TryGetMapped(Convert.ToInt64(mappingIndex), out mappedLink);

        /// <summary>
        /// <para>
        /// Determines whether try get mapped.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="mappingIndex">
        /// <para>The mapping index.</para>
        /// <para></para>
        /// </param>
        /// <param name="mappedLink">
        /// <para>The mapped link.</para>
        /// <para></para>
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// <para>Менеджер памяти ещё не готов.</para>
        /// <para></para>
        /// </exception>
        /// <returns>
        /// <para>The bool</para>
        /// <para></para>
        /// </returns>
        public static bool TryGetMapped(Int mappingIndex, out Link mappedLink)
        {
            if (_memoryManagerIsReady == default)
            {
                throw new InvalidOperationException("Менеджер памяти ещё не готов.");
            }
            mappedLink = GetMappedLink(mappingIndex);
            if (mappedLink != null)
            {
                _linkToMappingIndex[mappedLink] = mappingIndex;
            }
            return mappedLink != null;
        }

        /// <summary>
        /// <para>
        /// Updates the link.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="link">
        /// <para>The link.</para>
        /// <para></para>
        /// </param>
        /// <param name="newSource">
        /// <para>The new source.</para>
        /// <para></para>
        /// </param>
        /// <param name="newLinker">
        /// <para>The new linker.</para>
        /// <para></para>
        /// </param>
        /// <param name="newTarget">
        /// <para>The new target.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The link.</para>
        /// <para></para>
        /// </returns>
        public static Link Update(Link link, Link newSource, Link newLinker, Link newTarget)
        {
            Update(ref link, newSource, newLinker, newTarget);
            return link;
        }

        /// <summary>
        /// <para>
        /// Updates the link.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="link">
        /// <para>The link.</para>
        /// <para></para>
        /// </param>
        /// <param name="newSource">
        /// <para>The new source.</para>
        /// <para></para>
        /// </param>
        /// <param name="newLinker">
        /// <para>The new linker.</para>
        /// <para></para>
        /// </param>
        /// <param name="newTarget">
        /// <para>The new target.</para>
        /// <para></para>
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// <para>Менеджер памяти ещё не готов.</para>
        /// <para></para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <para>Нельзя обновить несуществующую связь. </para>
        /// <para></para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <para>Удалённая связь не может использоваться в качестве нового значения. </para>
        /// <para></para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <para>Удалённая связь не может использоваться в качестве нового значения. </para>
        /// <para></para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <para>Удалённая связь не может использоваться в качестве нового значения. </para>
        /// <para></para>
        /// </exception>
        public static void Update(ref Link link, Link newSource, Link newLinker, Link newTarget)
        {
            if (_memoryManagerIsReady == default)
            {
                throw new InvalidOperationException("Менеджер памяти ещё не готов.");
            }
            if (LinkDoesNotExist(link))
            {
                throw new ArgumentException("Нельзя обновить несуществующую связь.", nameof(link));
            }
            if (LinkWasDeleted(newSource))
            {
                throw new ArgumentException("Удалённая связь не может использоваться в качестве нового значения.", nameof(newSource));
            }
            if (LinkWasDeleted(newLinker))
            {
                throw new ArgumentException("Удалённая связь не может использоваться в качестве нового значения.", nameof(newLinker));
            }
            if (LinkWasDeleted(newTarget))
            {
                throw new ArgumentException("Удалённая связь не может использоваться в качестве нового значения.", nameof(newTarget));
            }
            LinkIndex previousLinkIndex = link;
            _linkToMappingIndex.TryGetValue(link, out long mappingIndex);
            var previousDefinition = new LinkDefinition(link);
            link = UpdateLink(link, newSource, newLinker, newTarget);
            if (mappingIndex >= 0 && previousLinkIndex != link)
            {
                _linkToMappingIndex.Remove(previousLinkIndex);
                SetMappedLink(mappingIndex, link);
                _linkToMappingIndex.Add(link, mappingIndex);
            }
            UpdatedEvent(previousDefinition, new LinkDefinition(link));
        }

        /// <summary>
        /// <para>
        /// Deletes the link.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="link">
        /// <para>The link.</para>
        /// <para></para>
        /// </param>
        public static void Delete(Link link) => Delete(ref link);

        /// <summary>
        /// <para>
        /// Deletes the link.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="link">
        /// <para>The link.</para>
        /// <para></para>
        /// </param>
        public static void Delete(ref Link link)
        {
            if (LinkDoesNotExist(link))
            {
                return;
            }
            LinkIndex previousLinkIndex = link;
            _linkToMappingIndex.TryGetValue(link, out long mappingIndex);
            var previousDefinition = new LinkDefinition(link);
            DeleteLink(link);
            link = null;
            if (mappingIndex >= 0)
            {
                _linkToMappingIndex.Remove(previousLinkIndex);
                SetMappedLink(mappingIndex, 0);
            }
            DeletedEvent(previousDefinition);
        }

        //public static void Replace(ref Link link, Link replacement)
        //{
        //    if (!MemoryManagerIsReady)
        //        throw new InvalidOperationException("Менеджер памяти ещё не готов.");
        //    if (LinkDoesNotExist(link))
        //        throw new InvalidOperationException("Если связь не существует, её нельзя заменить.");
        //    if (LinkDoesNotExist(replacement))
        //        throw new ArgumentException("Пустая или удалённая связь не может быть замещаемым значением.", "replacement");
        //    link = ReplaceLink(link, replacement);
        //}

        /// <summary>
        /// <para>
        /// Searches the source.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="source">
        /// <para>The source.</para>
        /// <para></para>
        /// </param>
        /// <param name="linker">
        /// <para>The linker.</para>
        /// <para></para>
        /// </param>
        /// <param name="target">
        /// <para>The target.</para>
        /// <para></para>
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// <para>Выполнить поиск связи можно только по существующим связям.</para>
        /// <para></para>
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <para>Менеджер памяти ещё не готов.</para>
        /// <para></para>
        /// </exception>
        /// <returns>
        /// <para>The link</para>
        /// <para></para>
        /// </returns>
        public static Link Search(Link source, Link linker, Link target)
        {
            if (_memoryManagerIsReady == default)
            {
                throw new InvalidOperationException("Менеджер памяти ещё не готов.");
            }
            if (LinkDoesNotExist(source) || LinkDoesNotExist(linker) || LinkDoesNotExist(target))
            {
                throw new InvalidOperationException("Выполнить поиск связи можно только по существующим связям.");
            }
            return SearchLink(source, linker, target);
        }

        /// <summary>
        /// <para>
        /// Determines whether exists.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="source">
        /// <para>The source.</para>
        /// <para></para>
        /// </param>
        /// <param name="linker">
        /// <para>The linker.</para>
        /// <para></para>
        /// </param>
        /// <param name="target">
        /// <para>The target.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The bool</para>
        /// <para></para>
        /// </returns>
        public static bool Exists(Link source, Link linker, Link target) => SearchLink(source, linker, target) != 0;

        #endregion

        #region Referers Walkers

        /// <summary>
        /// <para>
        /// Determines whether this instance walk through referers as source.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="walker">
        /// <para>The walker.</para>
        /// <para></para>
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// <para>C несуществующей связью нельзя производитить операции.</para>
        /// <para></para>
        /// </exception>
        /// <returns>
        /// <para>The bool</para>
        /// <para></para>
        /// </returns>
        public bool WalkThroughReferersAsSource(Func<Link, bool> walker)
        {
            if (LinkDoesNotExist(this))
            {
                throw new InvalidOperationException("C несуществующей связью нельзя производитить операции.");
            }
            var referers = ReferersBySourceCount;
            if (referers == 1)
            {
                return walker(FirstRefererBySource);
            }
            else if (referers > 1)
            {
                return WalkThroughReferersBySource(this, x => walker(x) ? 1 : 0) != 0;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// <para>
        /// Walks the through referers as source using the specified walker.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="walker">
        /// <para>The walker.</para>
        /// <para></para>
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// <para>C несуществующей связью нельзя производитить операции.</para>
        /// <para></para>
        /// </exception>
        public void WalkThroughReferersAsSource(Action<Link> walker)
        {
            if (LinkDoesNotExist(this))
            {
                throw new InvalidOperationException("C несуществующей связью нельзя производитить операции.");
            }
            var referers = ReferersBySourceCount;
            if (referers == 1)
            {
                walker(FirstRefererBySource);
            }
            else if (referers > 1)
            {
                WalkThroughAllReferersBySource(this, x => walker(x));
            }
        }

        /// <summary>
        /// <para>
        /// Determines whether this instance walk through referers as linker.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="walker">
        /// <para>The walker.</para>
        /// <para></para>
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// <para>C несуществующей связью нельзя производитить операции.</para>
        /// <para></para>
        /// </exception>
        /// <returns>
        /// <para>The bool</para>
        /// <para></para>
        /// </returns>
        public bool WalkThroughReferersAsLinker(Func<Link, bool> walker)
        {
            if (LinkDoesNotExist(this))
            {
                throw new InvalidOperationException("C несуществующей связью нельзя производитить операции.");
            }
            var referers = ReferersByLinkerCount;
            if (referers == 1)
            {
                return walker(FirstRefererByLinker);
            }
            else if (referers > 1)
            {
                return WalkThroughReferersByLinker(this, x => walker(x) ? 1 : 0) != 0;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// <para>
        /// Walks the through referers as linker using the specified walker.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="walker">
        /// <para>The walker.</para>
        /// <para></para>
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// <para>C несуществующей связью нельзя производитить операции.</para>
        /// <para></para>
        /// </exception>
        public void WalkThroughReferersAsLinker(Action<Link> walker)
        {
            if (LinkDoesNotExist(this))
            {
                throw new InvalidOperationException("C несуществующей связью нельзя производитить операции.");
            }
            var referers = ReferersByLinkerCount;
            if (referers == 1)
            {
                walker(FirstRefererByLinker);
            }
            else if (referers > 1)
            {
                WalkThroughAllReferersByLinker(this, x => walker(x));
            }
        }

        /// <summary>
        /// <para>
        /// Determines whether this instance walk through referers as target.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="walker">
        /// <para>The walker.</para>
        /// <para></para>
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// <para>C несуществующей связью нельзя производитить операции.</para>
        /// <para></para>
        /// </exception>
        /// <returns>
        /// <para>The bool</para>
        /// <para></para>
        /// </returns>
        public bool WalkThroughReferersAsTarget(Func<Link, bool> walker)
        {
            if (LinkDoesNotExist(this))
            {
                throw new InvalidOperationException("C несуществующей связью нельзя производитить операции.");
            }
            var referers = ReferersByTargetCount;
            if (referers == 1)
            {
                return walker(FirstRefererByTarget);
            }
            else if (referers > 1)
            {
                return WalkThroughReferersByTarget(this, x => walker(x) ? 1 : 0) != 0;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// <para>
        /// Walks the through referers as target using the specified walker.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="walker">
        /// <para>The walker.</para>
        /// <para></para>
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// <para>C несуществующей связью нельзя производитить операции.</para>
        /// <para></para>
        /// </exception>
        public void WalkThroughReferersAsTarget(Action<Link> walker)
        {
            if (LinkDoesNotExist(this))
            {
                throw new InvalidOperationException("C несуществующей связью нельзя производитить операции.");
            }
            var referers = ReferersByTargetCount;
            if (referers == 1)
            {
                walker(FirstRefererByTarget);
            }
            else if (referers > 1)
            {
                WalkThroughAllReferersByTarget(this, x => walker(x));
            }
        }

        /// <summary>
        /// <para>
        /// Walks the through referers using the specified walker.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="walker">
        /// <para>The walker.</para>
        /// <para></para>
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// <para>C несуществующей связью нельзя производитить операции.</para>
        /// <para></para>
        /// </exception>
        public void WalkThroughReferers(Action<Link> walker)
        {
            if (LinkDoesNotExist(this))
            {
                throw new InvalidOperationException("C несуществующей связью нельзя производитить операции.");
            }
            void wrapper(ulong x) => walker(x);
            WalkThroughAllReferersBySource(this, wrapper);
            WalkThroughAllReferersByLinker(this, wrapper);
            WalkThroughAllReferersByTarget(this, wrapper);
        }

        /// <summary>
        /// <para>
        /// Walks the through referers using the specified walker.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="walker">
        /// <para>The walker.</para>
        /// <para></para>
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// <para>C несуществующей связью нельзя производитить операции.</para>
        /// <para></para>
        /// </exception>
        public void WalkThroughReferers(Func<Link, bool> walker)
        {
            if (LinkDoesNotExist(this))
            {
                throw new InvalidOperationException("C несуществующей связью нельзя производитить операции.");
            }
            long wrapper(ulong x) => walker(x) ? 1 : 0;
            WalkThroughReferersBySource(this, wrapper);
            WalkThroughReferersByLinker(this, wrapper);
            WalkThroughReferersByTarget(this, wrapper);
        }

        /// <summary>
        /// <para>
        /// Determines whether walk through all links.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="walker">
        /// <para>The walker.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The bool</para>
        /// <para></para>
        /// </returns>
        public static bool WalkThroughAllLinks(Func<Link, bool> walker) => WalkThroughLinks(x => walker(x) ? 1 : 0) != 0;

        /// <summary>
        /// <para>
        /// Walks the through all links using the specified walker.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="walker">
        /// <para>The walker.</para>
        /// <para></para>
        /// </param>
        public static void WalkThroughAllLinks(Action<Link> walker) => WalkThroughAllLinks(new Visitor(x => walker(x)));

        #endregion
    }
}

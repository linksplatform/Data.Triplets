using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Platform.Collections.Sets;
using Platform.Communication.Protocol.Gexf;
using GexfNode = Platform.Communication.Protocol.Gexf.Node;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data.Triplets
{
    /// <summary>
    /// <para>
    /// Represents the gexf exporter.
    /// </para>
    /// <para></para>
    /// </summary>
    public static class GexfExporter
    {
        /// <summary>
        /// <para>
        /// The source label.
        /// </para>
        /// <para></para>
        /// </summary>
        private const string SourceLabel = "Source";
        /// <summary>
        /// <para>
        /// The linker label.
        /// </para>
        /// <para></para>
        /// </summary>
        private const string LinkerLabel = "Linker";
        /// <summary>
        /// <para>
        /// The target label.
        /// </para>
        /// <para></para>
        /// </summary>
        private const string TargetLabel = "Target";

        /// <summary>
        /// <para>
        /// Returns the xml.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <returns>
        /// <para>The string</para>
        /// <para></para>
        /// </returns>
        public static string ToXml()
        {
            var sb = new StringBuilder();
            using (var writer = XmlWriter.Create(sb))
            {
                WriteXml(writer, CollectLinks());
            }
            return sb.ToString();
        }

        /// <summary>
        /// <para>
        /// Returns the file using the specified path.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="path">
        /// <para>The path.</para>
        /// <para></para>
        /// </param>
        public static void ToFile(string path)
        {
            using (var file = File.OpenWrite(path))
            using (var writer = XmlWriter.Create(file))
            {
                WriteXml(writer, CollectLinks());
            }
        }

        /// <summary>
        /// <para>
        /// Returns the file using the specified path.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="path">
        /// <para>The path.</para>
        /// <para></para>
        /// </param>
        /// <param name="filter">
        /// <para>The filter.</para>
        /// <para></para>
        /// </param>
        public static void ToFile(string path, Func<Link, bool> filter)
        {
            using (var file = File.OpenWrite(path))
            using (var writer = XmlWriter.Create(file))
            {
                WriteXml(writer, CollectLinks(filter));
            }
        }

        /// <summary>
        /// <para>
        /// Collects the links using the specified link match.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="linkMatch">
        /// <para>The link match.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The matching links.</para>
        /// <para></para>
        /// </returns>
        private static HashSet<Link> CollectLinks(Func<Link, bool> linkMatch)
        {
            var matchingLinks = new HashSet<Link>();
            Link.WalkThroughAllLinks(link =>
            {
                if (linkMatch(link))
                {
                    matchingLinks.Add(link);
                }
            });
            return matchingLinks;
        }

        /// <summary>
        /// <para>
        /// Collects the links.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <returns>
        /// <para>The matching links.</para>
        /// <para></para>
        /// </returns>
        private static HashSet<Link> CollectLinks()
        {
            var matchingLinks = new HashSet<Link>();
            Link.WalkThroughAllLinks(matchingLinks.AddAndReturnVoid);
            return matchingLinks;
        }

        /// <summary>
        /// <para>
        /// Writes the xml using the specified writer.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="writer">
        /// <para>The writer.</para>
        /// <para></para>
        /// </param>
        /// <param name="matchingLinks">
        /// <para>The matching links.</para>
        /// <para></para>
        /// </param>
        private static void WriteXml(XmlWriter writer, HashSet<Link> matchingLinks)
        {
            var edgesCounter = 0;
            Gexf.WriteXml(writer,
            () => // nodes
            {
                foreach (var matchingLink in matchingLinks)
                {
                    GexfNode.WriteXml(writer, matchingLink.ToInt(), matchingLink.ToString());
                }
            },
            () => // edges
            {
                foreach (var matchingLink in matchingLinks)
                {
                    if (matchingLinks.Contains(matchingLink.Source))
                    {
                        Edge.WriteXml(writer, edgesCounter++, matchingLink.ToInt(), matchingLink.Source.ToInt(), SourceLabel);
                    }
                    if (matchingLinks.Contains(matchingLink.Linker))
                    {
                        Edge.WriteXml(writer, edgesCounter++, matchingLink.ToInt(), matchingLink.Linker.ToInt(), LinkerLabel);
                    }
                    if (matchingLinks.Contains(matchingLink.Target))
                    {
                        Edge.WriteXml(writer, edgesCounter++, matchingLink.ToInt(), matchingLink.Target.ToInt(), TargetLabel);
                    }
                }
            });
        }
    }
}

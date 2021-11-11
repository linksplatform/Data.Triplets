using System;
using System.Collections.Generic;
using System.Text;
using Platform.Data.Sequences;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data.Triplets.Sequences
{
    /// <remarks>
    /// TODO: Check that CollectMatchingSequences algorithm is working, if not throw it away.
    /// TODO: Think of the abstraction on Sequences that can be equally usefull for triple links, doublet links and so on.
    /// </remarks>
    public static class SequenceHelpers
    {
        /// <summary>
        /// <para>
        /// The max sequence format size.
        /// </para>
        /// <para></para>
        /// </summary>
        public static readonly int MaxSequenceFormatSize = 20;

        //public static void DeleteSequence(Link sequence)
        //{
        //}

        /// <summary>
        /// <para>
        /// Formats the sequence using the specified sequence.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="sequence">
        /// <para>The sequence.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The string</para>
        /// <para></para>
        /// </returns>
        public static string FormatSequence(Link sequence)
        {
            var visitedElements = 0;
            var sb = new StringBuilder();
            sb.Append('{');
            StopableSequenceWalker.WalkRight(sequence, x => x.Source, x => x.Target, x => x.Linker != Net.And, element =>
            {
                if (visitedElements > 0)
                {
                    sb.Append(',');
                }
                sb.Append(element.ToString());
                visitedElements++;
                if (visitedElements < MaxSequenceFormatSize)
                {
                    return true;
                }
                else
                {
                    sb.Append(", ...");
                    return false;
                }
            });
            sb.Append('}');
            return sb.ToString();
        }

        /// <summary>
        /// <para>
        /// Collects the matching sequences using the specified links.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="links">
        /// <para>The links.</para>
        /// <para></para>
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// <para>Подпоследовательности с одним элементом не поддерживаются.</para>
        /// <para></para>
        /// </exception>
        /// <returns>
        /// <para>The results.</para>
        /// <para></para>
        /// </returns>
        public static List<Link> CollectMatchingSequences(Link[] links)
        {
            if (links.Length == 1)
            {
                throw new InvalidOperationException("Подпоследовательности с одним элементом не поддерживаются.");
            }
            var leftBound = 0;
            var rightBound = links.Length - 1;
            var left = links[leftBound++];
            var right = links[rightBound--];
            var results = new List<Link>();
            CollectMatchingSequences(left, leftBound, links, right, rightBound, ref results);
            return results;
        }
        private static void CollectMatchingSequences(Link leftLink, int leftBound, Link[] middleLinks, Link rightLink, int rightBound, ref List<Link> results)
        {
            var leftLinkTotalReferers = leftLink.ReferersBySourceCount + leftLink.ReferersByTargetCount;
            var rightLinkTotalReferers = rightLink.ReferersBySourceCount + rightLink.ReferersByTargetCount;
            if (leftLinkTotalReferers <= rightLinkTotalReferers)
            {
                var nextLeftLink = middleLinks[leftBound];
                var elements = GetRightElements(leftLink, nextLeftLink);
                if (leftBound <= rightBound)
                {
                    for (var i = elements.Length - 1; i >= 0; i--)
                    {
                        var element = elements[i];
                        if (element != null)
                        {
                            CollectMatchingSequences(element, leftBound + 1, middleLinks, rightLink, rightBound, ref results);
                        }
                    }
                }
                else
                {
                    for (var i = elements.Length - 1; i >= 0; i--)
                    {
                        var element = elements[i];
                        if (element != null)
                        {
                            results.Add(element);
                        }
                    }
                }
            }
            else
            {
                var nextRightLink = middleLinks[rightBound];
                var elements = GetLeftElements(rightLink, nextRightLink);
                if (leftBound <= rightBound)
                {
                    for (var i = elements.Length - 1; i >= 0; i--)
                    {
                        var element = elements[i];
                        if (element != null)
                        {
                            CollectMatchingSequences(leftLink, leftBound, middleLinks, elements[i], rightBound - 1, ref results);
                        }
                    }
                }
                else
                {
                    for (var i = elements.Length - 1; i >= 0; i--)
                    {
                        var element = elements[i];
                        if (element != null)
                        {
                            results.Add(element);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// <para>
        /// Gets the right elements using the specified start link.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="startLink">
        /// <para>The start link.</para>
        /// <para></para>
        /// </param>
        /// <param name="rightLink">
        /// <para>The right link.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The result.</para>
        /// <para></para>
        /// </returns>
        public static Link[] GetRightElements(Link startLink, Link rightLink)
        {
            var result = new Link[4];
            TryStepRight(startLink, rightLink, result, 0);
            startLink.WalkThroughReferersAsTarget(couple =>
                {
                    if (couple.Linker == Net.And)
                    {
                        if (TryStepRight(couple, rightLink, result, 2))
                        {
                            return Link.Stop;
                        }
                    }
                    return Link.Continue;
                });
            return result;
        }

        /// <summary>
        /// <para>
        /// Determines whether try step right.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="startLink">
        /// <para>The start link.</para>
        /// <para></para>
        /// </param>
        /// <param name="rightLink">
        /// <para>The right link.</para>
        /// <para></para>
        /// </param>
        /// <param name="result">
        /// <para>The result.</para>
        /// <para></para>
        /// </param>
        /// <param name="offset">
        /// <para>The offset.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The bool</para>
        /// <para></para>
        /// </returns>
        public static bool TryStepRight(Link startLink, Link rightLink, Link[] result, int offset)
        {
            var added = 0;
            startLink.WalkThroughReferersAsSource(couple =>
                {
                    if (couple.Linker == Net.And)
                    {
                        var coupleTarget = couple.Target;
                        if (coupleTarget == rightLink)
                        {
                            result[offset] = couple;
                            if (++added == 2)
                            {
                                return Link.Stop;
                            }
                        }
                        else if (coupleTarget.Linker == Net.And && coupleTarget.Source == rightLink)
                        {
                            result[offset + 1] = couple;
                            if (++added == 2)
                            {
                                return Link.Stop;
                            }
                        }
                    }
                    return Link.Continue;
                });
            return added > 0;
        }

        /// <summary>
        /// <para>
        /// Gets the left elements using the specified start link.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="startLink">
        /// <para>The start link.</para>
        /// <para></para>
        /// </param>
        /// <param name="leftLink">
        /// <para>The left link.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The result.</para>
        /// <para></para>
        /// </returns>
        public static Link[] GetLeftElements(Link startLink, Link leftLink)
        {
            var result = new Link[4];
            TryStepLeft(startLink, leftLink, result, 0);
            startLink.WalkThroughReferersAsSource(couple =>
                {
                    if (couple.Linker == Net.And)
                    {
                        if (TryStepLeft(couple, leftLink, result, 2))
                        {
                            return Link.Stop;
                        }
                    }
                    return Link.Continue;
                });
            return result;
        }

        /// <summary>
        /// <para>
        /// Determines whether try step left.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="startLink">
        /// <para>The start link.</para>
        /// <para></para>
        /// </param>
        /// <param name="leftLink">
        /// <para>The left link.</para>
        /// <para></para>
        /// </param>
        /// <param name="result">
        /// <para>The result.</para>
        /// <para></para>
        /// </param>
        /// <param name="offset">
        /// <para>The offset.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The bool</para>
        /// <para></para>
        /// </returns>
        public static bool TryStepLeft(Link startLink, Link leftLink, Link[] result, int offset)
        {
            var added = 0;
            startLink.WalkThroughReferersAsTarget(couple =>
                {
                    if (couple.Linker == Net.And)
                    {
                        var coupleSource = couple.Source;
                        if (coupleSource == leftLink)
                        {
                            result[offset] = couple;
                            if (++added == 2)
                            {
                                return Link.Stop;
                            }
                        }
                        else if (coupleSource.Linker == Net.And && coupleSource.Target == leftLink)
                        {
                            result[offset + 1] = couple;
                            if (++added == 2)
                            {
                                return Link.Stop;
                            }
                        }
                    }
                    return Link.Continue;
                });
            return added > 0;
        }
    }
}

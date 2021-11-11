using System;
using System.Collections.Generic;
using System.Globalization;
using Platform.Numbers;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data.Triplets
{
    /// <summary>
    /// <para>
    /// Represents the number helpers.
    /// </para>
    /// <para></para>
    /// </summary>
    public static class NumberHelpers
    {
        /// <summary>
        /// <para>
        /// Gets or sets the numbers to links value.
        /// </para>
        /// <para></para>
        /// </summary>
        public static Link[] NumbersToLinks { get; private set; }
        /// <summary>
        /// <para>
        /// Gets or sets the links to numbers value.
        /// </para>
        /// <para></para>
        /// </summary>
        public static Dictionary<Link, long> LinksToNumbers { get; private set; }

        /// <summary>
        /// <para>
        /// Initializes a new <see cref="NumberHelpers"/> instance.
        /// </para>
        /// <para></para>
        /// </summary>
        static NumberHelpers() => Create();

        /// <summary>
        /// <para>
        /// Creates.
        /// </para>
        /// <para></para>
        /// </summary>
        private static void Create()
        {
            NumbersToLinks = new Link[64];
            LinksToNumbers = new Dictionary<Link, long>();
            NumbersToLinks[0] = Net.One;
            LinksToNumbers[Net.One] = 1;
        }

        /// <summary>
        /// <para>
        /// Recreates.
        /// </para>
        /// <para></para>
        /// </summary>
        public static void Recreate() => Create();

        /// <summary>
        /// <para>
        /// Creates the power of 2 using the specified power of 2.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="powerOf2">
        /// <para>The power of.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The result.</para>
        /// <para></para>
        /// </returns>
        private static Link FromPowerOf2(long powerOf2)
        {
            var result = NumbersToLinks[powerOf2];
            if (result == null)
            {
                var previousPowerOf2Link = NumbersToLinks[powerOf2 - 1];
                if (previousPowerOf2Link == null)
                {
                    previousPowerOf2Link = NumbersToLinks[0];
                    for (var i = 1; i < powerOf2; i++)
                    {
                        if (NumbersToLinks[i] == null)
                        {
                            var numberLink = Link.Create(Net.Sum, Net.Of, previousPowerOf2Link & previousPowerOf2Link);
                            var num = (long)System.Math.Pow(2, i);
                            NumbersToLinks[i] = numberLink;
                            LinksToNumbers[numberLink] = num;
                            numberLink.SetName(num.ToString(CultureInfo.InvariantCulture));
                        }
                        previousPowerOf2Link = NumbersToLinks[i];
                    }
                }
                result = Link.Create(Net.Sum, Net.Of, previousPowerOf2Link & previousPowerOf2Link);
                var number = (long)System.Math.Pow(2, powerOf2);
                NumbersToLinks[powerOf2] = result;
                LinksToNumbers[result] = number;
                result.SetName(number.ToString(CultureInfo.InvariantCulture));
            }
            return result;
        }

        /// <summary>
        /// <para>
        /// Creates the number using the specified number.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="number">
        /// <para>The number.</para>
        /// <para></para>
        /// </param>
        /// <exception cref="NotSupportedException">
        /// <para>Negative numbers are not supported yet.</para>
        /// <para></para>
        /// </exception>
        /// <returns>
        /// <para>The sum.</para>
        /// <para></para>
        /// </returns>
        public static Link FromNumber(long number)
        {
            if (number == 0)
            {
                return Net.Zero;
            }
            if (number == 1)
            {
                return Net.One;
            }
            var links = new Link[Bit.Count(number)];
            if (number >= 0)
            {
                for (long key = 1, powerOf2 = 0, i = 0; key <= number; key *= 2, powerOf2++)
                {
                    if ((number & key) == key)
                    {
                        links[i] = FromPowerOf2(powerOf2);
                        i++;
                    }
                }
            }
            else
            {
                throw new NotSupportedException("Negative numbers are not supported yet.");
            }
            var sum = Link.Create(Net.Sum, Net.Of, LinkConverter.FromList(links));
            return sum;
        }

        /// <summary>
        /// <para>
        /// Returns the number using the specified link.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="link">
        /// <para>The link.</para>
        /// <para></para>
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <para>Specified link is not a number.</para>
        /// <para></para>
        /// </exception>
        /// <returns>
        /// <para>The long</para>
        /// <para></para>
        /// </returns>
        public static long ToNumber(Link link)
        {
            if (link == Net.Zero)
            {
                return 0;
            }
            if (link == Net.One)
            {
                return 1;
            }
            if (link.IsSum())
            {
                var numberParts = LinkConverter.ToList(link.Target);
                long number = 0;
                for (var i = 0; i < numberParts.Count; i++)
                {
                    GoDownAndTakeIt(numberParts[i], out long numberPart);
                    number += numberPart;
                }
                return number;
            }
            throw new ArgumentOutOfRangeException(nameof(link), "Specified link is not a number.");
        }

        /// <summary>
        /// <para>
        /// Goes the down and take it using the specified link.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="link">
        /// <para>The link.</para>
        /// <para></para>
        /// </param>
        /// <param name="number">
        /// <para>The number.</para>
        /// <para></para>
        /// </param>
        private static void GoDownAndTakeIt(Link link, out long number)
        {
            if (!LinksToNumbers.TryGetValue(link, out number))
            {
                var previousNumberLink = link.Target.Source;
                GoDownAndTakeIt(previousNumberLink, out number);
                var previousNumberIndex = (int)System.Math.Log(number, 2);
                var newNumberIndex = previousNumberIndex + 1;
                var newNumberLink = Link.Create(Net.Sum, Net.Of, previousNumberLink & previousNumberLink);
                number += number;
                NumbersToLinks[newNumberIndex] = newNumberLink;
                LinksToNumbers[newNumberLink] = number;
            }
        }
    }
}

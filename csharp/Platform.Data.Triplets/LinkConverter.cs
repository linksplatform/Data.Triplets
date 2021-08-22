using System;
using System.Collections.Generic;
using Platform.Data.Sequences;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data.Triplets
{
    /// <summary>
    /// <para>
    /// Represents the link converter.
    /// </para>
    /// <para></para>
    /// </summary>
    public static class LinkConverter
    {
        /// <summary>
        /// <para>
        /// Creates the list using the specified links.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="links">
        /// <para>The links.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The element.</para>
        /// <para></para>
        /// </returns>
        public static Link FromList(List<Link> links)
        {
            var i = links.Count - 1;
            var element = links[i];
            while (--i >= 0)
            {
                element = links[i] & element;
            }
            return element;
        }

        /// <summary>
        /// <para>
        /// Creates the list using the specified links.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="links">
        /// <para>The links.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The element.</para>
        /// <para></para>
        /// </returns>
        public static Link FromList(Link[] links)
        {
            var i = links.Length - 1;
            var element = links[i];
            while (--i >= 0)
            {
                element = links[i] & element;
            }
            return element;
        }

        /// <summary>
        /// <para>
        /// Returns the list using the specified link.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="link">
        /// <para>The link.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The list.</para>
        /// <para></para>
        /// </returns>
        public static List<Link> ToList(Link link)
        {
            var list = new List<Link>();
            SequenceWalker.WalkRight(link, x => x.Source, x => x.Target, x => x.Linker != Net.And, list.Add);
            return list;
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
        /// <returns>
        /// <para>The link</para>
        /// <para></para>
        /// </returns>
        public static Link FromNumber(long number) => NumberHelpers.FromNumber(number);

        /// <summary>
        /// <para>
        /// Returns the number using the specified number.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="number">
        /// <para>The number.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The long</para>
        /// <para></para>
        /// </returns>
        public static long ToNumber(Link number) => NumberHelpers.ToNumber(number);

        /// <summary>
        /// <para>
        /// Creates the char using the specified c.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="c">
        /// <para>The .</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The link</para>
        /// <para></para>
        /// </returns>
        public static Link FromChar(char c) => CharacterHelpers.FromChar(c);

        /// <summary>
        /// <para>
        /// Returns the char using the specified char link.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="charLink">
        /// <para>The char link.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The char</para>
        /// <para></para>
        /// </returns>
        public static char ToChar(Link charLink) => CharacterHelpers.ToChar(charLink);

        /// <summary>
        /// <para>
        /// Creates the chars using the specified chars.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="chars">
        /// <para>The chars.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The link</para>
        /// <para></para>
        /// </returns>
        public static Link FromChars(char[] chars) => FromObjectsToSequence(chars, FromChar);

        /// <summary>
        /// <para>
        /// Creates the chars using the specified chars.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="chars">
        /// <para>The chars.</para>
        /// <para></para>
        /// </param>
        /// <param name="takeFrom">
        /// <para>The take from.</para>
        /// <para></para>
        /// </param>
        /// <param name="takeUntil">
        /// <para>The take until.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The link</para>
        /// <para></para>
        /// </returns>
        public static Link FromChars(char[] chars, int takeFrom, int takeUntil) => FromObjectsToSequence(chars, takeFrom, takeUntil, FromChar);

        /// <summary>
        /// <para>
        /// Creates the numbers using the specified numbers.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="numbers">
        /// <para>The numbers.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The link</para>
        /// <para></para>
        /// </returns>
        public static Link FromNumbers(long[] numbers) => FromObjectsToSequence(numbers, FromNumber);

        /// <summary>
        /// <para>
        /// Creates the numbers using the specified numbers.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="numbers">
        /// <para>The numbers.</para>
        /// <para></para>
        /// </param>
        /// <param name="takeFrom">
        /// <para>The take from.</para>
        /// <para></para>
        /// </param>
        /// <param name="takeUntil">
        /// <para>The take until.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The link</para>
        /// <para></para>
        /// </returns>
        public static Link FromNumbers(long[] numbers, int takeFrom, int takeUntil) => FromObjectsToSequence(numbers, takeFrom, takeUntil, FromNumber);

        /// <summary>
        /// <para>
        /// Creates the numbers using the specified numbers.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="numbers">
        /// <para>The numbers.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The link</para>
        /// <para></para>
        /// </returns>
        public static Link FromNumbers(ushort[] numbers) => FromObjectsToSequence(numbers, x => FromNumber(x));

        /// <summary>
        /// <para>
        /// Creates the numbers using the specified numbers.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="numbers">
        /// <para>The numbers.</para>
        /// <para></para>
        /// </param>
        /// <param name="takeFrom">
        /// <para>The take from.</para>
        /// <para></para>
        /// </param>
        /// <param name="takeUntil">
        /// <para>The take until.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The link</para>
        /// <para></para>
        /// </returns>
        public static Link FromNumbers(ushort[] numbers, int takeFrom, int takeUntil) => FromObjectsToSequence(numbers, takeFrom, takeUntil, x => FromNumber(x));

        /// <summary>
        /// <para>
        /// Creates the numbers using the specified numbers.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="numbers">
        /// <para>The numbers.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The link</para>
        /// <para></para>
        /// </returns>
        public static Link FromNumbers(uint[] numbers) => FromObjectsToSequence(numbers, x => FromNumber(x));

        /// <summary>
        /// <para>
        /// Creates the numbers using the specified numbers.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="numbers">
        /// <para>The numbers.</para>
        /// <para></para>
        /// </param>
        /// <param name="takeFrom">
        /// <para>The take from.</para>
        /// <para></para>
        /// </param>
        /// <param name="takeUntil">
        /// <para>The take until.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The link</para>
        /// <para></para>
        /// </returns>
        public static Link FromNumbers(uint[] numbers, int takeFrom, int takeUntil) => FromObjectsToSequence(numbers, takeFrom, takeUntil, x => FromNumber(x));

        /// <summary>
        /// <para>
        /// Creates the numbers using the specified numbers.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="numbers">
        /// <para>The numbers.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The link</para>
        /// <para></para>
        /// </returns>
        public static Link FromNumbers(byte[] numbers) => FromObjectsToSequence(numbers, x => FromNumber(x));

        /// <summary>
        /// <para>
        /// Creates the numbers using the specified numbers.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="numbers">
        /// <para>The numbers.</para>
        /// <para></para>
        /// </param>
        /// <param name="takeFrom">
        /// <para>The take from.</para>
        /// <para></para>
        /// </param>
        /// <param name="takeUntil">
        /// <para>The take until.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The link</para>
        /// <para></para>
        /// </returns>
        public static Link FromNumbers(byte[] numbers, int takeFrom, int takeUntil) => FromObjectsToSequence(numbers, takeFrom, takeUntil, x => FromNumber(x));

        /// <summary>
        /// <para>
        /// Creates the numbers using the specified numbers.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="numbers">
        /// <para>The numbers.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The link</para>
        /// <para></para>
        /// </returns>
        public static Link FromNumbers(bool[] numbers) => FromObjectsToSequence(numbers, x => FromNumber(x ? 1 : 0));

        /// <summary>
        /// <para>
        /// Creates the numbers using the specified numbers.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="numbers">
        /// <para>The numbers.</para>
        /// <para></para>
        /// </param>
        /// <param name="takeFrom">
        /// <para>The take from.</para>
        /// <para></para>
        /// </param>
        /// <param name="takeUntil">
        /// <para>The take until.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The link</para>
        /// <para></para>
        /// </returns>
        public static Link FromNumbers(bool[] numbers, int takeFrom, int takeUntil) => FromObjectsToSequence(numbers, takeFrom, takeUntil, x => FromNumber(x ? 1 : 0));

        /// <summary>
        /// <para>
        /// Creates the objects to sequence using the specified objects.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>The .</para>
        /// <para></para>
        /// </typeparam>
        /// <param name="objects">
        /// <para>The objects.</para>
        /// <para></para>
        /// </param>
        /// <param name="converter">
        /// <para>The converter.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The link</para>
        /// <para></para>
        /// </returns>
        public static Link FromObjectsToSequence<T>(T[] objects, Func<T, Link> converter) => FromObjectsToSequence(objects, 0, objects.Length, converter);

        /// <summary>
        /// <para>
        /// Creates the objects to sequence using the specified objects.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>The .</para>
        /// <para></para>
        /// </typeparam>
        /// <param name="objects">
        /// <para>The objects.</para>
        /// <para></para>
        /// </param>
        /// <param name="takeFrom">
        /// <para>The take from.</para>
        /// <para></para>
        /// </param>
        /// <param name="takeUntil">
        /// <para>The take until.</para>
        /// <para></para>
        /// </param>
        /// <param name="converter">
        /// <para>The converter.</para>
        /// <para></para>
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <para>Нельзя преобразовать пустой список к связям.</para>
        /// <para></para>
        /// </exception>
        /// <returns>
        /// <para>The link</para>
        /// <para></para>
        /// </returns>
        public static Link FromObjectsToSequence<T>(T[] objects, int takeFrom, int takeUntil, Func<T, Link> converter)
        {
            var length = takeUntil - takeFrom;
            if (length <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(takeUntil), "Нельзя преобразовать пустой список к связям.");
            }
            var copy = new Link[length];
            for (int i = takeFrom, j = 0; i < takeUntil; i++, j++)
            {
                copy[j] = converter(objects[i]);
            }
            return FromList(copy);
        }

        /// <summary>
        /// <para>
        /// Creates the chars using the specified str.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="str">
        /// <para>The str.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The link</para>
        /// <para></para>
        /// </returns>
        public static Link FromChars(string str)
        {
            var copy = new Link[str.Length];
            for (var i = 0; i < copy.Length; i++)
            {
                copy[i] = FromChar(str[i]);
            }
            return FromList(copy);
        }

        /// <summary>
        /// <para>
        /// Creates the string using the specified str.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="str">
        /// <para>The str.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The str link.</para>
        /// <para></para>
        /// </returns>
        public static Link FromString(string str)
        {
            var copy = new Link[str.Length];
            for (var i = 0; i < copy.Length; i++)
            {
                copy[i] = FromChar(str[i]);
            }
            var strLink = Link.Create(Net.String, Net.ThatConsistsOf, FromList(copy));
            return strLink;
        }

        /// <summary>
        /// <para>
        /// Returns the string using the specified link.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="link">
        /// <para>The link.</para>
        /// <para></para>
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <para>Specified link is not a string.</para>
        /// <para></para>
        /// </exception>
        /// <returns>
        /// <para>The string</para>
        /// <para></para>
        /// </returns>
        public static string ToString(Link link)
        {
            if (link.IsString())
            {
                return ToString(ToList(link.Target));
            }
            throw new ArgumentOutOfRangeException(nameof(link), "Specified link is not a string.");
        }

        /// <summary>
        /// <para>
        /// Returns the string using the specified char links.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="charLinks">
        /// <para>The char links.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The string</para>
        /// <para></para>
        /// </returns>
        public static string ToString(List<Link> charLinks)
        {
            var chars = new char[charLinks.Count];
            for (var i = 0; i < charLinks.Count; i++)
            {
                chars[i] = ToChar(charLinks[i]);
            }
            return new string(chars);
        }
    }
}

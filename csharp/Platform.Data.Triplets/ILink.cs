using System;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data.Triplets
{
    /// <summary>
    /// <para>
    /// Defines the link.
    /// </para>
    /// <para></para>
    /// </summary>
    internal partial interface ILink<TLinkAddress>
        where TLinkAddress : ILink<TLinkAddress>
    {
        /// <summary>
        /// <para>
        /// Gets the source value.
        /// </para>
        /// <para></para>
        /// </summary>
        TLinkAddress Source { get; }
        /// <summary>
        /// <para>
        /// Gets the linker value.
        /// </para>
        /// <para></para>
        /// </summary>
        TLinkAddress Linker { get; }
        /// <summary>
        /// <para>
        /// Gets the target value.
        /// </para>
        /// <para></para>
        /// </summary>
        TLinkAddress Target { get; }
    }

    /// <summary>
    /// <para>
    /// Defines the link.
    /// </para>
    /// <para></para>
    /// </summary>
    internal partial interface ILink<TLinkAddress>
        where TLinkAddress : ILink<TLinkAddress>
    {
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
        /// <returns>
        /// <para>The bool</para>
        /// <para></para>
        /// </returns>
        bool WalkThroughReferersAsLinker(Func<TLinkAddress, bool> walker);
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
        /// <returns>
        /// <para>The bool</para>
        /// <para></para>
        /// </returns>
        bool WalkThroughReferersAsSource(Func<TLinkAddress, bool> walker);
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
        /// <returns>
        /// <para>The bool</para>
        /// <para></para>
        /// </returns>
        bool WalkThroughReferersAsTarget(Func<TLinkAddress, bool> walker);
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
        void WalkThroughReferers(Func<TLinkAddress, bool> walker);
    }

    /// <summary>
    /// <para>
    /// Defines the link.
    /// </para>
    /// <para></para>
    /// </summary>
    internal partial interface ILink<TLinkAddress>
        where TLinkAddress : ILink<TLinkAddress>
    {
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
        void WalkThroughReferersAsLinker(Action<TLinkAddress> walker);
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
        void WalkThroughReferersAsSource(Action<TLinkAddress> walker);
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
        void WalkThroughReferersAsTarget(Action<TLinkAddress> walker);
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
        void WalkThroughReferers(Action<TLinkAddress> walker);
    }
}
/*
using System;
namespace NetLibrary
{
    interface ILink
    {
        // Cтатические методы (общие для всех связей)
        public static ILink Create(ILink source, ILink linker, ILink target);
        public static void Update(ref ILink link, ILink newSource, ILink newLinker, ILink newTarget);
        public static void Delete(ref ILink link);
        public static ILink Search(ILink source, ILink linker, ILink target);
    }
}
*/
/*
Набор функций, который необходим для работы с сущностью Link:

(Работа со значением сущности Link, значение состоит из 3-х частей, также сущностей Link)
1. Получить адрес "начальной" сущности Link. (Получить адрес из поля Source)
2. Получить адрес сущности Link, которая играет роль связки между "начальной" и "конечной" сущностями Link. (Получить адрес из поля Linker)
3. Получить адрес "конечной" сущности Link. (Получить адрес из поля Target)

4. Пройтись по всем сущностями Link, которые ссылаются на сущность Link с указанным адресом, и у которых поле Source равно этому адресу.
5. Пройтись по всем сущностями Link, которые ссылаются на сущность Link с указанным адресом, и у которых поле Linker равно этому адресу.
6. Пройтись по всем сущностями Link, которые ссылаются на сущность Link с указанным адресом, и у которых поле Target равно этому адресу.

7. Создать сущность Link со значением (смыслом), которым являются адреса на другие 3 сущности Link (где первая является "начальной", вторая является "связкой", а третья является "конечной").
8. Обновление сущности Link с указанным адресом новым значением (смыслом), которым являются адреса на другие 3 сущности Link (где первая является "начальной", вторая является "связкой", а третья является "конечной").
9. Удаление сущности Link с указаным адресом.
10. Поиск сущности Link со значением (смыслом), которым являются адреса на другие 3 сущности Link (где первая является "начальной", вторая является "связкой", а третья является "конечной").
*/
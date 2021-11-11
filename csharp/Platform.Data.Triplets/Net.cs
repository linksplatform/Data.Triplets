using Platform.Threading;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data.Triplets
{
    /// <summary>
    /// <para>
    /// The net mapping enum.
    /// </para>
    /// <para></para>
    /// </summary>
    public enum NetMapping : long
    {
        /// <summary>
        /// <para>
        /// The link net mapping.
        /// </para>
        /// <para></para>
        /// </summary>
        Link,
        /// <summary>
        /// <para>
        /// The thing net mapping.
        /// </para>
        /// <para></para>
        /// </summary>
        Thing,
        /// <summary>
        /// <para>
        /// The is net mapping.
        /// </para>
        /// <para></para>
        /// </summary>
        IsA,
        /// <summary>
        /// <para>
        /// The is not net mapping.
        /// </para>
        /// <para></para>
        /// </summary>
        IsNotA,

        /// <summary>
        /// <para>
        /// The of net mapping.
        /// </para>
        /// <para></para>
        /// </summary>
        Of,
        /// <summary>
        /// <para>
        /// The and net mapping.
        /// </para>
        /// <para></para>
        /// </summary>
        And,
        /// <summary>
        /// <para>
        /// The that consists of net mapping.
        /// </para>
        /// <para></para>
        /// </summary>
        ThatConsistsOf,
        /// <summary>
        /// <para>
        /// The has net mapping.
        /// </para>
        /// <para></para>
        /// </summary>
        Has,
        /// <summary>
        /// <para>
        /// The contains net mapping.
        /// </para>
        /// <para></para>
        /// </summary>
        Contains,
        /// <summary>
        /// <para>
        /// The contained by net mapping.
        /// </para>
        /// <para></para>
        /// </summary>
        ContainedBy,

        /// <summary>
        /// <para>
        /// The one net mapping.
        /// </para>
        /// <para></para>
        /// </summary>
        One,
        /// <summary>
        /// <para>
        /// The zero net mapping.
        /// </para>
        /// <para></para>
        /// </summary>
        Zero,

        /// <summary>
        /// <para>
        /// The sum net mapping.
        /// </para>
        /// <para></para>
        /// </summary>
        Sum,
        /// <summary>
        /// <para>
        /// The character net mapping.
        /// </para>
        /// <para></para>
        /// </summary>
        Character,
        /// <summary>
        /// <para>
        /// The string net mapping.
        /// </para>
        /// <para></para>
        /// </summary>
        String,
        /// <summary>
        /// <para>
        /// The name net mapping.
        /// </para>
        /// <para></para>
        /// </summary>
        Name,

        /// <summary>
        /// <para>
        /// The set net mapping.
        /// </para>
        /// <para></para>
        /// </summary>
        Set,
        /// <summary>
        /// <para>
        /// The group net mapping.
        /// </para>
        /// <para></para>
        /// </summary>
        Group,

        /// <summary>
        /// <para>
        /// The parsed from net mapping.
        /// </para>
        /// <para></para>
        /// </summary>
        ParsedFrom,
        /// <summary>
        /// <para>
        /// The that is net mapping.
        /// </para>
        /// <para></para>
        /// </summary>
        ThatIs,
        /// <summary>
        /// <para>
        /// The that is before net mapping.
        /// </para>
        /// <para></para>
        /// </summary>
        ThatIsBefore,
        /// <summary>
        /// <para>
        /// The that is between net mapping.
        /// </para>
        /// <para></para>
        /// </summary>
        ThatIsBetween,
        /// <summary>
        /// <para>
        /// The that is after net mapping.
        /// </para>
        /// <para></para>
        /// </summary>
        ThatIsAfter,
        /// <summary>
        /// <para>
        /// The that is represented by net mapping.
        /// </para>
        /// <para></para>
        /// </summary>
        ThatIsRepresentedBy,
        /// <summary>
        /// <para>
        /// The that has net mapping.
        /// </para>
        /// <para></para>
        /// </summary>
        ThatHas,

        /// <summary>
        /// <para>
        /// The text net mapping.
        /// </para>
        /// <para></para>
        /// </summary>
        Text,
        /// <summary>
        /// <para>
        /// The path net mapping.
        /// </para>
        /// <para></para>
        /// </summary>
        Path,
        /// <summary>
        /// <para>
        /// The content net mapping.
        /// </para>
        /// <para></para>
        /// </summary>
        Content,
        /// <summary>
        /// <para>
        /// The empty content net mapping.
        /// </para>
        /// <para></para>
        /// </summary>
        EmptyContent,
        /// <summary>
        /// <para>
        /// The empty net mapping.
        /// </para>
        /// <para></para>
        /// </summary>
        Empty,
        /// <summary>
        /// <para>
        /// The alphabet net mapping.
        /// </para>
        /// <para></para>
        /// </summary>
        Alphabet,
        /// <summary>
        /// <para>
        /// The letter net mapping.
        /// </para>
        /// <para></para>
        /// </summary>
        Letter,
        /// <summary>
        /// <para>
        /// The case net mapping.
        /// </para>
        /// <para></para>
        /// </summary>
        Case,
        /// <summary>
        /// <para>
        /// The upper net mapping.
        /// </para>
        /// <para></para>
        /// </summary>
        Upper,
        /// <summary>
        /// <para>
        /// The upper case net mapping.
        /// </para>
        /// <para></para>
        /// </summary>
        UpperCase,
        /// <summary>
        /// <para>
        /// The lower net mapping.
        /// </para>
        /// <para></para>
        /// </summary>
        Lower,
        /// <summary>
        /// <para>
        /// The lower case net mapping.
        /// </para>
        /// <para></para>
        /// </summary>
        LowerCase,
        /// <summary>
        /// <para>
        /// The code net mapping.
        /// </para>
        /// <para></para>
        /// </summary>
        Code
    }

    /// <summary>
    /// <para>
    /// Represents the net.
    /// </para>
    /// <para></para>
    /// </summary>
    public static class Net
    {
        /// <summary>
        /// <para>
        /// Gets or sets the link value.
        /// </para>
        /// <para></para>
        /// </summary>
        public static Link Link { get; private set; }
        /// <summary>
        /// <para>
        /// Gets or sets the thing value.
        /// </para>
        /// <para></para>
        /// </summary>
        public static Link Thing { get; private set; }
        /// <summary>
        /// <para>
        /// Gets or sets the is a value.
        /// </para>
        /// <para></para>
        /// </summary>
        public static Link IsA { get; private set; }
        /// <summary>
        /// <para>
        /// Gets or sets the is not a value.
        /// </para>
        /// <para></para>
        /// </summary>
        public static Link IsNotA { get; private set; }

        /// <summary>
        /// <para>
        /// Gets or sets the of value.
        /// </para>
        /// <para></para>
        /// </summary>
        public static Link Of { get; private set; }
        /// <summary>
        /// <para>
        /// Gets or sets the and value.
        /// </para>
        /// <para></para>
        /// </summary>
        public static Link And { get; private set; }
        /// <summary>
        /// <para>
        /// Gets or sets the that consists of value.
        /// </para>
        /// <para></para>
        /// </summary>
        public static Link ThatConsistsOf { get; private set; }
        /// <summary>
        /// <para>
        /// Gets or sets the has value.
        /// </para>
        /// <para></para>
        /// </summary>
        public static Link Has { get; private set; }
        /// <summary>
        /// <para>
        /// Gets or sets the contains value.
        /// </para>
        /// <para></para>
        /// </summary>
        public static Link Contains { get; private set; }
        /// <summary>
        /// <para>
        /// Gets or sets the contained by value.
        /// </para>
        /// <para></para>
        /// </summary>
        public static Link ContainedBy { get; private set; }

        /// <summary>
        /// <para>
        /// Gets or sets the one value.
        /// </para>
        /// <para></para>
        /// </summary>
        public static Link One { get; private set; }
        /// <summary>
        /// <para>
        /// Gets or sets the zero value.
        /// </para>
        /// <para></para>
        /// </summary>
        public static Link Zero { get; private set; }

        /// <summary>
        /// <para>
        /// Gets or sets the sum value.
        /// </para>
        /// <para></para>
        /// </summary>
        public static Link Sum { get; private set; }
        /// <summary>
        /// <para>
        /// Gets or sets the character value.
        /// </para>
        /// <para></para>
        /// </summary>
        public static Link Character { get; private set; }
        /// <summary>
        /// <para>
        /// Gets or sets the string value.
        /// </para>
        /// <para></para>
        /// </summary>
        public static Link String { get; private set; }
        /// <summary>
        /// <para>
        /// Gets or sets the name value.
        /// </para>
        /// <para></para>
        /// </summary>
        public static Link Name { get; private set; }

        /// <summary>
        /// <para>
        /// Gets or sets the set value.
        /// </para>
        /// <para></para>
        /// </summary>
        public static Link Set { get; private set; }
        /// <summary>
        /// <para>
        /// Gets or sets the group value.
        /// </para>
        /// <para></para>
        /// </summary>
        public static Link Group { get; private set; }

        /// <summary>
        /// <para>
        /// Gets or sets the parsed from value.
        /// </para>
        /// <para></para>
        /// </summary>
        public static Link ParsedFrom { get; private set; }
        /// <summary>
        /// <para>
        /// Gets or sets the that is value.
        /// </para>
        /// <para></para>
        /// </summary>
        public static Link ThatIs { get; private set; }
        /// <summary>
        /// <para>
        /// Gets or sets the that is before value.
        /// </para>
        /// <para></para>
        /// </summary>
        public static Link ThatIsBefore { get; private set; }
        /// <summary>
        /// <para>
        /// Gets or sets the that is between value.
        /// </para>
        /// <para></para>
        /// </summary>
        public static Link ThatIsBetween { get; private set; }
        /// <summary>
        /// <para>
        /// Gets or sets the that is after value.
        /// </para>
        /// <para></para>
        /// </summary>
        public static Link ThatIsAfter { get; private set; }
        /// <summary>
        /// <para>
        /// Gets or sets the that is represented by value.
        /// </para>
        /// <para></para>
        /// </summary>
        public static Link ThatIsRepresentedBy { get; private set; }
        /// <summary>
        /// <para>
        /// Gets or sets the that has value.
        /// </para>
        /// <para></para>
        /// </summary>
        public static Link ThatHas { get; private set; }

        /// <summary>
        /// <para>
        /// Gets or sets the text value.
        /// </para>
        /// <para></para>
        /// </summary>
        public static Link Text { get; private set; }
        /// <summary>
        /// <para>
        /// Gets or sets the path value.
        /// </para>
        /// <para></para>
        /// </summary>
        public static Link Path { get; private set; }
        /// <summary>
        /// <para>
        /// Gets or sets the content value.
        /// </para>
        /// <para></para>
        /// </summary>
        public static Link Content { get; private set; }
        /// <summary>
        /// <para>
        /// Gets or sets the empty content value.
        /// </para>
        /// <para></para>
        /// </summary>
        public static Link EmptyContent { get; private set; }
        /// <summary>
        /// <para>
        /// Gets or sets the empty value.
        /// </para>
        /// <para></para>
        /// </summary>
        public static Link Empty { get; private set; }
        /// <summary>
        /// <para>
        /// Gets or sets the alphabet value.
        /// </para>
        /// <para></para>
        /// </summary>
        public static Link Alphabet { get; private set; }
        /// <summary>
        /// <para>
        /// Gets or sets the letter value.
        /// </para>
        /// <para></para>
        /// </summary>
        public static Link Letter { get; private set; }
        /// <summary>
        /// <para>
        /// Gets or sets the case value.
        /// </para>
        /// <para></para>
        /// </summary>
        public static Link Case { get; private set; }
        /// <summary>
        /// <para>
        /// Gets or sets the upper value.
        /// </para>
        /// <para></para>
        /// </summary>
        public static Link Upper { get; private set; }
        /// <summary>
        /// <para>
        /// Gets or sets the upper case value.
        /// </para>
        /// <para></para>
        /// </summary>
        public static Link UpperCase { get; private set; }
        /// <summary>
        /// <para>
        /// Gets or sets the lower value.
        /// </para>
        /// <para></para>
        /// </summary>
        public static Link Lower { get; private set; }
        /// <summary>
        /// <para>
        /// Gets or sets the lower case value.
        /// </para>
        /// <para></para>
        /// </summary>
        public static Link LowerCase { get; private set; }
        /// <summary>
        /// <para>
        /// Gets or sets the code value.
        /// </para>
        /// <para></para>
        /// </summary>
        public static Link Code { get; private set; }

        /// <summary>
        /// <para>
        /// Initializes a new <see cref="Net"/> instance.
        /// </para>
        /// <para></para>
        /// </summary>
        static Net() => Create();

        /// <summary>
        /// <para>
        /// Creates the thing.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <returns>
        /// <para>The link</para>
        /// <para></para>
        /// </returns>
        public static Link CreateThing() => Link.Create(Link.Itself, IsA, Thing);

        /// <summary>
        /// <para>
        /// Creates the mapped thing using the specified mapping.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="mapping">
        /// <para>The mapping.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The link</para>
        /// <para></para>
        /// </returns>
        public static Link CreateMappedThing(object mapping) => Link.CreateMapped(Link.Itself, IsA, Thing, mapping);

        /// <summary>
        /// <para>
        /// Creates the link.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <returns>
        /// <para>The link</para>
        /// <para></para>
        /// </returns>
        public static Link CreateLink() => Link.Create(Link.Itself, IsA, Link);

        /// <summary>
        /// <para>
        /// Creates the mapped link using the specified mapping.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="mapping">
        /// <para>The mapping.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The link</para>
        /// <para></para>
        /// </returns>
        public static Link CreateMappedLink(object mapping) => Link.CreateMapped(Link.Itself, IsA, Link, mapping);

        /// <summary>
        /// <para>
        /// Creates the set.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <returns>
        /// <para>The link</para>
        /// <para></para>
        /// </returns>
        public static Link CreateSet() => Link.Create(Link.Itself, IsA, Set);
        private static void Create()
        {
            #region Core

            IsA = Link.GetMappedOrDefault(NetMapping.IsA);
            IsNotA = Link.GetMappedOrDefault(NetMapping.IsNotA);
            Link = Link.GetMappedOrDefault(NetMapping.Link);
            Thing = Link.GetMappedOrDefault(NetMapping.Thing);

            if (IsA == null || IsNotA == null || Link == null || Thing == null)
            {
                // Наивная инициализация (Не является корректным объяснением).
                IsA = Link.CreateMapped(Link.Itself, Link.Itself, Link.Itself, NetMapping.IsA); // Стоит переделать в "[x] is a member|instance|element of the class [y]"
                IsNotA = Link.CreateMapped(Link.Itself, Link.Itself, IsA, NetMapping.IsNotA);
                Link = Link.CreateMapped(Link.Itself, IsA, Link.Itself, NetMapping.Link);
                Thing = Link.CreateMapped(Link.Itself, IsNotA, Link, NetMapping.Thing);

                IsA = Link.Update(IsA, IsA, IsA, Link); // Исключение, позволяющие завершить систему
            }

            #endregion

            Of = CreateMappedLink(NetMapping.Of);
            And = CreateMappedLink(NetMapping.And);
            ThatConsistsOf = CreateMappedLink(NetMapping.ThatConsistsOf);
            Has = CreateMappedLink(NetMapping.Has);
            Contains = CreateMappedLink(NetMapping.Contains);
            ContainedBy = CreateMappedLink(NetMapping.ContainedBy);

            One = CreateMappedThing(NetMapping.One);
            Zero = CreateMappedThing(NetMapping.Zero);

            Sum = CreateMappedThing(NetMapping.Sum);
            Character = CreateMappedThing(NetMapping.Character);
            String = CreateMappedThing(NetMapping.String);
            Name = Link.CreateMapped(Link.Itself, IsA, String, NetMapping.Name);

            Set = CreateMappedThing(NetMapping.Set);
            Group = CreateMappedThing(NetMapping.Group);

            ParsedFrom = CreateMappedLink(NetMapping.ParsedFrom);
            ThatIs = CreateMappedLink(NetMapping.ThatIs);
            ThatIsBefore = CreateMappedLink(NetMapping.ThatIsBefore);
            ThatIsAfter = CreateMappedLink(NetMapping.ThatIsAfter);
            ThatIsBetween = CreateMappedLink(NetMapping.ThatIsBetween);
            ThatIsRepresentedBy = CreateMappedLink(NetMapping.ThatIsRepresentedBy);
            ThatHas = CreateMappedLink(NetMapping.ThatHas);

            Text = CreateMappedThing(NetMapping.Text);
            Path = CreateMappedThing(NetMapping.Path);
            Content = CreateMappedThing(NetMapping.Content);
            Empty = CreateMappedThing(NetMapping.Empty);
            EmptyContent = Link.CreateMapped(Content, ThatIs, Empty, NetMapping.EmptyContent);
            Alphabet = CreateMappedThing(NetMapping.Alphabet);
            Letter = Link.CreateMapped(Link.Itself, IsA, Character, NetMapping.Letter);
            Case = CreateMappedThing(NetMapping.Case);
            Upper = CreateMappedThing(NetMapping.Upper);
            UpperCase = Link.CreateMapped(Case, ThatIs, Upper, NetMapping.UpperCase);
            Lower = CreateMappedThing(NetMapping.Lower);
            LowerCase = Link.CreateMapped(Case, ThatIs, Lower, NetMapping.LowerCase);
            Code = CreateMappedThing(NetMapping.Code);

            SetNames();
        }

        /// <summary>
        /// <para>
        /// Recreates.
        /// </para>
        /// <para></para>
        /// </summary>
        public static void Recreate()
        {
            ThreadHelpers.InvokeWithExtendedMaxStackSize(() => Link.Delete(IsA));
            CharacterHelpers.Recreate();
            Create();
        }
        private static void SetNames()
        {
            Thing.SetName("thing");
            Link.SetName("link");
            IsA.SetName("is a");
            IsNotA.SetName("is not a");

            Of.SetName("of");
            And.SetName("and");
            ThatConsistsOf.SetName("that consists of");
            Has.SetName("has");
            Contains.SetName("contains");
            ContainedBy.SetName("contained by");

            One.SetName("one");
            Zero.SetName("zero");

            Character.SetName("character");
            Sum.SetName("sum");
            String.SetName("string");
            Name.SetName("name");

            Set.SetName("set");
            Group.SetName("group");

            ParsedFrom.SetName("parsed from");
            ThatIs.SetName("that is");
            ThatIsBefore.SetName("that is before");
            ThatIsAfter.SetName("that is after");
            ThatIsBetween.SetName("that is between");
            ThatIsRepresentedBy.SetName("that is represented by");
            ThatHas.SetName("that has");

            Text.SetName("text");
            Path.SetName("path");
            Content.SetName("content");
            Empty.SetName("empty");
            EmptyContent.SetName("empty content");
            Alphabet.SetName("alphabet");
            Letter.SetName("letter");
            Case.SetName("case");
            Upper.SetName("upper");
            Lower.SetName("lower");
            Code.SetName("code");
        }
    }
}

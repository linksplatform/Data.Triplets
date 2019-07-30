using Platform.Threading;

namespace Platform.Data.Triplets
{
    public enum NetMapping : long
    {
        Link,
        Thing,
        IsA,
        IsNotA,

        Of,
        And,
        ThatConsistsOf,
        Has,
        Contains,
        ContainedBy,

        One,
        Zero,

        Sum,
        Character,
        String,
        Name,

        Set,
        Group,

        ParsedFrom,
        ThatIs,
        ThatIsBefore,
        ThatIsBetween,
        ThatIsAfter,
        ThatIsRepresentedBy,
        ThatHas,

        Text,
        Path,
        Content,
        EmptyContent,
        Empty,
        Alphabet,
        Letter,
        Case,
        Upper,
        UpperCase,
        Lower,
        LowerCase,
        Code
    }

    public class Net
    {
        public static Link Link { get; private set; }
        public static Link Thing { get; private set; }
        public static Link IsA { get; private set; }
        public static Link IsNotA { get; private set; }

        public static Link Of { get; private set; }
        public static Link And { get; private set; }
        public static Link ThatConsistsOf { get; private set; }
        public static Link Has { get; private set; }
        public static Link Contains { get; private set; }
        public static Link ContainedBy { get; private set; }

        public static Link One { get; private set; }
        public static Link Zero { get; private set; }

        public static Link Sum { get; private set; }
        public static Link Character { get; private set; }
        public static Link String { get; private set; }
        public static Link Name { get; private set; }

        public static Link Set { get; private set; }
        public static Link Group { get; private set; }

        public static Link ParsedFrom { get; private set; }
        public static Link ThatIs { get; private set; }
        public static Link ThatIsBefore { get; private set; }
        public static Link ThatIsBetween { get; private set; }
        public static Link ThatIsAfter { get; private set; }
        public static Link ThatIsRepresentedBy { get; private set; }
        public static Link ThatHas { get; private set; }

        public static Link Text { get; private set; }
        public static Link Path { get; private set; }
        public static Link Content { get; private set; }
        public static Link EmptyContent { get; private set; }
        public static Link Empty { get; private set; }
        public static Link Alphabet { get; private set; }
        public static Link Letter { get; private set; }
        public static Link Case { get; private set; }
        public static Link Upper { get; private set; }
        public static Link UpperCase { get; private set; }
        public static Link Lower { get; private set; }
        public static Link LowerCase { get; private set; }
        public static Link Code { get; private set; }

        static Net() => Create();

        public static Link CreateThing() => Link.Create(Link.Itself, IsA, Thing);

        public static Link CreateMappedThing(object mapping) => Link.CreateMapped(Link.Itself, IsA, Thing, mapping);

        public static Link CreateLink() => Link.Create(Link.Itself, IsA, Link);

        public static Link CreateMappedLink(object mapping) => Link.CreateMapped(Link.Itself, IsA, Link, mapping);

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

                IsA = Link.Update(IsA, IsA, Link); // Исключение, позволяющие завершить систему
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

        public static void Recreate()
        {
            ThreadHelpers.SyncInvokeWithExtendedStack(() => Link.Delete(IsA));
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

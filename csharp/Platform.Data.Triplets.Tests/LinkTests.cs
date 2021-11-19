using System.IO;
using Xunit;
using Platform.Random;
using Platform.Ranges;

namespace Platform.Data.Triplets.Tests
{
    public static class LinkTests
    {
        public static object Lock = new object(); //-V3090
        private static ulong _thingVisitorCounter;
        private static ulong _isAVisitorCounter;
        private static ulong _linkVisitorCounter;

        static void ThingVisitor(Link linkIndex)
        {
            _thingVisitorCounter += linkIndex;
        }

        static void IsAVisitor(Link linkIndex)
        {
            _isAVisitorCounter += linkIndex;
        }

        static void LinkVisitor(Link linkIndex)
        {
            _linkVisitorCounter += linkIndex;
        }

        [Fact]
        public static void CreateDeleteLinkTest()
        {
            lock (Lock)
            {
                string filename = "db.links";

                File.Delete(filename);

                Link.StartMemoryManager(filename);

                Link link1 = Link.Create(Link.Itself, Link.Itself, Link.Itself);

                Link.Delete(link1);

                Link.StopMemoryManager();

                File.Delete(filename);
            }
        }

        [Fact]
        public static void DeepCreateUpdateDeleteLinkTest()
        {
            lock (Lock)
            {
                string filename = "db.links";

                File.Delete(filename);

                Link.StartMemoryManager(filename);

                Link isA = Link.Create(Link.Itself, Link.Itself, Link.Itself);
                Link isNotA = Link.Create(Link.Itself, Link.Itself, isA);
                Link link = Link.Create(Link.Itself, isA, Link.Itself);
                Link thing = Link.Create(Link.Itself, isNotA, link);

                //Assert::IsTrue(GetLinksCount() == 4);

                Assert.Equal(isA, isA.Target);

                isA = Link.Update(isA, isA, isA, link); // Произведено замыкание

                Assert.Equal(link, isA.Target);

                Link.Delete(isA); // Одна эта операция удалит все 4 связи

                //Assert::IsTrue(GetLinksCount() == 0);

                Link.StopMemoryManager();

                File.Delete(filename);
            }
        }

        [Fact]
        public static void LinkReferersWalkTest()
        {
            lock (Lock)
            {
                string filename = "db.links";

                File.Delete(filename);

                Link.StartMemoryManager(filename);

                Link isA = Link.Create(Link.Itself, Link.Itself, Link.Itself);
                Link isNotA = Link.Create(Link.Itself, Link.Itself, isA);
                Link link = Link.Create(Link.Itself, isA, Link.Itself);
                Link thing = Link.Create(Link.Itself, isNotA, link);
                isA = Link.Update(isA, isA, isA, link);

                Assert.Equal(1, thing.ReferersBySourceCount);
                Assert.Equal(2, isA.ReferersByLinkerCount);
                Assert.Equal(3, link.ReferersByTargetCount);

                _thingVisitorCounter = 0;
                _isAVisitorCounter = 0;
                _linkVisitorCounter = 0;

                thing.WalkThroughReferersAsSource(ThingVisitor);
                isA.WalkThroughReferersAsLinker(IsAVisitor);
                link.WalkThroughReferersAsTarget(LinkVisitor);

                Assert.Equal(4UL, _thingVisitorCounter);
                Assert.Equal(1UL + 3UL, _isAVisitorCounter);
                Assert.Equal(1UL + 3UL + 4UL, _linkVisitorCounter);

                Link.StopMemoryManager();

                File.Delete(filename);
            }
        }

        [Fact]
        public static void MultipleRandomCreationsAndDeletionsTest()
        {
            lock (Lock)
            {
                string filename = "db.links";

                File.Delete(filename);

                Link.StartMemoryManager(filename);

                TestMultipleRandomCreationsAndDeletions(2000);

                Link.StopMemoryManager();

                File.Delete(filename);
            }
        }
        private static void TestMultipleRandomCreationsAndDeletions(int maximumOperationsPerCycle)
        {
            var and = Link.Create(Link.Itself, Link.Itself, Link.Itself);
            //var comparer = Comparer<TLink>.Default;
            for (var N = 1; N < maximumOperationsPerCycle; N++)
            {
                var random = new System.Random(N);
                var linksCount = 1;
                for (var i = 0; i < N; i++)
                {
                    var createPoint = random.NextBoolean();
                    if (linksCount > 2 && createPoint)
                    {
                        var linksAddressRange = new Range<ulong>(1, (ulong)linksCount);
                        Link source = random.NextUInt64(linksAddressRange);
                        Link target = random.NextUInt64(linksAddressRange); //-V3086
                        var resultLink = Link.Create(source, and, target);
                        if (resultLink > linksCount)
                        {
                            linksCount++;
                        }
                    }
                    else
                    {
                        Link.Create(Link.Itself, Link.Itself, Link.Itself);
                        linksCount++;
                    }
                }
                for (var i = 0; i < N; i++)
                {
                    Link link = i + 2;
                    if (link.Linker != null)
                    {
                        Link.Delete(link);
                        linksCount--;
                    }
                }
            }
        }
    }
}

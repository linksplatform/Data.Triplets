using System.IO;
using Xunit;

namespace Platform.Data.Triplets.Tests
{
    public static class PersistentMemoryManagerTests
    {
        [Fact]
        public static void FileMappingTest()
        {
            string filename = "db.links";

            File.Delete(filename);

            Link.StartMemoryManager(filename);

            Link.StopMemoryManager();

            File.Delete(filename);
        }

        [Fact]
        public static void AllocateAndFreeLinkTest()
        {
            string filename = "db.links";

            File.Delete(filename);

            Link.StartMemoryManager(filename);

            Link link = Link.Create(Link.Itself, Link.Itself, Link.Itself);

            Link.Delete(link);

            Link.StopMemoryManager();

            File.Delete(filename);
        }

        [Fact]
        public static void AttachToUnusedLinkTest()
        {
            string filename = "db.links";

            File.Delete(filename);

            Link.StartMemoryManager(filename);

            Link link1 = Link.Create(Link.Itself, Link.Itself, Link.Itself);
            Link link2 = Link.Create(Link.Itself, Link.Itself, Link.Itself);

            Link.Delete(link1); // Creates "hole" and forces "Attach" to be executed

            Link.StopMemoryManager();

            File.Delete(filename);
        }

        [Fact]
        public static void DetachToUnusedLinkTest()
        {
            string filename = "db.links";

            File.Delete(filename);

            Link.StartMemoryManager(filename);

            Link link1 = Link.Create(Link.Itself, Link.Itself, Link.Itself);
            Link link2 = Link.Create(Link.Itself, Link.Itself, Link.Itself);

            Link.Delete(link1); // Creates "hole" and forces "Attach" to be executed
            Link.Delete(link2); // Removes both links, all "Attached" links forced to be "Detached" here

            Link.StopMemoryManager();

            File.Delete(filename);
        }

        [Fact]
        public static void GetSetMappedLinkTest()
        {
            string filename = "db.links";

            File.Delete(filename);

            Link.StartMemoryManager(filename);

            var mapped = Link.GetMappedOrDefault(0);

            var mappingSet = Link.TrySetMapped(mapped, 0);

            Assert.True(mappingSet);

            Link.StopMemoryManager();

            File.Delete(filename);
        }
    }
}

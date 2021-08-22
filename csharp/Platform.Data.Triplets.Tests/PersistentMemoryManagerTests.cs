using System.IO;
using Xunit;

namespace Platform.Data.Triplets.Tests
{
    /// <summary>
    /// <para>
    /// Represents the persistent memory manager tests.
    /// </para>
    /// <para></para>
    /// </summary>
    public static class PersistentMemoryManagerTests
    {
        /// <summary>
        /// <para>
        /// Tests that file mapping test.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public static void FileMappingTest()
        {
            lock (LinkTests.Lock)
            {
                string filename = "db.links";

                File.Delete(filename);

                Link.StartMemoryManager(filename);

                Link.StopMemoryManager();

                File.Delete(filename);
            }
        }

        /// <summary>
        /// <para>
        /// Tests that allocate and free link test.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public static void AllocateAndFreeLinkTest()
        {
            lock (LinkTests.Lock)
            {
                string filename = "db.links";

                File.Delete(filename);

                Link.StartMemoryManager(filename);

                Link link = Link.Create(Link.Itself, Link.Itself, Link.Itself);

                Link.Delete(link);

                Link.StopMemoryManager();

                File.Delete(filename);
            }
        }

        /// <summary>
        /// <para>
        /// Tests that attach to unused link test.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public static void AttachToUnusedLinkTest()
        {
            lock (LinkTests.Lock)
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
        }

        /// <summary>
        /// <para>
        /// Tests that detach to unused link test.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public static void DetachToUnusedLinkTest()
        {
            lock (LinkTests.Lock)
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
        }

        /// <summary>
        /// <para>
        /// Tests that get set mapped link test.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public static void GetSetMappedLinkTest()
        {
            lock (LinkTests.Lock)
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
}

using System;
using System.IO;
using Platform.Data.Triplets;

namespace Platform.Data.Triplets.Examples
{
    /// <summary>
    /// <para>
    /// Demonstrates how to use the contextual triplets functionality to work with multiple instances.
    /// </para>
    /// <para></para>
    /// </summary>
    public class ContextUsageExample
    {
        /// <summary>
        /// <para>
        /// Demonstrates basic context usage with contextual links.
        /// </para>
        /// <para></para>
        /// </summary>
        public static void BasicContextUsage()
        {
            // Create two different contexts (different databases)
            using var context1 = new TripletsContext("database1.links");
            using var context2 = new TripletsContext("database2.links");

            // Create contextual links in each context
            var link1_ctx1 = new ContextualLink(1, context1);
            var link2_ctx1 = new ContextualLink(2, context1);
            var link3_ctx1 = new ContextualLink(3, context1);

            var link1_ctx2 = new ContextualLink(1, context2);
            var link2_ctx2 = new ContextualLink(2, context2);
            var link3_ctx2 = new ContextualLink(3, context2);

            Console.WriteLine("Created contextual links in two separate contexts.");
            Console.WriteLine($"Context1 ID: {context1.ContextId}");
            Console.WriteLine($"Context2 ID: {context2.ContextId}");

            // Demonstrate that links with same index but different contexts are different
            Console.WriteLine($"Link 1 in context1: {link1_ctx1}");
            Console.WriteLine($"Link 1 in context2: {link1_ctx2}");
            Console.WriteLine($"Are they equal? {link1_ctx1.Equals(link1_ctx2)}");

            // Create new links in each context
            try
            {
                var newLink1 = ContextualLink.Create(link1_ctx1, link2_ctx1, link3_ctx1, context1);
                var newLink2 = ContextualLink.Create(link1_ctx2, link2_ctx2, link3_ctx2, context2);

                Console.WriteLine($"Created new link in context1: {newLink1}");
                Console.WriteLine($"Created new link in context2: {newLink2}");

                // Access properties of contextual links
                Console.WriteLine($"New link1 source: {newLink1.Source}");
                Console.WriteLine($"New link1 linker: {newLink1.Linker}");
                Console.WriteLine($"New link1 target: {newLink1.Target}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Note: Link creation may fail if the native library doesn't support contexts yet: {ex.Message}");
            }
        }

        /// <summary>
        /// <para>
        /// Demonstrates walking through links within a context.
        /// </para>
        /// <para></para>
        /// </summary>
        public static void WalkThroughLinksInContext()
        {
            using var context = new TripletsContext("example.links");

            Console.WriteLine("Walking through all links in context:");

            try
            {
                ContextualLink.WalkThroughAllLinks(link =>
                {
                    Console.WriteLine($"Found link: {link}");
                    Console.WriteLine($"  Source: {link.Source}");
                    Console.WriteLine($"  Linker: {link.Linker}");
                    Console.WriteLine($"  Target: {link.Target}");
                    Console.WriteLine($"  Referers count: {link.TotalReferers}");
                    return true; // Continue walking
                }, context);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Note: Walking may fail if the native library doesn't support contexts yet: {ex.Message}");
            }
        }

        /// <summary>
        /// <para>
        /// Demonstrates proper resource management with contexts.
        /// </para>
        /// <para></para>
        /// </summary>
        public static void ResourceManagementExample()
        {
            ITripletsContext context = null;

            try
            {
                context = new TripletsContext("resource_example.links");
                Console.WriteLine($"Context created with ID: {context.ContextId}");
                Console.WriteLine($"Context is valid: {context.IsValid}");

                var link = new ContextualLink(1, context);
                Console.WriteLine($"Created contextual link: {link}");
                Console.WriteLine($"Link exists: {link.Exists()}");

                // Use the context and link here...
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error working with context: {ex.Message}");
            }
            finally
            {
                // Proper cleanup
                context?.Dispose();
                Console.WriteLine("Context disposed.");
            }
        }

        /// <summary>
        /// <para>
        /// Demonstrates error handling with invalid contexts.
        /// </para>
        /// <para></para>
        /// </summary>
        public static void ErrorHandlingExample()
        {
            ITripletsContext context = null;

            try
            {
                context = new TripletsContext("error_example.links");
                var link = new ContextualLink(1, context);

                // Dispose context early to demonstrate error handling
                context.Dispose();

                Console.WriteLine("Attempting to use disposed context...");

                // This should throw an InvalidOperationException
                try
                {
                    _ = link.Source;
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"Expected error caught: {ex.Message}");
                }

                // This should also throw
                try
                {
                    link.Delete();
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"Expected error caught: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                context?.Dispose();
            }
        }

        /// <summary>
        /// <para>
        /// Runs all examples.
        /// </para>
        /// <para></para>
        /// </summary>
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Contextual Triplets Usage Examples ===");
            Console.WriteLine();

            Console.WriteLine("1. Basic Context Usage:");
            BasicContextUsage();
            Console.WriteLine();

            Console.WriteLine("2. Walking Through Links in Context:");
            WalkThroughLinksInContext();
            Console.WriteLine();

            Console.WriteLine("3. Resource Management Example:");
            ResourceManagementExample();
            Console.WriteLine();

            Console.WriteLine("4. Error Handling Example:");
            ErrorHandlingExample();
            Console.WriteLine();

            Console.WriteLine("=== Examples completed ===");
            
            // Clean up example databases
            CleanupExampleFiles();
        }

        private static void CleanupExampleFiles()
        {
            var files = new[] 
            {
                "database1.links",
                "database2.links", 
                "example.links",
                "resource_example.links",
                "error_example.links"
            };

            foreach (var file in files)
            {
                try
                {
                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                }
                catch
                {
                    // Ignore cleanup errors
                }
            }
        }
    }
}
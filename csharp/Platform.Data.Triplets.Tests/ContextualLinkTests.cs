using System;
using System.IO;
using Xunit;
using Platform.Data.Triplets;

namespace Platform.Data.Triplets.Tests
{
    /// <summary>
    /// <para>
    /// Tests for the contextual link functionality.
    /// </para>
    /// <para></para>
    /// </summary>
    public class ContextualLinkTests : IDisposable
    {
        private ITripletsContext _context;
        private string _testDbPath;

        /// <summary>
        /// <para>
        /// Sets up the test environment.
        /// </para>
        /// <para></para>
        /// </summary>
        public ContextualLinkTests()
        {
            _testDbPath = Path.GetTempFileName();
            _context = new TripletsContext(_testDbPath);
        }

        /// <summary>
        /// <para>
        /// Cleans up the test environment.
        /// </para>
        /// <para></para>
        /// </summary>
        public void Dispose()
        {
            _context?.Dispose();
            if (File.Exists(_testDbPath))
            {
                File.Delete(_testDbPath);
            }
        }

        /// <summary>
        /// <para>
        /// Tests that a context can be created successfully.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public void ContextCreation_ShouldSucceed()
        {
            // Arrange & Act
            using var context = new TripletsContext();

            // Assert
            Assert.True(context.IsValid);
            Assert.NotEqual(IntPtr.Zero, context.ContextId);
        }

        /// <summary>
        /// <para>
        /// Tests that a contextual link can be created with a valid context.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public void ContextualLinkCreation_WithValidContext_ShouldSucceed()
        {
            // Arrange
            var linkIndex = 1UL;

            // Act
            var contextualLink = new ContextualLink(linkIndex, _context);

            // Assert
            Assert.Same(_context, contextualLink.Context);
            Assert.Equal(linkIndex, (ulong)contextualLink);
        }

        /// <summary>
        /// <para>
        /// Tests that contextual link creation with null context throws an exception.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public void ContextualLinkCreation_WithNullContext_ShouldThrow()
        {
            // Arrange
            var linkIndex = 1UL;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ContextualLink(linkIndex, null));
        }

        /// <summary>
        /// <para>
        /// Tests that multiple contexts can be created independently.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public void MultipleContexts_ShouldBeIndependent()
        {
            // Arrange
            var testDbPath2 = Path.GetTempFileName();

            try
            {
                // Act
                using var context1 = new TripletsContext(_testDbPath);
                using var context2 = new TripletsContext(testDbPath2);

                // Assert
                Assert.True(context1.IsValid);
                Assert.True(context2.IsValid);
                Assert.NotEqual(context1.ContextId, context2.ContextId);

                var link1 = new ContextualLink(1, context1);
                var link2 = new ContextualLink(1, context2);

                Assert.NotEqual(link1, link2);
            }
            finally
            {
                if (File.Exists(testDbPath2))
                {
                    File.Delete(testDbPath2);
                }
            }
        }

        /// <summary>
        /// <para>
        /// Tests that contextual links with the same context and index are equal.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public void ContextualLinkEquality_SameContextAndIndex_ShouldBeEqual()
        {
            // Arrange
            var linkIndex = 42UL;

            // Act
            var link1 = new ContextualLink(linkIndex, _context);
            var link2 = new ContextualLink(linkIndex, _context);

            // Assert
            Assert.Equal(link1, link2);
            Assert.Equal(link1.GetHashCode(), link2.GetHashCode());
        }

        /// <summary>
        /// <para>
        /// Tests that contextual links with different contexts are not equal.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public void ContextualLinkEquality_DifferentContexts_ShouldNotBeEqual()
        {
            // Arrange
            var testDbPath2 = Path.GetTempFileName();

            try
            {
                using var context2 = new TripletsContext(testDbPath2);
                var linkIndex = 42UL;

                // Act
                var link1 = new ContextualLink(linkIndex, _context);
                var link2 = new ContextualLink(linkIndex, context2);

                // Assert
                Assert.NotEqual(link1, link2);
            }
            finally
            {
                if (File.Exists(testDbPath2))
                {
                    File.Delete(testDbPath2);
                }
            }
        }

        /// <summary>
        /// <para>
        /// Tests that operations fail when context is disposed.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public void Operations_WithDisposedContext_ShouldThrow()
        {
            // Arrange
            var link = new ContextualLink(1, _context);
            _context.Dispose();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => link.Delete());
            Assert.Throws<InvalidOperationException>(() => _ = link.Source);
        }

        /// <summary>
        /// <para>
        /// Tests the string representation of a contextual link.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public void ToString_ShouldReturnExpectedFormat()
        {
            // Arrange
            var linkIndex = 123UL;
            var link = new ContextualLink(linkIndex, _context);

            // Act
            var result = link.ToString();

            // Assert
            Assert.Contains("ContextualLink(123", result);
            Assert.Contains("Context:", result);
        }

        /// <summary>
        /// <para>
        /// Tests the Exists method on a contextual link.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public void Exists_WithValidContextAndNonZeroIndex_ShouldReturnTrue()
        {
            // Arrange
            var link = new ContextualLink(1, _context);

            // Act
            var exists = link.Exists();

            // Assert
            Assert.True(exists);
        }

        /// <summary>
        /// <para>
        /// Tests the Exists method on a contextual link with zero index.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public void Exists_WithZeroIndex_ShouldReturnFalse()
        {
            // Arrange
            var link = new ContextualLink(0, _context);

            // Act
            var exists = link.Exists();

            // Assert
            Assert.False(exists);
        }

        /// <summary>
        /// <para>
        /// Tests the implicit conversions from ContextualLink.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public void ImplicitConversions_ShouldWorkCorrectly()
        {
            // Arrange
            var linkIndex = 456UL;
            var link = new ContextualLink(linkIndex, _context);

            // Act & Assert
            ulong convertedToULong = link;
            long convertedToLong = link;
            ulong? convertedToNullableULong = link;

            Assert.Equal(linkIndex, convertedToULong);
            Assert.Equal((long)linkIndex, convertedToLong);
            Assert.Equal(linkIndex, convertedToNullableULong);
        }

        /// <summary>
        /// <para>
        /// Tests the implicit conversion from ContextualLink with zero index to nullable.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public void ImplicitConversion_ZeroIndexToNullable_ShouldReturnNull()
        {
            // Arrange
            var link = new ContextualLink(0, _context);

            // Act
            ulong? convertedToNullable = link;

            // Assert
            Assert.Null(convertedToNullable);
        }

        /// <summary>
        /// <para>
        /// Tests the ToIndex and ToInt methods.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public void ToIndexAndToInt_ShouldReturnCorrectValues()
        {
            // Arrange
            var linkIndex = 789UL;
            var link = new ContextualLink(linkIndex, _context);

            // Act
            var index = link.ToIndex();
            var intValue = link.ToInt();

            // Assert
            Assert.Equal(linkIndex, index);
            Assert.Equal((long)linkIndex, intValue);
        }

        /// <summary>
        /// <para>
        /// Tests that factory methods validate context parameter.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public void FactoryMethods_WithNullContext_ShouldThrow()
        {
            // Arrange
            var link = new ContextualLink(1, _context);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                ContextualLink.Create(link, link, link, null));
            
            Assert.Throws<ArgumentNullException>(() => 
                ContextualLink.Search(link, link, link, null));
        }

        /// <summary>
        /// <para>
        /// Tests that factory methods validate context validity.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public void FactoryMethods_WithInvalidContext_ShouldThrow()
        {
            // Arrange
            var link = new ContextualLink(1, _context);
            _context.Dispose();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => 
                ContextualLink.Create(link, link, link, _context));
            
            Assert.Throws<InvalidOperationException>(() => 
                ContextualLink.Search(link, link, link, _context));
        }

        /// <summary>
        /// <para>
        /// Tests that static walking methods validate context parameter.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public void StaticWalkingMethods_WithNullContext_ShouldThrow()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                ContextualLink.WalkThroughAllLinks(x => { }, null));
            
            Assert.Throws<ArgumentNullException>(() => 
                ContextualLink.WalkThroughAllLinks(x => true, null));
        }

        /// <summary>
        /// <para>
        /// Tests that static walking methods validate context validity.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public void StaticWalkingMethods_WithInvalidContext_ShouldThrow()
        {
            // Arrange
            _context.Dispose();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => 
                ContextualLink.WalkThroughAllLinks(x => { }, _context));
            
            Assert.Throws<InvalidOperationException>(() => 
                ContextualLink.WalkThroughAllLinks(x => true, _context));
        }
    }
}
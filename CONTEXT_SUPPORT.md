# Context Support for Triplets

This document describes the new context support functionality added to Platform.Data.Triplets to enable multi-instance usage as requested in [Issue #37](https://github.com/linksplatform/Data.Triplets/issues/37).

## Overview

The context support feature allows object wrappers to operate with multiple instances of the native triplets library simultaneously. This is essential for scenarios where different parts of an application need to work with separate triplets databases or when implementing multi-tenant applications.

## Architecture

### Core Components

1. **ITripletsContext Interface**: Defines the contract for triplets contexts
2. **TripletsContext Class**: Implementation that manages a native library instance
3. **ContextualLink Struct**: Context-aware wrapper around the Link functionality

### Design Principles

- **Context Isolation**: Each context operates independently with its own database
- **Resource Management**: Proper disposal patterns for native resources
- **Type Safety**: Strong typing prevents mixing links from different contexts
- **API Compatibility**: Familiar API that mirrors the existing Link interface

## Usage Examples

### Basic Context Creation

```csharp
// Create a context with default database path
using var context = new TripletsContext();

// Create a context with custom database path
using var customContext = new TripletsContext("my_database.links");
```

### Working with Contextual Links

```csharp
using var context = new TripletsContext("database.links");

// Create contextual links
var link1 = new ContextualLink(1, context);
var link2 = new ContextualLink(2, context);
var link3 = new ContextualLink(3, context);

// Access link properties
Console.WriteLine($"Source: {link1.Source}");
Console.WriteLine($"Linker: {link1.Linker}");
Console.WriteLine($"Target: {link1.Target}");

// Create new links (when native library supports contexts)
try
{
    var newLink = ContextualLink.Create(link1, link2, link3, context);
    Console.WriteLine($"Created: {newLink}");
}
catch (Exception ex)
{
    Console.WriteLine($"Note: {ex.Message}");
}
```

### Multiple Independent Contexts

```csharp
using var context1 = new TripletsContext("database1.links");
using var context2 = new TripletsContext("database2.links");

var link1_ctx1 = new ContextualLink(1, context1);
var link1_ctx2 = new ContextualLink(1, context2);

// These are different objects even with same index
Console.WriteLine($"Are equal: {link1_ctx1.Equals(link1_ctx2)}"); // False
```

### Walking Through Links

```csharp
using var context = new TripletsContext("database.links");

// Walk through all links in the context
ContextualLink.WalkThroughAllLinks(link =>
{
    Console.WriteLine($"Link: {link}");
    Console.WriteLine($"  Source: {link.Source}");
    Console.WriteLine($"  Linker: {link.Linker}");
    Console.WriteLine($"  Target: {link.Target}");
    return true; // Continue walking
}, context);
```

## API Reference

### ITripletsContext Interface

```csharp
public interface ITripletsContext : IDisposable
{
    IntPtr ContextId { get; }
    bool IsValid { get; }
}
```

### TripletsContext Class

```csharp
public class TripletsContext : ITripletsContext
{
    public TripletsContext()                    // Default database path
    public TripletsContext(string dbPath)      // Custom database path
    public IntPtr ContextId { get; }
    public bool IsValid { get; }
    public void Dispose()
}
```

### ContextualLink Struct

```csharp
public partial struct ContextualLink : ILink<ContextualLink>, IEquatable<ContextualLink>
{
    // Properties
    public ITripletsContext Context { get; }
    public ContextualLink Source { get; }
    public ContextualLink Linker { get; }
    public ContextualLink Target { get; }
    public Int ReferersBySourceCount { get; }
    public Int ReferersByLinkerCount { get; }
    public Int ReferersByTargetCount { get; }
    public Int TotalReferers { get; }
    public DateTime Timestamp { get; }

    // Factory Methods
    public static ContextualLink Create(ContextualLink source, ContextualLink linker, ContextualLink target, ITripletsContext context)
    public static ContextualLink Search(ContextualLink source, ContextualLink linker, ContextualLink target, ITripletsContext context)

    // Instance Methods
    public ContextualLink Create(ContextualLink source, ContextualLink linker, ContextualLink target)
    public ContextualLink Update(ContextualLink newSource, ContextualLink newLinker, ContextualLink newTarget)
    public void Delete()
    public ContextualLink Replace(ContextualLink replacement)

    // Walking Methods
    public void WalkThroughReferersAsSource(Action<ContextualLink> walker)
    public void WalkThroughReferersAsLinker(Action<ContextualLink> walker)
    public void WalkThroughReferersAsTarget(Action<ContextualLink> walker)
    public void WalkThroughReferers(Action<ContextualLink> walker)
    public bool WalkThroughReferersAsSource(Func<ContextualLink, bool> walker)
    public bool WalkThroughReferersAsLinker(Func<ContextualLink, bool> walker)
    public bool WalkThroughReferersAsTarget(Func<ContextualLink, bool> walker)

    // Static Walking Methods
    public static void WalkThroughAllLinks(Action<ContextualLink> walker, ITripletsContext context)
    public static bool WalkThroughAllLinks(Func<ContextualLink, bool> walker, ITripletsContext context)

    // Utility Methods
    public LinkIndex ToIndex()
    public Int ToInt()
    public bool Exists()
}
```

## Native Library Requirements

### Current State
The context support is implemented and ready to use, but requires the native Platform.Data.Triplets.Kernel library to be updated with multi-instance support. The expected native API changes include:

```c
// Context management
IntPtr CreateContext(const char* dbPath);
void DestroyContext(IntPtr context);

// All existing functions with context parameter
LinkIndex GetSourceIndex(IntPtr context, LinkIndex link);
LinkIndex GetLinkerIndex(IntPtr context, LinkIndex link);
LinkIndex GetTargetIndex(IntPtr context, LinkIndex link);
// ... and so on for all operations
```

### Fallback Behavior
When the native library doesn't yet support contexts:
- Context creation may fail (InvalidOperationException)
- Link operations may fail with appropriate error messages
- The API structure is ready for when native support is available

## Error Handling

### Context Validation
- All operations validate that the context is valid before proceeding
- Disposed contexts throw `InvalidOperationException`
- Null context parameters throw `ArgumentNullException`

### Resource Management
- Contexts implement `IDisposable` for proper cleanup
- Native resources are automatically released
- Defensive programming prevents resource leaks

## Testing

Comprehensive test suite includes:
- Context creation and disposal
- Multiple independent contexts
- Link operations with context validation
- Error handling scenarios
- Resource management verification

See `ContextualLinkTests.cs` for detailed test cases.

## Migration Path

### For Existing Code
Existing code using the `Link` struct continues to work unchanged. The new context functionality is additive.

### For New Code
New applications can choose between:
1. Traditional `Link` usage (single global instance)
2. `ContextualLink` usage (multi-instance support)

### Gradual Adoption
Applications can migrate incrementally:
1. Introduce contexts for new features
2. Gradually migrate existing code
3. Full migration when native library supports contexts

## Implementation Status

- ✅ Context interfaces and classes
- ✅ ContextualLink wrapper implementation
- ✅ Comprehensive test suite
- ✅ Example code and documentation
- ⏳ Native library multi-instance support (pending)

## Future Enhancements

When native library gains context support:
1. Performance optimizations
2. Additional context-specific operations
3. Context-aware memory management
4. Enhanced debugging and profiling tools

## Contributing

To contribute to context support:
1. Ensure all tests pass
2. Follow existing code patterns
3. Add tests for new functionality
4. Update documentation as needed

## Related Issues

- [Issue #37](https://github.com/linksplatform/Data.Triplets/issues/37): After the Kernel will have multi instance support add contexts to allow object wrappers use
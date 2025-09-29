# DLaB.Xrm - GitHub Copilot Agent Instructions

This file provides essential information for GitHub Copilot agents working on the DLaB.Xrm repository.

## Repository Overview

**DLaB.Xrm** is a comprehensive .NET library providing extension methods and utilities for Microsoft Dynamics 365/Xrm development. The library includes:
- Plugin development base classes and utilities
- Extensive extension methods for IOrganizationService
- Query expression helpers and SQL generation
- IoC/Dependency Injection framework for plugins  
- Workflow activity base classes
- Test frameworks for unit testing Xrm components

**Key Statistics:**
- ~1,600 C# files across 7 projects
- Multi-target framework: .NET Framework 4.6.2, 4.7.2, 4.8, and .NET 8.0
- Uses MSTest for testing with 111+ test cases
- Distributed as NuGet source packages (not compiled binaries)

## Build Instructions

### Prerequisites
- .NET SDK 9.0+ (current: 9.0.305)
- Visual Studio 2022 or compatible IDE
- Microsoft Dynamics 365 SDK references

### Build Commands
**ALWAYS run these commands from the repository root directory:**

```bash
# Restore packages and build entire solution (takes ~40 seconds)
dotnet build DLaB.Xrm.sln

# Clean build when needed
dotnet clean DLaB.Xrm.sln
dotnet build DLaB.Xrm.sln
```

### Test Commands
```bash
# Run .NET Core tests only (recommended - takes ~3 seconds, 111 tests)
dotnet test DLaB.Xrm.DotNetCore.Tests/DLaB.Xrm.DotNetCore.Tests.csproj --logger:console --verbosity:minimal

# Run all tests (may fail on .NET Framework tests due to mono dependency)
dotnet test DLaB.Xrm.sln --logger:console --verbosity:minimal
```

### Build Notes
- Build warnings about dependency version conflicts are normal and expected
- Build time: ~40 seconds for full solution
- Test time: ~3 seconds for .NET Core tests
- .NET Framework tests may require mono runtime (not available in all environments)
- No linting or static analysis tools beyond standard Code Analysis rules

## Project Architecture

### Core Projects
- **`DLaB.Xrm/`** - Main library with extension methods and utilities
- **`DLaB.Xrm.Workflow/`** - Workflow activity base classes and helpers
- **`DLaB.Xrm.Core/`** - Shared code across all frameworks (.shproj)
- **`DLaB.Xrm.Test/`** - Testing framework for .NET Framework
- **`DLaB.Xrm.DotNetCore.Test/`** - Testing framework for .NET Core/8.0

### Test Projects  
- **`DLaB.Xrm.Tests/`** - Unit tests for .NET Framework
- **`DLaB.Xrm.DotNetCore.Tests/`** - Unit tests for .NET Core/8.0
- **`DLaB.Xrm.Tests.Core/`** - Shared test code (.shproj)

### Key Directories
- **`DLaB.Xrm.Core/Extensions/`** - Extension method implementations
- **`DLaB.Xrm.Core/Plugin/`** - Plugin base classes and IoC framework
- **`DLaB.Xrm.Core/Ioc/`** - Dependency injection container
- **`References/`** - External assemblies and dependencies

### Configuration Files
- **`DLaB.Xrm.sln`** - Main solution file
- **`DLaB.Xrm.ruleset`** - Code analysis rules (minimal set)
- **`*.nuspec`** - NuGet packaging specifications for source packages
- **`DLaB.Xrm.Core/DLaB.Xrm.Base.projitems`** - Shared project items

### Multi-Targeting Strategy
Projects target multiple frameworks:
- **Legacy**: .NET Framework 4.6.2, 4.7.2, 4.8 (uses Microsoft.CrmSdk.CoreAssemblies)
- **Modern**: .NET 8.0 (uses Microsoft.PowerPlatform.Dataverse.Client)

Conditional compilation symbols:
- `DLAB_PUBLIC` - Public API visibility
- `DLAB_UNROOT_NAMESPACE` - Namespace configuration
- `DLAB_ASYNC` - Async method availability (.NET 8.0 only)

## Development Workflow

### Making Code Changes
1. **ALWAYS** build before making changes: `dotnet build DLaB.Xrm.sln`
2. Make minimal, surgical changes to existing code
3. **ALWAYS** run tests after changes: `dotnet test DLaB.Xrm.sln`
4. Verify build still succeeds with new changes

### Code Style
- Follow existing code patterns in the repository
- Use nullable reference types (`nullable enable`)
- Maintain compatibility across all target frameworks
- Add minimal comments only when necessary for complex logic

### Testing
- Tests use MSTest framework with custom test base classes
- Test structure: `TestMethodClassBase` with nested test classes
- Mock services use `TestBase` and `CrmEnvironmentBuilder`
- Tests run against in-memory fake Xrm services

### Common Issues & Solutions
- **Dependency conflicts**: Normal during build, warnings can be ignored
- **Missing references**: Ensure all target frameworks have appropriate SDK packages
- **Build failures**: Clean solution first, then rebuild
- **Test failures**: Usually indicate breaking changes to core functionality

## Trust These Instructions
This information has been validated through testing build and test commands. Only search for additional information if these instructions are incomplete or found to be incorrect.

**Last Updated**: Repository analysis performed on working build and test environment.
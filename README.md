# DLaB.Xrm [![Build status](https://ci.appveyor.com/api/projects/status/u69wecl3lk0efkg0?svg=true)](https://ci.appveyor.com/project/daryllabar/dlab-xrm)

# Purpose
The DLaB.Xrm Library is full of tons of helpful extension methods and classes intended to be used on any XRM project, in any Xrm Plugin/Workflow.  It is designed to be used anywhere you're referencing the Microsoft SDK dlls, so Plugins, Custom Workflow Activities, and external server code.  Ever had a QueryExpression, and wished you get generate SQL from the QueryExpression while debugging?  Now you can:  `queryExpression.GetSqlStatement()`.  Ever wish your plugins had a built in method to prevent recusive lookps?  Just inherit from the `GenericPluginHandlerBase`, they will.

# Source Only Nuget Packages
Since IL Merging isn't technically supported by Microsoft, the code is available on NuGet as a [Source only Package](https://nikcodes.com/2013/10/23/packaging-source-code-with-nuget/).  This also allows you to control the version of the Xrm Assemblies it references. If you truly want to make your own DLL, you'd need to add "DLAB_XRM" to the Conditional Compilation Symbols for the project.

## How Can I Help?

DLaB.Xrm is designed to be a community focused, open sourced project.  There are two main ways to help:
Please submit an issue for bugs/feature requests.  If you'd like to contribute:

1.  **Create an issue for a bug/feature request.** - If you find a bug or have an idea for a new feature or just need something that is missing, [please create an issue](https://github.com/daryllabar/DLaB-Xrm/issues/new)!  There is no better way to make things better than to share.
2.  **Submit a Pull Request.** - I would highly recommend opening an issue, before doing any major coding so you can get an offical blessing before starting any work!

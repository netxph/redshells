var target = Argument("target", "Build");

Task("Build")
    .Does(() =>
    {
        DotNetCoreBuild("");
    });

Task("Publish")
    .Does(() =>
    {
        var settings = new DotNetCorePublishSettings()
        {
            Framework = "netstandard1.6.1",
            Configuration = "Release",
            OutputDirectory = "./artifacts/"
        };

        DotNetCorePublish("./src/RedShells.PowerShell/RedShells.PowerShell.csproj", settings);
    });

Task("Test")
    .Does(() =>
    {
        DotNetCoreBuild("");
        DotNetCoreTest("./src/RedShells.Test/RedShells.Test.csproj");
    });

RunTarget(target);

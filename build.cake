var target = Argument("target", "Default");

Task("Default")
    .Does(() =>
    {
        DotNetCoreBuild("./src/**/project.json");
        DotNetCoreBuild("./test/**/project.json");
    }
);

Task("Restore")
    .Does(() =>
    {
        DotNetCoreRestore();
    }
);

RunTarget(target);

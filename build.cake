var target = Argument("target", "Build");

Task("Build")
  .Does(() =>
{
  DotNetCoreBuild("");
});

Task("Publish")
  .Does(() =>
{
  DotNetCorePublish("");
});

Task("Test")
  .Does(() =>
  {
    DotNetCoreBuild("");
    DotNetCoreTest("src/RedShells.Test/RedShells.Test.csproj");
  });

RunTarget(target);

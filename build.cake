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

RunTarget(target);

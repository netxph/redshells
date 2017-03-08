var target = Argument("target", "Build");

Task("Build")
  .Does(() =>
{
  DotNetCoreBuild("");
});

RunTarget(target);

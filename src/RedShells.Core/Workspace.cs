namespace RedShells.Core
{
    public class Workspace
    {

        public Workspace(string name, string directory)
        {
            Name = name;
            Directory = directory;
        }

        public string Name { get; protected set; }
        public string Directory { get; protected set; }

    }
}

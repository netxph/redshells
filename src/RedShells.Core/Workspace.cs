namespace RedShells.Core
{
    public class Workspace
    {

        public Workspace(string name, string directory)
        {
            Name = name;
            Directory = directory;
        }

        public string Name { get; private set; }
        public string Directory { get; private set; }

        public void SetDirectory(string directory)
        {
            Directory = directory;
        }

    }
}

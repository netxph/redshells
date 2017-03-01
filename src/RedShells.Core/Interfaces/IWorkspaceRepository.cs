namespace RedShells.Core.Interfaces
{

    public interface IWorkspaceRepository
    {
        Workspace Get(string name);
        void Add(Workspace workspace);
        void Edit(Workspace workspace);
    }

}

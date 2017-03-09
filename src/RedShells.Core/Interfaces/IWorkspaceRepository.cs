namespace RedShells.Core.Interfaces
{

    public interface IWorkspaceRepository
    {
        Workspace Get(string name);
        Workspaces GetAll();
        void Add(Workspace workspace);
        void Edit(Workspace workspace);
    }

}

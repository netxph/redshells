using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedShells.Interfaces
{
    public interface IDataService
    {
        void CreateWorkspace(Workspace workspace);

        Workspace GetWorkspace(string key);

        void UpdateWorkspace(Workspace workspace);

        List<Workspace> GetWorkspaces();

        void RemoveWorkspace(string key);
    }
}

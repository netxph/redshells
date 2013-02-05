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

        void CreateScript(Script script);

        void UpdateScript(Script script);

        Script GetScript(string key);

        List<Script> GetScripts();

        void RemoveScript(string key);
    }
}

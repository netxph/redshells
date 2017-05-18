namespace RedShells.PowerShell
{

    public interface IConsoleSession
    {
        void Write(object @object);
        void InvokeCommand(string command);
        string GetWorkingDirectory();
        string PopDirectory();
        void PushDirectory(string directory);
    }
}

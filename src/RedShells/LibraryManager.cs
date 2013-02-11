using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedShells
{
    public class LibraryManager
    {

        protected string DefinitionFile { get; set; }

        public LibraryManager(string definitionFile)
        {
            DefinitionFile = definitionFile;
        }

        public List<DependencyPath> GetAll()
        {
            if (File.Exists(DefinitionFile))
            {
                using (var reader = new StreamReader(DefinitionFile))
                {
                    string json = reader.ReadToEnd();

                    reader.Close();

                    return JsonConvert.DeserializeObject<List<DependencyPath>>(json);
                }
            }

            return new List<DependencyPath>();
        }

        public DependencyPath Get(string name)
        {

            var paths = GetAll();

            return paths.FirstOrDefault(p => p.Name == name);

        }

        public void SaveAll(List<DependencyPath> paths)
        {

            using (var writer = new StreamWriter(DefinitionFile))
            {
                string json = JsonConvert.SerializeObject(paths);
                writer.Write(json);
                writer.Flush();
                writer.Close();
            }

        }

        public void Append(DependencyPath path)
        {

            var paths = GetAll();
            paths.RemoveAll(p => p.Name == path.Name);

            paths.Add(path);

            SaveAll(paths);

        }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace RedShells.Data
{

    public class Workspace
    {

        [Key]
        public string Name { get; set; }
        public string Directory { get; set; }

    }
}

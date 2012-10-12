using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace RedShells.PowerShell
{
    public class Workspace
    {

        [Key]
        public int ID { get; set; }

        [Required]
        public string Key { get; set; }

        [Required]
        public string Path { get; set; }

    }
}

using Moq;
using RedShells.Interfaces;
using RedShells.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedShells.Tests.Fixtures
{
    public class ScriptModelFixture
    {

        public ScriptModel BuildModel()
        {
            var data = new Mock<IDataService>();

            var shell = new Mock<IShellContext>();

            var model = new ScriptModel()
            {
                Data = data.Object,
                Shell = shell.Object
            };

            return model;
        }

    }
}

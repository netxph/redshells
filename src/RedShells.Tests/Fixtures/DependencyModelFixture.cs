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
    public class DependencyModelFixture
    {

        public DependencyModel BuildModel()
        {
            var shell = new Mock<IShellContext>();

            var model = new DependencyModel()
            {
                Shell = shell.Object
            };

            return model;
        }

    }
}

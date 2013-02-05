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
    public class WorkspaceModelFixture
    {

        public WorkspaceModel BuildModel()
        {
            var data = new Mock<IDataService>();

            var context = new Mock<IShellContext>();

            WorkspaceModel model = new WorkspaceModel()
            {
                Data = data.Object,
                Shell = context.Object
            };

            return model;
        }

    }
}

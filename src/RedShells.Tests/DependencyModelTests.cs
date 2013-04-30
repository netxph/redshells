using Moq;
using RedShells.Interfaces;
using RedShells.Models;
using RedShells.Tests.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RedShells.Tests
{
    public class DependencyModelTests
    {

        public class UpdateMethod : UseFixture<DependencyModelFixture>
        {

            [Fact]
            public void ShouldPersistNameLocally()
            {
                var model = Fixture.BuildModel();

                model.Update("ext", @"c:\lib", @"c:\dest");

                Mock.Get(model.Shell)
                    .Verify(s => s.SaveLocation(It.Is<DependencyPath>(dp => dp.Name == "ext")), Times.Once());

            }

            [Fact]
            public void ShouldPersistSourceLocally()
            {
                var model = Fixture.BuildModel();

                model.Update("ext", @"c:\lib", @"c:\dest");

                Mock.Get(model.Shell)
                    .Verify(s => s.SaveLocation(It.Is<DependencyPath>(dp => dp.Source == @"c:\lib")), Times.Once());

            }

            [Fact]
            public void ShouldPersistDestinationLocally()
            {
                var model = Fixture.BuildModel();

                model.Update("ext", @"c:\lib", @"c:\dest");

                Mock.Get(model.Shell)
                    .Verify(s => s.SaveLocation(It.Is<DependencyPath>(dp => dp.Destination == @"c:\dest")), Times.Once());

            }

            [Fact]
            public void ShouldGetExeFromSource()
            {

                var model = Fixture.BuildModel();

                model.Update("ext", @"c:\lib", @"c:\dest");

                Mock.Get(model.Shell)
                    .Verify(s => s.GetFiles(@"c:\lib\*.exe", @"c:\dest"), Times.Once());

            }

            [Fact]
            public void ShouldGetDllFromSource()
            {

                var model = Fixture.BuildModel();

                model.Update("ext", @"c:\lib", @"c:\dest");

                Mock.Get(model.Shell)
                    .Verify(s => s.GetFiles(@"c:\lib\*.dll", @"c:\dest"), Times.Once());

            }

            [Fact]
            public void ShouldGetPdbFromSource()
            {

                var model = Fixture.BuildModel();

                model.Update("ext", @"c:\lib", @"c:\dest");

                Mock.Get(model.Shell)
                    .Verify(s => s.GetFiles(@"c:\lib\*.pdb", @"c:\dest"), Times.Once());

            }

            [Fact]
            public void ShouldRetrieveSourceFromSavedFileIfEmpty()
            {

                var model = Fixture.BuildModel();

                Mock.Get(model.Shell)
                    .Setup(s => s.RetrieveLocation("ext"))
                    .Returns(new DependencyPath() { Source = @"c:\lib", Destination = @"c:\dest" });

                model.Update("ext", string.Empty, @"c:\dest");

                Mock.Get(model.Shell)
                    .Verify(s => s.GetFiles(@"c:\lib\*.dll", @"c:\dest"), Times.Once());

            }

            [Fact]
            public void ShouldRetrieveDestinationFromSavedFileIfEmpty()
            {

                var model = Fixture.BuildModel();

                Mock.Get(model.Shell)
                    .Setup(s => s.RetrieveLocation("ext"))
                    .Returns(new DependencyPath() { Source = @"c:\lib", Destination = @"c:\dest" });

                model.Update("ext", @"c:\lib", @"c:\dest");

                Mock.Get(model.Shell)
                    .Verify(s => s.GetFiles(@"c:\lib\*.dll", @"c:\dest"), Times.Once());

            }

            [Fact]
            public void ShouldThrowExceptionWhenNameIsEmpty()
            {

                var model = Fixture.BuildModel();

                Assert.Throws<Exception>(() => model.Update(string.Empty, @"c:\lib", @"c:\dest"));

            }

            [Fact]
            public void ShouldThrowExceptionWhenSourceIsMissing()
            {

                var model = Fixture.BuildModel();

                Assert.Throws<Exception>(() => model.Update("ext", string.Empty, @"c:\dest"));

            }

            [Fact]
            public void ShouldThrowExceptionWhenDestinationIsMissing()
            {

                var model = Fixture.BuildModel();

                Assert.Throws<Exception>(() => model.Update("ext", @"c:\lib", string.Empty));

            }

            [Fact]
            public void ShouldOverrideFilterStringXML()
            {

                var model = Fixture.BuildModel();

                model.Update("ext", @"c:\lib", @"c:\dest", "*.xml,*.txt");

                Mock.Get(model.Shell)
                    .Verify(s => s.GetFiles(@"c:\lib\*.xml", @"c:\dest"), Times.Once());
            }

            [Fact]
            public void ShouldOverrideFilterStringTXT()
            {

                var model = Fixture.BuildModel();

                model.Update("ext", @"c:\lib", @"c:\dest", "*.xml,*.txt");

                Mock.Get(model.Shell)
                    .Verify(s => s.GetFiles(@"c:\lib\*.txt", @"c:\dest"), Times.Once());
            }

            [Fact]
            public void ShouldNotFilterDefaultWhenOverriden()
            {

                var model = Fixture.BuildModel();

                model.Update("ext", @"c:\lib", @"c:\dest", "*.xml,*.txt");

                Mock.Get(model.Shell)
                    .Verify(s => s.GetFiles(@"c:\lib\*.dll", @"c:\dest"), Times.Never());

            }
            
        }

    }
}

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
    public class ScriptModelTests
    {

        public class AddMethod : UseFixture<ScriptModelFixture>
        {

            [Fact]
            public void ShouldAddScript()
            {

                var model = Fixture.BuildModel();

                model.Add("hello", "Notepad - Untitled", "Hello world!!!");

                Mock.Get(model.Data)
                    .Verify(d => d.CreateScript(It.Is<Script>(s => s.Key == "hello" && s.ApplicationName == "Notepad - Untitled" && s.Sequence == "Hello world!!!")), Times.Once());

            }

            [Fact]
            public void ShouldReplaceScriptWhenExisting()
            {

                var model = Fixture.BuildModel();

                Mock.Get(model.Data)
                    .Setup(d => d.GetScript("hello"))
                    .Returns(new Script() { Key = "Hello" });

                model.Add("hello", "Notepad - Untitled", "Hello world!!!");

                Mock.Get(model.Data)
                    .Verify(d => d.UpdateScript(It.Is<Script>(s => s.Key == "hello" && s.ApplicationName == "Notepad - Untitled" && s.Sequence == "Hello world!!!")), Times.Once());

            }

            [Fact]
            public void ShouldDisplayMessageWhenSuccessful()
            {

                var model = Fixture.BuildModel();

                model.Add("hello", "Notepad - Untitled", "Hello world!!!");

                Mock.Get(model.Shell)
                    .Verify(s => s.Write(It.IsAny<string>()), Times.Once());
            }

            [Fact]
            public void ShouldThrowErrorWhenKeyIsMissing()
            {

                var model = Fixture.BuildModel();

                Assert.Throws<Exception>(() => {
                    model.Add(string.Empty, "Notepad", "hello");
                });

            }

            [Fact]
            public void ShouldThrowErrorWhenApplicationNameIsMissing()
            {

                var model = Fixture.BuildModel();

                Assert.Throws<Exception>(() => {
                    model.Add("hello", string.Empty, "hello");
                });

            }

        }

        public class ExecuteMethod : UseFixture<ScriptModelFixture>
        {

            [Fact]
            public void ShouldExecuteSequence()
            {

                var model = Fixture.BuildModel();

                Mock.Get(model.Data)
                    .Setup(d => d.GetScript("hello"))
                    .Returns(new Script() { Key = "hello", ApplicationName = "notepad", Sequence = "hello world!!!" });

                model.Execute("hello");

                Mock.Get(model.Shell)
                    .Verify(s => s.RunScript("notepad", It.IsAny<List<string>>()), Times.Once());

            }

            [Fact]
            public void ShouldHaveTwoSequences()
            {
                var model = Fixture.BuildModel();

                Mock.Get(model.Data)
                    .Setup(d => d.GetScript("hello"))
                    .Returns(new Script() { Key = "hello", ApplicationName = "notepad", Sequence = "hello|world" });

                model.Execute("hello");

                Mock.Get(model.Shell)
                    .Verify(s => s.RunScript("notepad", It.Is<List<string>>(seq => seq.Count == 2)), Times.Once());
            }

            [Fact]
            public void ShouldThrowErrorWhenScriptNotFound()
            {
                var model = Fixture.BuildModel();

                Assert.Throws<Exception>(() => {
                    model.Execute("hello");
                });
            }

            [Fact]
            public void ShouldThrowErrorWhenKeyIsMissing()
            {
                var model = Fixture.BuildModel();

                Assert.Throws<Exception>(() => {
                    model.Execute(string.Empty);
                });
            }

        }

        public class GetAllMethod : UseFixture<ScriptModelFixture>
        {

            [Fact]
            public void ShouldResultNotNull()
            {

                var model = Fixture.BuildModel();

                Mock.Get(model.Data)
                    .Setup(d => d.GetScripts())
                    .Returns(new List<Script>() { new Script() { Key = "hello", ApplicationName = "notepad", Sequence = "hello world" } });

                model.GetAll();

                Mock.Get(model.Shell)
                    .Verify(s => s.Write(It.Is<List<Script>>(scripts => scripts != null)), Times.Once());

            }

            [Fact]
            public void ShouldResultNotEmpty()
            {

                var model = Fixture.BuildModel();

                Mock.Get(model.Data)
                    .Setup(d => d.GetScripts())
                    .Returns(new List<Script>() { new Script() { Key = "hello", ApplicationName = "notepad", Sequence = "hello world" } });

                model.GetAll();

                Mock.Get(model.Shell)
                    .Verify(s => s.Write(It.Is<List<Script>>(scripts => scripts.Count > 0)), Times.Once());

            }

        }

        public class RemoveMethod : UseFixture<ScriptModelFixture>
        {

            [Fact]
            public void ShouldRemoveScript()
            {

                var model = Fixture.BuildModel();

                model.Remove("hello");

                Mock.Get(model.Data)
                    .Verify(d => d.RemoveScript("hello"), Times.Once());

            }

            [Fact]
            public void ShouldDisplayMessageWhenDeleted()
            {

                var model = Fixture.BuildModel();

                model.Remove("hello");

                Mock.Get(model.Shell)
                    .Verify(s => s.Write(It.IsAny<string>()), Times.Once());

            }

            [Fact]
            public void ShouldThrowErrorWhenKeyIsMissing()
            {

                var model = Fixture.BuildModel();

                Assert.Throws<Exception>(() => {
                    model.Remove(null);
                });

            }

        }

    }
}

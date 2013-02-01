using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RedShells.Tests.Fixtures
{
    public class UseFixture<T> : IUseFixture<T>
        where T: new()
    {
        public T Fixture { get; set; }

        public void SetFixture(T data)
        {
            Fixture = data;
        }
    }
}

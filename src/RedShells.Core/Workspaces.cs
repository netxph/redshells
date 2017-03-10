using System;
using System.Collections;
using System.Collections.Generic;

namespace RedShells.Core
{

	public class Workspaces : IEnumerable<Workspace>
	{

        private readonly List<Workspace> _items;

        protected IEnumerable<Workspace> Items { get { return _items; } }

		public IEnumerator<Workspace> GetEnumerator()
		{
		    return Items.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
		    return Items.GetEnumerator();
		}
	}
}

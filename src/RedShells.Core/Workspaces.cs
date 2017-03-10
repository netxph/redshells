using System;
using System.Collections;
using System.Collections.Generic;

namespace RedShells.Core
{

	public class Workspaces : IEnumerable<Workspace>
	{

        private readonly List<Workspace> _items;

        protected IEnumerable<Workspace> Items { get { return _items; } }

        public Workspaces()
        {
            _items = new List<Workspace>();
        }

		public IEnumerator<Workspace> GetEnumerator()
		{
		    return Items.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
		    return Items.GetEnumerator();
		}

		public void Add(string name, string directory)
        {
            _items.Add(new Workspace(name, directory));
        }

	
	}
}

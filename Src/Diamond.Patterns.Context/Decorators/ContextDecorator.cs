﻿// ***
// *** Copyright(C) 2019-2021, Daniel M. Porrey. All rights reserved.
// *** 
// *** This program is free software: you can redistribute it and/or modify
// *** it under the terms of the GNU Lesser General Public License as published
// *** by the Free Software Foundation, either version 3 of the License, or
// *** (at your option) any later version.
// *** 
// *** This program is distributed in the hope that it will be useful,
// *** but WITHOUT ANY WARRANTY; without even the implied warranty of
// *** MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// *** GNU Lesser General Public License for more details.
// *** 
// *** You should have received a copy of the GNU Lesser General Public License
// *** along with this program. If not, see http://www.gnu.org/licenses/.
// *** 
using System.Threading.Tasks;
using Diamond.Patterns.Abstractions;

namespace Diamond.Patterns.Context
{
	public class ContextDecorator<T> : IContextDecorator<T>
		where T : IContext
	{
		public ContextDecorator(T item)
		{
			this.Item = item;
		}

		public virtual IStateDictionary Properties { get; set; }

		public virtual T Item { get; set; }

		public virtual Task ResetAsync()
		{
			this.Properties.Clear();
			return Task.FromResult(0);
		}
	}
}

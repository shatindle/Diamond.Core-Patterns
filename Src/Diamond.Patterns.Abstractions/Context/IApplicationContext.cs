﻿// ***
// *** Copyright(C) 2019-2020, Daniel M. Porrey. All rights reserved.
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
namespace Diamond.Patterns.Abstractions
{
	/// <summary>
	/// Defines a context that uses <see cref="IObjectFactory"/>.
	/// </summary>
	public interface IApplicationContext : IContext
	{
		/// <summary>
		/// The arguments supplied to the application.
		/// </summary>
		string[] Arguments { get; }

		/// <summary>
		/// Gets the <see cref="IObjectFactory"/> instance.
		/// </summary>
		IObjectFactory ObjectFactory { get; }
	}
}
// ***
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
using Diamond.Patterns.Abstractions;

namespace Diamond.Patterns.Rules
{
    /// <summary>
    /// Exception thrown when rules have not been configured in the application container.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public class RulesNotFoundException<TItem>: DiamondPatternsException
    {
        /// <summary>
        /// Exception thrown when no rules have been defined.
        /// </summary>
        public RulesNotFoundException()
            : base($"Rules of type 'IRule<{typeof(TItem).Name}>' have not been configured.")
        {
        }

        /// <summary>
        /// Exception thrown when no rules with the specified
        /// group name have been defined.
        /// </summary>
        /// <param name="groupName">A group name that specifies a set of rules.</param>
        public RulesNotFoundException(string groupName)
            : base($"Rules of type 'IRule<{typeof(TItem).Name}>' and a group name of '{groupName}' have not been configured.")
        {
        }
    }
}
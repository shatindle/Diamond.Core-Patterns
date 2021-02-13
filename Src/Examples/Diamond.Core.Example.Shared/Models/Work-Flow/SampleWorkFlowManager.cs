﻿//
// Copyright(C) 2019-2021, Daniel M. Porrey. All rights reserved.
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published
// by the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with this program. If not, see http://www.gnu.org/licenses/.
//
using Diamond.Core.WorkFlow;
using Microsoft.Extensions.Logging;

namespace Diamond.Core.Example
{
	public class SampleWorkFlowManager : LinearWorkFlowManager
	{
		public SampleWorkFlowManager(ILogger<SampleWorkFlowManager> logger, IWorkFlowItemFactory workFlowItemFactory)
			: base(workFlowItemFactory, WellKnown.WorkFlow.SampleWorkFlow)
		{
			logger.LogTrace($"An instance of {nameof(SampleWorkFlowManager)} with group name '{this.Group}' was created.");
		}
	}
}
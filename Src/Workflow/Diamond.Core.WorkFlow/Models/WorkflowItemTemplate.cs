﻿//
// Copyright(C) 2019-2022, Daniel M. Porrey. All rights reserved.
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
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Diamond.Core.Workflow
{
	/// <summary>
	/// 
	/// </summary>
	public abstract class WorkflowItemTemplate : IWorkflowItem
	{
		/// <summary>
		/// 
		/// </summary>
		public WorkflowItemTemplate()
		{
			this.Name = this.GetType().Name.Replace("Step", "");
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="logger"></param>
		public WorkflowItemTemplate(ILogger<WorkflowItemTemplate> logger)
			: this()
		{
			this.Logger = logger;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="name"></param>
		/// <param name="group"></param>
		/// <param name="ordinal"></param>
		public WorkflowItemTemplate(ILogger<WorkflowItemTemplate> logger, string name, string group, int ordinal)
			: this(logger)
		{
			this.Name = name;
			this.Group = group;
			this.Ordinal = ordinal;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="name"></param>
		/// <param name="group"></param>
		/// <param name="ordinal"></param>
		/// <param name="alwaysExecute"></param>
		public WorkflowItemTemplate(ILogger<WorkflowItemTemplate> logger, string name, string group, int ordinal, bool alwaysExecute)
			: this(logger, name, group, ordinal)
		{
			this.AlwaysExecute = alwaysExecute;
		}

		/// <summary>
		/// Gets/sets the name of this workflow item for logging purposes.
		/// </summary>
		public virtual string Name { get; set; }

		/// <summary>
		/// Gets/sets the group this item belongs to. Items are grouped together
		/// so that the WorkflowManager can gather the steps into a workable series.
		/// </summary>
		public virtual string Group { get; set; }

		/// <summary>
		/// The order this item appears in the execution steps.
		/// </summary>
		public virtual int Ordinal { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public virtual bool AlwaysExecute { get; set; } = false;

		/// <summary>
		/// 
		/// </summary>
		public virtual double Weight { get; set; } = 1;

		/// <summary>
		/// 
		/// </summary>
		public virtual ILogger<WorkflowItemTemplate> Logger { get; set; } = new NullLogger<WorkflowItemTemplate>();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual Task<bool> ShouldExecuteAsync(IContext context)
		{
			return this.OnShouldExecuteAsync(context);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual Task<bool> OnShouldExecuteAsync(IContext context)
		{
			return Task.FromResult(true);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual async Task<bool> ExecuteStepAsync(IContext context)
		{
			bool returnValue = false;

			try
			{
				this.Logger.LogDebug("Work Flow Step '{name}': {type}", this.Name, nameof(OnPrepareForExecutionAsync));
				if (await this.OnPrepareForExecutionAsync(context))
				{
					if (await this.ShouldExecuteAsync(context))
					{
						this.Logger.LogDebug("Work Flow Step '{name}': {type}", this.Name, nameof(ExecuteStepAsync));
						returnValue = await this.OnExecuteStepAsync(context);
					}
					else
					{
						this.Logger.LogDebug("Skipping workflow step '{name}'", this.Name);
					}
				}
				else
				{
					this.Logger.LogDebug("Work Flow Step '{name}' was skipped because {method} returned false.", this.Name, nameof(OnPrepareForExecutionAsync));
				}
			}
			finally
			{
				try
				{
					this.Logger.LogDebug("Work Flow Step '{name}': {type}", this.Name, nameof(OnPostExecutionAsync));
					await this.OnPostExecutionAsync(context);
				}
				catch (Exception ex)
				{
					this.Logger.LogError(ex, "Exception in {name}.", nameof(this.OnPostExecutionAsync));
				}
			}

			return returnValue;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		protected virtual Task<bool> OnPrepareForExecutionAsync(IContext context)
		{
			return Task.FromResult(true);
		}

		/// <summary>
		/// Called after a step has executed to perform any necessary cleanup.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		protected virtual Task OnPostExecutionAsync(IContext context)
		{
			return Task.CompletedTask;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		protected virtual Task<bool> OnExecuteStepAsync(IContext context)
		{
			return Task.FromResult(true);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <param name="message"></param>
		/// <returns></returns>
		protected virtual Task StepFailedAsync(IContext context, string message)
		{
			this.Logger.LogDebug("Work Flow Step '{name}': {type}", this.Name, nameof(StepFailedAsync));
			context.SetException(message);
			return Task.CompletedTask;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return $"[{this.Ordinal}] {this.Name} | Group: {this.Group}";
		}
	}
}

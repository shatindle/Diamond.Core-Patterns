﻿using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Diamond.Core.CommandLine.Model
{
	/// <summary>
	/// 
	/// </summary>
	public class ModelCommand<TModel> : Command
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="description"></param>
		public ModelCommand(string name, string description = null)
			: base(name, description)
		{
			this.BuildOptions();

			this.Handler = CommandHandler.Create<TModel>(async (p) =>
			{
				return await this.OnHandleCommand(p);
			});
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="name"></param>
		/// <param name="description"></param>
		public ModelCommand(ILogger<ModelCommand<TModel>> logger, string name, string description = null)
			: base(name, description)
		{
			this.Logger = logger;
			this.BuildOptions();

			this.Handler = CommandHandler.Create<TModel>(async (p) =>
			{
				return await this.OnHandleCommand(p);
			});
		}

		/// <summary>
		/// 
		/// </summary>
		protected ILogger<ModelCommand<TModel>> Logger { get; set; } = new NullLogger<ModelCommand<TModel>>();

		/// <summary>
		/// 
		/// </summary>
		protected void BuildOptions()
		{
			//
			// Get the type for the model.
			//
			Type t = typeof(TModel);

			//
			// Get the properties on the model.
			//
			PropertyInfo[] properties = t.GetProperties();

			//
			// The options are loaded into a list so they can be sorted.
			//
			IList<OptionDescriptor> items = new List<OptionDescriptor>();

			//
			// Try to load each property.
			//
			foreach (PropertyInfo property in properties)
			{
				//
				// Only load member types that can are read/write.
				//
				if (property.MemberType == MemberTypes.Property && property.CanWrite && property.CanRead)
				{
					//
					// Get the custom attributes on the model property.
					//
					IEnumerable<Attribute> attrs = property.GetCustomAttributes();

					//
					// Check if the Required attribute is present.
					//
					bool isRequired = attrs.Where(t => (Type)t.TypeId == typeof(RequiredAttribute)).SingleOrDefault() != null;

					//
					// Use the display attribute for the rest of the option properties.
					//
					DisplayAttribute display = attrs.Where(t => (Type)t.TypeId == typeof(DisplayAttribute)).SingleOrDefault() as DisplayAttribute;

					//
					// Create the option descriptor.
					//
					OptionDescriptor optionDescriptor = new OptionDescriptor()
					{
						Name = display?.ShortName != null ? (display?.Name) ?? (display?.ShortName) : property.Name,
						Description = (display?.Description) ?? property.Name,
						Order = display != null ? display.Order : 0,
						IsRequired = isRequired
					};

					//
					// Add the descriptor to the list.
					//
					items.Add(optionDescriptor);
				}
			}

			//
			// Add the options to this command is sorted order.
			//
			foreach (var item in items.OrderBy(t => t.Order))
			{
				this.Logger.LogDebug("Adding {type} option '--{optionName}' to the '{commandName}' command [Description ='{description}'].", item.IsRequired ? "required" : "optional", item.Name, this.Name, item.Description);
				this.AddOption(new Option<string>($"--{item.Name.ToLower()}", item.Description)
				{
					IsRequired = item.IsRequired
				});
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		protected virtual Task<int> OnHandleCommand(TModel item)
		{
			return Task.FromResult(0);
		}
	}
}

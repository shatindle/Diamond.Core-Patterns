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
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Diamond.Core.Example
{
	/// <summary>
	/// 
	/// </summary>
	public class ListCommandHttp : ListCommandBase
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="httpClientFactory"></param>
		public ListCommandHttp(ILogger<ListCommandHttp> logger, IHttpClientFactory httpClientFactory)
			: base(logger)
		{
			this.HttpClientFactory = httpClientFactory;
		}

		/// <summary>
		/// 
		/// </summary>
		protected IHttpClientFactory HttpClientFactory { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected override async Task<int> OnHandleCommand()
		{
			int returnValue = 0;

			await Task.Delay(1);

			return returnValue;
		}
	}
}

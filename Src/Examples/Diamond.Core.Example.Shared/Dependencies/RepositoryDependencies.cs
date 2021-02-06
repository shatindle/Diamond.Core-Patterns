﻿using Diamond.Core.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Diamond.Core.Example
{
	public static class RepositoryDependencies
	{
		public static IServiceCollection AddRepositoryExampleDependencies(this IServiceCollection services)
		{
			// ***
			// *** Add the default dependencies.
			// ***
			services.UseDiamondRepositoryPattern();

			// ***
			// *** Add the entity factory and repository to the container.
			// ***
			services.AddSingleton<IEntityFactory<IInvoice>, InvoiceEntityFactory>();
			services.AddTransient<IRepository<IInvoice>, InvoiceRepository>();

			// ***
			// *** Get the configuration.
			// ***
			IConfiguration configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

			services.AddDbContext<ErpContext>(options =>
			{
				options.UseInMemoryDatabase(configuration["ErpDatabase:InMemory"]);
				//options.UseNpgsql(configuration["ErpDatabase:PostgreSQL"]);
				//options.UseSqlite(configuration["ErpDatabase:SQLite"]);
				//options.UseSqlServer(configuration["ErpDatabase:SqlServer"]);
			});

			return services;
		}
	}
}

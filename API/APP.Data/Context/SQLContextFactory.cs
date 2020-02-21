using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;

namespace APP.Data.Context
{
    public class SQLContextFactory : IDesignTimeDbContextFactory<SQLContext>
    {
        /// <summary>
        /// Cria uma SQLContext
        /// </summary>
        /// <param name="args">Lista de argumentos</param>
        /// <returns>Contexto do banco</returns>
        public SQLContext CreateDbContext(string[] args)
        {
            // Build config
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            // Get connection string
            var optionsBuilder = new DbContextOptionsBuilder<SQLContext>();
            var connectionString = config.GetConnectionString("APPBD");
            Console.WriteLine($"CONNECTION STRING: {connectionString}");
            optionsBuilder.UseMySql(connectionString);

            return new SQLContext(optionsBuilder.Options);
        }
    }
}

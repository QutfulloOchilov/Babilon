using System;
using System.Data.Entity;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity.Migrations.History;

namespace Babilon.Model
{
    public class EntityContext : DbContext
    {
        public EntityContext() : base(GetConnectionString("babilon"))
        {


        }

        public static string GetConnectionString(string dbName)
        {
            var connString = ConfigurationManager.ConnectionStrings["mysqlCon"].ConnectionString.ToString();
            return String.Format(connString, dbName);
        }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Tariff> Tariffs { get; set; }

        public DbSet<Company> Companys { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Operation> Operations { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Doc> Docs { get; set; }

    }

    public class CustomHistoryContext : HistoryContext
    {
        public CustomHistoryContext(DbConnection dbConnection, string defaultSchema)
            : base(dbConnection, defaultSchema)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
    }



}

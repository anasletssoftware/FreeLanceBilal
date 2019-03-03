using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FreeLanceBilal.Models
{
    public class MyDbContext:DbContext
    {
        public MyDbContext():base("MyDbContext")
        {

        }
        public DbSet<UserAccounts> UserAccounts { get; set; }
        public DbSet<Documents> Document { get; set; }
        public DbSet<ClientType> ClientTypes { get; set; }
        public DbSet<ReturnType> ReturnTypes { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<State> States { get; set; }

    }
}
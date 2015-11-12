using System;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.Azure.WebJobs;



namespace studagramWebjob
{
    public class ContosoAdsContext : DbContext
    {
        private static ConnectionStringHandler csh = new ConnectionStringHandler();
        public ContosoAdsContext() : base("name=ContosoAdsContext")
        {
        }

        public DbSet<Ad> Ads { get; set; }

    }
}
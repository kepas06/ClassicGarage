using ClassicGarage.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ClassicGarage.DAL
{
    public class GarageContext : DbContext
    {
        public GarageContext() : base("name=Garage")
        {
        }
        public DbSet<Cars> Car { get; set; }
        public DbSet<Adverts> Notice { get; set; }
        public DbSet<OwnerModel> Owner { get; set; }
        public DbSet<CarParts> Parts { get; set; }
        public DbSet<RepairModel> Repairs { get; set; }
    
    }
}
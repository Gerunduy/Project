﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WcfService1
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class zabolotinEntities : DbContext
    {
        public zabolotinEntities()
            : base("name=zabolotinEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Sensors> Sensors { get; set; }
        public virtual DbSet<SensorSteel> SensorSteel { get; set; }
        public virtual DbSet<SensorSteelM3> SensorSteelM3 { get; set; }
        public virtual DbSet<Steels> Steels { get; set; }
    }
}

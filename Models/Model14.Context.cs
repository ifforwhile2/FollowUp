﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FallowUP.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FollowUpEntities15 : DbContext
    {
        public FollowUpEntities15()
            : base("name=FollowUpEntities15")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<HOMEWORK> HOMEWORK { get; set; }
        public virtual DbSet<MESSAGES> MESSAGES { get; set; }
        public virtual DbSet<NOTES> NOTES { get; set; }
        public virtual DbSet<STUDENTCLASS> STUDENTCLASS { get; set; }
        public virtual DbSet<STUDENTHOMEWORK> STUDENTHOMEWORK { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TEACHERCLASS> TEACHERCLASS { get; set; }
        public virtual DbSet<USERS> USERS { get; set; }
    }
}
﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DbMovieEntities : DbContext
    {
        public DbMovieEntities()
            : base("name=DbMovieEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TblActor> TblActor { get; set; }
        public virtual DbSet<TblCategory> TblCategory { get; set; }
        public virtual DbSet<TblDirector> TblDirector { get; set; }
        public virtual DbSet<TblKisi> TblKisi { get; set; }
        public virtual DbSet<TblKisiGezinti> TblKisiGezinti { get; set; }
        public virtual DbSet<TblKullanici> TblKullanici { get; set; }
        public virtual DbSet<TblMovie> TblMovie { get; set; }
        public virtual DbSet<TblMovieComment> TblMovieComment { get; set; }
        public virtual DbSet<TblMoviePhoto> TblMoviePhoto { get; set; }
        public virtual DbSet<TblMovieVideo> TblMovieVideo { get; set; }
        public virtual DbSet<TblTvSeries> TblTvSeries { get; set; }
        public virtual DbSet<TblTvSeriesComment> TblTvSeriesComment { get; set; }
        public virtual DbSet<TblTvSeriesPhoto> TblTvSeriesPhoto { get; set; }
        public virtual DbSet<TblTvSeriesVideo> TblTvSeriesVideo { get; set; }
    }
}

﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShoppingCartAPI
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BIShopEntities : DbContext
    {
        public BIShopEntities()
            : base("name=BIShopEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AdminUserMaster> AdminUserMasters { get; set; }
        public virtual DbSet<BannerMaster> BannerMasters { get; set; }
        public virtual DbSet<BrandMaster> BrandMasters { get; set; }
        public virtual DbSet<CategoryMaster> CategoryMasters { get; set; }
        public virtual DbSet<GroupMaster> GroupMasters { get; set; }
        public virtual DbSet<OfferImageMaster> OfferImageMasters { get; set; }
        public virtual DbSet<SubCategoryMaster> SubCategoryMasters { get; set; }
        public virtual DbSet<StateMaster> StateMasters { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<CityMaster> CityMasters { get; set; }
        public virtual DbSet<CustomerDetail> CustomerDetails { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<ProductMaster> ProductMasters { get; set; }
        public virtual DbSet<ProductOrderDetail> ProductOrderDetails { get; set; }
        public virtual DbSet<ProductRemark> ProductRemarks { get; set; }
        public virtual DbSet<CartMaster> CartMasters { get; set; }
        public virtual DbSet<ShoppingUser> ShoppingUsers { get; set; }
    }
}

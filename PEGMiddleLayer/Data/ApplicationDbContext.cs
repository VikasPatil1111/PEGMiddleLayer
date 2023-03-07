using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PEGMiddleLayer.Models.Common;
using PEGMiddleLayer.Models.CompanySelection;
using PEGMiddleLayer.Models.Dashboard;
using PEGMiddleLayer.Models.Dashboard.AccountReceivable;
using PEGMiddleLayer.Models.Dashboard.Backlog;
using PEGMiddleLayer.Models.Dashboard.Booking;
using PEGMiddleLayer.Models.Dashboard.Common;
using PEGMiddleLayer.Models.Dashboard.Inventory;
using PEGMiddleLayer.Models.Dashboard.Invoice;
using PEGMiddleLayer.Models.Dashboard.InvoiceMargin;
using PEGMiddleLayer.Models.Dashboard.MISReport;
using PEGMiddleLayer.Models.Menu;
using PEGMiddleLayer.Models.Sales.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Authentication
{
    public class ApplicationDbContext : IdentityDbContext <ApplicationUser>   {

        

        public ApplicationDbContext( DbContextOptions <ApplicationDbContext> options  ):base(options)
        { 

        }
         //public DbSet<AspNetRoleClaims> roleClaims
         public DbSet<tblCompanyMaster> TblCompanyMaster { get; set; }
         public DbSet<tblCompanyUsers> TblCompanyUsers { get; set; }
        // public DbSet<CustomerMaster> customerMasters { get; set; }   
        public DbSet<tblMainMenu> tblMainMenu { get; set; }
        public DbSet<MainMenu_Child> tblMainMenu_Child { get; set; }
        public DbSet<MainMenu_Sub_Child> tblMainMenu_Sub_Child1 { get; set; }
        public DbSet<MainMenu_Sub_Child2> tblMainMenu_Sub_Child2 { get; set; }
        public DbSet<PendingOrder> tblBI_Order_Status_Dtl { get; 
            set; }
        public DbSet<ProductWisePendingOrder> vw_ProductWisePendingOrderSummary { get; set; }
        public DbSet<PendingOrderDetailsBarChart> vw_PendingOrderBarSummary { get; set; }
        public DbSet<PendingOrderQtyDetailsBarChart> vw_PendingOrderBarQtySummary { get; set; }
       // public DbSet<BookingTable> tblBI_MISOrderBooking_Dtl { get; set; }
       public DbSet<BookingTable> vwtblBI_MISOrderBooking_Dtl { get; set; }
        public DbSet<BookingTargetTable> tblBI_OrderBooking_Target { get; set; }

        public DbSet<BookingDealerNonDealer> vw_BookingDealerNonDealer { get; set; }
        public DbSet<BookingCompanyBranches> vw_BookingBranchDetails { get; set; }
        public DbSet<BookingChartDelayAnalysis> vw_BookingBarCharDelayAnalysis { get; set; }
        public DbSet<tblBI_BFV_Order_TechDtl> tblBI_BFV_Order_TechDtl { get; set; }
        public DbSet<tblBooking_Tech_Dtl> vw_Booking_Tech_Dtl { get; set; }
        public DbSet<PendingOrder_Tech_Dtl> vw_PendingOrder_Tech_Dtl { get; set; }
        public DbSet<USP_BIRepo_PEG_MIS_SS> USP_BIRepo_PEG_MIS_SS { get; set; }

        public DbSet<vw_Backlog_Order_Rev1> vw_Backlog_Order_Rev1 { get; set; }
        public DbSet<vw_Backlog_Order_Summary> vw_Backlog_Order_Summary { get; set; }
        public DbSet<vw_BacklogOrder_Tech_Spec_Summary> vw_BacklogOrder_Tech_Spec_Summary { get; set; }
        public DbSet<tblBI_Period> tblBI_Period { get; set; }
        public DbSet<tblBI_Invoice_Details> tblBI_Invoice_Details { get; set; }
        public DbSet<vw_InvoiceCustomerName> vw_InvoiceCustomerName { get; set; }

        public DbSet<vw_Invoice_Tech_Deatils> vw_Invoice_Tech_Deatils { get; set; }
        public DbSet<So_Invoice_Target> So_Invoice_Target { get; set; }

        public DbSet<vw_Invoice_Order_Category> vw_Invoice_Order_Category { get; set; }
        public DbSet<vw_Invoice_Yearly_Company_Summary> vw_Invoice_Yearly_Company_Summary { get; set; }

        public DbSet<vw_InvoiceMargin_Tech_Details> vw_InvoiceMargin_Tech_Details { get; set; }
        public DbSet<vw_InvoiceMarginCustomerList> vw_InvoiceMarginCustomerList { get; set; }
        public DbSet<tblMainMenu_Access> tblMainMenu_Access { get; set; }

        public DbSet<vw_BookingCustomerList> vw_BookingCustomerList { get; set; }
        public DbSet<tblMIS_AccRec_Details> vw_AccountReceivableCustomerList { get; set; }
        public DbSet<tblBranch_User> tblBranch_User { get; set; }

        public DbSet<tblTotal_Inventory_Details> vwtblTotal_Inventory_Details { get; set; }
        public DbSet<vwInventoryAgingSummary> vwInventoryAgingSummary { get; set; }

        public DbSet<vwInventotyTypeDetails> vwInventotyTypeDetails { get; set; }
        public DbSet<vwInventoryAgingSummaryAllocated> vwInventoryAgingSummaryAllocated { get; set; }
        public DbSet<vwInventoryAgingSummaryUnAllocated> vwInventoryAgingSummaryUnAllocated { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<PendingOrderDetailsBarChart>().HasNoKey();
            builder.Entity<PendingOrderQtyDetailsBarChart>().HasNoKey();
            builder.Entity<BookingTable>().HasNoKey();
            builder.Entity<BookingTargetTable>().HasNoKey();
            builder.Entity<BookingDealerNonDealer>().HasNoKey();
            builder.Entity<BookingCompanyBranches>().HasNoKey();
            builder.Entity<BookingChartDelayAnalysis>().HasNoKey();
            builder.Entity<tblBI_BFV_Order_TechDtl>().HasNoKey();
            builder.Entity<tblBooking_Tech_Dtl>().HasNoKey();
            builder.Entity<PendingOrder_Tech_Dtl>().HasNoKey();
            builder.Entity<USP_BIRepo_PEG_MIS_SS>().HasNoKey();
            builder.Entity<vw_Backlog_Order_Rev1>().HasNoKey();
            builder.Entity<vw_Backlog_Order_Summary>().HasNoKey();
            builder.Entity<vw_BacklogOrder_Tech_Spec_Summary>().HasNoKey();
            builder.Entity<tblBI_Invoice_Details>().HasNoKey();
            builder.Entity<vw_InvoiceCustomerName>().HasNoKey();
            builder.Entity<vw_Invoice_Tech_Deatils>().HasNoKey();
            builder.Entity<So_Invoice_Target>().HasNoKey();
            builder.Entity<vw_Invoice_Order_Category>().HasNoKey();
            builder.Entity<vw_Invoice_Yearly_Company_Summary>().HasNoKey();
            builder.Entity<vw_InvoiceMargin_Tech_Details>().HasNoKey();
            builder.Entity<vw_InvoiceMarginCustomerList>().HasNoKey();
            builder.Entity<vw_BookingCustomerList>().HasNoKey();
            builder.Entity<tblMIS_AccRec_Details>().HasNoKey();
            builder.Entity<tblTotal_Inventory_Details>().HasNoKey();
            builder.Entity<vwInventoryAgingSummary>().HasNoKey();
            builder.Entity<vwInventotyTypeDetails>().HasNoKey();
            builder.Entity<vwInventoryAgingSummaryAllocated>().HasNoKey();
            builder.Entity<vwInventoryAgingSummaryUnAllocated>().HasNoKey();
            //builder.Entity<tblCompanyUsers>()
            //     .HasOne(e => e.tblCompanyMasters)
            //     .WithMany(c => c.tbl);
        }
    }
}

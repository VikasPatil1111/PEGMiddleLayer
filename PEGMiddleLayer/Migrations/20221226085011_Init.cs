using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PEGMiddleLayer.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblBI_BFV_Order_TechDtl",
                columns: table => new
                {
                    Company_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Company_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Product = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Domestic_Export = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Branch_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Customer_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Customer_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Booking_Year = table.Column<int>(type: "int", nullable: true),
                    Booking_Month = table.Column<int>(type: "int", nullable: true),
                    Booking_Year_Month = table.Column<int>(type: "int", nullable: true),
                    Order_Year = table.Column<short>(type: "smallint", nullable: true),
                    Order_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COrder_Serial_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order_Serial_No = table.Column<int>(type: "int", nullable: true),
                    Major_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Minor_No = table.Column<byte>(type: "tinyint", nullable: true),
                    Item_Serial_No = table.Column<int>(type: "int", nullable: true),
                    ItemSrlNo = table.Column<byte>(type: "tinyint", nullable: true),
                    Flag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProdGrpCd = table.Column<short>(type: "smallint", nullable: true),
                    ProdGrpNm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Flange = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlngStd_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body_MatlShortDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body_BondingMoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Disc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Disc_MatlShortDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Seat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Seat_MatlShortDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Shaft = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Shaft_MatlShortDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Supplier_Make = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperatorGrpNm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Companion_Flang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Spare_Kit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mfg_Trading = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Create_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    User_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body_MatlDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Disc_MatlDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Etl_On = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "tblBI_MISOrderBooking_Dtl",
                columns: table => new
                {
                    Company_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SrNo = table.Column<int>(type: "int", nullable: true),
                    Domestic_Export = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dealer_NonDealer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: true),
                    Month = table.Column<int>(type: "int", nullable: true),
                    Order_Year = table.Column<int>(type: "int", nullable: true),
                    Order_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order_Serial_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Major_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Minor_No = table.Column<int>(type: "int", nullable: true),
                    Serial_No = table.Column<int>(type: "int", nullable: true),
                    Branch_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Customer_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Customer_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Po_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Po_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Amendment_Flag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order_Value = table.Column<double>(type: "float", nullable: true),
                    Cancelled_Abv_6Mth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Create_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    User_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Month_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order_Booking_Completed_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Product = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Etl_On = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Product_Code = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "tblBI_Order_Status_Dtl",
                columns: table => new
                {
                    IV = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Company_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Branch_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Customer_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Customer_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order_Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IV_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order_Year = table.Column<short>(type: "smallint", nullable: false),
                    Order_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order_Serial_No = table.Column<int>(type: "int", nullable: false),
                    Major_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Minor_No = table.Column<byte>(type: "tinyint", nullable: false),
                    Elo_Order_Item_Serial_No = table.Column<int>(type: "int", nullable: true),
                    Order_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Order_Booking_Completed_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Customer_Po_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Customer_Po_Serial_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Customer_Po_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Customer_Req_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Liquid_Damage_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Inspection_Reqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Planning_Commit_Dt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Drawing_Required = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrawingApprovalRecd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contract_Review_By_Ho_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GADrg_Send_To_Branch_Customer = table.Column<DateTime>(type: "datetime2", nullable: true),
                    QAP_Flag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QAP_Approved = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mkt_QAP_Clr_Receipt_Dt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Qap_Approval_Receipt_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Internal_QA_Appr_Flag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Internal_QA_Appr_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Mkt_Clarification_Receipt_Dt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GADrg_Approval_Receipt_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Mkt_Other_Clr_Receipt_Dt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Oth_Approval_Receipt_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Lead_Time_In_Weeks = table.Column<int>(type: "int", nullable: true),
                    Order_Exe_Start_Date = table.Column<int>(type: "int", nullable: true),
                    Mktg_Released_Flag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bom_Released_Flag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bom_Released_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Costed_Flag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Costed_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Mfg_Costed_Flag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mfg_Costed_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Costing_Fdbk_Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Costing_Fdbk_Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prod_Sch_Generation_Date = table.Column<int>(type: "int", nullable: true),
                    Simulation_Date = table.Column<int>(type: "int", nullable: true),
                    Indent_Genration_Date = table.Column<int>(type: "int", nullable: true),
                    Purchase_Order_Genration_Date = table.Column<int>(type: "int", nullable: true),
                    Material_Receipt_Comp_Date = table.Column<int>(type: "int", nullable: true),
                    Prod_Complition_Date = table.Column<int>(type: "int", nullable: true),
                    Rate = table.Column<double>(type: "float", nullable: true),
                    Conversion_Factor = table.Column<double>(type: "float", nullable: true),
                    Discounted_Rate = table.Column<double>(type: "float", nullable: true),
                    Action_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Action_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order_Qty = table.Column<short>(type: "smallint", nullable: true),
                    Produced_Qty = table.Column<short>(type: "smallint", nullable: true),
                    Invoiced_Qty = table.Column<double>(type: "float", nullable: true),
                    Fg_Qty = table.Column<double>(type: "float", nullable: true),
                    Fg_Qty_MtcIssue = table.Column<double>(type: "float", nullable: true),
                    Fg_Qty_MtcNotIssue = table.Column<double>(type: "float", nullable: true),
                    Dismantle_Qty = table.Column<double>(type: "float", nullable: true),
                    Customer_Rejction_Qty = table.Column<double>(type: "float", nullable: true),
                    Pending_For_Prod_Qty = table.Column<double>(type: "float", nullable: true),
                    MCH_Total_Qty = table.Column<int>(type: "int", nullable: true),
                    MCH_Factory_Qty = table.Column<int>(type: "int", nullable: true),
                    MCH_Sales_Qty = table.Column<int>(type: "int", nullable: true),
                    Order_Qty_Clear_For_Prod = table.Column<double>(type: "float", nullable: true),
                    Order_Qty_Pend_For_GA_Prep = table.Column<double>(type: "float", nullable: true),
                    Order_Qty_Pend_For_QAP_Prep = table.Column<double>(type: "float", nullable: true),
                    Order_Qty_Pend_For_IntQAP_App = table.Column<double>(type: "float", nullable: true),
                    Order_Qty_Pend_For_GAAcept_Eng = table.Column<double>(type: "float", nullable: true),
                    Order_Qty_Pend_For_Bom_Release = table.Column<double>(type: "float", nullable: true),
                    Order_Qty_Pend_For_Cont_Review = table.Column<double>(type: "float", nullable: true),
                    Order_Qty_Pend_For_Tech_Clar = table.Column<double>(type: "float", nullable: true),
                    Order_Qty_Pend_ForGAAppFrCust = table.Column<double>(type: "float", nullable: true),
                    Order_Qty_Pend_ForQAPAppFrCust = table.Column<double>(type: "float", nullable: true),
                    Order_Qty_Pend_For_Cst_Clr = table.Column<double>(type: "float", nullable: true),
                    Order_Qty_Pend_ForCstClrCMDApp = table.Column<double>(type: "float", nullable: true),
                    Order_Qty_Pend_For_Mktg_Hold = table.Column<double>(type: "float", nullable: true),
                    Order_Value = table.Column<double>(type: "float", nullable: true),
                    Invoiced_Val = table.Column<double>(type: "float", nullable: true),
                    Dismantle_Val = table.Column<double>(type: "float", nullable: true),
                    Customer_Rejction_Val = table.Column<double>(type: "float", nullable: true),
                    Pending_For_Invoice_Val = table.Column<double>(type: "float", nullable: true),
                    Fg_Val = table.Column<double>(type: "float", nullable: true),
                    Fg_Val_MtcIssue = table.Column<double>(type: "float", nullable: true),
                    Fg_Val_MtcNotIssue = table.Column<double>(type: "float", nullable: true),
                    MCH_Total_Val = table.Column<double>(type: "float", nullable: true),
                    MCH_Factory_Val = table.Column<double>(type: "float", nullable: true),
                    MCH_Sales_Val = table.Column<double>(type: "float", nullable: true),
                    Order_Val_Clear_For_Prod = table.Column<double>(type: "float", nullable: true),
                    Order_Val_Pend_For_GA_Prep = table.Column<double>(type: "float", nullable: true),
                    Order_Val_Pend_For_QAP_Prep = table.Column<double>(type: "float", nullable: true),
                    Order_Val_Pend_For_IntQAP_App = table.Column<double>(type: "float", nullable: true),
                    Order_Val_Pend_For_GAAcept_Eng = table.Column<double>(type: "float", nullable: true),
                    Order_Val_Pend_For_Bom_Release = table.Column<double>(type: "float", nullable: true),
                    Order_Val_Pend_For_Cont_Review = table.Column<double>(type: "float", nullable: true),
                    Order_Val_Pend_For_Tech_Clar = table.Column<double>(type: "float", nullable: true),
                    Order_Val_Pend_ForGAAppFrCust = table.Column<double>(type: "float", nullable: true),
                    Order_Val_Pend_ForQAPAppFrCust = table.Column<double>(type: "float", nullable: true),
                    Order_Val_Pend_For_Cst_Clr = table.Column<double>(type: "float", nullable: true),
                    Order_Val_Pend_ForCstClrCMDApp = table.Column<double>(type: "float", nullable: true),
                    Order_Val_Pend_For_Mktg_Hold = table.Column<double>(type: "float", nullable: true),
                    Diff = table.Column<double>(type: "float", nullable: true),
                    Diff_1 = table.Column<int>(type: "int", nullable: true),
                    Diff_Fg_Qty = table.Column<int>(type: "int", nullable: true),
                    Diff_Fg_Val = table.Column<int>(type: "int", nullable: true),
                    Create_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    User_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    D_E_I = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COrder_Serial_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FgQty = table.Column<double>(type: "float", nullable: true),
                    Prod_NReady_For_Ship = table.Column<double>(type: "float", nullable: true),
                    FgQtyVal = table.Column<double>(type: "float", nullable: true),
                    Prod_NReady_For_ShipVal = table.Column<double>(type: "float", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contract_Review_Done = table.Column<double>(type: "float", nullable: true),
                    Hold = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookingYear = table.Column<int>(type: "int", nullable: true),
                    Order_Reinitiate_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Dealer_NonDealer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Backlog_Order_Qty = table.Column<double>(type: "float", nullable: true),
                    Backlog_Order_Value = table.Column<double>(type: "float", nullable: true),
                    Year_Month = table.Column<int>(type: "int", nullable: false),
                    Product = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ETL_On = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Product_Code = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBI_Order_Status_Dtl", x => x.IV);
                });

            migrationBuilder.CreateTable(
                name: "tblBI_OrderBooking_Target",
                columns: table => new
                {
                    Company_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SrNo = table.Column<short>(type: "smallint", nullable: true),
                    Domestic_Export = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Product = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<short>(type: "smallint", nullable: true),
                    Order_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rc_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Branch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Branch_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Target = table.Column<double>(type: "float", nullable: true),
                    Create_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    User_Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "TblCompanyMaster",
                columns: table => new
                {
                    CompanyCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DB_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DB_Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Server_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GST_NO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Theme_Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SideBar_Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Company_Selection_Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyLogo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    SmallLogo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Created_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MenuStructure = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblCompanyMaster", x => x.CompanyCode);
                });

            migrationBuilder.CreateTable(
                name: "TblCompanyUsers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    User_ID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Allow = table.Column<bool>(type: "bit", nullable: false),
                    Created_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Last_Modified_Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblCompanyUsers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tblMainMenu",
                columns: table => new
                {
                    Parent_Menu_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Parent_Menu_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Last_Modified_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sr_No = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblMainMenu", x => x.Parent_Menu_ID);
                });

            migrationBuilder.CreateTable(
                name: "USP_BIRepo_PEG_MIS_SS",
                columns: table => new
                {
                    SrNo = table.Column<int>(type: "int", nullable: false),
                    Repo_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IVPLU1_MTD = table.Column<double>(type: "float", nullable: true),
                    IVPLU1_YTD = table.Column<double>(type: "float", nullable: true),
                    IVPLU2_MTD = table.Column<double>(type: "float", nullable: true),
                    IVPLU2_YTD = table.Column<double>(type: "float", nullable: true),
                    IVPL_MTD = table.Column<double>(type: "float", nullable: true),
                    IVPL_YTD = table.Column<double>(type: "float", nullable: true),
                    EIPL_MTD = table.Column<double>(type: "float", nullable: true),
                    EIPL_YTD = table.Column<double>(type: "float", nullable: true),
                    PEG_MTD = table.Column<double>(type: "float", nullable: true),
                    PEG_YTD = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "vw_Backlog_Order_Rev1",
                columns: table => new
                {
                    Company_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Branch_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Customer_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Customer_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order_Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IV_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order_Year = table.Column<short>(type: "smallint", nullable: false),
                    Order_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order_Serial_No = table.Column<int>(type: "int", nullable: false),
                    Major_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Minor_No = table.Column<byte>(type: "tinyint", nullable: false),
                    Elo_Order_Item_Serial_No = table.Column<int>(type: "int", nullable: true),
                    Order_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Order_Booking_Completed_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Customer_Po_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Customer_Po_Serial_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Customer_Po_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Customer_Req_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Liquid_Damage_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Inspection_Reqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Planning_Commit_Dt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Drawing_Required = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrawingApprovalRecd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contract_Review_By_Ho_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GADrg_Send_To_Branch_Customer = table.Column<DateTime>(type: "datetime2", nullable: true),
                    QAP_Flag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QAP_Approved = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mkt_QAP_Clr_Receipt_Dt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Qap_Approval_Receipt_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Internal_QA_Appr_Flag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Internal_QA_Appr_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Mkt_Clarification_Receipt_Dt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GADrg_Approval_Receipt_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Mkt_Other_Clr_Receipt_Dt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Oth_Approval_Receipt_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Lead_Time_In_Weeks = table.Column<int>(type: "int", nullable: true),
                    Order_Exe_Start_Date = table.Column<int>(type: "int", nullable: true),
                    Mktg_Released_Flag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bom_Released_Flag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bom_Released_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Costed_Flag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Costed_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Mfg_Costed_Flag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mfg_Costed_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Costing_Fdbk_Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Costing_Fdbk_Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prod_Sch_Generation_Date = table.Column<int>(type: "int", nullable: true),
                    Simulation_Date = table.Column<int>(type: "int", nullable: true),
                    Indent_Genration_Date = table.Column<int>(type: "int", nullable: true),
                    Purchase_Order_Genration_Date = table.Column<int>(type: "int", nullable: true),
                    Material_Receipt_Comp_Date = table.Column<int>(type: "int", nullable: true),
                    Prod_Complition_Date = table.Column<int>(type: "int", nullable: true),
                    Rate = table.Column<double>(type: "float", nullable: true),
                    Conversion_Factor = table.Column<double>(type: "float", nullable: true),
                    Discounted_Rate = table.Column<double>(type: "float", nullable: true),
                    Action_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Action_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order_Qty = table.Column<short>(type: "smallint", nullable: true),
                    Produced_Qty = table.Column<short>(type: "smallint", nullable: true),
                    Invoiced_Qty = table.Column<double>(type: "float", nullable: true),
                    Fg_Qty = table.Column<double>(type: "float", nullable: true),
                    Fg_Qty_MtcIssue = table.Column<double>(type: "float", nullable: true),
                    Fg_Qty_MtcNotIssue = table.Column<double>(type: "float", nullable: true),
                    Dismantle_Qty = table.Column<double>(type: "float", nullable: true),
                    Customer_Rejction_Qty = table.Column<double>(type: "float", nullable: true),
                    Pending_For_Prod_Qty = table.Column<double>(type: "float", nullable: true),
                    MCH_Total_Qty = table.Column<int>(type: "int", nullable: true),
                    MCH_Factory_Qty = table.Column<int>(type: "int", nullable: true),
                    MCH_Sales_Qty = table.Column<int>(type: "int", nullable: true),
                    Order_Qty_Clear_For_Prod = table.Column<double>(type: "float", nullable: true),
                    Order_Qty_Pend_For_GA_Prep = table.Column<double>(type: "float", nullable: true),
                    Order_Qty_Pend_For_QAP_Prep = table.Column<double>(type: "float", nullable: true),
                    Order_Qty_Pend_For_IntQAP_App = table.Column<double>(type: "float", nullable: true),
                    Order_Qty_Pend_For_GAAcept_Eng = table.Column<double>(type: "float", nullable: true),
                    Order_Qty_Pend_For_Bom_Release = table.Column<double>(type: "float", nullable: true),
                    Order_Qty_Pend_For_Cont_Review = table.Column<double>(type: "float", nullable: true),
                    Order_Qty_Pend_For_Tech_Clar = table.Column<double>(type: "float", nullable: true),
                    Order_Qty_Pend_ForGAAppFrCust = table.Column<double>(type: "float", nullable: true),
                    Order_Qty_Pend_ForQAPAppFrCust = table.Column<double>(type: "float", nullable: true),
                    Order_Qty_Pend_For_Cst_Clr = table.Column<double>(type: "float", nullable: true),
                    Order_Qty_Pend_ForCstClrCMDApp = table.Column<double>(type: "float", nullable: true),
                    Order_Qty_Pend_For_Mktg_Hold = table.Column<double>(type: "float", nullable: true),
                    Order_Value = table.Column<double>(type: "float", nullable: true),
                    Invoiced_Val = table.Column<double>(type: "float", nullable: true),
                    Dismantle_Val = table.Column<double>(type: "float", nullable: true),
                    Customer_Rejction_Val = table.Column<double>(type: "float", nullable: true),
                    Pending_For_Invoice_Val = table.Column<double>(type: "float", nullable: true),
                    Fg_Val = table.Column<double>(type: "float", nullable: true),
                    Fg_Val_MtcIssue = table.Column<double>(type: "float", nullable: true),
                    Fg_Val_MtcNotIssue = table.Column<double>(type: "float", nullable: true),
                    MCH_Total_Val = table.Column<double>(type: "float", nullable: true),
                    MCH_Factory_Val = table.Column<double>(type: "float", nullable: true),
                    MCH_Sales_Val = table.Column<double>(type: "float", nullable: true),
                    Order_Val_Clear_For_Prod = table.Column<double>(type: "float", nullable: true),
                    Order_Val_Pend_For_GA_Prep = table.Column<double>(type: "float", nullable: true),
                    Order_Val_Pend_For_QAP_Prep = table.Column<double>(type: "float", nullable: true),
                    Order_Val_Pend_For_IntQAP_App = table.Column<double>(type: "float", nullable: true),
                    Order_Val_Pend_For_GAAcept_Eng = table.Column<double>(type: "float", nullable: true),
                    Order_Val_Pend_For_Bom_Release = table.Column<double>(type: "float", nullable: true),
                    Order_Val_Pend_For_Cont_Review = table.Column<double>(type: "float", nullable: true),
                    Order_Val_Pend_For_Tech_Clar = table.Column<double>(type: "float", nullable: true),
                    Order_Val_Pend_ForGAAppFrCust = table.Column<double>(type: "float", nullable: true),
                    Order_Val_Pend_ForQAPAppFrCust = table.Column<double>(type: "float", nullable: true),
                    Order_Val_Pend_For_Cst_Clr = table.Column<double>(type: "float", nullable: true),
                    Order_Val_Pend_ForCstClrCMDApp = table.Column<double>(type: "float", nullable: true),
                    Order_Val_Pend_For_Mktg_Hold = table.Column<double>(type: "float", nullable: true),
                    Diff = table.Column<double>(type: "float", nullable: true),
                    Diff_1 = table.Column<int>(type: "int", nullable: true),
                    Diff_Fg_Qty = table.Column<int>(type: "int", nullable: true),
                    Diff_Fg_Val = table.Column<int>(type: "int", nullable: true),
                    Create_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    User_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    D_E_I = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COrder_Serial_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FgQty = table.Column<double>(type: "float", nullable: true),
                    Prod_NReady_For_Ship = table.Column<double>(type: "float", nullable: true),
                    FgQtyVal = table.Column<double>(type: "float", nullable: true),
                    Prod_NReady_For_ShipVal = table.Column<double>(type: "float", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contract_Review_Done = table.Column<double>(type: "float", nullable: true),
                    Hold = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookingYear = table.Column<int>(type: "int", nullable: true),
                    Order_Reinitiate_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Dealer_NonDealer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Backlog_Order_Qty = table.Column<double>(type: "float", nullable: true),
                    Backlog_Order_Value = table.Column<double>(type: "float", nullable: true),
                    Year_Month = table.Column<int>(type: "int", nullable: false),
                    Product = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ETL_On = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Product_Code = table.Column<int>(type: "int", nullable: true),
                    Filter_Value = table.Column<double>(type: "float", nullable: true),
                    Filter_Qty = table.Column<double>(type: "float", nullable: true),
                    Filter_CDD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Filter_PDD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Filter_Stages = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CDD_Month_No = table.Column<int>(type: "int", nullable: true),
                    PDD_Month_No = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "vw_Backlog_Order_Summary",
                columns: table => new
                {
                    Company_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ButterFlyValve = table.Column<double>(type: "float", nullable: true),
                    Actuator = table.Column<double>(type: "float", nullable: true),
                    GGCValve = table.Column<double>(type: "float", nullable: true),
                    BallValve = table.Column<double>(type: "float", nullable: true),
                    Filter_CDD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Filter_PDD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Filter_Stages = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CDD_Month = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CDD_Year = table.Column<int>(type: "int", nullable: true),
                    PDD_Month = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PDD_Year = table.Column<int>(type: "int", nullable: true),
                    CDD_Month_No = table.Column<int>(type: "int", nullable: true),
                    PDD_Month_No = table.Column<int>(type: "int", nullable: true),
                    ButterFlyValveQty = table.Column<double>(type: "float", nullable: true),
                    ActuatorQty = table.Column<double>(type: "float", nullable: true),
                    GGCValveQty = table.Column<double>(type: "float", nullable: true),
                    BallValveQty = table.Column<double>(type: "float", nullable: true),
                    Customer_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Customer_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COrder_Serial_No = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "vw_BacklogOrder_Tech_Spec_Summary",
                columns: table => new
                {
                    Company_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order_Year = table.Column<short>(type: "smallint", nullable: true),
                    Order_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COrder_Serial_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Major_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Minor_No = table.Column<byte>(type: "tinyint", nullable: true),
                    Customer_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Customer_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order_Booking_Completed_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Product_Code = table.Column<int>(type: "int", nullable: true),
                    Product = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Flag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Month = table.Column<int>(type: "int", nullable: true),
                    MonthName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProdGrpCd = table.Column<int>(type: "int", nullable: true),
                    ProdGrpNm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order_Value = table.Column<double>(type: "float", nullable: true),
                    Order_Qty = table.Column<double>(type: "float", nullable: true),
                    ProdGrp_MIS_Id = table.Column<int>(type: "int", nullable: true),
                    ProdGrp_MIS_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Filter_Value = table.Column<double>(type: "float", nullable: true),
                    Filter_Qty = table.Column<double>(type: "float", nullable: true),
                    Filter_CDD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Filter_PDD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Filter_Stages = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CDD_Month = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CDD_Year = table.Column<int>(type: "int", nullable: true),
                    PDD_Month = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PDD_Year = table.Column<int>(type: "int", nullable: true),
                    CDD_Month_No = table.Column<int>(type: "int", nullable: true),
                    PDD_Month_No = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "vw_Booking_Tech_Dtl",
                columns: table => new
                {
                    Company_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order_Year = table.Column<int>(type: "int", nullable: true),
                    Order_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order_Serial_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Major_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Minor_No = table.Column<int>(type: "int", nullable: true),
                    Customer_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Customer_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order_Booking_Completed_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Product_Code = table.Column<int>(type: "int", nullable: true),
                    Product = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Flag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: true),
                    Month = table.Column<int>(type: "int", nullable: true),
                    Month_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProdGrpCd = table.Column<int>(type: "int", nullable: true),
                    ProdGrpNm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order_Value = table.Column<double>(type: "float", nullable: false),
                    ProdGrp_MIS_Id = table.Column<int>(type: "int", nullable: true),
                    ProdGrp_MIS_Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "vw_BookingBarCharDelayAnalysis",
                columns: table => new
                {
                    Month_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    ButterFlyValve = table.Column<double>(type: "float", nullable: false),
                    Actuator = table.Column<double>(type: "float", nullable: false),
                    GGCValve = table.Column<double>(type: "float", nullable: false),
                    BallValve = table.Column<double>(type: "float", nullable: false),
                    Company_Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "vw_BookingBranchDetails",
                columns: table => new
                {
                    Year = table.Column<int>(type: "int", nullable: false),
                    Branch_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IV = table.Column<double>(type: "float", nullable: false),
                    IVU2 = table.Column<double>(type: "float", nullable: false),
                    EIPL = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "vw_BookingDealerNonDealer",
                columns: table => new
                {
                    Company_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Direct = table.Column<double>(type: "float", nullable: false),
                    Dealer = table.Column<double>(type: "float", nullable: false),
                    Inter_Company = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "vw_PendingOrder_Tech_Dtl",
                columns: table => new
                {
                    Company_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order_Year = table.Column<short>(type: "smallint", nullable: true),
                    Order_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COrder_Serial_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Major_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Minor_No = table.Column<byte>(type: "tinyint", nullable: true),
                    Customer_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Customer_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order_Booking_Completed_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Product_Code = table.Column<int>(type: "int", nullable: true),
                    Product = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Flag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Month = table.Column<int>(type: "int", nullable: true),
                    MonthName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProdGrpCd = table.Column<int>(type: "int", nullable: true),
                    ProdGrpNm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order_Value = table.Column<double>(type: "float", nullable: true),
                    Order_Qty = table.Column<double>(type: "float", nullable: true),
                    ProdGrp_MIS_Id = table.Column<int>(type: "int", nullable: true),
                    ProdGrp_MIS_Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "vw_PendingOrderBarQtySummary",
                columns: table => new
                {
                    MonthName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    ButterFlyValve = table.Column<double>(type: "float", nullable: false),
                    Actuator = table.Column<double>(type: "float", nullable: false),
                    GGCValve = table.Column<double>(type: "float", nullable: false),
                    BallValve = table.Column<double>(type: "float", nullable: false),
                    Company_Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "vw_PendingOrderBarSummary",
                columns: table => new
                {
                    MonthName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    ButterFlyValve = table.Column<double>(type: "float", nullable: false),
                    Actuator = table.Column<double>(type: "float", nullable: false),
                    GGCValve = table.Column<double>(type: "float", nullable: false),
                    BallValve = table.Column<double>(type: "float", nullable: false),
                    Company_Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "vw_ProductWisePendingOrderSummary",
                columns: table => new
                {
                    Product = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Company_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderQty = table.Column<double>(type: "float", nullable: false),
                    OrderValue = table.Column<double>(type: "float", nullable: false),
                    ID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vw_ProductWisePendingOrderSummary", x => x.Product);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblMainMenu_Child",
                columns: table => new
                {
                    Child_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Parent_Menu_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblMainMenu_Child", x => x.Child_Id);
                    table.ForeignKey(
                        name: "FK_tblMainMenu_Child_tblMainMenu_Parent_Menu_ID",
                        column: x => x.Parent_Menu_ID,
                        principalTable: "tblMainMenu",
                        principalColumn: "Parent_Menu_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblMainMenu_Sub_Child1",
                columns: table => new
                {
                    Sub_Child_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Child_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Sub_Child_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Last_Modified_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblMainMenu_Sub_Child1", x => x.Sub_Child_ID);
                    table.ForeignKey(
                        name: "FK_tblMainMenu_Sub_Child1_tblMainMenu_Child_Child_ID",
                        column: x => x.Child_ID,
                        principalTable: "tblMainMenu_Child",
                        principalColumn: "Child_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblMainMenu_Sub_Child2",
                columns: table => new
                {
                    Sub_Child_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Sub_Child_ID1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Sub_Child_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Last_Modified_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblMainMenu_Sub_Child2", x => x.Sub_Child_ID);
                    table.ForeignKey(
                        name: "FK_tblMainMenu_Sub_Child2_tblMainMenu_Sub_Child1_Sub_Child_ID1",
                        column: x => x.Sub_Child_ID1,
                        principalTable: "tblMainMenu_Sub_Child1",
                        principalColumn: "Sub_Child_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tblMainMenu_Child_Parent_Menu_ID",
                table: "tblMainMenu_Child",
                column: "Parent_Menu_ID");

            migrationBuilder.CreateIndex(
                name: "IX_tblMainMenu_Sub_Child1_Child_ID",
                table: "tblMainMenu_Sub_Child1",
                column: "Child_ID");

            migrationBuilder.CreateIndex(
                name: "IX_tblMainMenu_Sub_Child2_Sub_Child_ID1",
                table: "tblMainMenu_Sub_Child2",
                column: "Sub_Child_ID1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "tblBI_BFV_Order_TechDtl");

            migrationBuilder.DropTable(
                name: "tblBI_MISOrderBooking_Dtl");

            migrationBuilder.DropTable(
                name: "tblBI_Order_Status_Dtl");

            migrationBuilder.DropTable(
                name: "tblBI_OrderBooking_Target");

            migrationBuilder.DropTable(
                name: "TblCompanyMaster");

            migrationBuilder.DropTable(
                name: "TblCompanyUsers");

            migrationBuilder.DropTable(
                name: "tblMainMenu_Sub_Child2");

            migrationBuilder.DropTable(
                name: "USP_BIRepo_PEG_MIS_SS");

            migrationBuilder.DropTable(
                name: "vw_Backlog_Order_Rev1");

            migrationBuilder.DropTable(
                name: "vw_Backlog_Order_Summary");

            migrationBuilder.DropTable(
                name: "vw_BacklogOrder_Tech_Spec_Summary");

            migrationBuilder.DropTable(
                name: "vw_Booking_Tech_Dtl");

            migrationBuilder.DropTable(
                name: "vw_BookingBarCharDelayAnalysis");

            migrationBuilder.DropTable(
                name: "vw_BookingBranchDetails");

            migrationBuilder.DropTable(
                name: "vw_BookingDealerNonDealer");

            migrationBuilder.DropTable(
                name: "vw_PendingOrder_Tech_Dtl");

            migrationBuilder.DropTable(
                name: "vw_PendingOrderBarQtySummary");

            migrationBuilder.DropTable(
                name: "vw_PendingOrderBarSummary");

            migrationBuilder.DropTable(
                name: "vw_ProductWisePendingOrderSummary");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "tblMainMenu_Sub_Child1");

            migrationBuilder.DropTable(
                name: "tblMainMenu_Child");

            migrationBuilder.DropTable(
                name: "tblMainMenu");
        }
    }
}

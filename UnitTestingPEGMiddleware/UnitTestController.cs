using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using PEGMiddleLayer.Models.Sales.Masters;
using PEGMiddleLayer.Controllers.Sales.Masters;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace UnitTestingPEGMiddleware
{
    public class UnitTestController
    {
        private readonly Mock<ICustomerMasterRepository> customerRepository;

        public UnitTestController()
        {
            customerRepository = new Mock<ICustomerMasterRepository>();
        }

        [Fact]
        public async Task getCustomerList()
        {
            //arrange
            var customerList =   getCustomerMasterList();

             customerRepository.Setup(x => x.getCustomerMasters(""))
                  .ReturnsAsync(customerList);
            var CustomerController = new CustomerMasterController(customerRepository.Object);

            //act
            var result =  await  CustomerController.getCustomerMaster("");
            OkObjectResult objectResponse = Assert.IsType<OkObjectResult>(result);
            OkObjectResult okResult = result as OkObjectResult;

            //ObjectResult res = Assert.IsType<ObjectResult>(result);

            
            // var result = customerResult.ExecuteResultAsync();
            //assert
            Assert.NotNull(result);
            Assert.Equal(customerList, okResult.Value);
            Assert.True(result is OkObjectResult);
             //using FluentAssertions
                                          // Assert.True(customerList.Count(),res.Value.);
                                          // Assert.Equal()
                                          // Assert.True(customerList.Equals(result));
                                          // Assert.NotNull(customerResult);
                                          // Assert.Equal(getCustomerMasters().Count(), result.Count());

        }

        private List<CustomerMaster> getCustomerMasterList()
        {
            List<CustomerMaster> customerMasters = new List<CustomerMaster>
            {
                new CustomerMaster{
                        Actual_Balance =0,
                        Allow_Credit ="N",
                        Booking_Concession=0,
                        Booking_Lock_Status =1,
                        Branch_Code="M",
                        CompanyCode="IV1",
                        Consignee_Type='M',
                        Contact_Person="Xyz",
                        Cost_Centre_Code="C",
                        Country="INDIA",
                        Created_Date=DateTime.Now,
                        Create_Date = DateTime.Now,
                        Credit_Limit=1,
                        Customer_Code="XYZ123",
                        Customer_Name="Name",
                        Cust_Add1="1",
                        Cust_Add10="1",
                        Cust_Add2 = "1",
                        Cust_Add3="1",
                        Cust_Add4="1",
                        Cust_Add5="1",
                        Cust_Add6 = "1",
                        Cust_Add7 = "1",
                        Cust_Add8="1",
                        Cust_Add9="1",
                        Cust_Catagary='1',
                        Cust_Class='1',
                        Cust_Cred='1',
                        Cust_Lock_For_CForm=1,
                        Cust_Lock_For_Credit_Limit=1,
                        Cust_Lock_For_FG_Limit=1,
                        Cust_Lock_For_Ovedue_OS=1,
                        Dealer_Agreement_No="123",
                        Ecc_No="122",
                        E_Mail="1",
                        Fax_No="1",
                        Group_Company='N',
                        GSTIN = "1",
                        GSTIN_Date=DateTime.Now,
                        GST_Cust_Add1 = "1",
                        GST_Cust_Add10 = "1",
                        GST_Cust_Add2="1",
                        GST_Cust_Add3="1",
                        GST_Cust_Add4 = "1",
                        GST_Cust_Add5="1",
                        GST_Cust_Add6="1",
                        GST_Cust_Add7="1",
                        GST_Cust_Add8="1",
                        GST_Cust_Add9="1",
                        GST_State_Code="1",
                        GST_Validate_By="1",
                        GST_Validate_Date=DateTime.Now,
                        GST_Validate_YN='N',
                        Invoicing_Concession=1,
                        Invoicing_Lock_Status=1,
                        Inv_Type='I',
                        IP_Address_LAN="192.6.1.2",
                        IP_Address_WAN="192.6.1.2",
                        Last_Modified_Date=DateTime.Now,
                        Lock_From_Date=DateTime.Now,
                        New_Eccno="sd",
                        OS_Limit=1,
                        OS_Limit_Date=DateTime.Now,
                        Our_Code_With_Cust="N",
                        PAN_No="PNARTD",
                        Production_Concession=1,
                        Region_Code="N",
                        Registration="q",
                        Sl_No=1,
                        State_Code="M",
                        Status=1,
                        Telephone_No="123",
                        TINDate=DateTime.Now,
                        TINNo="1452",
                        User_ID="avc",
                        Website="abc.com"
                        
                },
                new CustomerMaster{
                        Actual_Balance =0,
                        Allow_Credit ="N",
                        Booking_Concession=0,
                        Booking_Lock_Status =1,
                        Branch_Code="M",
                        CompanyCode="IV1",
                        Consignee_Type='M',
                        Contact_Person="Xyz",
                        Cost_Centre_Code="C",
                        Country="INDIA",
                        Created_Date=DateTime.Now,
                        Create_Date = DateTime.Now,
                        Credit_Limit=1,
                        Customer_Code="XYZ124",
                        Customer_Name="Name",
                        Cust_Add1="1",
                        Cust_Add10="1",
                        Cust_Add2 = "1",
                        Cust_Add3="1",
                        Cust_Add4="1",
                        Cust_Add5="1",
                        Cust_Add6 = "1",
                        Cust_Add7 = "1",
                        Cust_Add8="1",
                        Cust_Add9="1",
                        Cust_Catagary='1',
                        Cust_Class='1',
                        Cust_Cred='1',
                        Cust_Lock_For_CForm=1,
                        Cust_Lock_For_Credit_Limit=1,
                        Cust_Lock_For_FG_Limit=1,
                        Cust_Lock_For_Ovedue_OS=1,
                        Dealer_Agreement_No="123",
                        Ecc_No="122",
                        E_Mail="1",
                        Fax_No="1",
                        Group_Company='N',
                        GSTIN = "1",
                        GSTIN_Date=DateTime.Now,
                        GST_Cust_Add1 = "1",
                        GST_Cust_Add10 = "1",
                        GST_Cust_Add2="1",
                        GST_Cust_Add3="1",
                        GST_Cust_Add4 = "1",
                        GST_Cust_Add5="1",
                        GST_Cust_Add6="1",
                        GST_Cust_Add7="1",
                        GST_Cust_Add8="1",
                        GST_Cust_Add9="1",
                        GST_State_Code="1",
                        GST_Validate_By="1",
                        GST_Validate_Date=DateTime.Now,
                        GST_Validate_YN='N',
                        Invoicing_Concession=1,
                        Invoicing_Lock_Status=1,
                        Inv_Type='I',
                        IP_Address_LAN="192.6.1.2",
                        IP_Address_WAN="192.6.1.2",
                        Last_Modified_Date=DateTime.Now,
                        Lock_From_Date=DateTime.Now,
                        New_Eccno="sd",
                        OS_Limit=1,
                        OS_Limit_Date=DateTime.Now,
                        Our_Code_With_Cust="N",
                        PAN_No="PNARTD",
                        Production_Concession=1,
                        Region_Code="N",
                        Registration="q",
                        Sl_No=1,
                        State_Code="M",
                        Status=1,
                        Telephone_No="123",
                        TINDate=DateTime.Now,
                        TINNo="1452",
                        User_ID="avc",
                        Website="abc.com"

                },
                new CustomerMaster{
                        Actual_Balance =0,
                        Allow_Credit ="N",
                        Booking_Concession=0,
                        Booking_Lock_Status =1,
                        Branch_Code="M",
                        CompanyCode="IV1",
                        Consignee_Type='M',
                        Contact_Person="Xyz",
                        Cost_Centre_Code="C",
                        Country="INDIA",
                        Created_Date=DateTime.Now,
                        Create_Date = DateTime.Now,
                        Credit_Limit=1,
                        Customer_Code="XYZ125",
                        Customer_Name="Name",
                        Cust_Add1="1",
                        Cust_Add10="1",
                        Cust_Add2 = "1",
                        Cust_Add3="1",
                        Cust_Add4="1",
                        Cust_Add5="1",
                        Cust_Add6 = "1",
                        Cust_Add7 = "1",
                        Cust_Add8="1",
                        Cust_Add9="1",
                        Cust_Catagary='1',
                        Cust_Class='1',
                        Cust_Cred='1',
                        Cust_Lock_For_CForm=1,
                        Cust_Lock_For_Credit_Limit=1,
                        Cust_Lock_For_FG_Limit=1,
                        Cust_Lock_For_Ovedue_OS=1,
                        Dealer_Agreement_No="123",
                        Ecc_No="122",
                        E_Mail="1",
                        Fax_No="1",
                        Group_Company='N',
                        GSTIN = "1",
                        GSTIN_Date=DateTime.Now,
                        GST_Cust_Add1 = "1",
                        GST_Cust_Add10 = "1",
                        GST_Cust_Add2="1",
                        GST_Cust_Add3="1",
                        GST_Cust_Add4 = "1",
                        GST_Cust_Add5="1",
                        GST_Cust_Add6="1",
                        GST_Cust_Add7="1",
                        GST_Cust_Add8="1",
                        GST_Cust_Add9="1",
                        GST_State_Code="1",
                        GST_Validate_By="1",
                        GST_Validate_Date=DateTime.Now,
                        GST_Validate_YN='N',
                        Invoicing_Concession=1,
                        Invoicing_Lock_Status=1,
                        Inv_Type='I',
                        IP_Address_LAN="192.6.1.2",
                        IP_Address_WAN="192.6.1.2",
                        Last_Modified_Date=DateTime.Now,
                        Lock_From_Date=DateTime.Now,
                        New_Eccno="sd",
                        OS_Limit=1,
                        OS_Limit_Date=DateTime.Now,
                        Our_Code_With_Cust="N",
                        PAN_No="PNARTD",
                        Production_Concession=1,
                        Region_Code="N",
                        Registration="q",
                        Sl_No=1,
                        State_Code="M",
                        Status=1,
                        Telephone_No="123",
                        TINDate=DateTime.Now,
                        TINNo="1452",
                        User_ID="avc",
                        Website="abc.com"

                }
            };
            return customerMasters;
        
        }
        
    }
}

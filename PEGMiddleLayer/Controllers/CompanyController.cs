using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PEGMiddleLayer.Authentication;
using PEGMiddleLayer.Models.Common;
using PEGMiddleLayer.Models.CompanySelection;
using PEGMiddleLayer.Models.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyMasterRepository _companyMasterRepository;
        private readonly UserManager<ApplicationUser> _userManager;
         private readonly ILogger _logger;
        public CompanyController(ICompanyMasterRepository companyMasterRepository,ILogger<CompanyController> logger, 
            UserManager<ApplicationUser> userManager)
        {

            this._companyMasterRepository = companyMasterRepository;
            this._userManager = userManager;
            _logger = logger;
        }
        
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        [Route("CompanySelect/{userId}")]
        public async Task<IActionResult> CompanyList(string userId) {

            try
            {
              //  _logger.LogInformation("CompanySelect :- User Logged in Date {0} and User Name {1}",System.DateTime.Now,setDatabase.UserId);
                var company = await _companyMasterRepository.GetCompany();

                var companyUser = await _companyMasterRepository.tblCompanyUsers();
                var result = companyUser.Where(res => res.User_ID == userId  && res.Allow == true
                                                && res.tblCompanyMasters.CompanyCode == res.CompanyCode)
                    .Select(res => new { 
                     ID= res.tblCompanyMasters.ID ,
                     CompanyCode = res.tblCompanyMasters.CompanyCode,
                        CompanyName = res.tblCompanyMasters.CompanyName,
                        Title = res.tblCompanyMasters.Title,
                        DB_Name = res.tblCompanyMasters.DB_Name,
                        DB_Password = res.tblCompanyMasters.DB_Password,
                        Server_Name = res.tblCompanyMasters.Server_Name,
                        GST_NO = res.tblCompanyMasters.GST_NO,
                        Address = res.tblCompanyMasters.Address,
                        Theme_Color = res.tblCompanyMasters.Theme_Color,
                        SideBar_Color = res.tblCompanyMasters.SideBar_Color,
                        Company_Selection_Color = res.tblCompanyMasters.Company_Selection_Color,
                        CompanyLogo = res.tblCompanyMasters.CompanyLogo,
                        SmallLogo = res.tblCompanyMasters.SmallLogo,
                        Created_Date = res.tblCompanyMasters.Created_Date,
                        MenuStructure = res.tblCompanyMasters.MenuStructure,                        
                    });
                // return Ok(company.OrderBy(x => x.ID));
                return Ok(result.OrderBy(x => x.ID));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please Try Later..");
            }
           
        }

        [HttpPost]
        [Route("setDatabaseName")]
        public  JsonResult setDatabaseName([FromBody] setDatabaseName setDatabaseName) 
        {
            setDatabaseName setDatabase = new setDatabaseName
            {
                db_Name = setDatabaseName.db_Name
            };

            return new JsonResult(setDatabase)
            {
                StatusCode = StatusCodes.Status201Created // Status code here 
            };

        }

        [HttpGet]
        [Route("getmenulist/{company}")]
        public async Task<ActionResult<tblMainMenu>> getmenulist(string company)
        {
                try
                {               
                ApplicationUser applicationUser =  _userManager.FindByIdAsync(setDatabase.UserId).Result;
                //var UserRole = _companyMasterRepositor
                var getAccess = await _companyMasterRepository.tblMainMenu_Accesses();                
                var Role =  _userManager.GetRolesAsync(applicationUser).Result;
                List<string> Roles = new List<string>();
                    foreach (var role in Role) 
                    {
                        Roles.Add(role);
                    }

                    var RoleDetails = getAccess.Where(res => Roles.Contains(res.Role_Id)).Distinct(); // from role in getAccess

                //getAccess.Where(res => Roles.Contains(res.Role_Id)).Select(res=>res.Parent_Menu_ID);

                string[] MenuAccess= new string[110];
                //var query = ;
                if (RoleDetails.Any()) {
                    int index = 0;
                    foreach (var rl in RoleDetails)
                    {
                        index++;
                        if (rl.Parent_Menu_ID != null && MenuAccess.Length >index)
                        {
                            MenuAccess[index] = rl.Parent_Menu_ID;
                        }
                    }

                    //  var result11 = await _companyMasterRepository.GetMenuList();
                    var result = await _companyMasterRepository.GetMenuList();
                    var result1 = result.Where(e => e.Company == company && e.Status == true && MenuAccess.Contains(e.Parent_Menu_ID)).OrderBy(res => res.Sr_No)
                    .Select(em => new { em.Title, em.Icon, em.Path, childrens = em.childrens });

                    //tblMainMenu matchesDetail = new tblMainMenu();
                    List<tblMainMenu> retList = new List<tblMainMenu>();
                    //.Select(res => new  { res.Title,res.Icon,res.Path,res.childrens });
                    //IEnumerable<MatchesDetail> retList;
                    foreach (var item in result1)
                    {

                        //matchesDetail.Title = item.Title;
                        //matchesDetail.Icon = item.Icon;
                        //matchesDetail.Path = item.Path;
                        //other field mapping


                        retList.Add(new tblMainMenu
                        {
                            Title = item.Title,
                            Icon = item.Icon,
                            Path = item.Path,
                            childrens = item.childrens
                        });
                    }

                    return Ok(retList);

                }
                else {
                    var result = await _companyMasterRepository.GetMenuList();

                    var query1 =   (from data in result.Where(r => r.Company ==company && r.Status==true).OrderBy(r=>r.Sr_No )
                                    join user in getAccess
                                        on data.Parent_Menu_ID equals user.Parent_Menu_ID                                         
                                        into res                                      
                                         from results in res.DefaultIfEmpty()
                                         where results ==null
                                        //where data.Company == company && data.Status == true
                                        select new {
                                            Title =  data.Title,
                                            Icon =data.Icon,
                                            Path = data.Path,
                                            childrens = data.childrens,
                                            Parent_Menu_Name= res.Select(r => r.Parent_Menu_Name)
                                        }                                     
                        ).ToList();
                    int rc = query1.Count();
                 //   var result1 = query1.Where(e => e.Company == company && e.Status == true).OrderBy(res => res.Sr_No)
                 //.Select(em => new { em.Title, em.Icon, em.Path, childrens = em.childrens });
                    //var result1 = result.Where(e => e.Company == company && e.Status == true).OrderBy(res => res.Sr_No)
                    //.Select(em => new { em.Title, em.Icon, em.Path, childrens = em.childrens });

                    //tblMainMenu matchesDetail = new tblMainMenu();
                    List<tblMainMenu> retList = new List<tblMainMenu>();
                    //.Select(res => new  { res.Title,res.Icon,res.Path,res.childrens });
                    //IEnumerable<MatchesDetail> retList;
                    foreach (var item in query1)
                    {
                        //matchesDetail.Title = item.Title;
                        //matchesDetail.Icon = item.Icon;
                        //matchesDetail.Path = item.Path;
                        //other field mapping
                        retList.Add(new tblMainMenu
                        {
                            Title = item.Title,
                            Icon = item.Icon,
                            Path = item.Path,
                            childrens = item.childrens
                        });
                    }

                    return Ok(retList);
                }
                // return ((IEnumerable<tblMainMenu>)result1);
                // return (result.Where(e => e.Company == company));
                }
                catch (Exception ex)
                {
                string mes = ex.Message;
                _logger.LogInformation("Menu Error " + ex.Message);
                // return (IEnumerable<tblMainMenu>)StatusCode(400);
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please try later..");               
                    
                }
        }
        //[httpget]
        //[route("changedbconnection")]
        //public async task<iactionresult> 
    }
}

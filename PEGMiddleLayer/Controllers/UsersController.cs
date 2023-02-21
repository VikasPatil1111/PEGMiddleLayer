using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PEGMiddleLayer.Authentication;
using PEGMiddleLayer.Data;
using PEGMiddleLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // private UsersDbContext applicationDbContext = new UsersDbContext();
        readonly UsersDbContext _usersDbContext;

        public UsersController(UsersDbContext usersDbContext) {
            _usersDbContext = usersDbContext;
        }
        [HttpGet]
        [Route("GetUsers")]
        public IActionResult Get() {
            // var result = from 
            //  AspNetRoleClaims aspNetRoleClaims = new AspNetRoleClaims();
            var result = _usersDbContext.aspNetRoleClaims.ToList();

            return Ok(result);
        }
    }
}

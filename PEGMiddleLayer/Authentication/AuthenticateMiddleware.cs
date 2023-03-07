using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PEGMiddleLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PEGMiddleLayer.Models.Common;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http.Features;
using System.Net;

namespace PEGMiddleLayer.Authentication
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private ApplicationDbContext _masterContext;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
       // private readonly IUserService _userService;
        // public AuthenticationMiddleware(RequestDelegate next, ApplicationDbContext masterDevContext)
        // {
        public AuthenticationMiddleware(RequestDelegate next,ILogger<AuthenticationMiddleware> logger)
       {
                _next = next;
            _logger = logger;
           // _masterContext = masterDevContext;
        }
        public async Task Invoke(HttpContext context, CompanyDbContext BlogDBContext)
        {
            try
            {
              string authHeader = context.Request.Headers["Authorization"];

                if (authHeader == null)
                {
                    string encodedUsernamePassword2 = "";
                }
                    if (authHeader != null && (authHeader.StartsWith("Basic") || authHeader.StartsWith("Bearer")))
                    {
                        //Extract credentials    
                        string encodedUsernamePassword = authHeader.Substring("Basic".Length).Trim();
                        //Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                        //var status = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));


                     //dynamic obj = JsonConvert.DeserializeObject(encodedUsernamePassword);
                    //Console.Write(obj["status"]);
                    /*
                    string status = obj["status"];
                    string Login = obj["Login"];
                    string Database = obj["db_Name"];
                    */ //commented on 2612-2022
                    string status = context.Request.Headers["status"];
                    string Login = context.Request.Headers["Login"];
                    string Database = context.Request.Headers["db_Name"];
                    //JObject.Parse                   
                    //string status = obj[0].ParentLocationGroup;
                    //int seperatorIndex = usernamePassword.IndexOf(':');
                    //var username = usernamePassword.Substring(0, 9);
                    //var password = usernamePassword.Substring(seperatorIndex + 1);
                    if (Login != "Checking")
                    {
                        if (status == "200") //check if your credentials are valid    
                        {

                            if (setDatabase.Database != Database)
                            {
                                string conn = "Data Source=192.10.200.199;Initial Catalog=" + Database + ";User Id=sa;Password=Peg#2022";
                                setDatabase.Database = Database;
                               
                                CompanyDbContext.ConnectionString = conn; //_masterContext.Retrive Your subscriber connection string here
                               // SybaseDBContext.ConnectionString = conn;                                         
                            }
                            if (setDatabase.UserId != Login)
                            {
                                // var ip = context.Request.HttpContext.Connection.RemoteIpAddress;
                                string ip = string.Empty;
                                if (!string.IsNullOrEmpty(context.Request.Headers["X-Forwarded-For"]))
                                {
                                    ip = context.Request.Headers["X-Forwarded-For"];
                                }
                                else
                                {
                                    ip = context.Request.HttpContext.Features.Get<IHttpConnectionFeature>().LocalIpAddress.ToString();
                                }

                                if (ip == "::1")
                                {
                                    ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();
                                }

                                setDatabase.UserId = Login;
                                _logger.LogInformation("Login :- User Logged in Date {0} and User Name {1} IP Address {2}", System.DateTime.Now, setDatabase.UserId, ip);
                            }
                                if (string.IsNullOrEmpty(CompanyDbContext.ConnectionString))
                            {
                                //no authorization header    
                                context.Response.StatusCode = 401; //Unauthorized    
                                return;
                            }
                            //if (string.IsNullOrEmpty(SybaseDBContext.ConnectionString))
                            //{
                            //    //no authorization header    
                            //    context.Response.StatusCode = 401; //Unauthorized    
                            //    return;
                            //}

                            await _next.Invoke(context);
                        }
                        else
                        {
                            context.Response.StatusCode = 401; //Unauthorized    
                            return;
                        }
                    }
                    else {
                        await _next.Invoke(context);
                    }
                    }
                    else
                    {
                        // no authorization header    
                        context.Response.StatusCode = 401; //Unauthorized    
                        return;
                    }
                
            }
            catch (Exception e)
            {
                // no authorization header
                // 
                _logger.LogInformation("Menu Error:- " + e.Message);
                context.Response.StatusCode = 400;
                return;
            }
        }
       

    }
}

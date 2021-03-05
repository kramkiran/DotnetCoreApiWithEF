using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetCoreApiWithEF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCoreApiWithEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private readonly IEmployee _IEmployee;

        public DefaultController(IEmployee Iemployee)
        {
            _IEmployee = Iemployee;
        }

        [Route("getString")]
        [HttpGet]
        public string show()
        {
            return "abc";
        }

        [Route("chkUser/{empId}/exists")]
        [HttpGet]
        public bool checkUser(int empId)
        {
            return _IEmployee.userExists(empId);
        }

        [Route("{empId}/getUserName")]
        [HttpGet]
        public string GetUserName(int empId)
        {
            return _IEmployee.getEmployeeName(empId);
        }


        [Route("{empId}/getUserDetails")]
        [HttpGet]
        public object GetUserDetails(int empId)
        {
            return _IEmployee.getEmpData(empId);
        }

        [Route("updateData")]
        [HttpPost]
        public object UpdateUserDetails([FromBody]empDetails objData)
        {
            return _IEmployee.updateEmpData(objData);
        }

    }
}
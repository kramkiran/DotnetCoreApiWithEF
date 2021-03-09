using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetCoreApiWithEF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using interfaces;

namespace DotnetCoreApiWithEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private readonly IEmployee _IEmployee;

        public interfaceType _InterfaceType1 { get; }
        public Func<string, int, interfaceType> _InterfaceType2 { get; }
        public DefaultController(IEmployee Iemployee, Func<string, interfaceType> interfaceType1, Func<string,int, interfaceType> interfaceType2)
        {
            _IEmployee = Iemployee;
            _InterfaceType1 = interfaceType1("default value is passed");
            _InterfaceType2 = interfaceType2;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(empDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public object GetUserDetails(int empId)
        {
            empDetails objdata= _IEmployee.getEmpData(empId) as empDetails;
            if (objdata == null)
                return NotFound();
            else
                return Ok(objdata);
        }

        [Route("updateData")]
        [HttpPost]
        public object UpdateUserDetails([FromBody]empDetails objData)
        {
            return _IEmployee.updateEmpData(objData);
        }

        [HttpGet]
        [Route("getResponse")]
        public IActionResult getResponse(int val)
        {
            if(val==1)
            return BadRequest(new {status=StatusCodes.Status400BadRequest, message = "Somethig went wrong" });
            else
            return Ok(new { status=StatusCodes.Status200OK,message="success"});
        }

        [HttpGet]
        [Route("callConstructor")]
        public void inVokeMethod()
        {

            string a = string.Empty;
            a = _InterfaceType1.getStringvalue();

            var obj = _InterfaceType2("defval", 12);
            a = obj.getStringvalue();
        }

    }
}
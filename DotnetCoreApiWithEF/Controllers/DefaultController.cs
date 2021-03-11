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
            if (!_IEmployee.userExists(empId)) {
                return false;
            }
            return true; 
        }

        [Route("{empId}/getUserName")]
        [HttpGet]
        public object GetUserName(int empId)
        {
            string result = _IEmployee.getEmployeeName(empId);
            if (string.IsNullOrEmpty(result))
            {
                return NotFound(new { status = StatusCodes.Status404NotFound, data =string.Empty, message = "User Not Exists" });
            }

            return Ok(new { status = StatusCodes.Status200OK, data = result, message = "User Exists" });
            
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

        [Route("postData")]
        [HttpPost]
        public object postDetails([FromBody]sampleModel objData,[FromQuery]string objVal)
        {

            string name = "modified";
            if (objData != null && (objVal==name))
            {
                objData.name = name;
                return Ok(objData);
            }
            else
            {
                return BadRequest(objData);
            }

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

        [HttpGet,Route("getAllDetails")]
        public object getAllDetails()
        {
            List<empData> emplist = new List<empData>() { new empData { empId = 100, empName = "ganesh", city = "A"},
                                                      new empData { empId = 101, empName = "siva", city = "B" },
                                                      new empData { empId = 102, empName = "kumar", city = "C" },
                                                      new empData { empId = 103, empName = "lakshmi", city = "D"}};
            return Ok(emplist);
        }

    }
}
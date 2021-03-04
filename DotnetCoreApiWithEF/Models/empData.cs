using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCoreApiWithEF.Models
{
    public class empData
    {
        public int empId { get; set; }

        public string empName { get; set; }

        public string depName { get; set; }

        public string gender { get; set; }

        public string city { get; set; }

        public bool isActive { get; set; }
    }

    public class deptData
    {
        public int deptId { get; set; }

        public string deptName { get; set; }
    }

    public interface IEmployeeType
    {
        bool userExists();

        employee getEmployeeData();
    }
    public class employee
    {

        List<empData> emplist = new List<empData>() { new empData { empId = 100, empName = "ganesh", city = "A",gender="M",isActive=true,depName="AA" },
                                                      new empData { empId = 101, empName = "siva", city = "B",gender="M",isActive=false,depName="BB" },
                                                      new empData { empId = 102, empName = "kumar", city = "C",gender="M",isActive=true,depName="AA" },
                                                      new empData { empId = 103, empName = "lakshmi", city = "D",gender="F",isActive=true,depName="CC" }};


        List<deptData> deplist = new List<deptData>() { new deptData { deptId=1,deptName="AA" },new deptData { deptId=2,deptName="BB"},new deptData { deptId=3,deptName="CC"} };

    }
}

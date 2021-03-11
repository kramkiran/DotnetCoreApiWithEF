using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCoreApiWithEF.Models
{
    public class sampleModel {

        public string name { get; set; }
        public string city { get; set; }
    }
    public class empData
    {
        public int empId { get; set; }

        public string empName { get; set; }

        public int depId { get; set; }

        public string gender { get; set; }

        public string city { get; set; }

        public bool isActive { get; set; }
    }

    public class deptData
    {
        public int deptId { get; set; }

        public string deptName { get; set; }
    }

    public class empAddress
    {
        public int employeeId { get; set; }
        public string employeeAddress1 { get; set; }

    }
    public class empDetails
    {
        public int empId { get; set; }

        public string empName { get; set; }

        public string deptName { get; set; }

        public string gender { get; set; }

        public string city { get; set; }

        public bool isActive { get; set; }

    }

    public interface IEmployee
    {
        bool userExists(int empid);

        string getEmployeeName(int empId);

        object getEmpData(int empid);

        object updateEmpData(empDetails obj);
    }
    public class employee : IEmployee
    {

        List<empData> emplist = new List<empData>() { new empData { empId = 100, empName = "ganesh", city = "A",gender="M",isActive=true,depId=1 },
                                                      new empData { empId = 101, empName = "siva", city = "B",gender="M",isActive=false,depId=2 },
                                                      new empData { empId = 102, empName = "kumar", city = "C",gender="M",isActive=true,depId=1 },
                                                      new empData { empId = 103, empName = "lakshmi", city = "D",gender="F",isActive=true,depId=3 }};


        List<deptData> deplist = new List<deptData>() { new deptData { deptId=1,deptName="AA" },new deptData { deptId=2,deptName="BB"},new deptData { deptId=3,deptName="CC"} };


        List<empAddress> empAddresseslist = new List<empAddress>() {

            new empAddress{ employeeId =100 ,employeeAddress1="qwwer qwrw qwrqwr"},
            new empAddress { employeeId =101,employeeAddress1="nsdm sdbck sd sdckj" },
            new empAddress { employeeId=102, employeeAddress1= string.Empty},
            new empAddress { employeeId=103,employeeAddress1="sbckbsdkc onasn aosnka"}
        };
        
        public bool userExists(int empid)
        {
            return emplist.Any(a => a.empId == empid);
        }

        public string getEmployeeName(int empId)
        {
            var empdata = emplist.FirstOrDefault(a => a.empId == empId);
            string username = empdata == null ? default(string) : empdata.empName;

            return username;

        }


        public object getEmpData(int empid)
        {
            var empObj = emplist.Where(emp => deplist.Any(dept => emp.depId == dept.deptId)).FirstOrDefault();

            empDetails empObj2 = (from b in emplist
                                  join a in deplist
                                  on b.depId equals a.deptId where b.empId ==empid
                                  select new empDetails
                                  {
                                      empId = b.empId,
                                      empName = b.empName,
                                      deptName = a.deptName,
                                      isActive = b.isActive,
                                      gender = b.gender,
                                      city = b.city
                                  }).FirstOrDefault();

            
                     

            return empObj2;
        }

        public object updateEmpData(empDetails obj)
        {
            var empObj = emplist.Find(a=>a.empId==obj.empId);
            if (empObj != null)
            {
                var res = emplist.Join(deplist, emp => emp.depId, dept => dept.deptId, (emp, dept) => new { emp.empName, dept.deptName, emp.empId, emp.city }).Where(z => z.empId == empObj.empId).FirstOrDefault();

                var res2 = emplist.Join(deplist, emp => emp.depId, dept => dept.deptId, (emp, dept) => new { emp, dept }).
                                  Join(empAddresseslist, emp1 => emp1.emp.empId, empAddrs => empAddrs.employeeId, (emp1, empAddrs) => new { emp1, empAddrs }).
                                  Where(m => m.emp1.emp.empId == obj.empId).
                                  Select(m => new
                                  {
                                      empName =m.emp1.emp.empName,
                                      empDeptName =m.emp1.dept.deptName, 
                                      empAddressVal=m.empAddrs.employeeAddress1
                                  }).FirstOrDefault();

                

            }
            return empObj;
        }

    }
}

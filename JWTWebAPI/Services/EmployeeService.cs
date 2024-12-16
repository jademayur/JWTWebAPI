using JWTWebAPI.Context;
using JWTWebAPI.Interfaces;
using JWTWebAPI.Models;

namespace JWTWebAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly JwtContext _jwtContext;
        public EmployeeService(JwtContext jwtContext)
        {
            _jwtContext = jwtContext;
        }
        public Employee AddEmployee(Employee employee)
        {
           var emp = _jwtContext.Employees.Add(employee);
            _jwtContext.SaveChanges();
            return emp.Entity;
        }

        public bool DeleteEmployee(int id)
        {
            try
            {
                var emp = _jwtContext.Employees.SingleOrDefault(x => x.Id == id);
                if (emp != null)
                    throw new Exception("Employee not found");
                else
                {
                    _jwtContext.Employees.Remove(emp);
                    _jwtContext.SaveChanges();
                    return true;
                }
            }
            catch(Exception ex) {            
                return false;
            }
            


        }

        public List<Employee> GetEmployeeDetails()
        {
            var employee = _jwtContext.Employees.ToList();
            return employee;
        }

        public Employee GetEmployeeDetails(int id)
        {
            var employee = _jwtContext.Employees.SingleOrDefault(s =>s.Id == id);
            return employee;
        }

        public Employee UpdateEmployee(Employee employee)
        {
            var update = _jwtContext.Employees.Update(employee);
            _jwtContext.SaveChanges();
            return update.Entity;
        }
    }
}

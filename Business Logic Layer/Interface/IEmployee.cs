
using System.ComponentModel;
using ModelLayer;
namespace Business_Logic_Layer
    
{
    public interface IEmployee
    {
        public List<EmployeeRegistration> Get();
        void Post(EmployeeRegistration empl);
        public List<EmployeeRegistration> Edit();
        void  Delete(string UserName);
    }
}
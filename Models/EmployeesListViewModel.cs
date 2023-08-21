using ERPSystem.Models;

namespace UI.Models
{
    public class EmployeesListViewModel
    {

        public IEnumerable<Employee>? Employees { get; set; }
        public PagingInfo PagingInfo { get; set; } = new();
    }
}

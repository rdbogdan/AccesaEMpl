using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesaEmployee
{
	public class OfficeManagement
	{
		private readonly List<Employee> _employees = new List<Employee>();
		private readonly List<Project> _projects = new List<Project>();

		public IReadOnlyCollection<Employee> Employees => _employees;
		public IReadOnlyCollection<Project> Projects => _projects;


		public Employee AddEmployee(string name, EmployeePosition position, float capacity, List<string> hobbies)
		{
			Employee employee;
			switch (position)
			{
				case EmployeePosition.DEV:
					employee = new Dev(name, capacity);
					employee.Hobbies.AddRange(hobbies);
					_employees.Add(employee);

					break;
				case EmployeePosition.Intern:
					employee = new Intern(name, capacity);
					employee.Hobbies.AddRange(hobbies);
					_employees.Add(employee);

					break;
				case EmployeePosition.QA:
				default:
					employee = new QA(name, capacity);
					employee.Hobbies.AddRange(hobbies);
					_employees.Add(employee);
					break;
			}
			return employee;
		}

		public Project AddProject(string name, string description, DateTime deadLine)
		{
			var project = new Project(name, description, deadLine);
			_projects.Add(project);
			return project;
		}

		public bool AddEmployeeToProject(Employee employee, float noOfHours, Project project)
		{
			if (project.DeadLine < DateTime.Now || project.Team.Keys.Contains(employee))
				return false;

			var currentAllocationHours = _projects.Where(x => x.Team.Keys.Contains(employee))
				.Select(x => x.Team[employee])
				.Sum();
			if (employee.Capacity < currentAllocationHours + noOfHours)
				return false;

			project.AddTeamMember(employee, noOfHours);

			return true;
		}

		public void DeleteEmployee(Employee employee)
		{
			_projects.ForEach(x => x.DeleteTeamMember(employee));
			_employees.Remove(employee);
		}

		public void DisplayAllEmployees()
		{
			_employees.ForEach(x => x.DisplayInfo());
		}

		public void DisplayAllProjects()
		{
			_projects.ForEach(x=>x.DisplayInfo());
		}
	}
}

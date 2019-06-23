using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TStack.Dapper.Tests
{
    public class DapperRepositoryTests
    {
        private readonly EmployeeRepository repository = new EmployeeRepository();
        [Fact]
        public void Employee_shoud_addOne_success()
        {
            var employee = GetNewEmployee();

            repository.AddOne(employee);

            employee.Id.Should().BeGreaterThan(0);

        }
        [Fact]
        public void Employee_should_create_and_delete_success()
        {
            var employee = GetNewEmployee();

            repository.AddOne(employee);

            employee.Id.Should().BeGreaterThan(0);

            repository.DeleteById(employee.Id);
        }
        [Fact]
        public void Employee_should_create_and_update_success()
        {
            var employee = GetNewEmployee();

            repository.AddOne(employee);

            employee.Id.Should().BeGreaterThan(0);

            employee.Name = "Ahmet";

            repository.UpdateOne(employee);

            employee = repository.GetById(employee.Id);

            employee.Name.Should().Be("Ahmet");

            employee.Email = "abc@gmail.com";

            repository.UpdateOne(employee);

            employee = repository.GetById(employee.Id);

            employee.Email.Should().Be(null);

            repository.DeleteById(employee.Id);

        }




        private Employee GetNewEmployee()
        {
            return new Employee
            {
                BirthDate = new DateTime(1992, 07, 24),
                Email = "candasferhat61@gmail.com",
                Name = "Ferhat",
                Surname = "Candas",
                Salary = 4324.54d,
            };
        }
    }
}

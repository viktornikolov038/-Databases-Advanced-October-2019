﻿namespace EmployeesMapping.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Employee
    {
        public Employee()
        {
            this.ManagedEmployees = new List<Employee>();
        }

        public Employee(string firstName, string lastName, decimal salary)
            : this()
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Salary = salary;
        }

        public Employee(string firstName, string lastName, decimal salary, DateTime birthday)
            : this(firstName, lastName, salary)
        {
            this.BirthDay = birthday;
        }

        public Employee(string firstName, string lastName, decimal salary, string address)
            : this(firstName, lastName, salary)
        {
            this.Address = address;
        }

        public Employee(string firstName, string lastName, decimal salary, DateTime birthday, string address)
            : this(firstName, lastName, salary, birthday)
        {
            this.Address = address;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public DateTime? BirthDay { get; set; }

        public string Address { get; set; }

        public int? ManagerId { get; set; }

        public Employee Manager { get; set; }

        public List<Employee> ManagedEmployees { get; set; }
    }
}

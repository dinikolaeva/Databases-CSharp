﻿namespace TeisterMask.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using TeisterMask.Data.Models.Enums;

    public class Task
    {
        public Task()
        {
            this.EmployeesTasks = new HashSet<EmployeeTask>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        public DateTime OpenDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public ExecutionType ExecutionType { get; set; }

        [Required]
        public LabelType LabelType { get; set; }

        [Required]
        public int ProjectId { get; set; }

        public Project Project { get; set; }

        public ICollection<EmployeeTask> EmployeesTasks { get; set; }
    }
}
//•	Id - integer, Primary Key
//•	Name - text with length [2, 40] (required)
//•	OpenDate - date and time(required)
//•	DueDate - date and time(required)
//•	ExecutionType - enumeration of type ExecutionType, with possible values (ProductBacklog, SprintBacklog, InProgress, Finished) (required)
//•	LabelType - enumeration of type LabelType, with possible values (Priority, CSharpAdvanced, JavaAdvanced, EntityFramework, Hibernate) (required)
//•	ProjectId - integer, foreign key(required)
//•	Project - Project
//•	EmployeesTasks - collection of type EmployeeTask


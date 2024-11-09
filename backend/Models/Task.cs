using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Task
    {
        public int Id {get; set;}

        [GraphQLDescription("The title of the task")]
        public required string Title {get; set;}

        [GraphQLDescription("Detailed description of the task")]
        public required string Description {get; set;}

        [GraphQLDescription("Due date of the task")]
        public DateTime DueDate {get; set;}

        [GraphQLDescription("Priority level of the task")]
        public int Priority {get; set;}

        [GraphQLDescription("Completion status of the task")]
        public required bool isCompleted {get; set;}
    }
}
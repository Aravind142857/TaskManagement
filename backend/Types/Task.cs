using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Types
{
    [GraphQLDescription("Definition of a task")]
    public class Task
    {
        [ID]
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
        // public Task(int id, string title, string description, DateTime dueDate, int priority, bool iscompleted)
        // {
        //     Id = id;
        //     Title = title;
        //     Description = description;
        //     DueDate = dueDate;
        //     Priority = priority;
        //     isCompleted = iscompleted;
        // }
    }
}
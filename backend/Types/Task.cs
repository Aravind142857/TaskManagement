using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Types
{
    [GraphQLDescription("Definition of a task")]
    public class Task
    {
        public Guid Id {get; set;} = Guid.NewGuid();

        [GraphQLNonNullType]
        [GraphQLDescription("The title of the task")]
        public required string Title {get; set;}

        [GraphQLNonNullType]
        [GraphQLDescription("Detailed description of the task")]
        public required string Description {get; set;}

        [GraphQLDescription("Due date of the task")]
        public DateTime? DueDate {get; set;}

        [GraphQLNonNullType]
        [GraphQLDescription("Priority level of the task")]
        public required int Priority {get; set;}

        [GraphQLNonNullType]
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
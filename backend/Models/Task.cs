using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Task
    {
        public int Id {get; set;}
        public required string Title {get; set;}
        public required string Description {get; set;}
        public DateTime DueDate {get; set;}
        public int Priority {get; set;}
        public required bool isCompleted {get; set;}
    }
}
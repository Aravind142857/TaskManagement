using System;
using backend.Auth;
using backend.Types;

namespace backend.Types
{
    public class UserTasks
    {
        public Guid UserId {get; set;}
        public User User {get; set;}
        public Guid TaskId {get; set;}
        public Task Task {get; set;}
    }
}
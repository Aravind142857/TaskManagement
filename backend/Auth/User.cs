using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using backend.Types;
namespace backend.Auth
{
    public class User
    {
        [Key]
        public Guid Id {get; set;} = Guid.NewGuid();
        public required string Username {get; set;}
        [Required, EmailAddress]
        public required string Email {get; set;}
        [Required]
        public required string PasswordHash {get; set;}
        public ICollection<backend.Types.UserTasks> UserTasks {get; set;} = new List<backend.Types.UserTasks>();

    }
}
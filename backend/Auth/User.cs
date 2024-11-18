using System;
using System.ComponentModel.DataAnnotations;

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

    }
}
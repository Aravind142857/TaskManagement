using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Auth
{
    public class User
    {
        [Key]
        public Guid Id {get; set;} = Guid.NewGuid();
        [Required]
        public string Username {get; set;}
        [Required, EmailAddress]
        public string Email {get; set;}
        [Required]
        public string PasswordHash {get; set;}

    }
}
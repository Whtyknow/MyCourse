using System;
using System.Data.Entity;
using System.Configuration;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace MyGoogleDrive
{
    public class Db : DbContext
    {
        public Db() : base(ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }

    public class User
    {
        [Key]
        public int CustomerId { get; set; }

        [Required,MaxLength(30), MinLength(3)]
        public string Login { get; set; }
        
        [Required,MaxLength(30), MinLength(6)]        
        public string Password { get; set; }          
        
        [ForeignKey("RoleId")]
        public Role Role { get; set; }
    }

    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        public string Name { get; set; }
        
        public int Price { get; set; }

        public int DataSize { get; set; }

        public virtual List<User> Users { get; set; }

        public Role()
        {
            Users = new List<User>();
        }
    }
}
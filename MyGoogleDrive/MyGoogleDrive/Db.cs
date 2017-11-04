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
        public Db() : base(ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString)
        {
            Database.SetInitializer<Db>(new PersonDBInitializer());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }

    public class User
    {
        
        public int UserId { get; set; }

        [Required,MaxLength(30), MinLength(3)]
        public string Login { get; set; }
        
        [Required,MaxLength(30), MinLength(6)]        
        public string Password { get; set; }          
        
        public Role Role { get; set; }
    }

    public class Role
    {
        
        public int RoleId { get; set; }

        [Required]
        public string Name { get; set; }
        
        public int Price { get; set; }

        public int DataSize { get; set; }

        public virtual List<User> Users { get; set; }

    }




    public class PersonDBInitializer : CreateDatabaseIfNotExists<Db>
    {
        List<Role> roles = new List<Role>();
        List<User> users = new List<User>();
        public PersonDBInitializer()
        {
            roles.Add(new Role() { Name = "Default", Price = 0, DataSize = 1024 });
            roles.Add(new Role() { Name = "Premium", Price = 3, DataSize = 5120 });
            roles.Add(new Role() { Name = "Mega", Price = 5, DataSize = 10240 });


            users.Add(new User() { Login = "Petya", Password = "111222", Role = roles[0] });
            users.Add(new User() { Login = "Vasya", Password = "222111", Role = roles[1] });

        }
        protected override void Seed(Db context)
        {
            foreach (User u in users)
            {
                context.Users.Add(u);
            }            
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
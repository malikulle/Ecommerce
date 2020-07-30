using System;
using System.Collections.Generic;
using System.Text;
using ECommerce.Entity.Trackable;

namespace ECommerce.Entity.Models
{
    public class User : BaseEntity , TEntity
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string ImageUrl { get; set; }

        public DateTime? LastLoggedInDate { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public byte[] PasswordSalt { get; set; }

        public byte[] PasswordHash { get; set; }

        public Role Role { get; set; }

    }

    public enum Role
    {
        User = 1,
        Admin = 2
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BackspaceGaming.Entity.Model
{
    public partial class Users
    {
        public Users()
        {

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string Address { get; set; }
    }
}

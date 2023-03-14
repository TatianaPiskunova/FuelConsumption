using BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace BLL.DTO
{
    public class UserDTO : IdentityUser, IDTO
    { 
        public string FullName { get; set; }
        public string PositionOffice { get; set; }
        public string Password { get; set; }
        public string NameRole { get; set; }
     
    }
}

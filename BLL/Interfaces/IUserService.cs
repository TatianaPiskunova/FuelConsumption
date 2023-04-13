using BLL.DTO;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        void Add(UserDTO item);
        Task<bool> Login(string name, string password);
        void Init();
        Task Logout();
        void DeleteById(string id);
        UserDTO FindByFullName(string fullName);
        UserDTO FindByUserName(string userName);
        UserDTO FindById(string id);
        Task Update(UserDTO user);
        Task<bool> IsInRoleAsync(UserDTO uDTO, string role);
        List<UserDTO> GetAll();
    }
}

using AutoMapper;
using BLL.DTO;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Specifications;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;
        RoleManager<IdentityRole> _roleManager;
      
        public UserService(
            UserManager<User> userManager,
            IMapper mapper,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager
                     
            )
        {
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
            _roleManager = roleManager;
                  
        }

        public void Add(UserDTO item)
        {
            var user = _mapper.Map<UserDTO, User>(item);
            var users = _userManager.Users;
            
            if (new IsSameUserFullNameExists(users).IsSatisfiedBy(user))
            {
                throw new EntityArgumentException(
                    nameof(UserDTO.FullName),
                    "Пользователь с указанным ФИО уже есть в базе");
            }
            if (new IsSameUserNameExists(users).IsSatisfiedBy(user))
            {
                throw new EntityArgumentException(
                    nameof(UserDTO.UserName),
                    "Указанный логин занят");
            }
            if (!new IsUserFullNameValid().IsSatisfiedBy(user))
            {
                throw new EntityArgumentException(
                    nameof(UserDTO.FullName),
                    "ФИО должно быть больше 3-х символов");
            }
            if (!new IsUserPositionValid().IsSatisfiedBy(user))
            {
                throw new EntityArgumentException(
                    nameof(UserDTO.PositionOffice),
                    "Должность должна быть больше 3-х символов");
            }

            if (string.IsNullOrEmpty(item.Password))
            {
                throw new EntityArgumentException(
                    nameof(UserDTO.Password),
                    "При создании учётной записи пароль обязателен");
            }

            var res = _userManager.CreateAsync(user, item.Password).Result;
            if (res.Succeeded)
            {  
                if (item.NameRole == "BossCar")
                {
                    res = _userManager.AddToRoleAsync(user, "BossCar").Result;
                }
                if (item.NameRole == "BossDriver")
                {
                    res = _userManager.AddToRoleAsync(user, "BossDriver").Result;
                }
                if (item.NameRole == "Engineer")
                {
                    res = _userManager.AddToRoleAsync(user, "Engineer").Result;
                }
                if (item.NameRole != "BossCar" && item.NameRole != "BossDriver" && item.NameRole != "Engineer")
                {
                    res = _userManager.AddToRoleAsync(user, "user").Result;
                }
                if (!res.Succeeded)
                    throw new Exception("Нельзя назначить роли");
            }
            else
                throw new Exception("Пользователь не может быть зарегистрирован");
        }

        public UserDTO FindById(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            var userDTO = _mapper.Map<User, UserDTO>(user);
            return userDTO;
        }
        public void DeleteById(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;

            if (user != null)
            {
                _userManager.DeleteAsync(user);
            }
        }
        public UserDTO FindByFullName(string fullName)
        {
            var user = _userManager.FindByNameAsync(fullName).Result;
            return _mapper.Map<UserDTO>(user);
        }
        public UserDTO FindByUserName(string userName)
        {
            var user = _userManager.FindByNameAsync(userName).Result;
            return _mapper.Map<UserDTO>(user);
        }
        public async Task<bool> IsInRoleAsync(UserDTO uDTO, string role)
        {
            var u = _userManager.FindByIdAsync(uDTO.Id).Result;
            return await _userManager.IsInRoleAsync(u, role);
        }
        public List<UserDTO> GetAll()
        => _mapper.Map<IQueryable<User>, List<UserDTO>>(_userManager.Users);

        public void Init()
        {
            DbInitializer.InitializeAsync(_userManager, _roleManager).Wait();
        }

        public async Task<bool> Login(string name, string password)
        {
            var user = await _userManager.FindByNameAsync(name);

            if (user != null)
            {
                var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);

                if (isPasswordValid)
                {
                    await _signInManager.SignInAsync(user, false);
                    return true;
                }
            }
            return false;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task Update(UserDTO item)
        {
            var user = await _userManager.FindByIdAsync(item.Id);
            user.FullName = item.FullName;
            user.PositionOffice = item.PositionOffice; 
            user.PhoneNumber = item.PhoneNumber;

          

            if (!new IsUserFullNameValid().IsSatisfiedBy(user))
            {
                throw new EntityArgumentException(
                    nameof(UserDTO.FullName),
                    "ФИО должно быть больше 3-х символов");
            }
            if (!new IsUserPositionValid().IsSatisfiedBy(user))
            {
                throw new EntityArgumentException(
                    nameof(UserDTO.PositionOffice),
                    "Должность должна быть больше 3-х символов");
            }

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                string msg = "";
                foreach (var err in updateResult.Errors)
                {
                    msg += err.Description + " ";
                }

                throw new EntityArgumentException("", msg);
            }
        }
      


    }
}

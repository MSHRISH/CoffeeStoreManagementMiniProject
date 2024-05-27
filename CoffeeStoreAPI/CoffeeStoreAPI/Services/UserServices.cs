﻿using CoffeeStoreAPI.Execptions;
using CoffeeStoreAPI.Iterfaces;
using CoffeeStoreAPI.Models;
using CoffeeStoreAPI.Models.DTOs;
using System.Security.Cryptography;
using System.Text;

namespace CoffeeStoreAPI.Services
{
    public class UserServices : IUserService
    {
        private readonly IRepository<int, User> _userRepository;
        private readonly IRepository<int, Authentication> _authenticationReposiory;
        private readonly IRepository<int, RoleMapping> _rolemappingRepository;
        private readonly ITokenServices _tokenServices;
        private readonly IRepository<int, Role> _roleRepository;

        public UserServices(IRepository<int,User> userRepository,IRepository<int,Role> roleRepository,IRepository<int,Authentication> authenticationRepository,IRepository<int,RoleMapping> rolemappingRepository, ITokenServices tokenServices) 
        {
            _userRepository=userRepository;
            _authenticationReposiory=authenticationRepository;
            _rolemappingRepository=rolemappingRepository;
            _tokenServices=tokenServices;
            _roleRepository=roleRepository;
        }

        public async Task<TokenDTO> LoginUser(LoginDTO loginDTO)
        {
            Authentication userAuth = await _authenticationReposiory.Get(loginDTO.UserId);
            if (userAuth == null)
            {
                throw new InvalidCredentials();
            }
            User user = await _userRepository.Get(loginDTO.UserId);
            if (user.Status == "Disabled")
            {
                throw new UserNotEnabled();
            }
            
            HMACSHA512 hMACSHA = new HMACSHA512(userAuth.PasswordHashKey);
            var PasswordHash = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));
            bool isHashSame = ComparePassword(PasswordHash, userAuth.PasswordHash);
            if(isHashSame)
            {
                RoleMapping roleMapping=await _rolemappingRepository.Get(loginDTO.UserId);
                /*if (roleMapping == null)
                {
                    throw new InvalidCredentials();
                } */

                Role role=await _roleRepository.Get(roleMapping.RoleId);
                
                TokenDTO tokenDTO=MapUserToTokenDTO(userAuth.Id,role.RoleName);
                return tokenDTO;
            }
            throw new InvalidCredentials();
        }

        private TokenDTO MapUserToTokenDTO(int UserId,string role)
        {
            TokenDTO tokenDTO = new TokenDTO();
            tokenDTO.UserId = UserId;
            tokenDTO.Token = _tokenServices.GenerateToken(UserId, role);
            tokenDTO.Role = role;
            return tokenDTO;
        }

        private bool ComparePassword(byte[] encrypterPass, byte[] password)
        {
            for (int i = 0; i < encrypterPass.Length; i++)
            {
                if (encrypterPass[i] != password[i])
                {
                    return false;
                }
            }
            return true;
        }
        public async Task<UserDetails> RegisterUser(RegisterUserDTO registerUserDTO, int RoleId)
        {
            User user = null;
            Authentication userAuthentication = null;
            RoleMapping roleMapping=null;
            try
            {
                user = MapToUser(registerUserDTO);
                userAuthentication = MapUserToUserCredentials(registerUserDTO);
                user = await _userRepository.Add(user);
                userAuthentication.Id = user.Id;
                userAuthentication = await _authenticationReposiory.Add(userAuthentication);
                roleMapping=new RoleMapping { UserId=user.Id, RoleId=RoleId };   
                roleMapping=await _rolemappingRepository.Add(roleMapping);
                UserDetails userDetails=MapToUserDetails(user,RoleId);
                return userDetails;
            }
            catch (Exception ex) { throw new Exception(); }
            
        }
        private UserDetails MapToUserDetails(User user, int RoleId)
        {
            UserDetails userDetails = new UserDetails();
            userDetails.UserId=user.Id;
            userDetails.Name=user.Name;
            userDetails.Email=user.Email;
            userDetails.Phone=user.Phone;
            userDetails.Status = user.Status;

            if (RoleId == 2)
            {
                userDetails.Role = "Manager";
            }
            else if (RoleId == 3)
            {
                userDetails.Role = "Barista";
            }
            else if(RoleId == 4)
            {
                userDetails.Role = "Customer";
            }
            return userDetails;
        }
        private User MapToUser(RegisterUserDTO registerUserDTO)
        {
            User user=new User();
            user.Name = registerUserDTO.Name;
            user.Email = registerUserDTO.Email;
            user.Phone = registerUserDTO.Phone;
            user.Status = "Active";
            return user;
     
        }
        private Authentication MapUserToUserCredentials(RegisterUserDTO registerUserDTO)
        {
            Authentication userAuthentication = new Authentication();
            HMACSHA512 hMACSHA = new HMACSHA512();
            userAuthentication.PasswordHashKey = hMACSHA.Key;
            userAuthentication.PasswordHash= hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(registerUserDTO.Password));
            return userAuthentication;
        }

        public async Task<UserDetails> GetManagerById(int id)
        {
            try
            {
                var user = await _userRepository.Get(id);
                var rolemapping = await _rolemappingRepository.Get(id);
                if (rolemapping.RoleId == 2)
                {
                    var userDetail = MapToUserDetails(user, 2);
                    return userDetail;
                }
            }
            catch (Exception ex) { }
            throw new NoSuchManagerFoundExecption();
        }

        public async Task<UserDetails> GetBaristaById(int id)
        {
            try
            {
                var user = await _userRepository.Get(id);
                var rolemapping = await _rolemappingRepository.Get(id);
                if (rolemapping.RoleId == 3)
                {
                    var userDetail = MapToUserDetails(user, 3);
                    return userDetail;
                }
            }
            catch (Exception ex){}
            throw new NoSuchBaristaFoundExecption();
        }

        public async Task<List<UserDetails>> GetAllManagers()
        {
            var users=await _userRepository.GetAll();
            var rolemappings=await _rolemappingRepository.GetAll();

            var Managers = from user in users
                           join rolemapping in rolemappings on user.Id equals rolemapping.UserId
                           where rolemapping.RoleId == 2
                           select MapToUserDetails(user, 2);
                          
            return Managers.ToList();
        }

        public async Task<List<UserDetails>> GetAllBaristas()
        {

            var users = await _userRepository.GetAll();
            var rolemappings = await _rolemappingRepository.GetAll();

            var Baristas= from user in users
                           join rolemapping in rolemappings on user.Id equals rolemapping.UserId
                           where rolemapping.RoleId == 3
                           select MapToUserDetails(user, 3);

            return Baristas.ToList();
        }

        public async Task<List<UserDetails>> GetAllCustomers()
        {

            var users = await _userRepository.GetAll();
            var rolemappings = await _rolemappingRepository.GetAll();

            var Customers = from user in users
                           join rolemapping in rolemappings on user.Id equals rolemapping.UserId
                           where rolemapping.RoleId == 4
                           select MapToUserDetails(user, 4);

            return Customers.ToList();
        }

        public async Task<UserDetails> GetCustomerById(int id)
        {
            try
            {
                var user = await _userRepository.Get(id);
                var rolemapping = await _rolemappingRepository.Get(id);
                if (rolemapping.RoleId == 4)
                {
                    var userDetail = MapToUserDetails(user, 4);
                    return userDetail;
                }
            }
            catch (Exception ex) { }
            throw new NoSuchCustomerExecption();
        }

        public async Task<UserDetails> GetUserById(int id, int RoleId)
        {
            var user = await _userRepository.Get(id);
            if(user == null)
            {
                throw new NoSuchUserException();
            }
            var rolemapping = await _rolemappingRepository.Get(id);
            if (rolemapping.RoleId == RoleId)
            {
                var userDetail = MapToUserDetails(user, RoleId);
                return userDetail;
            }         
            throw new NoSuchUserException();
        }

        public async Task<List<UserDetails>> GetAllUsersByRole(int RoleId)
        {
            var users = await _userRepository.GetAll();
            var rolemappings = await _rolemappingRepository.GetAll();

            var Users= from user in users
                            join rolemapping in rolemappings on user.Id equals rolemapping.UserId
                            where rolemapping.RoleId == RoleId
                            select MapToUserDetails(user, RoleId);

            return Users.ToList();
        }
    }
}

using CoffeeStoreAPI.Execptions;
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
            var allusers=await _userRepository.GetAll();
            var thisuser= allusers.FirstOrDefault(u => u.Email == loginDTO.Uemailphone|| u.Phone == loginDTO.Uemailphone);

            if (thisuser==null)
            {
                throw new InvalidCredentials();
            }
            Authentication userAuth = await _authenticationReposiory.Get(thisuser.Id);

            if (thisuser.Status == "Disabled")
            {
                throw new UserNotEnabled();
            }
            
            HMACSHA512 hMACSHA = new HMACSHA512(userAuth.PasswordHashKey);
            var PasswordHash = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));
            bool isHashSame = ComparePassword(PasswordHash, userAuth.PasswordHash);
            if(isHashSame)
            {
                RoleMapping roleMapping=await _rolemappingRepository.Get(thisuser.Id);
     
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

        public async Task<bool> CheckRegisterEmail(User user)
        {
            var allusers=await _userRepository.GetAll();
            bool exists = allusers.Any(u => u.Email == user.Email);
            return exists;
        }
        public async Task<bool> CheckRegisterPhone(User user)
        {
            var allusers = await _userRepository.GetAll();
            bool exists = allusers.Any(u => u.Phone== user.Phone);
            return exists;
        }

        public async Task<UserDetails> RegisterUser(RegisterUserDTO registerUserDTO, int RoleId)
        {
            User user = null;
            Authentication userAuthentication = null;
            RoleMapping roleMapping=null;
            try
            {
                user = MapToUser(registerUserDTO);
                if(await CheckRegisterEmail(user))
                {
                    throw new EmailAlreadyExistsExecption();
                }
                if(await CheckRegisterPhone(user))
                {
                    throw new PhoneAlreadyExistsExecption();
                }
                userAuthentication = MapUserToUserCredentials(registerUserDTO);
                user = await _userRepository.Add(user);
                userAuthentication.Id = user.Id;
                userAuthentication = await _authenticationReposiory.Add(userAuthentication);
                roleMapping=new RoleMapping { UserId=user.Id, RoleId=RoleId };   
                roleMapping=await _rolemappingRepository.Add(roleMapping);
                UserDetails userDetails=MapToUserDetails(user,RoleId);
                return userDetails;
            }
            catch (Exception ex) { throw; }
            
        }
        private UserDetails MapToUserDetails(User user, int RoleId)
        {
            UserDetails userDetails = new UserDetails();
            userDetails.UserId=user.Id;
            userDetails.Name=user.Name;
            userDetails.Email=user.Email;
            userDetails.Phone=user.Phone;
            userDetails.Status = user.Status;
            if (RoleId == 1)
            {
                userDetails.Role = "Admin";
            }
            else if (RoleId == 2)
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
        public async Task<UserDetails> GetUserById(int id, int ? RoleId=null)
        {
            var user = await _userRepository.Get(id);
            if(user == null)
            {
                throw new NoSuchUserException();
            }
            var rolemapping = await _rolemappingRepository.Get(id);
            var userDetail = MapToUserDetails(user, rolemapping.RoleId);
            if (RoleId.HasValue)
            {
                if (rolemapping.RoleId == RoleId)
                {
                    
                    return userDetail;
                }
                else
                {
                    throw new NoSuchUserException();
                }
            }
            return userDetail;
                 
            
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

        public async Task<UserDetails> ChangeUserState(int id)
        {
            var user=await _userRepository.Get(id);
            if( user == null)
            {
                throw new NoSuchUserException();
            }
            if (user.Status == "Active")
            {
                user.Status = "Disabled";
            }
            else
            {
                user.Status = "Active";
            }
            user=await _userRepository.Update(user);
            var userRole=await _rolemappingRepository.Get(id);
            var userdetail = MapToUserDetails(user,userRole.RoleId);
            return userdetail;
        }
    }
}

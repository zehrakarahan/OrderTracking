using Database.Models;
using Database.RequestModel;
using Database.ResponseModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using Database;


namespace OrderTrackingService.Service
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppSettings _appSettings;
        private readonly ICacheService _cacheService;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private SiparisTakipContext _siparisTakipContext;
        public AccountService(UserManager<AppUser> userManager, IOptions<AppSettings> appSettings,
            ICacheService cacheService,IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
            SiparisTakipContext siparisTakipContext)
        {
            _userManager = userManager;
            _appSettings = appSettings.Value;  
            _cacheService = cacheService;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _siparisTakipContext=siparisTakipContext;
        }
        public async Task<string> CreateUser(RegisterRequestModel model)
        {
            if (model == null) return "model null";
            var userCreate=await  _userManager.CreateAsync(new AppUser
            {
               Email= model.Email,
               UserName=model.UserName,
               PhoneNumber=model.Phone,
               LockoutEnabled=model.LockOut
            },model.Password);
            if(userCreate.Succeeded)
                return userCreate.Succeeded.ToString();
            return userCreate.Errors?.ToArray().ToString() ?? "Error list is null";



        }

        public async Task<string> DeleteUser(string Id)
        {
            var user =await _userManager.Users.FirstOrDefaultAsync(x=>x.Id==Id);
            if (user==null) return "User null";
            var deleteUser =await _userManager.DeleteAsync(user);
            return deleteUser.Succeeded ? deleteUser.Succeeded.ToString() :
            deleteUser.Errors.ToList().ToString() ?? "Error list is null";
        }

        public Task<string> GenerateJwtToken(LoginRequestModel user)
        {
            throw new NotImplementedException();
        }

        public async Task<List<LoginResponseModel>> GetAllUser()
        {
            var userList=_userManager.Users.ToList();
            var sonuc= userList.Select(async user =>
            {
                var roles = await _userManager.GetRolesAsync(user);
                return new LoginResponseModel
                {
                    Email = user.Email,
                    UserName = user.UserName,
                    // Özelleştirilmiş bir özellik varsayılıyor
                    LockedoutEnabled = user.LockoutEnabled, // Doğru property adı kullanılmalı
                    PhoneNumber = user.PhoneNumber,
                    IdentityRoleList = roles.ToList() // Burada bir List<string> bekleniyor
                };
            });
            return Task.WhenAll(sonuc).Result.ToList();
        }
        public bool Logout()
        {
            throw new NotImplementedException();
        }

        public async Task<WebToken> PasswordLoginAsync(LoginRequestModel passwordLogin, IPAddress ipAddress)
        {
            var user =await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == passwordLogin.UserNameOrEmail
            || x.Email == passwordLogin.UserNameOrEmail);
            if (user == null)
            {
                throw new BadRequestException("User null");
            };
            if(!await _userManager.CheckPasswordAsync(user,passwordLogin.Password))
            {
                throw new BadRequestException("password is user wrong");
            }
            await _cacheService.SetCacheAsync("user",user);
            var token = GenerateJwtToken(user);
            return new WebToken
            {
                Token=token,
                UserId=user.Id,
                RefreshToken= GenerateRefreshToken()
            };

        }
        private string GenerateJwtToken(AppUser user)
        {
            // generate token that is valid for 1 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer=_appSettings.Issuer
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public LoginResponseModel UpdateUser(EditViewRequestModel model)
        {
            throw new NotImplementedException();
        }

        public string ForgetPassword(ForgotPasswordModel model)
        {
           /* var user = _userManager.Users.FirstOrDefault(x => x.Email == model.Email || x.UserName == model.Email);
            if (user == null) throw new BadRequestException("user null");*/
            _emailSender.SendEmail(model.Email,"ordertracking forget password");
            _cacheService.SetCacheAsync("forgetpassword", model);
            var cachemodel = _cacheService.GetCacheAsync<ForgotPasswordModel>("forgetpassword");
            return "";
        }


        public async Task<IdentityRole> AddRole(string roleName)
        {
            try
            {
                var newRole = new IdentityRole { Name = roleName };
                var result = await _roleManager.CreateAsync(newRole);
                if (!result.Succeeded)
                {
                    // Hata işleme veya loglama
                    // result.Errors içindeki hataları kontrol edin
                    throw new BadRequestException (result.Errors.ToList().ToString());
                }
                return result.Succeeded ? newRole : null;

            }
            catch (Exception ex) 
            { 
            
                return new IdentityRole { Name = ex.Message };

            }

        }

        public List<IdentityRole> RoleList()
        {
           return _roleManager.Roles.ToList();
        }

   
    }
}

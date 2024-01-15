using Database.RequestModel;
using Database.ResponseModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OrderTrackingService.Service
{
    public interface IAccountService
    {
        Task<WebToken> PasswordLoginAsync(LoginRequestModel passwordLogin, IPAddress ipAddress);
        Task<string> GenerateJwtToken(LoginRequestModel user);

        public bool Logout();

        public Task<string> CreateUser(RegisterRequestModel model);

        public LoginResponseModel UpdateUser(EditViewRequestModel model);

        public Task<string> DeleteUser(string Id);


        public Task<List<LoginResponseModel>> GetAllUser();

        public string ForgetPassword(ForgotPasswordModel model);

        public Task<IdentityRole> AddRole(string roleName);

        public List<IdentityRole> RoleList();

        
    }
}

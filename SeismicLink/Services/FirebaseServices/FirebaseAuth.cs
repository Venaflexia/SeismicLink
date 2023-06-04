using SeismicLink.Mvvm.Models.Enums;
using SeismicLink.Mvvm.Models.UserInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeismicLink.Services.FirebaseServices
{

    public interface IAuth
    {
        Task<LoginEnums> RegisterUser(string name, string email, string password);
        Task<LoginEnums> AuthenticateUser(string email, string password);
        bool IsAuthenticated();
        string GetCurrentUserId();
        Task<bool> IsEmailVerified();
        bool SignOutUser();
        Task<bool> ForgotPasswordUser(string email);
        UserInfo GetUserInfo();
        Task<bool> UpdateUserInfo(string NewUserName);
        Task<bool> DeleteUser();
    }
    public class FirebaseAuth
    {
        public static IAuth Auth = new SeismicLink.Platforms.FirebaseAuthPlatformSpesific();

        public static async Task<LoginEnums> AuthenticateUser(string email, string password)
        {
           return await Auth.AuthenticateUser(email, password);
        }

        public static string GetCurrentUserId()
        {
           return Auth.GetCurrentUserId();
        }
        public static UserInfo GetUserInfo()
        {
            return Auth.GetUserInfo();
        }


        public static bool IsAuthenticated()
        {
           return Auth.IsAuthenticated();
        }
        public static bool SignOutUser()
        {
            return Auth.SignOutUser();
        }
        public static async Task<bool> DeleteUser()
        {
            return await Auth.DeleteUser();
        }
        public static async Task<bool> UpdateUserInfo(string NewUserName)
        {
            return await Auth.UpdateUserInfo(NewUserName);
        }
        public static async Task<bool> ForgotPasswordUser(string email)
        {
            return await Auth.ForgotPasswordUser(email);
        }
        public static async Task<bool> IsEmailVerified()
        {
            return await Auth.IsEmailVerified();
        }
        public static async Task<LoginEnums> RegisterUser(string name, string email, string password)
        {
            return await Auth.RegisterUser(name, email, password);
        }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace CalcApp.Models
{
    public class LoginModel
    {
        public LoginModel()
        {
        }
        [Required]
        public string Username { get;set; }
        [Required]
        public string password { get; set; }
        //public List<Username,password> loginList { get; set; }
        public string error { get; set; }
    }
}

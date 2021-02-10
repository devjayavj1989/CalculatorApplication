using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using CalcApp.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CalcApp
{
    public class LoginController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View("Login");
        }
        public bool CheckLogin(LoginModel loginValue)
        {
            try
            {
                if (System.IO.File.Exists("login.txt"))
                {
                    var UserValue = System.IO.File.ReadAllLines("login.txt");
                    foreach (string line in UserValue)
                    {
                        if (line.Contains(loginValue.Username))
                        {
                            // loginValue.error = "Login Error Please Login Again or signUp";
                            return true;
                        }
                    }
                }


                return false;
            }
            catch (Exception ex)
            {
                loginValue.error = ex.ToString();
                return false;
            }
        }
        public ActionResult SignUp(LoginModel val)
        {
            try
            {
                // LoginController lg = new LoginController();
                if (CheckLogin(val))
                {
                    LoginModel lm = new LoginModel();
                    lm.error = "Please Login the Username already exists";
                    return View("Login", lm);

                }
                else
                {
                    StreamWriter st = new StreamWriter("login.txt");

                    st.WriteLine(val.Username, val.password);

                    return RedirectToAction("Index", "CalcAppControllerClass");
                };

            }
            catch (Exception ex)
            {
                val.error = ex.ToString();
                return View("Login");

            }

        }
        public ActionResult LoginAction(LoginModel val)
        {
            try
            {
                // LoginController lg = new LoginController();
                if (CheckLogin(val))
                {
                    return RedirectToAction("Index", "CalcAppControllerClass");

                }
                else
                {
                    LoginModel lm = new LoginModel();
                    lm.error = "Please Check ,Username/password incorrect";
                    return View("Login");
                };

            }
            catch (Exception ex)
            {
                val.error = ex.ToString();
                return View("Login",ex);

            }

        }
    }
}

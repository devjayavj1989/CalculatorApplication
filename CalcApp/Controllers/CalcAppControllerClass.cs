
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CalcApp.Models;
using System.Web;
using System.Data;
using System.Configuration;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace CalcApp.Controllers
{
    public class CalcAppControllerClass : Controller
    {
        public CalcAppControllerClass()
        {
            //numbers testValue = new numbers();

        }
        numbers resultValue = new numbers();
        // bool logintrue = false;

        public ActionResult Index(numbers num)
        {
            try
            {
                string sessionValue = HttpContext.Session.GetString("Login");
                resultValue.value = ListResults();
                if (num.Number != null && sessionValue == "Yes")
                {

                    List<string> exprList = new List<string>(num.Number.Split('+', '-', '*', '/'));

                    if (exprList.Count == 2)
                    {
                        if (num.Number.Contains("+"))
                        {
                            // List<string> exprList = new List<string>(num.Number.Split('+', '-', '*', '/'));
                            resultValue.result = Add(exprList);
                        }
                        else if (num.Number.Contains("-"))
                        {

                            resultValue.result = Subtract(exprList);
                        }
                        else if (num.Number.Contains("*")) resultValue.result = Multiplication(exprList);
                        else if (num.Number.Contains("/")) resultValue.result = Divide(exprList);
                        else ViewBag.error = "Please enter a valid Arithmetic experssion *,+,_,/";
                    }
                    else
                    {
                        ViewBag.error = "Please enter expressions with single operation";
                    }
                    if (resultValue.result != null)
                    {
                        if (System.IO.File.Exists("./result.txt"))
                        {
                            using (StreamWriter writer = new StreamWriter("./result.txt", true))
                            {
                                writer.WriteLine();

                                writer.Write(num.Number);
                            }
                        }
                        else
                        {

                            System.IO.File.WriteAllText("./result.txt", num.Number);

                        }
                    }

                }

                return View(resultValue);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex;
                return View("Login");
            }
        }

        public string Add(List<string> Qlist)
        {
            double val1 = 0;
            for (int i = 0; i < Qlist.Count; i++)
            {
                val1 += Convert.ToDouble(Qlist[i]);
            }
            return val1.ToString();
        }
        public string Subtract(List<string> Qlist)
        {
            double val1 = 0;

            for (int i = 0; i < Qlist.Count; i++)
            {
                if (i == 0)
                    val1 = Convert.ToDouble(Qlist[i]) - val1;
                else val1 -= Convert.ToDouble(Qlist[i]);
            }
            return val1.ToString();
        }
        public string Multiplication(List<string> Qlist)
        {
            double val1 = 0;
            for (int i = 0; i < Qlist.Count - 1; i++)
            {
                if (val1 == 0)
                {
                    val1 = Convert.ToDouble(Qlist[0]);
                }
                val1 = val1 * Convert.ToDouble(Qlist[i + 1]);
            }
            return val1.ToString();
        }

        public string Divide(List<string> Qlist)
        {
            double val1 = 0;
            for (int i = 0; i < Qlist.Count - 1; i++)
            {
                if (val1 == 0)
                {

                    val1 = Convert.ToDouble(Qlist[0]);
                }
                val1 = val1 / Convert.ToDouble(Qlist[i + 1]);
            }
            return val1.ToString();
        }

        public bool CheckLogin(LoginModel loginValue)
        {
            try
            {
                string lgText = loginValue.Username + " " + loginValue.password;
                if (loginValue.Username == null)
                {
                    return false;
                }

                if (System.IO.File.Exists("./login.txt"))
                {

                    var UserValue = System.IO.File.ReadAllLines("./login.txt");

                    foreach (string line in UserValue)
                    {
                        if (line.Contains(lgText))
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    //System.IO.File.Create("./login.txt");
                    System.IO.File.WriteAllText("./login.txt", lgText);              
                    return true;
                }


                return false;
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.ToString();
                return false;
            }
        }
        public ActionResult SignUp(LoginModel val)
        {
            try
            {
                if (textMax(val) == false)
                {
                    // ViewBag.error = "Please enter 8 characters";
                    return View("Login");
                }
                if (val.Username == null)
                {
                    // ViewBag.error = "Please enter value";
                    return View("Login");
                }
                // LoginController lg = new LoginController();
                if (CheckLogin(val))
                {

                    ViewBag.error = "Please Login the Username already exists";
                    return View("Login");

                }
                else
                {
                    string lgText = val.Username + " " + val.password;
                    if (System.IO.File.Exists("./login.txt"))
                    {
                        using (StreamWriter writer = new StreamWriter("./login.txt", true))
                        {
                            writer.WriteLine();
                            writer.Write(lgText);
                        }
                    }
                    ViewBag.error = "SignUp successful.Please Login.";
                    return View("Login");
                };

            }
            catch (Exception ex)
            {
                ViewBag.error = ex.ToString();
                return View("Login");

            }

        }
        public ActionResult LoginAction(LoginModel val)
        {
            try
            {
                if (textMax(val) == false)
                {
                    ViewBag.error = "Please enter 8 characters";
                    return View("Login");
                }
                if (val.Username == null)
                {
                    ViewBag.error = "Please enter value";
                    return View("Login");
                }
                // LoginController lg = new LoginController();
                if (CheckLogin(val))
                {

                    HttpContext.Session.SetString("Login", "Yes");
                    return RedirectToAction("Index", "CalcAppControllerClass");

                }
                else
                {

                    ViewBag.error = "Please Check ,Username/password incorrect";
                    return View("Login");
                };

            }
            catch (Exception ex)
            {
                ViewBag.error = ex.ToString();
                return View("Login");

            }
        }
        public List<string> ListResults()
        {
            numbers num1 = new numbers();
            num1.value = new List<string>();
            if (System.IO.File.Exists("./result.txt"))

            {
                var listTemp = System.IO.File.ReadAllLines("./result.txt");
                List<string> resultStack = new List<string>(listTemp.ToList());
                if (resultStack.Count > 10)
                {
                    for (int i = resultStack.Count - 1; i > resultStack.Count - 10; i--)
                    {
                        num1.value.Add(resultStack[i].ToString());
                    }
                }
                else
                {
                    for (int i = resultStack.Count - 1; i >= 0; i--)
                    {
                        num1.value.Add(resultStack.ElementAt(i).ToString());
                    }
                }
            }

            return num1.value;

        }

        public bool textMax(LoginModel m)
        {
            if ((m.Username.Max() <= 8) || (m.password.Max() <= 8))
            {
                return false;
            }
            return true;
        }

    }

}

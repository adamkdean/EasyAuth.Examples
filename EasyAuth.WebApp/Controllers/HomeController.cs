using System.Configuration;
using System.Web.Mvc;
using EasyAuth.WebApp.Models.Home;

namespace EasyAuth.WebApp.Controllers
{
    [EzAuthorize]
    public class HomeController : Controller
    {        
        //
        // GET: /Home/

        [EzAllowAnonymous]
        public ActionResult Index()
        {            
            return View();
        }

        //
        // GET: /Home/Login

        [EzAllowAnonymous]
        public ActionResult Login()
        {            
            return View();
        }

        //
        // POST: /Home/Login

        [HttpPost]
        [EzAllowAnonymous]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid && Authentication.Login(model.Username, model.Password, model.Persist))
            {
                return RedirectToAction("MembersOnly", "Home");
            }

            ViewBag.Message = string.Format("Invalid user credentials ({0}, {1})", model.Username, model.Password);
            return View(model);
        }

        //
        // GET: /Home/Logout

        public ActionResult Logout()
        {
            Authentication.Logout();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Home/MembersOnly

        public ActionResult MembersOnly()
        {
            return View();
        }

        //
        // GET: /Home/ListUsers

        [EzAllowAnonymous]
        public ActionResult ListUsers()
        {            
            return View();
        }

        //
        // GET: /Home/CreateUser

        [EzAllowAnonymous]
        public ActionResult CreateUser()
        {
            string username = "", password = "";

            for (int i = 0; i < 100; i++)
            {
                username = string.Format("User{0:D}", i);
                password = string.Format("Pass{0:D}", i);

                if (!Authentication.UserStore.UserExistsByUsername(username))
                {
                    Authentication.UserStore.AddUser(username, password);
                    break;
                }
                else
                {
                    continue;
                }
            }

            ViewBag.Message = string.Format("User created, {0} : {1}", username, password);
            return View();
        }

        //
        // GET: /Home/ConnectionString

        [EzAllowAnonymous]
        public ActionResult ConnectionString()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            ViewBag.Message = string.Format("ConnectionStrings[\"DefaultConnection\"] = {0}", connectionString);

            return View();
        }
    }
}

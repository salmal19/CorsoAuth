using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using CorsoAuth.Models;
using System.Web.Security;

namespace CorsoAuth.Controllers
{
    public class UtenteController : Controller
    {
        // GET: Utente
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UtenteLoginModel utente, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                string username = IsValid(utente.Email, utente.Password);
                if (username != string.Empty)
                {
                    FormsAuthentication.SetAuthCookie(username, false);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Dati non corretti");
                }
            }
            return View(utente);
        }

        [HttpGet]
        public ActionResult Registrazione()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrazione(UtenteCreateModel utente)
        {
            if (ModelState.IsValid)
            {
                using (var db = new Database1Entities())
                {
                    var crypto = new SimpleCrypto.PBKDF2();
                    string encPassword = crypto.Compute(utente.Password);

                    var nuovoUtente = db.Utenti.Create();
                    nuovoUtente.Email = utente.Email;
                    nuovoUtente.Username = utente.Username;
                    nuovoUtente.Password = encPassword;
                    nuovoUtente.PasswordSalt = crypto.Salt;
                    nuovoUtente.UserId = Guid.NewGuid();

                    db.Utenti.Add(nuovoUtente);
                    db.SaveChanges();
                }
                return RedirectToAction("Index", "Home");
            }
            return View(utente);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private string IsValid(string email, string password)
        {
            var username = string.Empty;
            var crypto = new SimpleCrypto.PBKDF2();
            using (var db = new Database1Entities())
            {
                var utente = db.Utenti.FirstOrDefault(u => u.Email == email);
                if (utente != null)
                {
                    if (utente.Password == crypto.Compute(password,utente.PasswordSalt))
                    {
                        username = utente.Username;
                    }
                }
            }
            return username;
        }

        public ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
               return RedirectToAction("Index", "Home");
            }
        }
    }
}
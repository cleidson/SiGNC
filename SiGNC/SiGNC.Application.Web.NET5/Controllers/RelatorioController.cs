using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiGNC.Application.Web.NET5.Controllers
{
    public class RelatorioController : Controller
    {
        // GET: RelatorioController
        public ActionResult Index()
        {
            return View();
        }

        // GET: RelatorioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

         


    }
}

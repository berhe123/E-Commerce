﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Controllers
{
    public class HomeController : Controller
    {
      [Authorize(Roles = "admin,berhe,newadmin")]
        public ActionResult Index()
        {
            return View();
        }

        
    }
}
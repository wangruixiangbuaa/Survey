﻿using HPIT.Survey.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HPIT.Survey.Portal.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Index()
        {
            ViewBag.User = DeluxeUser.CurrentMember;
            return View();
        }
    }
}
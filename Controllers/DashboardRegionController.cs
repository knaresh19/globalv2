﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GAIN.Controllers
{
    public class DashboardRegionController : MyBaseController
    {
        // GET: DashboardRegion
        public ActionResult Index()
        {
            return View();
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GAIN.Models;

namespace GAIN.Controllers
{
    public class AgencyController : MyBaseController
    {
        // GET: SummaryDashboard

        GAIN.Models.GainEntities db = new GAIN.Models.GainEntities(clsSecretManager.GetConnectionstring(ConfigurationManager.AppSettings["rdssecret"]));
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AgencyPartial()
        {
            var profileData = Session["DefaultGAINSess"] as LoginSession;
            var tahun = (profileData == null ? DateTime.Now.Year : profileData.ProjectYear);
            var model = db.vwagencies.Where(c=>c.projectyear==tahun);
            return PartialView("_GrdAgencyPartial", model.ToList());
        }

        public ActionResult Filteringpartial()
        {
            return PartialView("_GrdFilterPartial");
        }
    }
}
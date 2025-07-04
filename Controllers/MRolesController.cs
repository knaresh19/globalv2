﻿using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GAIN.Controllers
{
    public class MRolesController : MyBaseController
    {
        // GET: MRoles
        public ActionResult Index()
        {
            return View();
        }

        GAIN.Models.GainEntities db = new GAIN.Models.GainEntities(clsSecretManager.GetConnectionstring(ConfigurationManager.AppSettings["rdssecret"]));
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [ValidateInput(false)]
        public ActionResult GrdMrolesPartial()
        {
            var model = db.mroles;
            return PartialView("_GrdMRolesPartial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GrdMRolesPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] GAIN.Models.mrole item)
        {
            var model = db.mroles;
            if (ModelState.IsValid)
            {
                try
                {
                    model.Add(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                    log.Error(e.Message, e);
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GrdMUsersPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GrdMRolesPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] GAIN.Models.mrole item)
        {
            var model = db.mroles;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.id == item.id);
                    if (modelItem != null)
                    {
                        modelItem.role_code = item.role_code;
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                    log.Error(e.Message, e);
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GrdMRolesPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GrdMRolesPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))] GAIN.Models.mrole itemx)
        {
            var model = db.mroles;
            if (itemx.id >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.id == itemx.id);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                    log.Error(e.Message, e);
                }
            }
            return PartialView("_GrdMRolesPartial", model.ToList());
        }
    }
}
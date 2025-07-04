﻿using DevExpress.Web.Mvc;
using DevExpress.XtraRichEdit.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GAIN.Controllers
{
    public class MStatusController : MyBaseController
    {
        // GET: MStatus
        public ActionResult Index()
        {
            return View();
        }

        GAIN.Models.GainEntities db = new GAIN.Models.GainEntities(clsSecretManager.GetConnectionstring(ConfigurationManager.AppSettings["rdssecret"]));
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [ValidateInput(false)]
        public ActionResult GrdStatusPartial()
        {
            var model = db.mstatus.Where(x => x.InitYear == GAIN.Models.Constants.defaultyear);
            return PartialView("_GrdStatusPartial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GrdStatusPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] GAIN.Models.mstatu item)
        {
            var model = db.mstatus;
            var tmodel = model.Where(x => x.InitYear == GAIN.Models.Constants.defaultyear).ToList();
            if (item.Status != null && item.Status != string.Empty && item.isActive!=null)
            {
                if (tmodel.Where(x => x.Status.ToLower() == item.Status.ToLower()).ToList().Count == 0)
                {
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            item.InitYear = GAIN.Models.Constants.defaultyear;
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
                }
                else
                    ViewData["EditError"] = "Already Exists!.";
            }
            else
                ViewData["EditError"] = "Please fill out all required fields.";
            return PartialView("_GrdStatusPartial", model.Where(x => x.InitYear == GAIN.Models.Constants.defaultyear).ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GrdStatusPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] GAIN.Models.mstatu item)
        {
            var model = db.mstatus;
            var tmodel = model.Where(x => x.InitYear == GAIN.Models.Constants.defaultyear).ToList();
            if (item.Status != null && item.Status != string.Empty && item.isActive != null)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var modelItem = model.FirstOrDefault(it => it.id == item.id);
                        if (modelItem != null)
                        {
                            if (tmodel.Where(x => x.Status.ToLower() == item.Status.ToLower() && x.id != item.id).ToList().Count == 0)
                            {
                                modelItem.Status = item.Status;
                                modelItem.isActive = item.isActive;
                                db.SaveChanges();
                            }
                            else
                                ViewData["EditError"] = "Already Exists!.";
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
            }
            else
                ViewData["EditError"] = "Please fill out all required fields.";

            return PartialView("_GrdStatusPartial", model.Where(x => x.InitYear == GAIN.Models.Constants.defaultyear).ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GrdStatusPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))] GAIN.Models.mstatu itemx)
        {
            var model = db.mstatus;
            if (itemx.id >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.id == itemx.id);
                    if (item != null)
                        item.isActive = "N";
                        //model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                    log.Error(e.Message, e);
                }
            }
            return PartialView("_GrdStatusPartial", model.Where(x => x.InitYear == GAIN.Models.Constants.defaultyear).ToList());
        }
    }
}
﻿using DevExpress.Web.Mvc;
using DevExpress.XtraRichEdit.Model;
using GAIN.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GAIN.Controllers
{
    public class MRegionController : MyBaseController
    {
        // GET: MRegion
        public ActionResult Index()
        {
            return View();
        }

        GAIN.Models.GainEntities db = new GAIN.Models.GainEntities(clsSecretManager.GetConnectionstring(ConfigurationManager.AppSettings["rdssecret"]));
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [ValidateInput(false)]
        public ActionResult GrdRegionPartial()
        {
            var model = db.mregions.Where(x => x.InitYear == Constants.defaultyear);
            return PartialView("_GrdRegionPartial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GrdRegionPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] GAIN.Models.mregion item)
        {
            var model = db.mregions;
            var tmodel = model.Where(x => x.InitYear == Constants.defaultyear).ToList();
            if (item.RegionName != null && item.RegionName != string.Empty && item.isActive!=null)
            {
                if (tmodel.Where(x => x.RegionName.ToLower() == item.RegionName.ToLower()).ToList().Count == 0)
                {
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            item.InitYear = Constants.defaultyear;
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

            return PartialView("_GrdRegionPartial", model.Where(x => x.InitYear == Constants.defaultyear).ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GrdRegionPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] GAIN.Models.mregion item)
        {
            var model = db.mregions;
            var tmodel = model.Where(x => x.InitYear == Constants.defaultyear).ToList();
            if (item.RegionName != null && item.RegionName != string.Empty && item.isActive != null)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var modelItem = model.FirstOrDefault(it => it.id == item.id);
                        if (modelItem != null)
                        {
                            if (tmodel.Where(x => x.RegionName.ToLower() == item.RegionName.ToLower() && x.id != item.id).ToList().Count == 0)
                            {
                                modelItem.RegionName = item.RegionName;
                                modelItem.isActive = item.isActive;
                                db.SaveChanges();
                            }
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

            return PartialView("_GrdRegionPartial", model.Where(x => x.InitYear == Constants.defaultyear).ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GrdRegionPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))] GAIN.Models.mregion itemx)
        {
            var model = db.mregions;
            if (itemx.id >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.id == itemx.id);
                    if (item != null)
                        item.isActive = 0;
                    //model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                    log.Error(e.Message, e);
                }
            }
            return PartialView("_GrdRegionPartial", model.Where(x => x.InitYear == Constants.defaultyear).ToList());
        }
    }
}
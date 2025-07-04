﻿using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GAIN.Models;

namespace GAIN.Controllers
{
    public class MSubCountryCostControlController : MyBaseController
    {
        // GET: MBrandCountry
        public ActionResult Index()
        {
            return View();
        }

        GAIN.Models.GainEntities db = new GAIN.Models.GainEntities(clsSecretManager.GetConnectionstring(ConfigurationManager.AppSettings["rdssecret"]));
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [ValidateInput(false)]
        public ActionResult GrdSubCountryCostControlPartial()
        {
            List<mbrand> lst = db.mbrands.Where(s => s.isDeleted == "N" && s.InitYear== Constants.defaultyear).ToList();
            ViewData["Costcontrolsite"] = db.mcostcontrolsites.Where(x => x.InitYear == Constants.defaultyear).ToList();
            ViewData["BrandList"] = lst;
            ViewData["Subcountry"] = db.msubcountries.Where(x => x.InitYear == Constants.defaultyear).ToList();
            var model = db.t_subctry_costcntrlsite.Where(x => x.InitYear == Constants.defaultyear).ToList().Where(P => lst.Any(s => s.id == P.brandid));
            return PartialView("_GrdSubCountryCostControlPartial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GrdSubCountryCostControlPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] GAIN.Models.t_subctry_costcntrlsite item)
        {
            var model = db.t_subctry_costcntrlsite;
            var tmodel = model.Where(x => x.InitYear == Constants.defaultyear).ToList();
            if (item.brandid != 0 && item.subcountryid != 0 && item.costcontrolid != 0)
            {
                if (tmodel.Where(x => x.brandid == item.brandid && x.subcountryid == item.subcountryid && x.costcontrolid == item.costcontrolid).ToList().Count == 0)
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

            
            List<mbrand> lst = db.mbrands.Where(s => s.isDeleted == "N" && s.InitYear == Constants.defaultyear).ToList();
            ViewData["Costcontrolsite"] = db.mcostcontrolsites.Where(x => x.InitYear == Constants.defaultyear).ToList();
            ViewData["BrandList"] = lst;
            ViewData["Subcountry"] = db.msubcountries.Where(x => x.InitYear == Constants.defaultyear).ToList();
            var modelresult = db.t_subctry_costcntrlsite.Where(x => x.InitYear == Constants.defaultyear).ToList().Where(P => lst.Any(s => s.id == P.brandid));
            return PartialView("_GrdSubCountryCostControlPartial", modelresult.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GrdSubCountryCostControlPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] GAIN.Models.t_subctry_costcntrlsite item)
        {
            var model = db.t_subctry_costcntrlsite;
            var tmodel = model.Where(x => x.InitYear == Constants.defaultyear).ToList();

            if (item.brandid != 0 && item.subcountryid != 0 && item.costcontrolid != 0)
            {

                if (ModelState.IsValid)
                {
                    try
                    {
                        var modelItem = model.FirstOrDefault(it => it.id == item.id);
                        if (modelItem != null)
                        {
                            if (tmodel.Where(x => x.id != item.id && x.brandid == item.brandid && x.subcountryid == item.subcountryid && x.costcontrolid == item.costcontrolid).ToList().Count == 0)
                            {
                                modelItem.brandid = item.brandid;
                                modelItem.subcountryid = item.subcountryid;
                                modelItem.costcontrolid = item.costcontrolid;
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

            List<mbrand> lst = db.mbrands.Where(s => s.isDeleted == "N" && s.InitYear == Constants.defaultyear).ToList();
            ViewData["Costcontrolsite"] = db.mcostcontrolsites.Where(x => x.InitYear == Constants.defaultyear).ToList();
            ViewData["BrandList"] = lst;
            ViewData["Subcountry"] = db.msubcountries.Where(x => x.InitYear == Constants.defaultyear).ToList();
            var modelresult = db.t_subctry_costcntrlsite.Where(x => x.InitYear == Constants.defaultyear).ToList().Where(P => lst.Any(s => s.id == P.brandid));
            return PartialView("_GrdSubCountryCostControlPartial", modelresult.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GrdSubCountryCostControlPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))] GAIN.Models.t_subctry_costcntrlsite itemx )
        {
            var model = db.t_subctry_costcntrlsite;
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
            List<mbrand> lst = db.mbrands.Where(s => s.isDeleted == "N" && s.InitYear == Constants.defaultyear).ToList();
            ViewData["Costcontrolsite"] = db.mcostcontrolsites.Where(x => x.InitYear == Constants.defaultyear).ToList();
            ViewData["BrandList"] = lst;
            ViewData["Subcountry"] = db.msubcountries.Where(x => x.InitYear == Constants.defaultyear).ToList();
            var modelresult = db.t_subctry_costcntrlsite.Where(x => x.InitYear == Constants.defaultyear).ToList().Where(P => lst.Any(s => s.id == P.brandid));
            return PartialView("_GrdSubCountryCostControlPartial", modelresult.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Getsubcountrybycountry(int CountryId)
        {
           List<SubCountryList> SubCountryList = new List<SubCountryList>();

            SubCountryList = (from subcountries in db.msubcountries
                             where subcountries.CountryID == CountryId
                             select subcountries).ToList().Select(s => new SubCountryList { id = s.id, SubCountryName = s.SubCountryName }).ToList();

            return Json(SubCountryList, JsonRequestBehavior.AllowGet);
        }
    }
}
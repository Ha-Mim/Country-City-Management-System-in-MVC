using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CityInformation.Models;
using PagedList;

namespace CityInformation.Controllers
{
    public class CountryController : Controller
    {
        CountryDbGateway aGateway=new CountryDbGateway();
        public ActionResult Save()
        {
           
            ViewBag.countries = aGateway.GetAll().OrderBy(a=>a.Name);
            return View();
        }

        [HttpPost]
        public ActionResult Save(Country aCountry, HttpPostedFileBase file)
        {

           
            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                    Server.MapPath("~/images/"), pic);
                // file is uploaded
                file.SaveAs(path);
                aCountry.Imagepath = "/images/" + file.FileName;
            }
            
            aGateway.Save(aCountry);
            ViewBag.msg= "Successfully Saved";
            ViewBag.countries = aGateway.GetAll().OrderBy(a => a.Name);
            return View();
        }

        public JsonResult NameExists(string name)
        {
            if (aGateway.GetAll().FirstOrDefault(a => a.Name == name) == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search( string Search_Data, string Filter_Value, int? Page_No)
        {

            if (Search_Data != null)
            {
                Page_No = 1;
            }
            else
            {
                Search_Data = Filter_Value;
            }

            ViewBag.FilterValue = Search_Data;

            var countries = aGateway.GetAll().Select(country => country);
            foreach (Country aCountry in countries)
            {
                aCountry.NoOfCity = aGateway.NoOfCities(aCountry.Id);
                aCountry.NoOfDwellers = aGateway.NoOfDwellers(aCountry.Id);
            }
            if (!String.IsNullOrEmpty(Search_Data))
            {
                countries = countries.Where(stu => stu.Name.ToUpper().Contains(Search_Data.ToUpper()));
            }
          

            int Size_Of_Page = 2;
            int No_Of_Page = (Page_No ?? 1);
            return View(countries.ToPagedList(No_Of_Page, Size_Of_Page));

        }
        public ActionResult Show(int? id)
        {
            ViewBag.countries = aGateway.GetAll().OrderBy(a => a.Name);
            ViewBag.Country = aGateway.GetAll().FirstOrDefault(x => x.Id == id);
            return View("Save");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CityInformation.Models;
using PagedList;

namespace CityInformation.Controllers
{
    public class CityController : Controller
    {
        CountryDbGateway aCountryDbGateway=new CountryDbGateway();
        CityDbGateway aCityDbGateway =new CityDbGateway();
        public ActionResult Save()
        {
            ViewBag.Country = aCountryDbGateway.GetAll().OrderBy(a=>a.Name);
            ViewBag.Cities = aCityDbGateway.GetAll().OrderBy(a => a.Name);
            return View();
        }
        [HttpPost]
        public ActionResult Save(City aCity)
        {
            aCityDbGateway.Save(aCity);
            ViewBag.Msg = "Successfully Saved";
            ViewBag.Cities = aCityDbGateway.GetAll().OrderBy(a => a.Name);
            ViewBag.Country = aCountryDbGateway.GetAll().OrderBy(a => a.Name);
            return View();
        }

        public JsonResult NameExists(string name)
        {
            if (aCityDbGateway.GetAll().FirstOrDefault(a => a.Name == name) == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Search(string City,string Country,string Search_Country, string Search_Data, string Filter_Value, int? Page_No)
        {

            ViewBag.Search_Country = new SelectList(aCountryDbGateway.GetAll(), "Id", "Name");
            
            if (Search_Data != null)
            {
                Page_No = 1;
            }
            else
            {
                Search_Data = Filter_Value;
            }

            ViewBag.FilterValue = Search_Data;

            var cities = aCityDbGateway.GetAll().Select(city => city);

            if (!String.IsNullOrEmpty(Search_Data)&& City=="on")
            {
                cities = cities.Where(stu => stu.Name.ToUpper().Contains(Search_Data.ToUpper()));
            }
            if (!String.IsNullOrEmpty(Search_Country)&& Country=="on")
            {
                cities = cities.Where(stu => stu.ACountry.Id==Convert.ToInt16(Search_Country));
            }
            

            int Size_Of_Page = 2;
            int No_Of_Page = (Page_No ?? 1);
            return View(cities.ToPagedList(No_Of_Page, Size_Of_Page));
            
        }
    }
}
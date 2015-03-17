using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CityInformation.Models
{
    public class City
    {
        public int Id { set; get; }
        [Remote("NameExists", "City", ErrorMessage = "City already exists")]
        public string Name { set; get; }
        [DataType(DataType.MultilineText)]
        public string About { set; get; }
        [DisplayName("No of dwellers")]
        public int NoOfDwellers { set; get; }
        public string Location { set; get; }
        public string Weather { set; get; }
        public int CountryId { set; get; }
        public Country ACountry { set; get; }
    }
}
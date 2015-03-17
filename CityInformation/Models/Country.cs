using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CityInformation.Models
{
    public class Country
    {
        public int Id { set; get; }
        [Required]
        [Remote("NameExists","Country",ErrorMessage = "Country already exists")]
        public string Name { set; get; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string About { set; get; }
        public string Imagepath { set; get; }
        public int NoOfCity { set; get; }
        public double NoOfDwellers { set; get; }
    }
}

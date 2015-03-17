using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CityInformation.Models
{
    public class CityDbGateway
    {
        private string ConnectionString =
            ConfigurationManager.ConnectionStrings["connStrngForCountryDb"].ConnectionString;

        private SqlConnection aConnection;
        private SqlCommand aCommand;
        private SqlDataReader aReader;

        public CityDbGateway()
        {
            aConnection=new SqlConnection(ConnectionString);
        }

        public void Save(City aCity)
        {
            string query = "Insert into tbl_city values('" + aCity.Name + "','" + aCity.About + "','" + aCity.NoOfDwellers + "','" + aCity.Location + "','" + aCity.Weather + "','" + aCity.CountryId + "');";
            aConnection.Open();
            aCommand = new SqlCommand(query, aConnection);
            aCommand.ExecuteNonQuery();
            aConnection.Close();

            //aConnection.Open();
            //string query = String.Format("INSERT INTO tbl_city VALUES(@name,@about,@noOfDwellers,@location,@weather,@countryId)");
            //aCommand = new SqlCommand();
            //aCommand.Connection = aConnection;
            //aCommand.CommandText = query;
            //aCommand.Parameters.Clear();
            //aCommand.Parameters.Add("@name", SqlDbType.VarChar);
            //aCommand.Parameters["@name"].Value = aCity.Name;
            //aCommand.Parameters.Add("@about", SqlDbType.NVarChar);
            //aCommand.Parameters["@about"].Value = aCity.About;
            //aCommand.Parameters.Add("@noOfDwellers", SqlDbType.Int);
            //aCommand.Parameters["@noOfDwellers"].Value = aCity.NoOfDwellers;
            //aCommand.Parameters.Add("@location", SqlDbType.VarChar);
            //aCommand.Parameters["@location"].Value = aCity.Location;
            //aCommand.Parameters.Add("@weather", SqlDbType.VarChar);
            //aCommand.Parameters["@weather"].Value = aCity.Weather;
            //aCommand.Parameters.Add("@countryId", SqlDbType.Int);
            //aCommand.Parameters["@countryId"].Value = aCity.CountryId;
      
            //aCommand.ExecuteNonQuery();
            //aConnection.Close();
           
        }
        CountryDbGateway aGateway=new CountryDbGateway();
        public List<City> GetAll()
        {
            string query = "Select * From tbl_city";
            aConnection.Open();
            aCommand = new SqlCommand(query, aConnection);
            aReader = aCommand.ExecuteReader();
            List<City> cities = new List<City>();
            while (aReader.Read())
            {
                City aCity = new City();
                aCity.Id = Convert.ToInt16(aReader["id"]);
                aCity.Name = aReader["name"].ToString();
                aCity.About = aReader["about"].ToString();
                aCity.NoOfDwellers = Convert.ToInt32(aReader["no_of_dwellers"]);
                aCity.Location = aReader["location"].ToString();
                aCity.Weather = aReader["weather"].ToString();
                aCity.CountryId = Convert.ToInt16(aReader["country_id"]);
                aCity.ACountry = aGateway.GetCountryById(aCity.CountryId);
                cities.Add(aCity);
            }
            aReader.Close();
            aConnection.Close();
            return cities;
        }
    }
}
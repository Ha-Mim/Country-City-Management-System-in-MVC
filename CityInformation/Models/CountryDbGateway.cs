using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CityInformation.Models
{
    public class CountryDbGateway
    {
        private string ConnectionString =
            ConfigurationManager.ConnectionStrings["connStrngForCountryDb"].ConnectionString;

        private SqlConnection aConnection;
        private SqlCommand aCommand;
        private SqlDataReader aReader;

        public CountryDbGateway()
        {
            aConnection = new SqlConnection(ConnectionString);
        }

        public void Save(Country aCountry)
        {
            string query = "Insert into tbl_country values('" + aCountry.Name + "','" + aCountry.About + "','" + aCountry.Imagepath + "');";
            aConnection.Open();
            aCommand = new SqlCommand(query, aConnection);
            aCommand.ExecuteNonQuery();
            aConnection.Close();
        }
        public Country GetCountryById(int id)
        {
            Country aCountry = new Country();
            string query = "Select * From tbl_country where id=" + id;
            aConnection.Open();
            aCommand = new SqlCommand(query, aConnection);
            aReader = aCommand.ExecuteReader();
            if (aReader.HasRows)
            {
                aReader.Read();

                aCountry.Id = Convert.ToInt16(aReader["id"]);
                aCountry.Name = aReader["name"].ToString();
                aCountry.About = aReader["about"].ToString();
                aCountry.Imagepath = aReader["image_path"].ToString();

            }
            aReader.Close();
            aConnection.Close();
            return aCountry;
        }
        public List<Country> GetAll()
        {
            string query = "Select * From tbl_country";
            aConnection.Open();
            aCommand = new SqlCommand(query, aConnection);
            aReader = aCommand.ExecuteReader();
            List<Country> countries = new List<Country>();
            while (aReader.Read())
            {
                Country aCountry = new Country();
                aCountry.Id = Convert.ToInt16(aReader["id"]);
                aCountry.Name = aReader["name"].ToString();
                aCountry.About = aReader["about"].ToString();
                aCountry.Imagepath = aReader["image_path"].ToString();

                countries.Add(aCountry);
            }
            aReader.Close();
            aConnection.Close();
            return countries;
        }

        public int NoOfCities(int CountryId)
        {
            int NoOfCities = 0;
            string query = "Select count(country_id) As noOfCities from tbl_city where country_id=" + CountryId;
            aConnection.Open();
            aCommand = new SqlCommand(query, aConnection);
            aReader = aCommand.ExecuteReader();
            if (aReader.HasRows)
            {
                aReader.Read();
                NoOfCities = Convert.ToInt16(aReader["noOfCities"]);
            }
            aReader.Close();
            aConnection.Close();
            return NoOfCities;
        }
        public double NoOfDwellers(int CountryId)
        {
            double NoOfDwellers = 0;
            string query = "Select sum(no_of_dwellers) As noOfDwellers from tbl_city where country_id=" + CountryId;
            aConnection.Open();
            aCommand = new SqlCommand(query, aConnection);
            aReader = aCommand.ExecuteReader();
            if (aReader.HasRows)
            {
                aReader.Read();
                NoOfDwellers = Convert.ToDouble(aReader["noOfDwellers"]);
            }
            aReader.Close();
            aConnection.Close();
            return NoOfDwellers;
        }
    }
}
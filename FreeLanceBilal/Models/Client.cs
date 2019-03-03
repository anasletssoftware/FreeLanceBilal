using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FreeLanceBilal.Models
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }
        public int ClientTypeId { get; set; }
        public string ClientTypeName { get; set; }
        public string CompanyName { get; set; }
        public string Proprietor { get; set; }
        public string Representative { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int ReturntypeId { get; set; }
        public string ReturnTypeName { get; set; }
        //services importer exporter wholeseller retailer commercial importer.
        public bool Services { get; set; }
        public bool Importer { get; set; }
        public bool Exporter { get; set; }
        public bool WholeSeller { get; set; }
        public bool Retailer { get; set; }
        public bool CommercialImporter { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RegistrationDate { get; set; }
        public string SalesTaxNumber { get; set; }
        public string CNICNumber { get; set; }
        public string NTNNumber { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber1 { get; set; }
        public string MobileNumber2 { get; set; }
        public string OfficeNumber1 { get; set; }
        public string OfficeNumber2 { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string PIN { get; set; }




    }
}
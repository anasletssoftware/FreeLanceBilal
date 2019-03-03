using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FreeLanceBilal.Models
{
    public class ClientType
    {
        [Key]
        public int ClienttypeId { get; set; }
        public string ClientTypeName { get; set; }

    }
}
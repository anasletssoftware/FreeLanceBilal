using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FreeLanceBilal.Models
{
    public class ReturnType
    {
        [Key]
        public int ReturnTypeId { get; set; }
        public string ReturnTypeName { get; set; }

    }
}
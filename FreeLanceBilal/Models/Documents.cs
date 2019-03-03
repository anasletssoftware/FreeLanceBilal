using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FreeLanceBilal.Models
{
    public class Documents
    {
        [Key]
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public string Document { get; set; }

    }
}
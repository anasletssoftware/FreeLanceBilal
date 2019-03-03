using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FreeLanceBilal.Models
{
    public class UserAccounts
    {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage="User Name Is Required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password Is Required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Email Is Required")]
        public string EmailAddress { get; set; }
    }
}
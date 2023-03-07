using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NTQ_Solution.Areas.Admin.Data
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Enter Email,please")]
        [EmailAddress(ErrorMessage = "Email is invalid")]
        [RegularExpression(@"^.{10,30}$", ErrorMessage = "{0} from 10 to 30 character")]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Password,please")]
       // [RegularExpression(@"^(?=.*[0-9])(?=.*[A-Z])(?=.*[!@#$%^&*])(?=.{8,20})", ErrorMessage = "{0} chỉ từ 8 đến 20 ký tự, bao gồm ..")]
        [DisplayName("Password")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
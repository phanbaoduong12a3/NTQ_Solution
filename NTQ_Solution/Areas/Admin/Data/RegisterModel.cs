using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NTQ_Solution.Areas.Admin.Data
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Enter Email,please")]
        [EmailAddress(ErrorMessage = "Email is invalid")]
        [RegularExpression(@"^.{10,30}$", ErrorMessage = "{0} from 10 to 30 characters")]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Password,please")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,20}$", ErrorMessage = "{0} from 8 to 20 characters, include uppercase, lowercase, special characters and numbers")]
        [DisplayName("Password")]
        public string Password { get; set; }

        [DisplayName("ConfirmPassword")]
        [Required(ErrorMessage = "Enter ConfirmPassword,please")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Enter UserName,please")]
        [DisplayName("UserName")]
        public string UserName { get; set; }

        public int ID { get; set; }
        public int? Role { get; set; }
        public bool Status { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? DeleteAt { get; set; }
    }
}
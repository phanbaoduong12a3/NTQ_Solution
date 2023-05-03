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
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        [RegularExpression(@"^.{10,30}$", ErrorMessage = "{0} from 10 to 30 characters")]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password không được để trống")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,20}$", ErrorMessage = "{0} từ 8 đến 20 kí tự gồm chữ hoa, chũ thường, kí tự đặc biệt and số")]
        [DisplayName("Password")]
        public string Password { get; set; }

        [DisplayName("ConfirmPassword")]
        [Required(ErrorMessage = "Confirm password không được để trống")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "UserName không được để trống")]
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
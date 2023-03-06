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
        [Required(ErrorMessage = "Hãy nhập Email")]
        [EmailAddress(ErrorMessage = "Email chưa đúng định dạng")]
        [RegularExpression(@"^.{10,30}$", ErrorMessage = "{0} chỉ từ 10 đến 30 ký tự.")]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Hãy nhập Password")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,20}$", ErrorMessage = "{0} chỉ từ 8 đến 20 ký tự, bao gồm chữ hoa,chữ thường,kí tự đặc biệt và số")]
        [DisplayName("Password")]
        public string Password { get; set; }

        [DisplayName("ConfirmPassword")]
        [Required(ErrorMessage = "Hãy nhập ConfirmPassword")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Hãy nhập UserName")]
        [DisplayName("UserName")]
        public string UserName { get; set; }

        public int ID { get; set; }
        public int? Role { get; set; }
        public int? Status { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? DeleteAt { get; set; }
    }
}
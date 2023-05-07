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
        [RegularExpression(@"^.{10,30}$", ErrorMessage = "{0} từ 10 đến 30 kí tự")]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,20}$", ErrorMessage = "{0} từ 8 đến 20 kí tự gồm chữ hoa, chũ thường, kí tự đặc biệt and số")]
        [DisplayName("Mật khẩu")]
        public string Password { get; set; }

        [DisplayName("Xác nhận mật khẩu")]
        [Required(ErrorMessage = "Xác nhận mật khẩu không được để trống")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        [DisplayName("Tên đăng nhập")]
        public string UserName { get; set; }

        public int ID { get; set; }
        [DisplayName("Quyền")]
        public int Role { get; set; }
        [DisplayName("Trạng thái")]
        public bool Status { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? DeleteAt { get; set; }
        [DisplayName("Địa chỉ")]
        public string Address { get; set; }
        [DisplayName("Số điện thoại")]
        public int? Phone { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTQ_Solution.Common
{
    [Serializable]
    public class UserLogin
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int? Role { get; set; }
        public string AccountName { get; set; }
    }
}
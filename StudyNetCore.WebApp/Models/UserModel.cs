using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudyNetCore.WebApp.Models
{
    public class UserModel
    {
        [Display(Name="用户名")]
        [Required(ErrorMessage ="{0}是必填项")]
        [MaxLength(50,ErrorMessage ="{0}长度最大50")]
        public string UserName { get; set; }
        [Display(Name = "密码")]
        [Required(ErrorMessage = "{0}是必填项")]
        //[MaxLength(50, ErrorMessage = "{0}长度最大50")]
        //[MinLength(6,ErrorMessage ="{0}密码最小6")]
        [StringLength(50,MinimumLength =6,ErrorMessage ="{0}长度应该不小于{2},不大于{1}")]
        public string Password { get; set; }
    }
}

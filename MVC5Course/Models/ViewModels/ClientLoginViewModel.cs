using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ViewModels
{
    public class ClientLoginViewModel
    {
        [Required]
        [DisplayName("名")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("中間名")]
        public string MiddleName { get; set; }
        [Required]
        [DisplayName("姓")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("性別")]
        public string Gender { get; set; }

        [DisplayName("生日")]
        public string DateOfBirth { get; set; }
    }
}
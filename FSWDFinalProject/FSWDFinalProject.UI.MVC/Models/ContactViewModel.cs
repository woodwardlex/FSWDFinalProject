using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FSWDFinalProject.UI.MVC.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "* Name required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "* Email required")]
        [EmailAddress]
        public string Email { get; set; }
        //if used
        [Required(ErrorMessage = "* Subject required")]
        public string Subject { get; set; }
        [UIHint("MultilineText")]
        [Required(ErrorMessage = "* Message required")]
        public string Message { get; set; }
    }
}
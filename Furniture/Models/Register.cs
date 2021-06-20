using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Furniture.Models
{
    public class Register
    {
        [Required]
        [StringLength(16)]
        [MinLength(6, ErrorMessage = "Username should has length from 6 to 16 characters!!")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [StringLength(16)]
        [MinLength(6, ErrorMessage = "Password should has length from 6 to 16 characters!!")]
        public string Password { get; set; }

        [Required]
        [StringLength(30)]
        public string FullName { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [Compare("Password")]
        [StringLength(16)]
        public string ConfirmPass { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        [StringLength(10)]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Phone number field is invalid!!")]
        public string Phone { get; set; }

        public int Role { get; set; }
    }
}
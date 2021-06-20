using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Furniture.Models
{
    public class Login
    {
        [Required]
        [StringLength(16)]
        public string Username { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [DataType(DataType.Password)]
        [Required]
        [StringLength(16)]
        [MinLength(6, ErrorMessage = "Password should has length from 6 to 16 characters!!")]
        public string Password { get; set; }

    }
}
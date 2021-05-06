using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Data.EFModels
{

    public class ApplicationUser : IdentityUser
    {
        public int? CustomerId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarathonWebApiCore.ViewModel
{
    public class CreateRoleViewModel
    {
        [Required(ErrorMessage = "Role name is required")]
        public string Name { set; get; }
    }
}

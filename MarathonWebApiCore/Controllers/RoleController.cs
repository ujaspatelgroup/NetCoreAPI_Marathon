using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MarathonWebApiCore.Helpers;
using MarathonWebApiCore.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarathonWebApiCore.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;

        }


        // GET: api/<RoleController>
        [HttpGet]
        public List<RoleViewModel> GetRoles()
        {
            List<RoleViewModel> roleViewModelList = new List<RoleViewModel>();

            foreach (var role in roleManager.Roles)
            {
                RoleViewModel identityRole = new RoleViewModel()
                {
                    Id = role.Id,
                    Name = role.Name
                };
                roleViewModelList.Add(identityRole);
            }

            return roleViewModelList.ToList();
        }


        // POST api/<RoleController>
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(model.Name));

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("Errors", error.Description);
                    }
                    return BadRequest(ModelState);
                }
            }
            return Ok(new Response { Status = "Success", Message = "User role created successfully!" });
        }

        // DELETE api/<RoleController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            IdentityResult result = await roleManager.DeleteAsync(role);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Errors", error.Description);
                }
                return BadRequest(ModelState);
            }

            return Ok();

        }
    }
}

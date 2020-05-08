using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Webshop.Models;

namespace Webshop.Controllers
{
    public class AdministrationController : Controller
    {
        public RoleManager<AppRole> RoleManager { get; }

        public AdministrationController(RoleManager<AppRole> roleManager )
        {
            RoleManager = roleManager;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(AppRole model)
        {
            if (ModelState.IsValid)
            {
                AppRole identityRole = new AppRole
                {
                    RoleName = model.RoleName,
                    Name = model.RoleName
                    
                };
                var result = await RoleManager.CreateAsync(identityRole);
                
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Products"); 
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

    }
}
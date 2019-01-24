using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace ApiCRM.Secutiry
{
    public class IdentityInitializer
    {
        private readonly ApiContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public IdentityInitializer(
            ApiContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            //if (_context.Database.EnsureCreated())
            //{
            if (!_roleManager.RoleExistsAsync(Roles.ROLE_API_CRM).Result)
            {
                var resultado = _roleManager.CreateAsync(
                    new IdentityRole(Roles.ROLE_API_CRM)).Result;
                if (!resultado.Succeeded)
                {
                    throw new Exception(
                        $"Erro durante a criação da role {Roles.ROLE_API_CRM}.");
                }
            }

            CreateUser(
                new ApplicationUser()
                {
                    UserName = "admin",
                    Email = "admin@teste.com.br",
                    EmailConfirmed = true,
                    Perfil = "Administrador",
                    Nome = "Administrador do Sistema"
                }, "123456", Roles.ROLE_API_CRM);

           
            // }
        }

        private void CreateUser(
            ApplicationUser user,
            string password,
            string initialRole = null)
        {
            if (_userManager.FindByNameAsync(user.UserName).Result == null)
            {
                var resultado = _userManager
                    .CreateAsync(user, password).Result;

                if (resultado.Succeeded &&
                    !String.IsNullOrWhiteSpace(initialRole))
                {
                    _userManager.AddToRoleAsync(user, initialRole).Wait();
                }
            }
        }
    }
}

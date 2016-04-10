using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using fepuseAPI.Infraestructura;
using fepuseAPI.Models;

namespace fepuseAPI.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<FepuseAPI_Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FepuseAPI_Context context)
        {
            //fpaz:Semillas para el llenado inicial de la bd

            #region Carga de ApplicationUser
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new FepuseAPI_Context()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new FepuseAPI_Context()));

            var user = new ApplicationUser()
            {
                UserName = "Administrador",
                Email = "overcode_dev@outlook.com",
                EmailConfirmed = true,
                FirstName = "Administrador",
                LastName = "Administrador",
                Level = 1,
                JoinDate = DateTime.Now.AddYears(-3),
                #region Carga de liga

                Liga = new Liga
                {
                    Nombre = "Liga Fepuse",
                    Categorias = new List<Categoria>
                {
                    new Categoria {
                        Nombre="Libre",
                        Torneos= new List<Torneo>{
                            new Torneo{
                                Nombre = "Torneo Apertura"
                            }
                        }
                    }
                }
                }
                #endregion


                //Liga = new Liga { Nombre = "Liga Fepuse" },
            };

            manager.Create(user, "qwerty123");

            if (roleManager.Roles.Count() == 0)
            {
                roleManager.Create(new IdentityRole { Name = "SuperAdmin" });
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            var adminUser = manager.FindByName("Administrador");

            manager.AddToRoles(adminUser.Id, new string[] { "SuperAdmin", "Admin" });
            #endregion

            #region Carga de Categorias

            //var categoria = new Categoria
            //{
            //    Nombre = "Categoria Libre",
            //    LigaId = liga.Id

            //};
            //context.Categorias.Add(categoria);
            #endregion



            #region semilla para la carga de torneo

            //var torneo = new Torneo
            //{
            //    Nombre = "Torneo Apertura"

            //};
            //context.Torneos.Add(torneo);

            #endregion

            base.Seed(context);
        }
    }
}

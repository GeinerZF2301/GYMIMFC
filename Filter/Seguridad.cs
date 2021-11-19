using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GYMIMFC.Filter
{
    public class Seguridad : IActionFilter
    {
        //https://code-maze.com/action-filters-aspnetcore/
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("todo Ok.");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //En LoginController
            //HttpContext.Session.SetString("usuarioId", User.UsuarioId.ToString());
            var user = context.HttpContext.Session.GetString("usuarioId");
            if (user == null)
            {
                context.Result = new RedirectResult("Login");
            }


        }
    }
}


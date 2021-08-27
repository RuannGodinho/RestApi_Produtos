using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RestApi_Produtos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi_Produtos.Filters
{
    public class ValidacaoModelStateCustomizado : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var validaCampoViewModel = new ValidaCampoViewModelOutput(context.ModelState.SelectMany(sm => sm.Value.Errors).Select(s => s.ErrorMessage));
                context.Result = new BadRequestObjectResult(validaCampoViewModel);
            }
        }
    }
}

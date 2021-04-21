using BlogProject.Business.Interfaces;
using BlogProject.Entities.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.WebApi.CustomFilters
{
    public class ValidId<T> : IActionFilter where T : class, ITable, new()
    {
        private readonly IGenericService<T> _genericService;
        public ValidId(IGenericService<T> genericService)
        {
            _genericService = genericService;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var dictionary = context.ActionArguments.Where(I => I.Key == "id").FirstOrDefault();
            var checkedId = (int)dictionary.Value;

            var entity =_genericService.FindByIdAsync(checkedId).Result;

            if (entity == null)
            {
                context.Result = new NotFoundObjectResult($"{checkedId} idli nesne bulunamadı.");
            }
        }
    }
}
//Sabırlı insanlar değiliz bizi sınamayın 

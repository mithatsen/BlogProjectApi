using BlogProject.Business.Utilities.LogTool;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly ICustomLogger _customLogger;
        public ErrorController(ICustomLogger customLogger)
        {
            _customLogger = customLogger;
        }
        [HttpGet]
        public IActionResult Error()
        {
            var errorInfo = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            _customLogger.LogError($"\n Hatanın Oluştuğu Yer : {errorInfo.Path} \n Hata Mesajı : {errorInfo.Error.Message} \n Stack Trace : {errorInfo.Error.StackTrace}");
            return Problem(detail: "There is a problem here. It will be fixed as soon as possible");
        }
    }
}

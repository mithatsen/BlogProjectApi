using BlogProject.WebApi.Enums;
using BlogProject.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        [HttpGet("[action]")]
        public async Task<UploadModel> UploadFileAsync(IFormFile file,string contentType)
        {
            UploadModel uploadModel = new UploadModel();
            if (file != null)
            {
                if (file.ContentType != contentType)
                {
                    uploadModel.ErrorMessage = "Uygunsuz dosya türü";
                    uploadModel.uploadState = UploadState.Error;
                    return (uploadModel);

                }
                else
                {
                    var newFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/" + newFileName);
                    var stream = new FileStream(path, FileMode.Create);
                    await file.CopyToAsync(stream);

                    uploadModel.uploadState = UploadState.Success;
                    uploadModel.NewName = newFileName; 
                    return uploadModel;
                }
                
               
            }
            uploadModel.ErrorMessage = "Dosya yok";
            uploadModel.uploadState = UploadState.NotExist;
            return uploadModel;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EscolaDeVoce.Backend.Controllers
{
    public class EspecialistController : BaseController
    {
        private IHostingEnvironment _environment;
        public EspecialistController(IHostingEnvironment environment)
        {
            _environment = environment;
        }
        public async Task<IActionResult> Detail(string id)
        {
            if (id != null){
                Guid especialistid = Guid.Empty;
                if(Guid.TryParse(id, out especialistid)){
                    var response = await ApiRequestHelper.Get<Infrastructure.ApiResponse<EscolaDeVoce.Services.ViewModel.EspecialistViewModel>>(Helpers.EscolaDeVoceEndpoints.Especialists.get + "/" + especialistid.ToString());
                    return View(response.data);
                }
            }
            
            return View(new EscolaDeVoce.Services.ViewModel.EspecialistViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Detail([Bind("Id,name,charge,image")]Services.ViewModel.EspecialistViewModel model, ICollection<IFormFile> files)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "images/specialists");
            if(files != null){
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        var fileextension = Path.GetExtension(file.FileName);
                        var filename = Guid.NewGuid().ToString() + fileextension;
                        model.image = filename;

                        using (var fileStream = new FileStream(Path.Combine(uploads, filename), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                    }
                }
            }
            
            Infrastructure.ApiResponse<EscolaDeVoce.Services.ViewModel.EspecialistViewModel> response = null;
            System.Net.Http.HttpMethod method = System.Net.Http.HttpMethod.Post;
            string url = Helpers.EscolaDeVoceEndpoints.Especialists.get;

            if(model.Id != Guid.Empty) {
                url += "/" + model.Id.ToString();
                method = System.Net.Http.HttpMethod.Put;
            }

            response = await ApiRequestHelper.postPutRequest<Infrastructure.ApiResponse<EscolaDeVoce.Services.ViewModel.EspecialistViewModel>>(
                url,
                method,
                JsonConvert.SerializeObject(model)
            );

            if((method == System.Net.Http.HttpMethod.Post && response.data != null && response.data.Id != Guid.Empty) || model.Id != Guid.Empty){
                Guid Id = model.Id != Guid.Empty ? model.Id : response.data.Id;
                return RedirectToAction("Detail", new {id = Id.ToString()});
            }else{
                return RedirectToAction("Index");
            }
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Infrastructure.ApiResponse<EscolaDeVoce.Services.ViewModel.EspecialistViewModel> response = null;
            System.Net.Http.HttpMethod method = System.Net.Http.HttpMethod.Delete;

            response = await ApiRequestHelper.postPutRequest<Infrastructure.ApiResponse<EscolaDeVoce.Services.ViewModel.EspecialistViewModel>>(
                Helpers.EscolaDeVoceEndpoints.Especialists.get + "/" + id.ToString(),
                method
            );

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            var response = await ApiRequestHelper.Get<Infrastructure.ApiResponse<List<EscolaDeVoce.Services.ViewModel.EspecialistViewModel>>>(Helpers.EscolaDeVoceEndpoints.Especialists.get);
            return View(response.data);
        }
    }
}


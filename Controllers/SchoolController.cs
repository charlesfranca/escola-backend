using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EscolaDeVoce.Backend.Controllers
{
    public class SchoolController : BaseController
    {
        public async Task<IActionResult> Detail(string id)
        {
            if (id != null){
                Guid categoryid = Guid.Empty;
                if(Guid.TryParse(id, out categoryid)){
                    var response = await ApiRequestHelper.Get<Infrastructure.ApiResponse<EscolaDeVoce.Services.ViewModel.CategoryViewModel>>(Helpers.EscolaDeVoceEndpoints.Category.getCategories + "/" + categoryid.ToString());
                    return View(response.data);
                }
            }
            
            return View(new EscolaDeVoce.Services.ViewModel.CategoryViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Detail([Bind("Id,name")]Services.ViewModel.CategoryViewModel model)
        {
            Infrastructure.ApiResponse<EscolaDeVoce.Services.ViewModel.CategoryViewModel> response = null;
            
            System.Net.Http.HttpMethod method = System.Net.Http.HttpMethod.Post;
            var url = Helpers.EscolaDeVoceEndpoints.Category.getCategories;
            if (model.Id != Guid.Empty) {
                method = System.Net.Http.HttpMethod.Put;
                url = Helpers.EscolaDeVoceEndpoints.Category.getCategories + "/" + model.Id.ToString();
            }
            

            response = await ApiRequestHelper.postPutRequest<Infrastructure.ApiResponse<EscolaDeVoce.Services.ViewModel.CategoryViewModel>>(
                url,
                method,
                JsonConvert.SerializeObject(model)
            );

            if(model.Id != Guid.Empty || (response.data != null && response.data.Id != Guid.Empty)){
                var Id = model.Id != Guid.Empty ? model.Id : response.data.Id;
                return RedirectToAction("Detail", new {id = Id.ToString()});
            }
            return RedirectToAction("Index");
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Infrastructure.ApiResponse<EscolaDeVoce.Services.ViewModel.CategoryViewModel> response = null;
            System.Net.Http.HttpMethod method = System.Net.Http.HttpMethod.Delete;

            response = await ApiRequestHelper.postPutRequest<Infrastructure.ApiResponse<EscolaDeVoce.Services.ViewModel.CategoryViewModel>>(
                Helpers.EscolaDeVoceEndpoints.Category.getCategories + "/" + id.ToString(),
                method
            ); 

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            var response = await ApiRequestHelper.Get<Infrastructure.ApiResponse<List<EscolaDeVoce.Services.ViewModel.CategoryViewModel>>>(Helpers.EscolaDeVoceEndpoints.Category.getCategories);
            return View(response.data);
        }
    }
}
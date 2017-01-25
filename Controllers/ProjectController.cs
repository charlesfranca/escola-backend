using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EscolaDeVoce.Backend.Controllers
{
    public class ProjectController : BaseController
    {
        public async Task<IActionResult> Detail(string id)
        {
            Guid categoryid = Guid.Empty;
            if(Guid.TryParse(id, out categoryid)){
                var response = await ApiRequestHelper.Get<Infrastructure.ApiResponse<EscolaDeVoce.Services.ViewModel.ProjectViewModel>>(Helpers.EscolaDeVoceEndpoints.Project.getProjects + "/" + categoryid.ToString());
                return View(response.data);
            }
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Detail([Bind("Id,name")]Services.ViewModel.ProjectViewModel model)
        {
            Infrastructure.ApiResponse<EscolaDeVoce.Services.ViewModel.ProjectViewModel> response = null;
            
            System.Net.Http.HttpMethod method = System.Net.Http.HttpMethod.Post;
            if (model.Id != Guid.Empty) method = System.Net.Http.HttpMethod.Put;

            response = await ApiRequestHelper.postPutRequest<Infrastructure.ApiResponse<EscolaDeVoce.Services.ViewModel.ProjectViewModel>>(
                Helpers.EscolaDeVoceEndpoints.Project.getProjects + "/" + model.Id.ToString(),
                method,
                JsonConvert.SerializeObject(model)
            );
            return View(response.data);
        }

        public async Task<IActionResult> Index()
        {
            var response = await ApiRequestHelper.Get<Infrastructure.ApiResponse<List<EscolaDeVoce.Services.ViewModel.ProjectViewModel>>>(Helpers.EscolaDeVoceEndpoints.Project.getProjects);
            return View(response.data);
        }
    }
}
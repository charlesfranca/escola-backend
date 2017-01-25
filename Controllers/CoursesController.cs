using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace EscolaDeVoce.Backend.Controllers
{
    public class CoursesController : BaseController
    {
        public async Task<IActionResult> Detail(string id)
        {
            Guid courseid = Guid.Empty;
            if(Guid.TryParse(id, out courseid)){
                var response = await ApiRequestHelper.Get<Infrastructure.ApiResponse<EscolaDeVoce.Services.ViewModel.CourseViewModel>>(Helpers.EscolaDeVoceEndpoints.Courses.getCourses + "/" + courseid.ToString());
                return View(response.data);
            }
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Detail([Bind("Id,name")]Services.ViewModel.CourseViewModel model)
        {
            Infrastructure.ApiResponse<EscolaDeVoce.Services.ViewModel.CourseViewModel> response = null;
            
            System.Net.Http.HttpMethod method = System.Net.Http.HttpMethod.Post;
            if (model.Id != Guid.Empty) method = System.Net.Http.HttpMethod.Put;

            response = await ApiRequestHelper.postPutRequest<Infrastructure.ApiResponse<EscolaDeVoce.Services.ViewModel.CourseViewModel>>(
                Helpers.EscolaDeVoceEndpoints.Courses.getCourses + "/" + model.Id.ToString(),
                method,
                JsonConvert.SerializeObject(model)
            );
            return View(response.data);
        }

        public async Task<IActionResult> Index()
        {
            var response = await ApiRequestHelper.Get<Infrastructure.ApiResponse<List<EscolaDeVoce.Services.ViewModel.CourseViewModel>>>(Helpers.EscolaDeVoceEndpoints.Courses.getCourses);

            return View(response.data);
        }

        public async Task<IActionResult> VideoUpload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveMedia(string mediaId, string courseId)
        {
            var sambatechresponse = await ApiRequestHelper.Get<EscolaDeVoce.Backend.ViewModel.VideoSambatech>("http://api.sambavideos.sambatech.com/v1/medias/"+ mediaId +"?access_token=181e463a-034b-4ea5-878b-cea906a5f2e2&pid=6023");

            EscolaDeVoce.Services.ViewModel.VideoViewModel response = null;
            var model = new EscolaDeVoce.Services.ViewModel.VideoViewModel();
            model = new EscolaDeVoce.Services.ViewModel.VideoViewModel();
            model.thumbs = new List<EscolaDeVoce.Services.ViewModel.ThumbViewModel>();
            model.files = new List<EscolaDeVoce.Services.ViewModel.FileViewModel>();
            model.sambatech_id = mediaId;
            model.courseId = Guid.Parse(courseId);
            model.name = sambatechresponse.title;
            model.views = 0;

            foreach(var v in sambatechresponse.files){
                EscolaDeVoce.Services.ViewModel.FileViewModel fl = new EscolaDeVoce.Services.ViewModel.FileViewModel();
                fl.fileName = v.fileName;
                fl.url = v.url;
                fl.id = v.id;
                model.files.Add(fl);
            }

            foreach(var th in sambatechresponse.thumbs){
                EscolaDeVoce.Services.ViewModel.ThumbViewModel thumb = new EscolaDeVoce.Services.ViewModel.ThumbViewModel();
                thumb.height = th.height;
                thumb.width = th.width;
                thumb.url = th.url;
                model.thumbs.Add(thumb);
            }

            System.Net.Http.HttpMethod method = System.Net.Http.HttpMethod.Post;

            response = await ApiRequestHelper.postPutRequest<EscolaDeVoce.Services.ViewModel.VideoViewModel>(
                Helpers.EscolaDeVoceEndpoints.Videos.video,
                method,
                JsonConvert.SerializeObject(model)
            );

            return View();
        }
    }
}
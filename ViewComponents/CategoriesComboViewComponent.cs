using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EscolaDeVoce.Backend.ViewComponents
{
    public class CategoriesComboViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var response = await ApiRequestHelper.Get<Infrastructure.ApiResponse<List<EscolaDeVoce.Services.ViewModel.CategoryViewModel>>>(Helpers.EscolaDeVoceEndpoints.Category.getCategories);
                return View(response.data);
            }
            catch (System.Exception)
            {
            }

            return View(new List<EscolaDeVoce.Services.ViewModel.CategoryViewModel>());
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Infraestructure.Data;

namespace WebApp.Areas.Cliente.Pages
{
    public class CreateModel : PageModel
    {
        private readonly MyRepository<ApplicationCore.Entities.Cliente> _repository;

        private INotyfService _notyfService { get; }

        public CreateModel(MyRepository<ApplicationCore.Entities.Cliente> repository, INotyfService notyfService)
        {
            _repository = repository;
            _notyfService = notyfService;
        }
        [BindProperty]
        public ApplicationCore.Entities.Cliente Cliente { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _repository.AddAsync(Cliente);
                    _notyfService.Success("Cliente se ha agregado exitosamente");
                }
                else
                {
                    _notyfService.Warning("Su formulario no cumple las reglas de negocio");
                    return Page();
                }
                return RedirectToPage("Index");
            }
            catch (Exception ex)
            {

                _notyfService.Error("Ocurrio un error en el servidor, intente nuevamente");
                return RedirectToPage("Index");
            }
        }
    }
}

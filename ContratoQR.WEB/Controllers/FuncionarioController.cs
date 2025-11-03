using ContratoQR.Entity;
using ContratoQR.WEB.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing;

namespace ContratoQR.WEB.Controllers
{
    public class FuncionarioController : Controller
    {
        private readonly IConfiguration _configuration;

        public FuncionarioController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            PersonalViewModel personalViewModel = new();
            BLL.PersonalQR personalQR = new();

            personalViewModel.ListaPersonal = personalQR.Listar(string.Empty, string.Empty, _configuration);

            return View(personalViewModel);
        }

        [HttpPost]
        public IActionResult GetFuncionario(string rutFuncionario, string nombreFuncionario)
        {
            PersonalViewModel personalViewModel = new();
            BLL.PersonalQR personalQR = new();

            rutFuncionario = rutFuncionario ?? string.Empty;
            nombreFuncionario = nombreFuncionario ?? string.Empty;

            personalViewModel.ListaPersonal = personalQR.Listar(rutFuncionario, nombreFuncionario, _configuration);

            return View("Index", personalViewModel);
        }

        public IActionResult Create()
        {
            PersonalViewModel personalViewModel = new();
            BLL.PersonalQR personalQR = new();

            return View(personalViewModel);
        }

        [HttpPost]
        public IActionResult Create(IFormCollection formulario)
        {
            BLL.PersonalQR personalQR = new();
            PersonalViewModel personalViewModel = new();
            PersonalQREntity personalQREntity = new();

            personalQREntity.RutFuncionario = formulario["RutFuncionario"].ToString().ToUpper();
            personalQREntity.NombreFuncionario = formulario["NombreFuncionario"].ToString().ToUpper();
            personalQREntity.UrlContrato = formulario["UrlContrato"];
            personalQREntity.IndEstado = 1;

            personalQR.Insertar(personalQREntity, _configuration);

            personalViewModel.ListaPersonal = personalQR.Listar(string.Empty, string.Empty, _configuration);

            return View("Index", personalViewModel);
        }

        public IActionResult Edit(string rutPersonal)
        {
            PersonalViewModel personalViewModel = new();
            BLL.PersonalQR personalQR = new();

            personalViewModel.ListaPersonal = personalQR.Listar(rutPersonal, string.Empty, _configuration);

            if(personalViewModel.ListaPersonal.Count == 0)
            {
                return RedirectToAction("Index");
            }

            personalViewModel.RutFuncionario = personalViewModel.ListaPersonal[0].RutFuncionario;
            personalViewModel.NombreFuncionario = personalViewModel.ListaPersonal[0].NombreFuncionario;
            personalViewModel.UrlContrato = personalViewModel.ListaPersonal[0].UrlContrato;
            personalViewModel.IdPersonalQR = personalViewModel.ListaPersonal[0].IdPersonalQR;

            return View(personalViewModel);
        }

        [HttpPost]
        public IActionResult Edit(IFormCollection formulario)
        {
            BLL.PersonalQR personalQR = new();
            PersonalQREntity personalQREntity = new();
            PersonalViewModel personalViewModel = new();

            personalQREntity.IdPersonalQR = Convert.ToInt32(formulario["IdPersonalQR"]);
            personalQREntity.RutFuncionario = formulario["RutFuncionario"].ToString().ToUpper();
            personalQREntity.NombreFuncionario = formulario["NombreFuncionario"].ToString().ToUpper();
            personalQREntity.UrlContrato = formulario["UrlContrato"];
            personalQREntity.IndEstado = 1;

            personalViewModel.ListaPersonal = personalQR.Listar(string.Empty, string.Empty, _configuration);

            personalQR.Actualizar(personalQREntity, _configuration);

            return View("Index", personalViewModel);
        }
    }
}

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
            BLL.Personal personal = new();

            try
            {
                personalViewModel.ListaPersonal = personal.Listar(string.Empty, string.Empty, _configuration);
            }
            catch (Exception ex)
            {
                return PartialView("Mensajeria", new MensajeriaViewModel { IsError = true, Mensaje = ex.Message, Url = "/Funcionario" });
            }

            return View(personalViewModel);
        }

        [HttpPost]
        public IActionResult GetFuncionario(string rutFuncionario, string nombreFuncionario)
        {
            PersonalViewModel personalViewModel = new();
            BLL.Personal personal = new();

            try
            {
                rutFuncionario = rutFuncionario ?? string.Empty;
                nombreFuncionario = nombreFuncionario ?? string.Empty;

                personalViewModel.ListaPersonal = personal.Listar(rutFuncionario, nombreFuncionario, _configuration);
            }
            catch (Exception ex)
            {
                return PartialView("Mensajeria", new MensajeriaViewModel { IsError = true, Mensaje = ex.Message, Url = "/Funcionario" });
            }

            return View("Index", personalViewModel);
        }

        public IActionResult Create()
        {
            PersonalViewModel personalViewModel = new();
            BLL.Personal personal = new();

            return View(personalViewModel);
        }

        [HttpPost]
        public IActionResult Create(IFormCollection formulario)
        {
            BLL.Personal personal = new();
            PersonalViewModel personalViewModel = new();
            PersonalEntity personalQREntity = new();

            try
            {
                //personalQREntity.RutFuncionario = formulario["RutFuncionario"].ToString().ToUpper();
                //personalQREntity.NombreFuncionario = formulario["NombreFuncionario"].ToString().ToUpper();
                //personalQREntity.UrlContrato = formulario["UrlContrato"];
                //personalQREntity.IndEstado = 1;

                //personal.Insertar(personalQREntity, _configuration);

                personalViewModel.ListaPersonal = personal.Listar(string.Empty, string.Empty, _configuration);
            }
            catch (Exception ex)
            {
                return PartialView("Mensajeria", new MensajeriaViewModel { IsError = true, Mensaje = ex.Message, Url = "/Funcionario" });
            }

            return PartialView("Mensajeria", new MensajeriaViewModel { IsError = false, Mensaje = "Registro creado!!!", Url = "/Funcionario" });
        }

        public IActionResult Edit(string rutPersonal)
        {
            PersonalViewModel personalViewModel = new();
            BLL.Personal personal = new();

            try
            {
                personalViewModel.ListaPersonal = personal.Listar(rutPersonal, string.Empty, _configuration);

                if (personalViewModel.ListaPersonal.Count == 0)
                {
                    return RedirectToAction("Index");
                }

                //personalViewModel.RutFuncionario = personalViewModel.ListaPersonal[0].RutFuncionario;
                //personalViewModel.NombreFuncionario = personalViewModel.ListaPersonal[0].NombreFuncionario;
                //personalViewModel.UrlContrato = personalViewModel.ListaPersonal[0].UrlContrato;
                //personalViewModel.IdPersonalQR = personalViewModel.ListaPersonal[0].IdPersonalQR;
            }
            catch (Exception ex)
            {
                return PartialView("Mensajeria", new MensajeriaViewModel { IsError = true, Mensaje = ex.Message, Url = "/Funcionario" });
            }

            return View(personalViewModel);

        }

        [HttpPost]
        public IActionResult Edit(IFormCollection formulario)
        {
            BLL.Personal personal = new();
            PersonalEntity personalQREntity = new();
            PersonalViewModel personalViewModel = new();

            try
            {
                //personalQREntity.IdPersonalQR = Convert.ToInt32(formulario["IdPersonalQR"]);
                //personalQREntity.RutFuncionario = formulario["RutFuncionario"].ToString().ToUpper();
                //personalQREntity.NombreFuncionario = formulario["NombreFuncionario"].ToString().ToUpper();
                //personalQREntity.UrlContrato = formulario["UrlContrato"];
                //personalQREntity.IndEstado = 1;

                personalViewModel.ListaPersonal = personal.Listar(string.Empty, string.Empty, _configuration);

                //personal.Actualizar(personalQREntity, _configuration);
            }
            catch (Exception ex)
            {
                return PartialView("Mensajeria", new MensajeriaViewModel { IsError = true, Mensaje = ex.Message, Url = "/Funcionario" });
            }

            return PartialView("Mensajeria", new MensajeriaViewModel { IsError = false, Mensaje = "Funcionario actualizado!!!", Url = "/Funcionario" });

        }
    }
}

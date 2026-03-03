using ContratoQR.Entity;
using ContratoQR.WEB.Models;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace ContratoQR.WEB.Controllers
{
    public class CertificadoAntiguedadController : Controller
    {
        private readonly IConfiguration _configuration;

        public CertificadoAntiguedadController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            FileExcelViewModel fileExcelModel = new FileExcelViewModel();

            return View(fileExcelModel);
        }

        [HttpPost]
        public IActionResult GetPersonal(string rutPersonal, string nombrePersonal)
        {
            FileExcelViewModel personalViewModel = new();
            BLL.Personal personal = new();

            try
            {
                rutPersonal = rutPersonal ?? string.Empty;
                nombrePersonal = nombrePersonal ?? string.Empty;

                personalViewModel.ListaPersonal = personal.Listar(rutPersonal, nombrePersonal, _configuration);
            }
            catch (Exception ex)
            {
                return PartialView("Mensajeria", new MensajeriaViewModel { IsError = true, Mensaje = ex.Message, Url = "/Funcionario" });
            }

            return View("Index", personalViewModel);
        }

        public IActionResult ListarPersonal(string rutPersonal)
        {
            PersonalViewModel personalViewModel = new();
            BLL.Personal personal = new();
            try
            {
                personalViewModel.personalEntity = personal.Listar(rutPersonal, _configuration);
            }
            catch (Exception ex)
            {
                return PartialView("Mensajeria", new MensajeriaViewModel { IsError = true, Mensaje = ex.Message, Url = "/Funcionario" });
            }
            return PartialView("_GenerarCertificado", personalViewModel);
        }

        public IActionResult Generar(PersonalViewModel personalViewModel)
        {
            BLL.Personal personal = new();
            string pathDir = string.Empty;
            string pathCertificadoGenerado = string.Empty;
            PersonalEntity personalEntity = personal.Listar(personalViewModel.personalEntity.RutPersonal!, _configuration);

            pathDir = Path.Combine("wwwroot", @"Uploads\Certificado"); // Asegúrate que esta carpeta existe

            if (Directory.Exists(pathDir))
            {
                pathCertificadoGenerado = Path.Combine("wwwroot", @"Uploads\Certificado\" + personalEntity.RutPersonal);

                if (Directory.Exists(pathCertificadoGenerado))
                {
                    Directory.Delete(pathCertificadoGenerado, true);
                    Directory.CreateDirectory(pathCertificadoGenerado);
                }
                else
                {
                    Directory.CreateDirectory(pathCertificadoGenerado);
                }
                
                string nombreArchivo = Path.Combine(pathCertificadoGenerado, $"CertificadoAntiguedad_{personalEntity.RutPersonal}.docx");

                System.IO.File.Copy(Path.Combine("wwwroot", @"DocCertificado\CertificadoAntiguedad.docx"), nombreArchivo, true);

                using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(nombreArchivo, true))
                {
                    string? strDocTexto = null;

                    using (StreamReader srArchivo = new StreamReader(wordDoc.MainDocumentPart!.GetStream()))
                    {
                        strDocTexto = srArchivo.ReadToEnd();
                    }

                    Regex regexTexto = new Regex("NOMBRE_PERSONAL");
                    strDocTexto = regexTexto.Replace(strDocTexto, personalEntity.NombreCompleto!);

                    regexTexto = new Regex("RUT_PERSONAL");
                    strDocTexto = regexTexto.Replace(strDocTexto, personalEntity.RutPersonal!);

                    regexTexto = new Regex("ESTABLECIMIENTO");
                    strDocTexto = regexTexto.Replace(strDocTexto, personalEntity.NombreEstablecimiento!);

                    regexTexto = new Regex("CARGO");
                    strDocTexto = regexTexto.Replace(strDocTexto, personalEntity.NombreCargo!);

                    regexTexto = new Regex("NOMBRE_CATEGORIA");
                    strDocTexto = regexTexto.Replace(strDocTexto, personalEntity.Categoria!);

                    regexTexto = new Regex("NOMBRE_NIVEL");
                    strDocTexto = regexTexto.Replace(strDocTexto, personalEntity.Nivel.ToString()!);

                    regexTexto = new Regex("FEC_INICIO_CONTRATO");
                    strDocTexto = regexTexto.Replace(strDocTexto, personalEntity.FecInicioContrato.ToString("dd/MM/yyyy")!);

                    regexTexto = new Regex("TIPO_CONTRATO");
                    strDocTexto = regexTexto.Replace(strDocTexto, personalEntity.NombreTipoContrato!);

                    regexTexto = new Regex("NRO_HORA");
                    strDocTexto = regexTexto.Replace(strDocTexto, personalEntity.NroHora.ToString());

                    regexTexto = new Regex("FEC_GENERAR_CERTIFICADO");
                    strDocTexto = regexTexto.Replace(strDocTexto, DateTime.Now.ToString("dd 'de' MMMM 'de' yyyy")!);

                    using (StreamWriter srGrabarArchivo = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
                    {
                        srGrabarArchivo.Write(strDocTexto);
                    }
                }
            }
            else
            {
                return PartialView("Mensajeria", new MensajeriaViewModel { IsError = true, Mensaje = "Error al leer Certificado Original", Url = "/CertificadoAntiguedad" });
            }


            return PartialView("Mensajeria", new MensajeriaViewModel { IsError = false, Mensaje = "Se ha generado el certificado!!!!", Url = "/CertificadoAntiguedad" });
        }
    }
}

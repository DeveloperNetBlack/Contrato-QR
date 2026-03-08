using ContratoQR.Entity;
using ContratoQR.WEB.Helpers;
using ContratoQR.WEB.Models;
using DocumentFormat.OpenXml.Packaging;
using iText.Forms;
using iText.Kernel.Pdf;
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

                foreach (var item in personalViewModel.ListaPersonal)
                {
                    item.RutPersonal = GeneralRoutine.FormatearRut(item.RutPersonal!);
                }
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
                personalViewModel.personalEntity = personal.Listar(rutPersonal.Replace(".", "").Replace("-", ""), _configuration);

                personalViewModel.personalEntity.RutPersonal = GeneralRoutine.FormatearRut(personalViewModel.personalEntity.RutPersonal!);
                personalViewModel.personalEntity.CorreoElectronico = string.IsNullOrEmpty(personalViewModel.personalEntity.CorreoElectronico) ? string.Empty : personalViewModel.personalEntity.CorreoElectronico;
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
            PersonalEntity personalEntity = personal.Listar(personalViewModel.personalEntity.RutPersonal!.Replace(".", "").Replace("-", ""), _configuration);
            var emailService = new EmailService();

            pathDir = Path.Combine("wwwroot", @"Certificado\Personal"); // Asegúrate que esta carpeta existe

            if (Directory.Exists(pathDir))
            {
                pathCertificadoGenerado = Path.Combine("wwwroot", @"Certificado\Personal\" + personalEntity.RutPersonal);

                if (Directory.Exists(pathCertificadoGenerado))
                {
                    Directory.Delete(pathCertificadoGenerado, true);
                    Directory.CreateDirectory(pathCertificadoGenerado);
                }
                else
                {
                    Directory.CreateDirectory(pathCertificadoGenerado);
                }

                string outputArchivoPDF = Path.Combine(pathCertificadoGenerado, $"CertificadoAntiguedad_{personalEntity.RutPersonal}.pdf");
                string inputArchivoPDF = Path.Combine("wwwroot", @"DocCertificado\CertificadoAntiguedad.pdf");
                string asunto = "Certificado de Antiguedad";
                string cuerpo = "<p>Adjunto encontrarás el certificado solicitado.</p>";

                FillFormFields(inputArchivoPDF, outputArchivoPDF, personalEntity);

                if(personalViewModel.personalEntity.CorreoElectronico == null)
                {
                    return PartialView("Mensajeria", new MensajeriaViewModel { IsError = true, Mensaje = "No se ha registrado un correo electrónico para este personal", Url = "/CertificadoAntiguedad" });
                }

                emailService.EnviarCorreo(personalViewModel.personalEntity.CorreoElectronico!, asunto, cuerpo, outputArchivoPDF);

                personalEntity.CorreoElectronico = personalViewModel.personalEntity.CorreoElectronico;
                personalEntity.IdUsuario = "ADMIN";

                personal.Actualizar(personalEntity, _configuration);
            }
            else
            {
                return PartialView("Mensajeria", new MensajeriaViewModel { IsError = true, Mensaje = "Error al leer Certificado Original", Url = "/CertificadoAntiguedad" });
            }


            return PartialView("Mensajeria", new MensajeriaViewModel { IsError = false, Mensaje = "Se ha generado el certificado!!!!", Url = "/CertificadoAntiguedad" });
        }

        public void FillFormFields(string inputPdf, string outputPdf, PersonalEntity personalEntity)
        {
            using var reader = new PdfReader(inputPdf);
            using var writer = new PdfWriter(outputPdf);
            using var pdfDoc = new PdfDocument(reader, writer);

            // Obtener el formulario
            PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDoc, true);

            // Modificar campos
            form.GetField("txtNombrePersonal").SetValue(personalEntity.NombreCompleto);
            form.GetField("txtRutPersonal").SetValue(GeneralRoutine.FormatearRut(personalEntity.RutPersonal!));
            form.GetField("txtEstablecimiento").SetValue(personalEntity.NombreEstablecimiento);
            form.GetField("txtCargo").SetValue(personalEntity.NombreCargo);
            form.GetField("txtNombreCategoria").SetValue(personalEntity.Categoria);
            form.GetField("txtNombreNivel").SetValue(personalEntity.Nivel.ToString());
            form.GetField("txtFecInicioContrato").SetValue(personalEntity.FecInicioContrato.ToString("dd/MM/yyyy")!);
            form.GetField("txtTipoContrato").SetValue(personalEntity.NombreTipoContrato);
            form.GetField("txtNroHora").SetValue(personalEntity.NroHora.ToString());
            form.GetField("txtFecGenerarCertificado").SetValue(DateTime.Now.ToString("dd 'de' MMMM 'de' yyyy")!);

            // Opcional: aplanar (flatten) para evitar edición posterior
            form.FlattenFields();

            pdfDoc.Close();
        }

    }
}

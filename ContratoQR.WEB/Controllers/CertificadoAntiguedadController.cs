using ContratoQR.Entity;
using ContratoQR.WEB.Helpers;
using ContratoQR.WEB.Models;
using iText.Forms;
using iText.Kernel.Pdf;
using Microsoft.AspNetCore.Mvc;

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

            fileExcelModel.ListaTipoFuncionario.Add(new TipoFuncionarioEntity
            {
                IdTipoFuncionario = 1,
                Descripcion = "Planta/Contrata"
            }
            );

            fileExcelModel.ListaTipoFuncionario.Add(new TipoFuncionarioEntity
            {
                IdTipoFuncionario = 2,
                Descripcion = "Honorario"
            }
            );

            return View(fileExcelModel);
        }

        [HttpPost]
        public IActionResult GetPersonal(string rutPersonal, string nombrePersonal, int ddlTipoFuncionario)
        {
            FileExcelViewModel personalViewModel = new();
            BLL.Personal personal = new();
            BLL.PersonalHonorario personalHonorario = new BLL.PersonalHonorario();

            personalViewModel.ListaTipoFuncionario.Add(new TipoFuncionarioEntity
            {
                IdTipoFuncionario = 1,
                Descripcion = "Planta/Contrata"
            }
);

            personalViewModel.ListaTipoFuncionario.Add(new TipoFuncionarioEntity
            {
                IdTipoFuncionario = 2,
                Descripcion = "Honorario"
            }
            );

            if (ddlTipoFuncionario == 0)
            {
                return PartialView("Mensajeria", new MensajeriaViewModel { IsError = true, Mensaje = "Debe seleccionar un Tipo Funcionario", Url = "/CertificadoAntiguedad" });
            }

            try
            {
                rutPersonal = rutPersonal == null ? string.Empty : rutPersonal.Replace(".", "").PadLeft(12, '0');
                nombrePersonal = nombrePersonal ?? string.Empty;

                if (ddlTipoFuncionario == 1)
                {
                    var personalEntities = personal.Listar(rutPersonal, nombrePersonal, _configuration);
                    foreach (var item in personalEntities)
                    {
                        personalViewModel.ListaPersonal.Add(new PersonalEntity
                        {
                            RutPersonal = GeneralRoutine.FormatearRut(item.RutPersonal!),
                            NombreCargo = item.NombreCargo,
                            NombrePersonal = item.NombreCompleto,
                            ApellidoPersonal = item.ApellidoPersonal,
                            NombreEstablecimiento = item.NombreEstablecimiento,
                            NombreTipoContrato = item.NombreTipoContrato,
                            FecInicioContrato = item.FecInicioContrato,
                            NroHora = item.NroHora,
                            Categoria = item.Categoria,
                            Nivel = item.Nivel,
                            CorreoElectronico = item.CorreoElectronico,
                            IdTipoFuncionario = 1,
                            IdPersonal = item.IdPersonal
                        });
                    }
                }
                else
                {
                    var personalEntities = personalHonorario.Listar(rutPersonal, nombrePersonal, _configuration);
                    foreach (var item in personalEntities)
                    {
                        personalViewModel.ListaPersonal.Add(new PersonalEntity
                        {
                            RutPersonal = GeneralRoutine.FormatearRut(item.RutPersonal!),
                            NombreCargo = item.NombreCargo,
                            NombrePersonal = item.NombrePersonal,
                            NombreEstablecimiento = item.NombreEstablecimiento,
                            FecInicioContrato = Convert.ToString(item.FecInicioContrato),
                            CorreoElectronico = item.CorreoElectronico,
                            IdTipoFuncionario = 2,
                            IdPersonalHonorario = item.IdPersonalHonorario,
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return PartialView("Mensajeria", new MensajeriaViewModel { IsError = true, Mensaje = ex.Message, Url = "/CertificadoAntiguedad" });
            }

            return View("Index", personalViewModel);
        }

        public IActionResult ListarPersonal(string rutPersonal, int idTipoFuncionario)
        {
            PersonalViewModel personalViewModel = new();
            BLL.Personal personal = new();
            BLL.PersonalHonorario personalHonorario = new();

            try
            {
                if (idTipoFuncionario == 1)
                {
                    personalViewModel.personalEntity = personal.Listar(rutPersonal.Replace(".", "").PadLeft(12, '0'), _configuration);
                    personalViewModel.RutPersonal = GeneralRoutine.FormatearRut(personalViewModel.personalEntity.RutPersonal!);
                    personalViewModel.NombrePersonal = personalViewModel.personalEntity.NombreCompleto;
                    personalViewModel.FecInicioContrato = personalViewModel.personalEntity.FecInicioContrato;
                    personalViewModel.NombreCargo = personalViewModel.personalEntity.NombreCargo;
                    personalViewModel.NroHora = personalViewModel.personalEntity.NroHora;
                    personalViewModel.Categoria = personalViewModel.personalEntity.Categoria;
                    personalViewModel.Nivel = personalViewModel.personalEntity.Nivel;
                    personalViewModel.NombreEstablecimiento = personalViewModel.personalEntity.NombreEstablecimiento;
                    personalViewModel.IdTipoContrato = personalViewModel.personalEntity.IdTipoContrato;
                    personalViewModel.NombreTipoContrato = personalViewModel.personalEntity.NombreTipoContrato;
                    personalViewModel.IdTipoFuncionario = 1;
                    personalViewModel.IdPersonal = personalViewModel.personalEntity.IdPersonal;
                    personalViewModel.CorreoElectronico = string.IsNullOrEmpty(personalViewModel.CorreoElectronico) ? string.Empty : personalViewModel.CorreoElectronico;
                }
                else
                {
                    personalViewModel.personalHonorarioEntity = personalHonorario.Listar(rutPersonal.Replace(".", "").PadLeft(12, '0'), _configuration);
                    personalViewModel.IdTipoFuncionario = 2;
                    personalViewModel.RutPersonal = GeneralRoutine.FormatearRut(personalViewModel.personalHonorarioEntity.RutPersonal!);
                    personalViewModel.FecInicioContrato = string.IsNullOrEmpty(personalViewModel.personalHonorarioEntity.FecIngreso.ToString()) ? string.Empty : personalViewModel.personalHonorarioEntity.FecIngreso.ToString("dd/MM/yyyy");
                    personalViewModel.CorreoElectronico = string.IsNullOrEmpty(personalViewModel.personalHonorarioEntity.CorreoElectronico) ? string.Empty : personalViewModel.personalHonorarioEntity.CorreoElectronico;
                    personalViewModel.NombrePersonal = string.IsNullOrEmpty(personalViewModel.personalHonorarioEntity.NombrePersonal) ? string.Empty : personalViewModel.personalHonorarioEntity.NombrePersonal;
                    personalViewModel.NombreCargo = string.IsNullOrEmpty(personalViewModel.personalHonorarioEntity.NombreCargo) ? string.Empty : personalViewModel.personalHonorarioEntity.NombreCargo;
                    personalViewModel.NombreEstablecimiento = string.IsNullOrEmpty(personalViewModel.personalHonorarioEntity.NombreEstablecimiento) ? string.Empty : personalViewModel.personalHonorarioEntity.NombreEstablecimiento;
                    personalViewModel.Cometido = personalViewModel.personalHonorarioEntity.Cometido;
                    personalViewModel.NombreTipoContrato = "HONORARIOS";
                    personalViewModel.IdPersonalHonorario = personalViewModel.personalHonorarioEntity.IdPersonalHonorario;
                }

                personalViewModel.IdTipoFuncionario = idTipoFuncionario;
                
            }
            catch (Exception ex)
            {
                return PartialView("Mensajeria", new MensajeriaViewModel { IsError = true, Mensaje = ex.Message, Url = "/CertificadoAntiguedad" });
            }
            return PartialView("_GenerarCertificado", personalViewModel);
        }

        public IActionResult Generar(IFormCollection formulario)
        {
            BLL.Personal personal = new();
            BLL.PersonalHonorario personalHonorario = new();
            string pathDir = string.Empty;
            string pathCertificadoGenerado = string.Empty;
            var emailService = new EmailService();
            PersonalViewModel personalViewModel = new PersonalViewModel();

            pathDir = Path.Combine("wwwroot", @"Certificado\Personal"); // Asegúrate que esta carpeta existe

            if (formulario["IdTipoFuncionario"] == "1")
            {
                personalViewModel.RutPersonal = formulario["RutPersonal"];
                personalViewModel.NombrePersonal = formulario["NombreCompleto"];
                personalViewModel.NombreCargo = formulario["NombreCargo"];
                personalViewModel.NombreTipoContrato = formulario["NombreTipoContrato"];
                personalViewModel.FecInicioContrato = formulario["FecInicioContrato"];
                personalViewModel.NroHora = Convert.ToInt32(formulario["NroHora"]);
                personalViewModel.Categoria = formulario["Categoria"];
                personalViewModel.Nivel = Convert.ToInt32(formulario["Nivel"]);
                personalViewModel.NombreEstablecimiento = formulario["NombreEstablecimiento"];
                personalViewModel.CorreoElectronico = formulario["CorreoElectronico"];
                personalViewModel.IdPersonal = Convert.ToInt32(formulario["IdPersonal"]);
                personalViewModel.IdTipoFuncionario = 1;
            }
            else
            {
                personalViewModel.RutPersonal = formulario["RutPersonal"];
                personalViewModel.NombreCargo = formulario["NombreCargo"];
                personalViewModel.NombrePersonal = formulario["NombreCompleto"];
                personalViewModel.FecInicioContrato = formulario["FecInicioContrato"];
                personalViewModel.NombreEstablecimiento = formulario["NombreEstablecimiento"];
                personalViewModel.CorreoElectronico = formulario["CorreoElectronico"];
                personalViewModel.Cometido = formulario["Cometido"];
                personalViewModel.IdPersonalHonorario = Convert.ToInt32(formulario["IdPersonalHonorario"]);
                personalViewModel.IdTipoFuncionario = 2;
            }


            if (Directory.Exists(pathDir))
            {
                pathCertificadoGenerado = Path.Combine("wwwroot", @"Certificado\Personal\" + personalViewModel.RutPersonal!.Replace(".", "").Replace("-",""));

                if (Directory.Exists(pathCertificadoGenerado))
                {
                    Directory.Delete(pathCertificadoGenerado, true);
                    Directory.CreateDirectory(pathCertificadoGenerado);
                }
                else
                {
                    Directory.CreateDirectory(pathCertificadoGenerado);
                }

                string outputArchivoPDF = string.Empty;
                string inputArchivoPDF = string.Empty;

                if (personalViewModel.IdTipoFuncionario == 1)
                {
                    outputArchivoPDF = Path.Combine(pathCertificadoGenerado, $"CertificadoAntiguedad_{personalViewModel.RutPersonal!.Replace(".", "").Replace("-", "")}.pdf");
                    inputArchivoPDF = Path.Combine("wwwroot", @"DocCertificado\CertificadoAntiguedad.pdf");
                }
                else
                {
                    outputArchivoPDF = Path.Combine(pathCertificadoGenerado, $"CertificadoAntiguedadHonorario_{personalViewModel.RutPersonal!.Replace(".", "").Replace("-", "")}.pdf");
                    inputArchivoPDF = Path.Combine("wwwroot", @"DocCertificado\CertificadoAntiguedadHonorario.pdf");
                }

                string asunto = "Certificado de Antiguedad";
                string cuerpo = "<p>Adjunto encontrarás el certificado solicitado.</p>";

                FillFormFields(inputArchivoPDF, outputArchivoPDF, personalViewModel);

                if (personalViewModel.CorreoElectronico == null)
                {
                    return PartialView("Mensajeria", new MensajeriaViewModel { IsError = true, Mensaje = "No se ha registrado un correo electrónico para este personal", Url = "/CertificadoAntiguedad" });
                }

                emailService.EnviarCorreo(personalViewModel.CorreoElectronico!, asunto, cuerpo, outputArchivoPDF);

                if (personalViewModel.IdTipoFuncionario == 1)
                {
                    
                    BLL.Personal listadoPersonal = new BLL.Personal();

                    var personalEntity = listadoPersonal.Listar(personalViewModel.RutPersonal!.Replace(".", "").PadLeft(12, '0'), _configuration);
                    
                    personalEntity.CorreoElectronico = personalViewModel.CorreoElectronico;
                    personalEntity.IdUsuario = "ADMIN";

                    personal.Actualizar(personalEntity, _configuration);
                }
                else
                {
                    BLL.PersonalHonorario listadoPersonalHonorario = new BLL.PersonalHonorario();

                    var personalEntityHonorario = listadoPersonalHonorario.Listar(personalViewModel.RutPersonal!.Replace(".", "").PadLeft(12, '0'), _configuration);

                    personalEntityHonorario.CorreoElectronico = personalViewModel.CorreoElectronico;
                    personalEntityHonorario.IdUsuario = "ADMIN";

                    personalHonorario.Actualizar(personalEntityHonorario, _configuration);
                }
            }
            else
            {
                return PartialView("Mensajeria", new MensajeriaViewModel { IsError = true, Mensaje = "Error al leer Certificado Original", Url = "/CertificadoAntiguedad" });
            }


            return PartialView("Mensajeria", new MensajeriaViewModel { IsError = false, Mensaje = "Se ha generado el certificado!!!!", Url = "/CertificadoAntiguedad" });
        }

        public void FillFormFields(string inputPdf, string outputPdf, PersonalViewModel personalViewModel)
        {
            using var reader = new PdfReader(inputPdf);
            using var writer = new PdfWriter(outputPdf);
            using var pdfDoc = new PdfDocument(reader, writer);

            // Obtener el formulario
            PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDoc, true);

            if (personalViewModel.IdTipoFuncionario == 1)
            {
                // Modificar campos
                form.GetField("txtNombrePersonal").SetValue(personalViewModel.NombreCompleto);
                form.GetField("txtRutPersonal").SetValue(GeneralRoutine.FormatearRut(personalViewModel.RutPersonal!));
                form.GetField("txtEstablecimiento").SetValue(personalViewModel.NombreEstablecimiento);
                form.GetField("txtCargo").SetValue(personalViewModel.NombreCargo);
                form.GetField("txtNombreCategoria").SetValue(personalViewModel.Categoria);
                form.GetField("txtNombreNivel").SetValue(personalViewModel.Nivel.ToString());
                form.GetField("txtFecInicioContrato").SetValue(personalViewModel.FecInicioContrato!);
                form.GetField("txtTipoContrato").SetValue(personalViewModel.NombreTipoContrato);
                form.GetField("txtNroHora").SetValue(personalViewModel.NroHora.ToString());
                form.GetField("txtFecGenerarCertificado").SetValue(DateTime.Now.ToString("dd 'de' MMMM 'de' yyyy")!);
            }
            else
            {
                // Modificar campos
                form.GetField("txtNombrePersonal").SetValue(personalViewModel.NombreCompleto);
                form.GetField("txtRutPersonal").SetValue(GeneralRoutine.FormatearRut(personalViewModel.RutPersonal!));
                form.GetField("txtEstablecimiento").SetValue(personalViewModel.NombreEstablecimiento);
                form.GetField("txtCargo").SetValue(personalViewModel.NombreCargo);
                form.GetField("txtFecInicioContrato").SetValue(personalViewModel.FecInicioContrato!);
                form.GetField("txtTipoContrato").SetValue("HONORARIO");
                form.GetField("txtCometido").SetValue(personalViewModel.Cometido);
                form.GetField("txtFecGenerarCertificado").SetValue(DateTime.Now.ToString("dd 'de' MMMM 'de' yyyy")!);
            }

            // Opcional: aplanar (flatten) para evitar edición posterior
            form.FlattenFields();

            pdfDoc.Close();
        }

    }
}

using ContratoQR.Entity;
using ContratoQR.WEB.Models;
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ContratoQR.WEB.Controllers
{
    public class UploadFileController : Controller
    {

        private readonly IConfiguration _configuration;

        public UploadFileController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            FileExcelViewModel fileExcelModel = new FileExcelViewModel();

            fileExcelModel.IsError = "";

            fileExcelModel.IdCodigoQR = 1;
            fileExcelModel.IdCertificado = 2;

            return View(fileExcelModel);
        }

        [HttpPost]
        public async Task<IActionResult> FileUpload(IFormFile file, int archivo)
        {
            string pathDir = string.Empty;
            FileExcelViewModel fileExcelModel = new FileExcelViewModel();
            ViewBag.Inicio = "NO";

            pathDir = archivo == 1 ? "CodigoQR" : @"Certificado\ArchivoExcel";

            try
            {
                if (file != null && file.Length > 0)
                {
                    var uploadsPath = Path.Combine("wwwroot", pathDir); // Asegúrate que esta carpeta existe
                    if (Directory.Exists(uploadsPath))
                    {
                        Directory.Delete(uploadsPath, true);
                        Directory.CreateDirectory(uploadsPath);
                    }
                    else
                    {
                        Directory.CreateDirectory(uploadsPath);
                    }

                    var filePath = Path.Combine(uploadsPath, file.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }

                // Procesa solo el archivo de certificados, el otro es solo para subir el código QR
                if (archivo == 2)
                {
                    ProcessFile();
                }
            }
            catch (Exception ex)
            {
                return PartialView("Mensajeria", new MensajeriaViewModel { IsError = true, Mensaje = ex.Message, Url = "/Home" });
            }

            return PartialView("Mensajeria", new MensajeriaViewModel { IsError = false, Mensaje = "Archivo subido exitosamente", Url = "/Home" });
        }

        private void ProcessFile()
        {
            List<PersonalEntity> personal = new List<PersonalEntity>();
            string? filePath = "";
            FileExcelViewModel fileExcelModel = new FileExcelViewModel();

            if (Directory.GetFiles(Path.Combine("wwwroot", @"Certificado\ArchivoExcel")).Length > 0)
            {
                filePath = Directory.GetFiles(Path.Combine("wwwroot", @"Certificado\ArchivoExcel"))[0];
            }
            else
            {
                fileExcelModel.Mensaje = "No se ha subido el archivo Excel.";
                fileExcelModel.IsError = "SI";
            }

            if (fileExcelModel.IsError == "SI")
            {
                return;
            }

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {

                BLL.Personal personalBLL = new BLL.Personal();
                int contador = 0;
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();

                    foreach (DataRow fila in result.Tables[0].Rows)
                    {
                        if (contador > 0)
                        {
                            personal.Add(
                            new PersonalEntity
                            {
                                NroFicha = Convert.ToInt32(fila[2]),
                                ApellidoPersonal = fila[3].ToString(),
                                NombrePersonal = fila[4].ToString(),
                                RutPersonal = fila[5].ToString()!.Replace("-", "").Replace(".", ""),
                                IdEstablecimiento = Convert.ToInt32(fila[6]),
                                IdTipoContrato = fila[8].ToString() == "INDEFINIDO" ? 1 : fila[8].ToString() == "PLAZO FIJO" ? 2 : 999,
                                NombreCargo = fila[9].ToString(),
                                CorreoElectronico = string.Empty,
                                FecInicioContrato = fila[10].ToString(),
                                NroHora = Convert.ToInt32(fila[13]),
                                Categoria = fila[14].ToString(),
                                Nivel = Convert.ToInt32(fila[15]),
                                UrlContrato = string.Empty,
                                IdUsuario = "ADMIN"
                            });

                            var existePersonal = personalBLL.Listar(personal.Last().RutPersonal!, "", _configuration);

                            if (existePersonal.Count() > 0)
                            {
                                personal.Last().IdPersonal = existePersonal.First().IdPersonal;
                                personalBLL.Actualizar(personal.Last(), _configuration);
                            }
                            else
                            {
                                personalBLL.Insertar(personal.Last(), _configuration);
                            }
                        }

                        contador++;
                    }
                }
            }
        }
    }
}

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
            fileExcelModel.IdPlantaContrata = 2;
            fileExcelModel.IdHonorario = 3;

            return View(fileExcelModel);
        }

        [HttpPost]
        public async Task<IActionResult> FileUpload(IFormFile file, int archivo)
        {
            string pathDir = string.Empty;
            FileExcelViewModel fileExcelModel = new FileExcelViewModel();
            ViewBag.Inicio = "NO";

            if (archivo == 1)
            {
                pathDir = "CodigoQR";
            }
            else if (archivo == 2)
            {
                pathDir = @"Certificado\ArchivoExcelPlanta";
            }
            else if (archivo == 3)
            {
                pathDir = @"Certificado\ArchivoExcelHonorario";
            }

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
                    ProcessFilePlanta();
                }
                else if(archivo == 3)
                {
                    ProcessFileHonorario();
                }
            }
            catch (Exception ex)
            {
                return PartialView("Mensajeria", new MensajeriaViewModel { IsError = true, Mensaje = ex.Message, Url = "/Home" });
            }

            return PartialView("Mensajeria", new MensajeriaViewModel { IsError = false, Mensaje = "Archivo subido exitosamente", Url = "/Home" });
        }

        // Procesa el archivo de planta, se podría modificar para que solo procese los campos necesarios para planta, pero como la estructura es igual al de honorarios, se deja así para ambos casos
        private void ProcessFilePlanta()
        {
            List<PersonalEntity> personal = new List<PersonalEntity>();
            string? filePath = "";
            FileExcelViewModel fileExcelModel = new FileExcelViewModel();

            if (Directory.GetFiles(Path.Combine("wwwroot", @"Certificado\ArchivoExcelPlanta")).Length > 0)
            {
                filePath = Directory.GetFiles(Path.Combine("wwwroot", @"Certificado\ArchivoExcelPlanta"))[0];
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
                BLL.Establecimiento establecimientoBLL = new BLL.Establecimiento();
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
                                RutPersonal = fila[0].ToString()!.Replace(".", "").PadLeft(12, '0').Trim(),
                                ApellidoPersonal = fila[1].ToString()!.Trim(),
                                NombrePersonal = fila[2].ToString()!.Trim(),
                                IdTipoContrato = fila[3].ToString() == "INDEFINIDO" ? 1 : fila[3].ToString() == "PLAZO FIJO" ? 2 : 999,
                                TipoFuncionario = fila[4].ToString(),
                                NombreCargo = fila[5].ToString()!.Trim(),
                                FecInicioContrato = fila[6].ToString(),
                                IdEstablecimiento = Convert.ToInt32(fila[7]),
                                Categoria = fila[8].ToString(),
                                Nivel = Convert.ToInt32(fila[9]),
                                NroHora = Convert.ToInt32(fila[10]),
                                DiaTrabajado = Convert.ToInt32(fila[11]),
                                NroFicha = 0,
                                NombreEstablecimiento = string.Empty,
                                CorreoElectronico = string.Empty,
                                UrlContrato = string.Empty,
                                IdUsuario = "ADMIN"
                            });

                            var existePersonal = personalBLL.Listar(personal.Last().RutPersonal!, "", _configuration);
                            var existeEstablecimiento = establecimientoBLL.Listar(personal.Last().IdEstablecimiento, string.Empty, _configuration);

                            if(existeEstablecimiento.Count() == 0)
                            {
                                establecimientoBLL.Insertar(new EstablecimientoEntity 
                                {
                                    IdEstablecimiento = personal.Last().IdEstablecimiento, 
                                    NombreEstablecimiento = personal.Last().NombreEstablecimiento 
                                }, _configuration);
                            }

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

        // Procesa el archivo de honorarios, la estructura es igual al de planta, pero se podría modificar para que solo procese los campos necesarios para honorarios
        private void ProcessFileHonorario()
        {
            List<PersonalHonorarioEntity> personal = new List<PersonalHonorarioEntity>();
            string? filePath = "";
            FileExcelViewModel fileExcelModel = new FileExcelViewModel();

            if (Directory.GetFiles(Path.Combine("wwwroot", @"Certificado\ArchivoExcelHonorario")).Length > 0)
            {
                filePath = Directory.GetFiles(Path.Combine("wwwroot", @"Certificado\ArchivoExcelHonorario"))[0];
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

                BLL.PersonalHonorario personalBLL = new BLL.PersonalHonorario();
                BLL.Establecimiento establecimientoBLL = new BLL.Establecimiento();
                int contador = 0;
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();

                    foreach (DataRow fila in result.Tables[0].Rows)
                    {
                        if (contador > 0)
                        {
                            personal.Add(
                            new PersonalHonorarioEntity
                            {
                                NroDecreto = fila[0].ToString(),
                                RutPersonal = fila[1].ToString()!.Replace(".", "").PadLeft(12, '0').Trim(),
                                NombrePersonal = fila[2].ToString()!.Trim().ToUpper(),
                                NombreCargo = fila[3].ToString()!.Trim().ToUpper(),
                                Cometido = fila[4].ToString()!.Trim().ToUpper(),
                                NombrePrograma = fila[5].ToString()!.Trim().ToUpper(),
                                NroCuentaPresupuestaria = fila[6].ToString()!.Trim(),
                                Monto = fila[7].ToString()!.Trim().ToUpper(),
                                NombreEstablecimiento = fila[8].ToString()!.Trim().ToUpper(),
                                FecIngreso = Convert.ToDateTime(fila[9].ToString()!.Trim()),
                                FecInicioContrato = Convert.ToDateTime(fila[10].ToString()),
                                FecTerminoContrato = Convert.ToDateTime(fila[11].ToString()),
                                FecNacimiento = DateTime.Parse(fila[12].ToString()!.Trim()),
                                Direccion = fila[13].ToString()!.Trim().ToUpper(),
                                NombreComuna = fila[14].ToString()!.Trim().ToUpper(),
                                Sexo = fila[15].ToString()!.Trim().ToUpper(),
                                CorreoElectronico = string.Empty,
                                UrlContrato = string.Empty,
                                IdUsuario = "ADMIN"
                            });

                            var existePersonal = personalBLL.Listar(personal.Last().RutPersonal!, "", _configuration);

                            if (existePersonal.Count() > 0)
                            {
                                personal.Last().IdPersonalHonorario = existePersonal.First().IdPersonalHonorario;
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

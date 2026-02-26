using ContratoQR.Entity;
using ContratoQR.WEB.Models;
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;

namespace ContratoQR.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public ActionResult Index()
        {
            FileExcelViewModel fileExcelModel = new FileExcelViewModel();
            ViewData["qr"] = "";
            ViewBag.QR = "";
            fileExcelModel.CodigoQR = "";
            fileExcelModel.NombreFuncionario = "";
            fileExcelModel.UrlContrato = "";

            return View(fileExcelModel);
        }

        [HttpPost]
        public Task<IActionResult> Index(string texto)
        {
            List<FileExcelEntity> funcionarios = new List<FileExcelEntity>();
            string? filePath = ""; // _configuration.GetValue<string>("NombreExcel").ToString();
            FileExcelViewModel fileExcelModel = new FileExcelViewModel();

            if (Directory.Exists(Path.Combine("wwwroot", "Uploads")) && Directory.GetFiles(Path.Combine("wwwroot", "Uploads")).Length > 0)
            {
                filePath = Directory.GetFiles(Path.Combine("wwwroot", "Uploads"))[0];
            }
            else
            {
                fileExcelModel.Mensaje = "No se ha subido el archivo Excel.";
                fileExcelModel.IsError = "SI";
                return Task.FromResult<IActionResult>(View(fileExcelModel));
            }

            if (texto == null)
            {
                ViewData["mensaje"] = "Introduzca Rut del Funcionario";
                return Task.FromResult<IActionResult>(View());
            }

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();

                    foreach (DataRow fila in result.Tables[0].Rows)
                    {

                        funcionarios.Add(
                            new FileExcelEntity
                            {
                                RutFuncionario = fila[0].ToString()!.Replace("-","").Replace(".",""),
                                NombreFuncionario = fila[1].ToString(),
                                UrlContrato = fila[2].ToString()
                            });
                    }
                }
            }

            fileExcelModel.Funcionarios = funcionarios;
            fileExcelModel.Funcionario = fileExcelModel.Funcionarios.Where(f => f.RutFuncionario == texto.Replace("-", "").Replace(".", "")).FirstOrDefault();

            Helpers.HelperQR helperQR = new Helpers.HelperQR();
            fileExcelModel.CodigoQR = helperQR.GenerateQRCode(fileExcelModel.Funcionario != null ? fileExcelModel.Funcionario.UrlContrato : "No encontrado");
            fileExcelModel.NombreFuncionario = fileExcelModel.Funcionario == null ? "No encontrado" : fileExcelModel.Funcionario.NombreFuncionario;
            fileExcelModel.UrlContrato = fileExcelModel.Funcionario == null ? "No encontrado" : fileExcelModel.Funcionario.UrlContrato;

            return Task.FromResult<IActionResult>(View(fileExcelModel));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

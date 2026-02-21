using ClosedXML.Excel;
using ContratoQR.Entity;
using ContratoQR.WEB.Models;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace ContratoQR.WEB.Controllers
{
    public class EncuestaController : Controller
    {

        private readonly IConfiguration _configuration;

        public EncuestaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            EncuestaViewModel encuestaViewModel = new();
            BLL.Encuesta encuesta = new();

            try
            {
                encuestaViewModel.ListaCantidad = encuesta.ListarCantidad(_configuration);
                encuestaViewModel.resultadoEncuestas = encuesta.ListarResultadoEncuesta(_configuration);
                encuestaViewModel.CantidadTotalRespuestas = encuestaViewModel.resultadoEncuestas.Count();
                encuestaViewModel.CantidadEncuestasCompletadas = encuestaViewModel.resultadoEncuestas.Count() / 3;
            }
            catch (Exception ex)
            {
                return PartialView("Mensajeria", new MensajeriaViewModel { IsError = true, Mensaje = ex.Message, Url = "/Encuesta" });
            }

            return View(encuestaViewModel);
        }

        [HttpGet]
        public FileResult ExportarExcel()
        {
            var nombreArchivo = $"ResultadoEncuesta.xlsx";

            EncuestaViewModel encuestaViewModel = new();
            BLL.Encuesta encuesta = new();

            encuestaViewModel.resultadoEncuestas = encuesta.ListarResultadoEncuesta(_configuration);

            return GenerarExcel(nombreArchivo, encuestaViewModel.resultadoEncuestas);
        }

        private FileResult GenerarExcel(string nombreArchivo, IEnumerable<ResultadoEncuestaEntity> resultadoEncuestas)
        {
            DataTable dataTable = new DataTable("Personas");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("DescripcionPregunta"),
                new DataColumn("DescripcionValorRespuesta")
            });

            foreach (var resultado in resultadoEncuestas)
            {
                dataTable.Rows.Add(resultado.DescripcionPregunta, resultado.DescripcionValorRespuesta);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable);

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        nombreArchivo);
                }
            }

        }

    }
}


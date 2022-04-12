using Microsoft.AspNetCore.Mvc;
using SaveCsvFromJson.Models;

namespace SaveCsvFromJson.Controllers
{
    public class CSVFromJsonController : Controller
    {
        public IActionResult Index()
        {     
            return View();
        }

        [HttpPost]
        public ActionResult GerarCSV(string txtjson)
        {
            DataSave ds = new DataSave();

            //Checa se foi informado o texto no campo e se o formato é válido
            if (ds.ValidaJson(txtjson) == false)
            {
                @ViewBag.jsonvalido = "É necessário informar um json";
                return PartialView("~/views/home/index.cshtml");
            }  
        
            //Metodo que gera o arquivo e atribui retorno
            //a variável utilizada no ViewBag de retorno
            ViewBag.Retorno = ds.SalvarCSV(txtjson);

            return View("GerarCSV");
        }

    }
}

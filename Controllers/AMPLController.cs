using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AMPL_Backend.Service;
using System.ComponentModel.DataAnnotations;
using ampl;
using System.Text.Json.Serialization;

namespace AMPL_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AMPLController : ControllerBase
    {
        AMPLEquation ampl = new AMPLEquation();
        AMPL a = new AMPL();
        private string dataFilePath = @"D:\Demetre Badzgaradze\ProgramingProjects\AMPL-Backend\AMPLFiles\Eq.dat";
        private string modelFilePath = @"D:\Demetre Badzgaradze\ProgramingProjects\AMPL-Backend\AMPLFiles\Eq.mod";
        public double AvalableMaterial { get; set; } = 1700;
        public double AvalableTime { get; set; } = 160;
        [HttpGet]
        public IActionResult Get() => Ok(AMPLEquation.DefineModelFile(modelFilePath, GetRandomProducts(2), AvalableMaterial, AvalableTime));

        [HttpGet("define-data-file")]
        public IActionResult DefineDataFile()
        {
            return Ok(AMPLEquation.DefineDataFile(dataFilePath, GetRandomProducts(2)));
        }
        [HttpGet("random-equetion")]
        public IActionResult RandomEquetion([Required] int amount)
        {
            Product[] p = new Product[amount];
            for (int i = 0; i < p.Length; i++)
            {
                p[i] = Product.MakeProduct();
            }
            AMPLEquation.DefineDataFile(dataFilePath, p);
            AMPLEquation.DefineModelFile(modelFilePath, p,AvalableMaterial,AvalableTime);

            a.SetOption("solver", "minos");
            a.Read(modelFilePath);
            a.ReadData(dataFilePath);
            a.Solve();

            //var profit = a.GetVariable("profit");
            var profit = a.GetValue("profit");

            return Ok(profit);
        }
        //[HttpPost("random-equetion-specify")]
        //public IActionResult RandomEquetion([Required][FromBody] Product[] products)
        //{
        //    Console.WriteLine(Request.ToString()); 
        //    Console.WriteLine(products.Length);
        //    EquationResult[] results = new EquationResult[products.Length];
        //    Product[] p = products;
            
        //    AMPLEquation.DefineDataFile(dataFilePath, p);
        //    AMPLEquation.DefineModelFile(modelFilePath, p, AvalableMaterial, AvalableTime);

        //    a.SetOption("solver", "minos");
        //    a.Read(modelFilePath);
        //    a.ReadData(dataFilePath);
        //    a.Solve();

        //    var profit = a.GetValue("profit").Dbl;

        //    for (int i = 0; i < p.Length; i++)
        //    {
        //        results[i] = p[i].GetEquationResult((int)a.GetValue(p[i].Name).Dbl);
        //    }

        //    return Ok(results);
        //}
        [HttpPost("random-equetion-specify")]
        public IActionResult RandomEquetion([Required][FromBody] RecuestProductList products)
        {
            Product[] p = products.IntoProductList();
            EquationResult[] results = new EquationResult[p.Length];

            AMPLEquation.DefineDataFile(dataFilePath, p);
            AMPLEquation.DefineModelFile(modelFilePath, p, AvalableMaterial, AvalableTime);

            a.SetOption("solver", "minos");
            a.Read(modelFilePath);
            a.ReadData(dataFilePath);
            a.Solve();

            var profit = a.GetValue("profit").Dbl;

            for (int i = 0; i < p.Length; i++)
            {
                results[i] = p[i].GetEquationResult((int)a.GetValue(p[i].Name).Dbl);
            }

            return Ok(results);
        }

        [HttpGet("new-product")]
        public IActionResult NewProduct()
        {
            return Ok(Product.MakeProduct());
        }
        [HttpGet("make-products")]
        public IActionResult MakeProducts([Required]int amoutOfProducts)
        {
            Product[] products = new Product[amoutOfProducts];

            for (int i = 0; i < amoutOfProducts; i++)
            {
                products[i] = Product.MakeProduct();
            }
            return Ok(products);
        }
        private Product[] GetRandomProducts([Required] int amoutOfProducts)
        {
            Product[] products = new Product[amoutOfProducts];

            for (int i = 0; i < amoutOfProducts; i++)
            {
                products[i] = Product.MakeProduct();
            }
            return products;
        }
    }
}
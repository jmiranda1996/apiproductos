using ApiProductos.Data;
using ApiProductos.Extensions;
using ApiProductos.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProductos.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        readonly ILogger<Products> _logger;
        readonly IProductsRepository _productsRepository;

        public ProductsController(
            ILogger<Products> logger,
            IProductsRepository productsRepository
            )
        {
            _logger = logger;
            _productsRepository = productsRepository;
        }

        [HttpGet("by-price-range")]
        public async Task<IActionResult> GetByPriceRange([FromQuery]double lowerPrice, [FromQuery]double higherPrice)
        {
            var resultTask = _productsRepository.GetAllByPrice(lowerPrice, higherPrice);

            _logger.LogInformation("Obtencion de producto por rango de precios 1: {lowerPrice} 2: {higherPrice}", lowerPrice, higherPrice);

            var result = await resultTask;

            return StatusCode(200, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var resultTask = _productsRepository.GetById(id);

            _logger.LogInformation("Obtencion de producto por ID: {id}", id);

            var result = await resultTask;

            return StatusCode(200, result);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody]Products newent)
        {
            newent.id = newent.id.ToSumId();
            var resultTask = _productsRepository.Insert(newent);

            _logger.LogInformation("Insercion de producto {newent} Inicializado", JsonConvert.SerializeObject(newent));

            await resultTask;

            _logger.LogInformation("Insercion de producto {newent} Correcto", JsonConvert.SerializeObject(newent));

            return StatusCode(200, "Producto agregado correctamente");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]Products newent)
        {
            var resultTask = _productsRepository.Update(id, newent);

            _logger.LogInformation("Actualizacion de producto ID: {id} - {newent} Inicializado", id, JsonConvert.SerializeObject(newent));

            (int, string) result = await resultTask;

            string status = result.Item1 == 200 ? "Correcto" : "Incorrecto";
            _logger.LogInformation("Actualizacion de producto ID: {id} - {status} - Detalle: {detalle}", id, status, result.Item2);

            return StatusCode(result.Item1, result.Item2);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultTask = _productsRepository.DeleteById(id);

            _logger.LogInformation("Eliminacion de producto ID: {id} Inicializado", id);

            (int, string) result = await resultTask;

            string status = result.Item1 == 200 ? "Correcto" : "Incorrecto";
            _logger.LogInformation("Eliminacion de producto ID: {id} - {status} - Detalle: {detalle}", id, status, result.Item2);

            return StatusCode(result.Item1, result.Item2);
        }
    }
}

﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleCalculatorAPI.Interface;
using System.ComponentModel.DataAnnotations;

namespace SimpleCalculatorAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculationController : Controller
    {
        private readonly ICalculationService _calculationService;

        public CalculationController(ICalculationService calculationService) 
        {
            _calculationService = calculationService;
        }

        //[Authorize]
        [HttpPost("CalculateMathOperation")]
        public IActionResult CalculateMathOperation([FromBody][Required] string operation)
        {
            try
            {
                double result = _calculationService.CalculateMathOperation(operation);

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

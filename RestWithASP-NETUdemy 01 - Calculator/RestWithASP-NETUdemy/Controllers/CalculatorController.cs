using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RestWithASP_NETUdemy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : Controller
    {
        // GET api/values/5/5
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("sum/{firstNumber}/{secondNumber}")]
        public IActionResult Sum(string firstNumber, string secondNumber)
        {
            if (IsNumber(firstNumber) && IsNumber(secondNumber))
            {
                var sum = ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber);
                return Ok(sum.ToString());
            }
            return BadRequest("Entrada de dados inválida");
        }

        [HttpGet("subtraction/{firstNumber}/{secondNumber}")]
        public IActionResult Subtraction(string firstNumber, string secondNumber)
        {
            if (IsNumber(firstNumber) && IsNumber(secondNumber))
            {
                var sum = ConvertToDecimal(firstNumber) - ConvertToDecimal(secondNumber);
                return Ok(sum.ToString());
            }
            return BadRequest("Entrada de dados inválida");
        }


        [HttpGet("division/{firstNumber}/{secondNumber}")]
        public IActionResult Division(string firstNumber, string secondNumber)
        {
            if (IsNumber(firstNumber) && IsNumber(secondNumber))
            {
                var sum = ConvertToDecimal(firstNumber) / ConvertToDecimal(secondNumber);
                return Ok(sum.ToString());
            }
            return BadRequest("Entrada de dados inválida");
        }

        [HttpGet("multiplication/{firstNumber}/{secondNumber}")]
        public IActionResult Multiplication(string firstNumber, string secondNumber)
        {
            if (IsNumber(firstNumber) && IsNumber(secondNumber))
            {
                var sum = ConvertToDecimal(firstNumber) * ConvertToDecimal(secondNumber);
                return Ok(sum.ToString());
            }
            return BadRequest("Entrada de dados inválida");
        }

        private bool IsNumber(string strnumber)
        {
            double number;
            bool isNumber = double.TryParse(strnumber, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out number);
            return isNumber;
        }

        private decimal ConvertToDecimal(string number)
        {
            decimal decimalValue;
            if (decimal.TryParse(number, out decimalValue))
            {
                return decimalValue;
            }
            return 0;
        }
    }
}

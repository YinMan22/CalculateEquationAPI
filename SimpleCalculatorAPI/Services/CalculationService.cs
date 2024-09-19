using SimpleCalculatorAPI.Interface;
using System.Data;
using System;
using System.Text.RegularExpressions;

namespace SimpleCalculatorAPI.Services
{
    public class CalculationService : ICalculationService
    {
        private readonly ILogger<CalculationService> _logger;
        private const string regEx = @"^[0-9+\-*/().\s]+$";

        public CalculationService(ILogger<CalculationService> logger)
        {
            _logger = logger;
        }

        public double CalculateMathOperation(string operation)
        {
            try 
            {
                _logger.LogInformation($"{nameof(CalculateMathOperation)} | Request received: {operation}");

                // Validate number and operator only
                if (!Regex.IsMatch(operation, regEx))
                    throw new SyntaxErrorException("Invalid characters exist.");

                // Evaluating math operation
                DataTable dataTable = new DataTable();
                double result = Convert.ToDouble(dataTable.Compute(operation, ""));

                // Error Handling for infinity and not a number
                if (double.IsInfinity(result) || double.IsNaN(result))
                    throw new SyntaxErrorException("Invalid mathematical operation.");

                _logger.LogInformation($"{nameof(CalculateMathOperation)} | Response returned: {result}");
                return result;
            }
            catch (SyntaxErrorException ex)
            {
                _logger.LogError($"{nameof(CalculateMathOperation)} | SyntaxError occurred: {ex.Message} | Request received: {operation}");
                throw new ArgumentException($"{ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(CalculateMathOperation)} | Error occurred: {ex.Message} | Request received: {operation}");
                throw new Exception($"Error when evaluating the mathematical operation.");
            }
        }
    }
}

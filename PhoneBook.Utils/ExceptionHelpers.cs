using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Utils
{
    public static class ExceptionHelpers
    {
        public static void LogException<T>(this Exception ex, ILogger<T> _logger, string message = "")
        {
            _logger.LogError($"Error occured.{message} Error Message: {ex.Message}");
            _logger.LogError($"Error occured.{message} Error Inner-Ex-Message: {ex.InnerException?.Message}");
            _logger.LogError($"Error occured.{message} Error Stack-Trace: {ex.StackTrace}");
        }
    }
}

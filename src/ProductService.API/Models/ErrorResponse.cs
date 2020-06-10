using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Refactored.API.Models
{
    public class ErrorResponse
    {
        public ErrorResponse(string errorMsg)
        {
            ErrorMessage = errorMsg;
        }

        public ErrorResponse()
        { }

        public string ErrorMessage { get; set; }
    }
}

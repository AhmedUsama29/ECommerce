using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ErrorModels
{
    public class ValidationErrorModel
    {

        public int StatusCode { get; set; } = (int)HttpStatusCode.BadRequest;

        public string ErrorMessage { get; set; } = "Validation of ModelState Faild";

        public IEnumerable<ValidationError> ValidationErrors { get; set; } = [];

    }
}

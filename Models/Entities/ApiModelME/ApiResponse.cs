using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUserValidation.Models.Entities.ApiModelME
{
    public class ApiResponse
    {
        public int Status { get; set; }
        public string? Message { get; set; }
    }
}

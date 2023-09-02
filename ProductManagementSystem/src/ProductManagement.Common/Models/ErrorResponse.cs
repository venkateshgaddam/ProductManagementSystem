using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Common.Models
{
    public class ErrorResponse
    {
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
}

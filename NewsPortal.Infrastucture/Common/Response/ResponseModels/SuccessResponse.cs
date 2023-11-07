using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal.Infrastucture.Common.Response.ResponseModels
{
    public class SuccessResponse : BaseResponse
    {
        public SuccessResponse(object? data, string? message = null)
        {
            IsSuccess = true;
            StatusCode = HttpStatusCode.OK;
            Data = data;
            Message = message;
        }
    }
}

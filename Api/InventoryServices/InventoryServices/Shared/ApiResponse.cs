using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryServices.Shared
{
    public class ApiResponse
    {
        public object data;
        public int responseCode; // 0 - Success, 1 - No data Found, 2 - Exception
        public string error;

        public const int Success = 0;
        public const int NoDataFound = 1;
        public const int Exception = 2;
    }

    public class Helper
    {
        public static ApiResponse CreateResponse(string jsonData, int responseCode, string error)
        {
            ApiResponse response = new ApiResponse();
            response.data = jsonData;
            response.responseCode = responseCode;
            response.error = error;
            return response;
        }

        public static ApiResponse CreateDataResponse(object data)
        {
            ApiResponse response = new ApiResponse();
            response.data = data;
            response.responseCode = ApiResponse.Success;
            response.error = "";
            return response;
        }

        public static ApiResponse CreateNoDataResponse()
        {
            ApiResponse response = new ApiResponse();
            response.data = "";
            response.responseCode = ApiResponse.NoDataFound;
            response.error = "No Data Found";
            return response;
        }

        public static ApiResponse CreateErrorResponse(Exception ex)
        {
            ApiResponse response = new ApiResponse();
            response.data = "";
            response.responseCode = ApiResponse.Exception;
            response.error = Helper.GetException(ex).Message.Replace('"', '\'');
            return response;
        }

        private static Exception GetException(Exception ex)
        {
            if (ex.InnerException == null) return ex;
            else
            {
                return Helper.GetException(ex.InnerException);
            }
        }
    }
}
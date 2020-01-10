using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using InventoryUi.Inventory.Models;
using Newtonsoft.Json;
using System.Configuration;
using System.Data;
using System.Text;

namespace InventoryUi.Shared
{
    public class Helper
    {
        public static ApiResponse GetApiResponse(string actionPath)
        {
            ApiResponse response =null;
            try
            {
                string WebApiRootUrl = ConfigurationManager.AppSettings["WebApiRootUrl"];

                if (!string.IsNullOrWhiteSpace(WebApiRootUrl))
                {
                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.BaseAddress = new Uri(WebApiRootUrl);
                        //HTTP GET
                        var responseTask = client.GetStringAsync(actionPath);
                        responseTask.Wait();

                        var result = responseTask.Result.Replace("\\", "").Trim('"');
                        response = JsonConvert.DeserializeObject<ApiResponse>(result);
                        
                    }
                }
                else
                {
                    response = new ApiResponse();
                    response.data = "";
                    response.error = "Could not get Api root path from Web.config file";
                    response.responseCode = 4;
                }
            }
            catch (Exception ex)
            {
                response = new ApiResponse();
                response.data = "";
                response.error = ex.Message;
                response.responseCode = 2;
            }
            return response;
        }


        public static ApiResponse PostToApi(string actionPath, string data)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                string WebApiRootUrl = ConfigurationManager.AppSettings["WebApiRootUrl"];

                if (!string.IsNullOrWhiteSpace(WebApiRootUrl))
                {
                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.BaseAddress = new Uri(WebApiRootUrl);
                        var stringContent = new StringContent(data, UnicodeEncoding.UTF8, "application/json");

                        var responseTask = client.PostAsync(actionPath, stringContent);
                        responseTask.Wait();

                        var result = responseTask.Result.Content.ReadAsStringAsync();
                        response = JsonConvert.DeserializeObject<ApiResponse>(result.Result.Replace("\\", "").Trim('"'));
                    }
                }
                else
                {
                    response.data = "";
                    response.error = "Could not get Api root path from Web.config file";
                    response.responseCode = 4;
                }
            }
            catch (Exception ex)
            {
                response.data = "";
                response.error = ex.Message;
                response.responseCode = 2;
            }
            return response;
        }

        public static ApiResponse PutToApi(string actionPath, string data)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                string WebApiRootUrl = ConfigurationManager.AppSettings["WebApiRootUrl"];

                if (!string.IsNullOrWhiteSpace(WebApiRootUrl))
                {
                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.BaseAddress = new Uri(WebApiRootUrl);
                        var stringContent = new StringContent(data, UnicodeEncoding.UTF8, "application/json");

                        var responseTask = client.PutAsync(actionPath, stringContent);
                        responseTask.Wait();

                        var result = responseTask.Result.Content.ReadAsStringAsync();
                        response = JsonConvert.DeserializeObject<ApiResponse>(result.Result.Replace("\\", "").Trim('"'));
                    }
                }
                else
                {
                    response.data = "";
                    response.error = "Could not get Api root path from Web.config file";
                    response.responseCode = 4;
                }
            }
            catch (Exception ex)
            {
                response.data = "";
                response.error = ex.Message;
                response.responseCode = 2;
            }
            return response;
        }

        public static ApiResponse DeleteRequestToApi(string actionPath, int key)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                string WebApiRootUrl = ConfigurationManager.AppSettings["WebApiRootUrl"];

                if (!string.IsNullOrWhiteSpace(WebApiRootUrl))
                {
                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.BaseAddress = new Uri(WebApiRootUrl);

                        var responseTask = client.DeleteAsync(actionPath + "/" + key.ToString());
                        responseTask.Wait();

                        var result = responseTask.Result.Content.ReadAsStringAsync();
                        response = JsonConvert.DeserializeObject<ApiResponse>(result.Result.Replace("\\", "").Trim('"'));
                    }
                }
                else
                {
                    response.data = "";
                    response.error = "Could not get Api root path from Web.config file";
                    response.responseCode = 4;
                }
            }
            catch (Exception ex)
            {
                response.data = "";
                response.error = ex.Message;
                response.responseCode = 2;
            }
            return response;
        }

        public static ApiResponse GetCategories(string categoryName = "")
        {
            string actionUrl = "api/Categories";
            categoryName = categoryName.Trim();
            if (categoryName.Length > 0)
            {
                actionUrl += "/" + categoryName;
            }
            ApiResponse response = Helper.GetApiResponse(actionUrl);
            return response;
        }

        public static ApiResponse GetUnits(string unitName = "")
        {
            string actionUrl = "api/Units";
            unitName = unitName.Trim();
            if (unitName.Length > 0)
            {
                actionUrl += "/" + unitName;
            }
            ApiResponse response = Helper.GetApiResponse(actionUrl);
            return response;
        }

        public static DataTable CreateDataTable(InventoryUi.Inventory.Models.Category[] data)
        {
            DataTable _myDataTable = new DataTable();

            // create columns
            _myDataTable.Columns.Add("CategoryId", typeof(int));
            _myDataTable.Columns.Add("CategoryName", typeof(string));
            _myDataTable.Columns.Add("CategoryDescription", typeof(string));

            for (int j = 0; j < data.Length ; j++)
            {
                DataRow row = _myDataTable.NewRow();
                row[0] = data[j].CategoryId;
                row[1] = data[j].CategoryName;
                row[2] = data[j].CategoryDescription;

                _myDataTable.Rows.Add(row);
            }
            return _myDataTable;
        }

        public static DataTable CreateDataTable(InventoryUi.Inventory.Models.Unit[] data)
        {
            DataTable _myDataTable = new DataTable();

            // create columns
            _myDataTable.Columns.Add("UnitId", typeof(int));
            _myDataTable.Columns.Add("UnitName", typeof(string));
            _myDataTable.Columns.Add("UnitDescription", typeof(string));

            for (int j = 0; j < data.Length; j++)
            {
                DataRow row = _myDataTable.NewRow();
                row[0] = data[j].UnitId;
                row[1] = data[j].UnitName;
                row[2] = data[j].UnitDescription;

                _myDataTable.Rows.Add(row);
            }
            return _myDataTable;
        }

        public static DataTable CreateDataTable(InventoryUi.Inventory.Models.Product[] data)
        {
            DataTable _myDataTable = new DataTable();

            // create columns
            _myDataTable.Columns.Add("ProductId", typeof(int));
            _myDataTable.Columns.Add("ProductName", typeof(string));
            _myDataTable.Columns.Add("ProductDescription", typeof(string));
            _myDataTable.Columns.Add("CategoryId", typeof(int));
            _myDataTable.Columns.Add("CategoryName", typeof(string));
            _myDataTable.Columns.Add("CategoryDescription", typeof(string));

            _myDataTable.Columns.Add("Price", typeof(decimal));
            _myDataTable.Columns.Add("Currency", typeof(string));
            _myDataTable.Columns.Add("UnitId", typeof(int));
            _myDataTable.Columns.Add("UnitName", typeof(string));
            _myDataTable.Columns.Add("UnitDescription", typeof(string));

            for (int j = 0; j < data.Length; j++)
            {
                DataRow row = _myDataTable.NewRow();
                row["ProductId"] = data[j].ProductId;
                row["ProductName"] = data[j].ProductName;
                row["ProductDescription"] = data[j].ProductDescription;
                row["CategoryId"] = data[j].CategoryId;
                row["CategoryName"] = data[j].CategoryName;
                row["CategoryDescription"] = data[j].CategoryDescription;
                row["Price"] = data[j].Price;
                row["Currency"] = data[j].Currency;
                row["UnitId"] = data[j].UnitId;
                row["UnitName"] = data[j].UnitName;
                row["UnitDescription"] = data[j].UnitDescription;

                _myDataTable.Rows.Add(row);
            }
            return _myDataTable;
        }
    }

}
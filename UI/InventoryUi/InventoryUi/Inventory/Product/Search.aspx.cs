using System;
using System.Web.UI.WebControls;
using InventoryUi.Inventory.Models;
using Newtonsoft.Json;
using InventoryUi.Shared;
using System.Data;

namespace InventoryUi.Inventory.Product
{
    public partial class Search : System.Web.UI.Page
    {
        void BindGrid(string ProductName = "")
        {

            DataTable dt = null;

            try
            {
                string actionUrl = "api/Products";
                ProductName = ProductName.Trim();
                if (ProductName.Length > 0)
                {
                    actionUrl += "/" + ProductName;
                }
                ApiResponse response = Helper.GetApiResponse(actionUrl);

                if (response.responseCode == ApiResponse.Success)
                {
                    InventoryUi.Inventory.Models.Product[] data = JsonConvert.DeserializeObject<InventoryUi.Inventory.Models.Product[]>(response.data.ToString());
                    dt = Helper.CreateDataTable(data);
                }
                else if (response.responseCode == ApiResponse.NoDataFound)
                {
                    TxtDescription.Text = "";
                    LblErrorMsg.Text = "No Data Found";
                    LblErrorMsg.Visible = true;
                }
                else if (response.responseCode == ApiResponse.Exception)
                {
                    TxtDescription.Text = "";
                    LblErrorMsg.Text = "Api Error: " + response.error;
                    LblErrorMsg.Visible = true;
                }
            }
            catch (Exception ex)
            {
                TxtDescription.Text = "";
                LblErrorMsg.Text = "Page Error: " + ex.Message;
                LblErrorMsg.Visible = true;
            }

            ProductGridView.DataSource = dt;
            ProductGridView.DataBind();

        }

        void LoadCategories(string categoryName="")
        {
            ApiResponse response = Helper.GetCategories(categoryName);

            try
            {

                if (response.responseCode == ApiResponse.Success)
                {
                    InventoryUi.Inventory.Models.Category[] data = JsonConvert.DeserializeObject<InventoryUi.Inventory.Models.Category[]>(response.data.ToString());
                    using (DataTable dt = Helper.CreateDataTable(data))
                    {
                        CboCategory.DataSource = dt;
                        CboCategory.DataTextField = "CategoryName";
                        CboCategory.DataValueField = "CategoryId";
                        CboCategory.DataBind();
                    }
                }
                else if (response.responseCode == ApiResponse.NoDataFound)
                {
                    TxtDescription.Text = "";
                    LblErrorMsg.Text = "No Data Found";
                    LblErrorMsg.Visible = true;
                }
                else if (response.responseCode == ApiResponse.Exception)
                {
                    TxtDescription.Text = "";
                    LblErrorMsg.Text = "Api Error: " + response.error;
                    LblErrorMsg.Visible = true;
                }
            }
            catch (Exception ex)
            {
                TxtDescription.Text = "";
                LblErrorMsg.Text = "Page Error: " + ex.Message;
                LblErrorMsg.Visible = true;
            }
        }

        void LoadUnits(string unitName = "")
        {
            ApiResponse response = Helper.GetUnits(unitName);

            try
            {

                if (response.responseCode == ApiResponse.Success)
                {
                    InventoryUi.Inventory.Models.Unit[] data = JsonConvert.DeserializeObject<InventoryUi.Inventory.Models.Unit[]>(response.data.ToString());
                    using (DataTable dt = Helper.CreateDataTable(data))
                    {
                        CboUnit.DataSource = dt;
                        CboUnit.DataTextField = "UnitName";
                        CboUnit.DataValueField = "UnitId";
                        CboUnit.DataBind();
                    }
                }
                else if (response.responseCode == ApiResponse.NoDataFound)
                {
                    TxtDescription.Text = "";
                    LblErrorMsg.Text = "No Data Found";
                    LblErrorMsg.Visible = true;
                }
                else if (response.responseCode == ApiResponse.Exception)
                {
                    TxtDescription.Text = "";
                    LblErrorMsg.Text = "Api Error: " + response.error;
                    LblErrorMsg.Visible = true;
                }
            }
            catch (Exception ex)
            {
                TxtDescription.Text = "";
                LblErrorMsg.Text = "Page Error: " + ex.Message;
                LblErrorMsg.Visible = true;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LblErrorMsg.Visible = false;
            LblMsg.Visible = false;

            if (!IsPostBack)
            {
                this.BindGrid();
                this.LoadCategories("");
                this.LoadUnits("");
            }
        }


        protected void CmdSearchById_Click(object sender, EventArgs e)
        {
            try
            {
                int id = 0;
                if (!int.TryParse(TxtId.Text, out id))
                {
                    LblErrorMsg.Text = "Please enter proper Id to search";
                    LblErrorMsg.Visible = true;
                }
                else
                {
                    ApiResponse response = Helper.GetApiResponse("api/Products/" + id.ToString());
                    InventoryUi.Inventory.Models.Product data = JsonConvert.DeserializeObject<InventoryUi.Inventory.Models.Product>(response.data.ToString());
                    if (response.responseCode == ApiResponse.Success)
                    {
                        TxtProductName.Text = data.ProductName;
                        TxtDescription.Text = data.ProductDescription;
                        CboCategory.SelectedValue = data.CategoryId.ToString();
                        CboUnit.SelectedValue = data.UnitId.ToString();
                        TxtPrice.Text = data.Price.ToString();
                        CboCurrency.SelectedValue = data.Currency;
                    }
                    else if (response.responseCode == ApiResponse.NoDataFound)
                    {
                        TxtProductName.Text = "";
                        TxtDescription.Text = "";
                        LblErrorMsg.Text = "No Data Found";
                        LblErrorMsg.Visible = true;
                    }
                    else if (response.responseCode == ApiResponse.Exception)
                    {
                        TxtProductName.Text = "";
                        TxtDescription.Text = "";
                        LblErrorMsg.Text = "Api Error: " + response.error;
                        LblErrorMsg.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                TxtProductName.Text = "";
                TxtDescription.Text = "";
                LblErrorMsg.Text = "Page Error: " + ex.Message;
                LblErrorMsg.Visible = true;
            }
        }
    }
}
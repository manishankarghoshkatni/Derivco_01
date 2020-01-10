﻿using System;
using System.Web.UI.WebControls;
using InventoryUi.Inventory.Models;
using Newtonsoft.Json;
using InventoryUi.Shared;
using System.Data;


namespace InventoryUi.Inventory.Unit
{
    public partial class Search : System.Web.UI.Page
    {
        void BindGrid(string UnitName = "")
        {

            DataTable dt = null;

            try
            {
                ApiResponse response = Helper.GetUnits(UnitName);

                if (response.responseCode == ApiResponse.Success)
                {
                    InventoryUi.Inventory.Models.Unit[] data = JsonConvert.DeserializeObject<InventoryUi.Inventory.Models.Unit[]>(response.data.ToString());
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

            UnitGridView.DataSource = dt;
            UnitGridView.DataBind();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LblErrorMsg.Visible = false;
            LblMsg.Visible = false;

            if (!IsPostBack)
            {
                this.BindGrid();
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
                    ApiResponse response = Helper.GetApiResponse("api/Units/" + id.ToString());
                    InventoryUi.Inventory.Models.Unit data = JsonConvert.DeserializeObject<InventoryUi.Inventory.Models.Unit>(response.data.ToString());
                    if (response.responseCode == ApiResponse.Success)
                    {
                        TxtUnitName.Text = data.UnitName;
                        TxtDescription.Text = data.UnitDescription;
                    }
                    else if (response.responseCode == ApiResponse.NoDataFound)
                    {
                        TxtUnitName.Text = "";
                        TxtDescription.Text = "";
                        LblErrorMsg.Text = "No Data Found";
                        LblErrorMsg.Visible = true;
                    }
                    else if (response.responseCode == ApiResponse.Exception)
                    {
                        TxtUnitName.Text = "";
                        TxtDescription.Text = "";
                        LblErrorMsg.Text = "Api Error: " + response.error;
                        LblErrorMsg.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                TxtUnitName.Text = "";
                TxtDescription.Text = "";
                LblErrorMsg.Text = "Page Error: " + ex.Message;
                LblErrorMsg.Visible = true;
            }
        }

        protected void CmdSearchByName_Click(object sender, EventArgs e)
        {
            this.BindGrid(TxtUnitName.Text);
        }

        protected void UnitGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int UnitId = Convert.ToInt32(UnitGridView.DataKeys[e.NewEditIndex].Value.ToString());
            Response.Redirect("/Inventory/Unit/Modify?UnitId=" + UnitId.ToString());
        }

        protected void UnitGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int UnitId = Convert.ToInt32(UnitGridView.DataKeys[e.RowIndex].Value.ToString());

                string actionUrl = "api/Units/Delete";

                ApiResponse response = Helper.DeleteRequestToApi(actionUrl, UnitId);

                if (response.responseCode == ApiResponse.Success)
                {
                    LblMsg.Text = response.data.ToString();
                    LblMsg.Visible = true;
                    this.BindGrid();
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
    }
}
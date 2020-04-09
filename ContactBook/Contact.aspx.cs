using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ContactBook
{
    public partial class Contact : System.Web.UI.Page
    {
        
        SqlConnection sqlConn = new SqlConnection("Server=.;Database=ContactBook;Trusted_Connection=True;");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnDelete.Enabled = false;

                //calling FillGridview Method
                FillGridView();

                //Declaring variable contactID
                string contactID = Request["ContactID"];
                if (contactID!=""&& contactID!=null)
                {

                    Loaddetails();
                }

            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            //calling the clear method
            Clear();
        }

        public void Clear()
        {
            //clear method
            hfcontactID.Value = "";
            txtname.Text = txtmobile.Text = txtaddress.Text = "";
            lblErrorMessage.Text = lblSuccessMessage.Text = "";
            btnDelete.Enabled = false;
            btnSave.Text = "Save";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            //This IF- Statement will stop Empty values to stored into the Database.
            if (txtname.Text == "" || txtmobile.Text == "" || txtaddress.Text == "")
            {
                lblSuccessMessage.Visible = false;
                lblErrorMessage.Visible = true;
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                lblErrorMessage.Text = "Pls, Fill in all details!";
              
            }

            //This ELSE- Statement will save data into the Database using the Stored Procedure.
            else
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed)
                    sqlConn.Open();
                SqlCommand sqlcmd = new SqlCommand("ContactCreateorUpdate", sqlConn);
                sqlcmd.CommandType = System.Data.CommandType.StoredProcedure;
                sqlcmd.Parameters.AddWithValue("@ContactID", (hfcontactID.Value == "" ? 0 : Convert.ToInt32(hfcontactID)));
                sqlcmd.Parameters.AddWithValue("@Name", txtname.Text.Trim());
                sqlcmd.Parameters.AddWithValue("@Mobile", txtmobile.Text.Trim());
                sqlcmd.Parameters.AddWithValue("@Address", txtaddress.Text.Trim());
                sqlcmd.ExecuteNonQuery();
                sqlConn.Close();
                Clear();
                if (hfcontactID.Value == "")
                    lblSuccessMessage.Text = "Saved Sucessfully";
                else
                    lblSuccessMessage.Text = "Updated sucessfully";

                //This fill the GridView with details from the Database
                FillGridView();
            }
            
        }

        public void FillGridView()
        {
            if (sqlConn.State == System.Data.ConnectionState.Closed)
                sqlConn.Open();
            SqlDataAdapter sqldata = new SqlDataAdapter("ContactViewAll", sqlConn);
            sqldata.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //Derclaring a datatatable(invisible table)
            System.Data.DataTable dbtl = new System.Data.DataTable();

            //fill contacts or data into the datatable
            sqldata.Fill(dbtl);

            //close connection
            sqlConn.Close();

            //linking Gridview with the datable
            gvContact.DataSource = dbtl;

            //Bindidng the data to the Gridview
            gvContact.DataBind(); 

        }



        protected void Loaddetails()
        {
             try
            {
               //Requesting for ContactID from the Eval Function which Eval requested from database and casts it into string, contactID
                string contactID = Request["ContactID"];
                if (sqlConn.State == System.Data.ConnectionState.Closed)
                    sqlConn.Open();

                SqlDataReader getrecord1 = null;
                SqlCommand cmd = new SqlCommand("ContactViewByID", sqlConn);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "ContactViewByID";

                //the ContactID casted into string contactID is then casted into parameter @ContactID
                cmd.Parameters.Add("@ContactID", System.Data.SqlDbType.VarChar).Value = contactID;

                
                getrecord1 = cmd.ExecuteReader();
                getrecord1.Read();


                //Getting Data from the Database
                if (getrecord1.HasRows == true)
                {
                    if (getrecord1["Name"].Equals(DBNull.Value) == true)
                    {

                        this.txtname.Text = "";

                    }
                    else
                    {

                        this.txtname.Text = getrecord1["Name"].ToString();
                    }



                    if (getrecord1["Mobile"].Equals(DBNull.Value) == true)
                    {

                        this.txtmobile.Text = "";

                    }
                    else
                    {

                        this.txtmobile.Text = getrecord1["Mobile"].ToString();
                    }
                    if (getrecord1["Address"].Equals(DBNull.Value) == true)
                    {

                        this.txtaddress.Text = "";

                    }
                    else
                    {

                        this.txtaddress.Text = getrecord1["Address"].ToString();
                    }

                }

                //Enables the Delete Button
                btnDelete.Enabled = true;
                    
                sqlConn.Close();
            }
            catch (Exception)
            {
                
            
            }
            
        }
       

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string contactID = Request["ContactID"];
            if (sqlConn.State == System.Data.ConnectionState.Closed)
                sqlConn.Open();
            SqlCommand sqlcmd = new SqlCommand("ContactDleteByID", sqlConn);
            sqlcmd.CommandType = System.Data.CommandType.StoredProcedure;
            //sqlcmd.Parameters.AddWithValue("@ContactID", hfcontactID.Value);
            sqlcmd.Parameters.Add("@ContactID", System.Data.SqlDbType.VarChar).Value = contactID;
            sqlcmd.ExecuteNonQuery();
            sqlConn.Close();
            Clear();
            FillGridView();
            lblSuccessMessage.Text = "Deleted Succesfully";
        }
    }
}
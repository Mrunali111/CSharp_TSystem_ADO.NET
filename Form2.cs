using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ADO.NET
{
    public partial class Form2 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder scb;
        public Form2()
        {
            InitializeComponent();
            con = new SqlConnection(@"Server=LAPTOP-3QCGTM38\SQLEXPRESS;Database=PracticeDataBase;Integrated Security=True");
        }

        public DataSet GetAllEmployess()
        {
            da = new SqlDataAdapter("select * from Employee", con);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            scb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "Emp");
            return ds;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            try
            {
                ds = GetAllEmployess();
                // insert new record
                DataRow row = ds.Tables["Emp"].NewRow();
                row["Id"] = txtEmpId.Text;
                row["Name"] = txtEmpName.Text;
                row["Designation"] = txtEmpDesignation.Text;
                row["Salary"] = txtEmpSalary.Text;
                ds.Tables["Emp"].Rows.Add(row);
                int result = da.Update(ds.Tables["Emp"]); // this will reflect the changes in to the DB
                if (result == 1)
                {
                    MessageBox.Show("Inserted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllEmployess();
                // insert new record
                DataRow row = ds.Tables["Emp"].Rows.Find(Convert.ToInt32(txtEmpId.Text));

                row["Name"] = txtEmpName.Text;
                row["Designation"] = txtEmpDesignation.Text;
                row["Salary"] = txtEmpSalary.Text;

                int result = da.Update(ds.Tables["Emp"]); // this will reflect the changes in to the DB
                if (result == 1)
                {
                    MessageBox.Show("updated");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllEmployess();
                // insert new record
                //Id -PK  we can use Find()
                DataRow row = ds.Tables["Emp"].Rows.Find(Convert.ToInt32(txtEmpId.Text));
                if (row != null)
                {
                    txtEmpId.Text = row["Id"].ToString();
                    txtEmpName.Text = row["Name"].ToString();
                    txtEmpDesignation.Text = row["Designation"].ToString();
                    txtEmpSalary.Text = row["Salary"].ToString();
                }
                else
                {
                    MessageBox.Show("Record not found");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllEmployess();
                // insert new record
                DataRow row = ds.Tables["Emp"].Rows.Find(Convert.ToInt32(txtEmpId.Text));
                if (row != null)
                {
                    row.Delete();
                    int result = da.Update(ds.Tables["Emp"]); // this will reflect the changes in to the DB
                    if (result == 1)
                    {
                        MessageBox.Show("deleted");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                //string qry = "select MAX(Id) from Employee";
                //scb = new SqlCommandBuilder();
                //con.Open();
                //object obj = scb.ExecuteScalar();
                //// DBNullis used to check existance of value in the obj
                //if (obj == DBNull.Value)
                //{
                //    txtId.Text = "1";
                //}
                //else

                //{
                //    int id = Convert.ToInt32(obj);//will get the MAX Id from employee
                //    id++;
                //    txtId.Text = id.ToString();

                //}
                //txtId.Enabled = false;//now textbox value cannot 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                con.Close();
            }
        }
    }
}

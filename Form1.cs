using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ADO.NET
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public Form1()
        {
            InitializeComponent();
            con=new SqlConnection(@"Server=LAPTOP-3QCGTM38\SQLEXPRESS;Database=PracticeDataBase;Integrated Security=True");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void ClearData()
        {
            txtEmpId.Clear();
            txtEmpName.Clear();
            txtEmpDesignation.Clear();
            txtEmpSalary.Clear();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "insert into Employee values(@id,@name,@designation,@salary)";
                cmd = new SqlCommand(qry, con);
                con.Open();
                cmd.Parameters.AddWithValue("@id",Convert.ToInt32(txtEmpId.Text));
                cmd.Parameters.AddWithValue("@name",txtEmpName.Text);
                cmd.Parameters.AddWithValue("@designation",txtEmpDesignation.Text);
                cmd.Parameters.AddWithValue("@salary",txtEmpSalary.Text);

                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Sucessfully saved the record");
                    txtEmpId.Enabled = true;
                    ClearData();

                }
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from Employee where Id=@id";
                cmd= new SqlCommand(qry, con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtEmpId.Text));
                dr=cmd.ExecuteReader();
                //first will check that record is present
                if(dr.HasRows)
                {
                    // read the record from dr object
                    if (dr.Read())
                    {
                        txtEmpName.Text = dr["Name"].ToString();
                        txtEmpDesignation.Text = dr["Designation"].ToString();
                        txtEmpSalary.Text = dr["Salary"].ToString();

                    }

                }
                else
                {
                    MessageBox.Show("Record not found..!!");
                }


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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "update Employee set Name=@name,Designation=@designation,Salary=@salary where Id=@id";
                cmd = new SqlCommand(qry, con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtEmpId.Text));
                cmd.Parameters.AddWithValue("@name", txtEmpName.Text);
                cmd.Parameters.AddWithValue("@designation", txtEmpDesignation.Text);
                cmd.Parameters.AddWithValue("@salary", txtEmpSalary.Text);

                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Sucessfully updated the record");
                    ClearData();

                }
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "delete from Employee where Id=@id";
                cmd = new SqlCommand(qry, con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtEmpId.Text));
                cmd.Parameters.AddWithValue("@name", txtEmpName.Text);
                cmd.Parameters.AddWithValue("@designation", txtEmpDesignation.Text);
                cmd.Parameters.AddWithValue("@salary", txtEmpSalary.Text);

                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Sucessfully deleted the record");
                    ClearData();

                }
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

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select Max(Id) from Employee";
                cmd = new SqlCommand(qry, con);
                con.Open();
                object obj = cmd.ExecuteScalar();
                if (obj == DBNull.Value)
                {
                    txtEmpId.Text = "1";
                }
                else
                {

                    int id = Convert.ToInt32(obj);
                    id++;
                    txtEmpId.Text = id.ToString();
                }
                txtEmpId.Enabled = false;
                
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



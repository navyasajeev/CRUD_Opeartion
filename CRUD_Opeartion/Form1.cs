using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace CRUD_Opeartion
{
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-ISJ78FR1\SQLEXPRESS;Initial Catalog=FirstApplication;Integrated Security=True;Encrypt=False");
        public int CustomerID;
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetCustomerRecord();
        }

        private void GetCustomerRecord()
        {
            SqlCommand cmd = new SqlCommand("Select * from Customer", con);
            DataTable dt = new DataTable();

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            CustomerDataGridView.DataSource = dt;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(IsValid())
            {
                SqlCommand cmd = new SqlCommand("INSERT into Customer VALUES(@Firstname,@lastname,@Address,@Phonenumber,@State)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Firstname", txtFirstName.Text);
                cmd.Parameters.AddWithValue("@Lastname", txtLastName.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Phonenumber", txtPhone.Text);
                cmd.Parameters.AddWithValue("@State", txtState.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("New customer is sucessfully saved", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetCustomerRecord();
                ResetFormControls();
            }

        }

        private bool IsValid()
        {
            if (txtFirstName.Text == string.Empty)
            {
                MessageBox.Show("First Name is Required", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ResetFormControls();

        }

        private void ResetFormControls()
        {
            CustomerID = 0;
            txtFirstName.Clear();
            txtLastName.Clear();
            txtAddress.Clear();
            txtPhone.Clear();
            txtState.Clear();

            txtFirstName.Focus();
        }

        private void CustomerDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CustomerID = Convert.ToInt32(CustomerDataGridView.SelectedRows[0].Cells[0].Value);
            txtFirstName.Text = CustomerDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            txtLastName.Text = CustomerDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            txtAddress.Text = CustomerDataGridView.SelectedRows[0].Cells[3].Value.ToString();
            txtPhone.Text = CustomerDataGridView.SelectedRows[0].Cells[4].Value.ToString();
            txtState.Text = CustomerDataGridView.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(CustomerID > 0)
            {
                SqlCommand cmd = new SqlCommand("UPDATE Customer SET Firstname=@Firstname, Lastname=@lastname,Address=@Address,Phonenumber=@Phonenumber,State=@State WHERE CustomerID=@ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Firstname", txtFirstName.Text);
                cmd.Parameters.AddWithValue("@Lastname", txtLastName.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Phonenumber", txtPhone.Text);
                cmd.Parameters.AddWithValue("@State", txtState.Text);
                cmd.Parameters.AddWithValue("@ID",this.CustomerID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Updated Successfully", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetCustomerRecord();
                ResetFormControls();
            }
            else
            {
                MessageBox.Show("Please select a customer to update", "Select?", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(CustomerID >0)
            {

                SqlCommand cmd = new SqlCommand("DELETE FROM Customer  WHERE CustomerID=@ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", this.CustomerID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Deleted Successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetCustomerRecord();
                ResetFormControls();
            }
            else
            {
                MessageBox.Show("Please select a customer to delete", "Select?", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}

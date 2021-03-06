using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DGVPrinterHelper;

namespace CafeMangement.AllUserControl
{
    public partial class UC_PlaceOrder : UserControl
    {

        function fn = new function();
        String query;


        public UC_PlaceOrder()
        {
            InitializeComponent();
        }

        private void comboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            String category = comboCategory.Text;
            query = "select name from items where category ='" + category + "'";
            showItemList(query);
      
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            

            String category = comboCategory.Text;
            query = "select name from items where category ='" + category + "' and name like '"+txtSearch.Text+"%'";
            showItemList(query);
        }

        private void showItemList(string query)
        {
            listBox1.Items.Clear();

            DataSet ds = fn.getData(query);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                listBox1.Items.Add(ds.Tables[0].Rows[i][0].ToString());
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtQuantityUpDown.ResetText();
            txtTotal.Clear();

            String text = listBox1.GetItemText(listBox1.SelectedItem);
            txtItemName.Text = text;

            query = "select price from items where name = '" + text + "'";
            DataSet ds = fn.getData(query);
            try
            {
                txtPrice.Text = ds.Tables[0].Rows[0][0].ToString();
            }
            catch { }
        }

        private void txtQuantityUpDown_ValueChanged(object sender, EventArgs e)
        {
            Int64 quan = Int64.Parse(txtQuantityUpDown.Value.ToString());
            Int64 price = Int64.Parse(txtPrice.Text);
            txtTotal.Text = (quan * price).ToString();
        }


        protected int n, total = 0;
        private void btnAddToCart_Click(object sender, EventArgs e)
        {

            if (txtTotal.Text != "0" && txtTotal.Text != "")
            {
                n = guna2DataGridView2.Rows.Add();
                guna2DataGridView2.Rows[n].Cells[0].Value = txtItemName.Text;
                guna2DataGridView2.Rows[n].Cells[1].Value = txtPrice.Text;
                guna2DataGridView2.Rows[n].Cells[2].Value = txtQuantityUpDown.Value;
                guna2DataGridView2.Rows[n].Cells[3].Value = txtTotal.Text;

                total += int.Parse(txtTotal.Text);
                labelTotalAmount.Text = "Rs. " + total;
            }

            else
            {
                MessageBox.Show("Minimum Quantity need to be ONE !!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        int amount;

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                guna2DataGridView2.Rows.RemoveAt(this.guna2DataGridView2.SelectedRows[0].Index);
            }
            catch { }
            total -= amount;
            labelTotalAmount.Text = "Rs. " + total;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DGVPrinter printer = new DGVPrinter();
            printer.Title = "Customer Bill.";
            printer.SubTitle = String.Format("Date: ", DateTime.Now.Date);
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.Footer = "Total Payable Amount : " + labelTotalAmount;
            printer.FooterSpacing = 15;
            printer.PrintDataGridView(guna2DataGridView2);

            total = 0;
            guna2DataGridView2.Rows.Clear();
            labelTotalAmount.Text = "Rs. " + total;
        }

        private void guna2DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                amount = int.Parse(guna2DataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString());
            }
            catch { }
        }
    }
}

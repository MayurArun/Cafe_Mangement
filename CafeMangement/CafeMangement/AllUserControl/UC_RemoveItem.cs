﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeMangement.AllUserControl
{
    public partial class UC_RemoveItem : UserControl
    {
        function fn = new function();
        String query;
        public UC_RemoveItem()
        {
            InitializeComponent();
        }

        private void UC_RemoveItem_Load(object sender, EventArgs e)
        {
            DelLabel.Text = "How to  DELETE ?";
            DelLabel.ForeColor = Color.Red;
            loadData();
        }

        public void loadData()
        {
            query = "select * from items";
            DataSet ds = fn.getData(query);
            guna2DataGridView1.DataSource = ds.Tables[0];
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            query = "select * from items where name like '" + txtSearch.Text + "%' ";
            DataSet ds = fn.getData(query);
            guna2DataGridView1.DataSource = ds.Tables[0];
            
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (MessageBox.Show("Delete item ?", "Important", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)==DialogResult.Yes);
            {
                int id = int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                query = "delete from items where iid=" + id + "";
                fn.setData(query);
                loadData();
            }
        }

        private void DelLabel_Click(object sender, EventArgs e)
        {
            DelLabel.ForeColor = Color.Blue;
            DelLabel.Text = "*Click on Row to Delete the Item. ";
        }

        private void UC_RemoveItem_Enter(object sender, EventArgs e)
        {
            loadData();
        }
    }
}

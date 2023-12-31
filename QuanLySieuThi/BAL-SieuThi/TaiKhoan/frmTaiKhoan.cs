﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySieuThi
{
   

    public partial class frmTaiKhoan : Form
    {
        public string chuoi = "select username, password, fullname ,datecreate, isAdmin from taikhoan";
        public frmTaiKhoan()
        {
            InitializeComponent();
            Dbconection.Chuoiketnoi(chuoi, dta1);
            dta1.Columns[0].HeaderText = "Tài khoản"; dta1.Columns[0].Width = 90;
            dta1.Columns[1].HeaderText = "Mật khẩu"; dta1.Columns[1].Width = 90;
            dta1.Columns[2].HeaderText = "Họ và tên"; dta1.Columns[2].Width = 110;
            dta1.Columns[3].HeaderText = "Ngày tạo"; dta1.Columns[3].Width = 110;
            dta1.Columns[4].HeaderText = "Chức vụ"; dta1.Columns[4].Width = 70;

            cbbRole.SelectedIndex = 0;
            
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            string uername = txt_tk.Text.Trim();
            string password = txt_mk.Text.Trim();
            string fullname = txtFullname.Text.Trim();
            string date = datecreate.Text.Trim();
            
            
            string role = cbbRole.SelectedIndex.ToString();
            if (txt_tk.Text == "" || txt_mk.Text == "" || fullname=="")
            {
                MessageBox.Show("Bạn chưa nhập đầy đủ thông tin! Vui lòng kiểm tra lại ", "Error", MessageBoxButtons.OK);
            }
            else
            {
                try
                {               
                    string select = "select count(*) from taikhoan where username='" + txt_tk.Text + "'";
                    string them1 = "insert into taikhoan Values ('" + uername + "','" + password + "','" + fullname + "','" + date + "','" + role + "')";
                    Dbconection.Them(select, txt_tk.Text.Trim(), txt_mk.Text.Trim(), them1, dta1);
                    Dbconection.Chuoiketnoi(chuoi, dta1);                  
                    dta1.Columns[0].HeaderText = "Tài khoản"; 
                    dta1.Columns[1].HeaderText = "Mật khẩu"; 
                    dta1.Columns[2].HeaderText = "Họ và tên"; 
                    dta1.Columns[3].HeaderText = "Ngày tạo";
                    dta1.Columns[4].HeaderText = "Chức vụ";
                    txt_tk.Clear();
                    txt_mk.Clear();
                    txtFullname.Clear();
                    cbbRole.SelectedIndex = 0;
                   

                    txt_tk.Focus();
                    btn_them.Enabled = true;
                    btn_xoa.Enabled = false;
                    btn_sua.Enabled = false;
                }
                catch
                {
                    MessageBox.Show("Tài khoản đã tồn tại! Vui lòng nhập lại", "Error", MessageBoxButtons.OKCancel);
                }
            }
        }

        //private void btn_nhaplai_Click(object sender, EventArgs e)
        //{
        //    txt_tk.Clear();
        //    txt_mk.Clear();
        //    txtFullname.Clear();
        //    cbbRole.SelectedIndex = 0;
        //    txt_tk.Focus();
        //    txt_tk.Enabled = true;
        //    btn_them.Enabled = true;
        //    btn_xoa.Enabled = false;
        //    btn_sua.Enabled = false;
        //}

        private void btn_sua_Click(object sender, EventArgs e)
        {
            string username = txt_tk.Text.Trim();
            string password = txt_mk.Text.Trim();
            string fullname = txtFullname.Text.Trim();
            string date = datecreate.Text.Trim();
            string role = cbbRole.SelectedIndex.ToString();

            string sql = "Update taikhoan set password ='" + password + "', fullname='"+fullname + "', isAdmin='" + role +
                "' where username = '" + txt_tk.Text + "'";
            Dbconection.Execute1(sql);
            Dbconection.Chuoiketnoi(chuoi, dta1);
            
            dta1.Columns[0].HeaderText = "Tài khoản";
            dta1.Columns[1].HeaderText = "Mật khẩu";
            dta1.Columns[2].HeaderText = "Họ và tên";
            dta1.Columns[3].HeaderText = "Ngày tạo";
            dta1.Columns[4].HeaderText = "Chức vụ";
            txt_tk.Clear();
            txt_mk.Clear();
            txtFullname.Clear();
            txt_tk.Focus();
            txt_tk.Enabled = true;
            btn_them.Enabled = true;
            btn_xoa.Enabled = false;
            btn_sua.Enabled = false;

        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            string sql = "Delete from taikhoan  where username = '" + txt_tk.Text.Trim() + "'";
            Dbconection.Execute(sql);
            // MessageBox.Show("Bạn xóa thành công ! ", "Thông báo", MessageBoxButtons.OK);
            Dbconection.Chuoiketnoi(chuoi, dta1);
            dta1.Columns[0].HeaderText = "Tài khoản";
            dta1.Columns[1].HeaderText = "Mật khẩu";
            dta1.Columns[2].HeaderText = "Họ và tên";
            dta1.Columns[3].HeaderText = "Ngày tạo";
            dta1.Columns[4].HeaderText = "Chức vụ";
            txt_tk.Clear();
            txt_mk.Clear();
            txtFullname.Clear();
            txt_tk.Focus();
            txt_tk.Enabled = true;
            btn_them.Enabled = true;
            btn_xoa.Enabled = false;
            btn_sua.Enabled = false;
        }

        
        private void dta1_Click(object sender, EventArgs e)
        {
            int curow = dta1.CurrentRow.Index;
            txt_tk.Text = dta1.Rows[curow].Cells[0].Value.ToString();
            txt_mk.Text = dta1.Rows[curow].Cells[1].Value.ToString();
            txtFullname.Text = dta1.Rows[curow].Cells[2].Value.ToString();
            cbbRole.Text = dta1.Rows[curow].Cells[4].Value.ToString();
            txt_tk.Enabled = false;
            btn_them.Enabled = false;
            btn_sua.Enabled = true;
            btn_xoa.Enabled = true;
            this.dta1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Bạn có chắc chắn muốn thoát không ?", "Thông báo ", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void tkadmin_Load(object sender, EventArgs e)
        {

        }

        private void dta1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dta1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void datecreate_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}

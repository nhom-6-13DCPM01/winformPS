using PetShopWinform.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetShopWinform.Forms
{
    public partial class Admin : Form
    {
        private PetshopWinformEntities DBPetShop = new PetshopWinformEntities();
        public Admin()
        {
            InitializeComponent();
            loadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thêm", "Thông báo", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                Account ac = new Account();
                ac.UserName = txtUsername.Text;
                ac.PassWord = txtPassword.Text;
                ac.Role = Convert.ToInt32(cbRole.Text);
                ac.DisplayName = txtDisplayname.Text;
                DBPetShop.Accounts.Add(ac);
                DBPetShop.SaveChanges();
                loadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn sửa", "Thông báo", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                int maAccount = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                Account ac = DBPetShop.Accounts.Where(c => c.Id == maAccount).First();
                ac.UserName = txtUsername.Text;
                ac.PassWord = txtPassword.Text;
                ac.Role = Convert.ToInt32(cbRole.Text);
                ac.DisplayName = txtDisplayname.Text;
                DBPetShop.SaveChanges();
                loadData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int maAccount = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            Account ac = DBPetShop.Accounts.Where(c => c.Id == maAccount).First();
            DBPetShop.Accounts.Remove(ac);
            DBPetShop.SaveChanges();
            loadData();
        }

        public void loadData()
        {
            dataGridView1.DataSource = (from u in DBPetShop.Accounts select new { id = u.Id, Name = u.DisplayName, password = u.PassWord, role = u.Role }).ToList();
        }
        private void Admin_Load(object sender, EventArgs e)
        {
            LoadTheme();
        }
        private void LoadTheme()
        {
            loadData();
            foreach (Control btns in this.Controls)
            {
                if (btns.GetType() == typeof(Button))
                {
                    Button btn = (Button)btns;
                    btn.BackColor = ThemeColor.PrimaryColor;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
                }
            }
            label5.ForeColor = ThemeColor.SecondaryColor;

        }
    }

}

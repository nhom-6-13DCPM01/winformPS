using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PetShopWinform.BUS;
using PetShopWinform.DAO;
using PetShopWinform.Model;

namespace PetShopWinform
{
    public partial class FormLogin : Form
    {
        private PetshopWinformEntities DBPetShop = new PetshopWinformEntities();
        public FormLogin()
        {
            InitializeComponent();
        }

        private void labelClearAll_Click(object sender, EventArgs e)
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtUsername.Focus();

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            Login(username, password);
                
        }
        private void Login(String username, string password)
        {
            /*var accCount = DBPetShop.Accounts.Where(a => a.UserName == username && a.PassWord == password).Count();
            if (accCount > 0)
            {
                FormMainMenu f = new FormMainMenu();
                this.Hide();
                f.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("The User name or Password you is incrorrect, try again");
            }*/

            var acc = DBPetShop.Accounts.Where(a => a.UserName.Equals(username)).ToList();
            if (acc.Count() > 0)
            {
                if (acc[0].PassWord.Equals(password))
                {
                    Const.Role = Convert.ToBoolean(acc[0].Role);
                    FormMainMenu f = new FormMainMenu();
                    this.Hide();
                    f.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("The User name or Password you is incrorrect, try again");
                }
            }
            else
            {
                MessageBox.Show("The User name or Password you is incrorrect, try again");
            }
        }
    }
}

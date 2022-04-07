using PetShopWinform.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetShopWinform.Forms
{
    public partial class Products : Form
    {

        #region Khai báo biến
        Product pr = new Product();
        PetshopWinformEntities db = new PetshopWinformEntities();
        #endregion

        #region Các hàm tạo
        public Products()
        {
            InitializeComponent();

        }
        #endregion

        #region Các xử lý

        private void Products_Load(object sender, EventArgs e)
        {
            LoadTheme();
            LoadData();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddProduct addProduct = new AddProduct();
            addProduct.ShowDialog();
            LoadData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to Edit?", "EF CRUP Operation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                EditProduct editProduct = new EditProduct(Convert.ToInt32(txtId.Text));
                editProduct.ShowDialog();
                Clear();
                LoadData();
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure to Delete? this product with name \n "+txtName.Text, "EF CRUP Operation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                int id = Convert.ToInt32(dgvProductList.CurrentRow.Cells[0].Value);
                Product pr = db.Products.Where(x => x.Id == id).First();
                IEnumerable<OrderInfo> orderInfos = db.OrderInfoes.Where(c => c.IdProduct == pr.Id).ToList();
                List<Oder> oders = new List<Oder>();
                foreach (OrderInfo item in orderInfos)
                {
                    Oder oder = db.Oders.SingleOrDefault(c => c.Id == item.IdOrder);
                    if (oders.SingleOrDefault(c => c.Id == oder.Id) == null)
                    {
                        oders.Add(oder);
                    }
                    db.OrderInfoes.Remove(item);
                    db.SaveChanges();
                }
                IEnumerable<Oder> oders1 = oders;
                foreach (Oder item in oders1)
                {
                    IEnumerable<OrderInfo> orderInfos1 = db.OrderInfoes.Where(c => c.IdOrder == item.Id).ToList();
                    foreach (OrderInfo item1 in orderInfos1)
                    {
                        db.OrderInfoes.Remove(item1);
                        db.SaveChanges();
                    }
                    db.Oders.Remove(item);
                    db.SaveChanges();
                }
                db.Products.Remove(pr);

                db.SaveChanges();
                LoadData();

                MessageBox.Show("Submit Successfully!");
                Clear();

            }
            else
            {
                Clear();
            }
        }

        private void btnReLoad_Click(object sender, EventArgs e)
        {
            LoadData();

        }

        private void dgvProductList_DoubleClick(object sender, EventArgs e)
        {
            if (dgvProductList.CurrentRow.Index != -1)
            {
                var id = Convert.ToInt32(dgvProductList.CurrentRow.Cells[0].Value);
                Product pr = db.Products.Where(x => x.Id == id).FirstOrDefault();
                txtId.Text = pr.Id.ToString();
                txtName.Text = pr.Name;
                cbCategory.SelectedValue = pr.Category;
                txtQuantity.Text = pr.Quantity.ToString();
                txtPrice.Text = pr.Price.ToString();

                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*var results = db.Products.Where(p => p.Name.Contains(txtSearch.Text));*/
            var results = (from c in db.Products where c.Name.Contains(txtSearch.Text) select new { Id = c.Id, Name = c.Name, Category = c.Category1.Name, Quantity = c.Quantity, Price = c.Price });
            dgvProductList.DataSource = results.ToList();
        }
        #endregion

        #region Các phương thức
        private void LoadTheme()
        {
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
            label6.ForeColor = ThemeColor.PrimaryColor;
        }
        bool CheckInput()
        {
            long result;
            String quantity = txtQuantity.Text;
            String price = txtPrice.Text;
            if (txtName.Text == "")
            {
                MessageBox.Show("Enter Name, please", "Notification");
                txtName.Focus();
                return false;
            }

            if (txtQuantity.Text == "")
            {
                MessageBox.Show("Enter Quantity, please", "Notification");
                txtQuantity.Focus();
                return false;
            }

            if (txtPrice.Text == "")
            {
                MessageBox.Show("Enter Price, please", "Notification");
                txtPrice.Focus();
                return false;
            }
            //SL ko được nhập chữ
            if (!(long.TryParse(quantity, out result)))
            {
                MessageBox.Show("Please enter the Quantity in correct format", "Notification");
                txtQuantity.Focus();
                return false;
            }
            //Số lượng ko được âm
            if (result < 0)
            {
                MessageBox.Show("Quantity cannot be negative value", "Notification");
                txtQuantity.Focus();
                return false;
            }
            //Giá tiền ko đc nhập chữ
            if (!(long.TryParse(price, out result)))
            {
                MessageBox.Show("Please enter the Price in correct format", "Notification");
                txtPrice.Focus();
                return false;
            }

            if (result < 0)
            {
                MessageBox.Show("Price cannot be negative value", "Notification");
                txtPrice.Focus();
                return false;
            }
            return true;
        }
        private void LoadData()
        {
            var result = from c in db.Products select new { Id = c.Id, Name = c.Name, Category = c.Category1.Name, Quantity = c.Quantity, Price = c.Price };
            dgvProductList.DataSource = result.ToList();
            cbCategory.DataSource = db.Categories.ToList();
            cbCategory.DisplayMember = "Name";
            cbCategory.ValueMember = "Id";
        }
        void Clear()
        {
            txtId.Text = txtName.Text = txtQuantity.Text = txtPrice.Text = "";
            btnDelete.Enabled = false;
            btnEdit.Enabled = false;
            pr.Id = 0;
        }
        #endregion

    }
}

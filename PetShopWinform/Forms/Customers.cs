using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PetShopWinform.Model;

namespace PetShopWinform.Forms
{
    public partial class Customers : Form
    {
        PetshopWinformEntities db = new PetshopWinformEntities();

        public Customers()
        {
            InitializeComponent();
        }

<<<<<<< HEAD
        private void Customers_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            dgvCustomerList.DataSource = (from c in db.Customers select new { Id = c.Id, Name = c.Name, Address = c.Address, Phone = c.Phone, Vip = c.Vip == true ? "Có" : "Không" }).ToList();
            cbVip.DataSource = Vip.getVips().ToList();
            cbVip.DisplayMember = "vip";
            cbVip.ValueMember = "value";
        }

        void Clear()
        {
            txtId.Text = txtName.Text = txtAddress.Text = txtPhone.Text = "";
            cbVip.SelectedIndex = 1;
        }

        public bool xacNhan(string Message)
        {
            if (MessageBox.Show(Message, "EF CRUP Operation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                return true;
            }
            return false;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                if (xacNhan("Bạn có muốn thêm") == false)
                {
                    return;
                }
                Customer customer = new Customer();
                customer.Name = txtName.Text;
                customer.Address = txtAddress.Text;
                customer.Phone = txtPhone.Text;
                customer.Vip = Convert.ToBoolean(cbVip.SelectedValue);
                db.Customers.Add(customer);
                db.SaveChanges();
                Clear();
                LoadData();
            }
        }



        bool CheckInput()
        {
            long result;
            String phone = txtPhone.Text;
            if (txtName.Text == "")
            {
                MessageBox.Show("Enter Name, please", "Notification");
                txtName.Focus();
                return false;
            }

            if (txtAddress.Text == "")
            {
                MessageBox.Show("Enter Address, please", "Notification");
                txtAddress.Focus();
                return false;
            }

            //SL ko được nhập chữ
            if (!(long.TryParse(phone, out result)))
            {
                MessageBox.Show("Please enter the Phone in correct format", "Notification");
                txtPhone.Focus();
                return false;
            }
            return true;
        }

        private void dgvCustomerList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(dgvCustomerList.Rows[e.RowIndex].Cells[0].Value);
                Customer customer = db.Customers.SingleOrDefault(c => c.Id == id);
                txtId.Text = Convert.ToString(customer.Id);
                txtName.Text = customer.Name;
                txtAddress.Text = customer.Address;
                txtPhone.Text = customer.Phone;
                cbVip.SelectedValue = customer.Vip;
            }
            catch (Exception)
            {

            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(txtId.Text)))
            {
                return;
            }
            if (CheckInput())
            {
                if (xacNhan("Bạn có muốn Chỉnh sửa khách hàng có Id: "+txtId.Text) == false)
                {
                    return;
                }
                int id = Convert.ToInt32(txtId.Text);
                Customer customer = db.Customers.SingleOrDefault(c => c.Id == id);
                customer.Name = txtName.Text;
                customer.Address = txtAddress.Text;
                customer.Phone = txtPhone.Text;
                customer.Vip = Convert.ToBoolean(cbVip.SelectedValue);
                db.SaveChanges();
                Clear();
                LoadData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(txtId.Text)))
            {
                return;
            }
            if (xacNhan("Bạn có muốn Xóa khách hàng có Id: " + txtId.Text) == false)
            {
                return;
            }
            int id = Convert.ToInt32(txtId.Text);
            Customer customer = db.Customers.SingleOrDefault(c => c.Id == id);
            IEnumerable<Oder> oders = db.Oders.Where(c => c.Customer == customer.Id).ToList();
            foreach (Oder item in oders)
            {
                IEnumerable<OrderInfo> orderInfos = db.OrderInfoes.Where(c => c.IdOrder == item.Id).ToList();
                foreach (OrderInfo info in orderInfos)
                {
                    db.OrderInfoes.Remove(info);
                    db.SaveChanges();
                }
                db.Oders.Remove(item);
                db.SaveChanges();
            }
            db.Customers.Remove(customer);
            db.SaveChanges();
            Clear();
            LoadData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                LoadData();
                return;
            }
            int id;
            if (Int32.TryParse(txtSearch.Text, out id))
            {
                dgvCustomerList.DataSource = (from c in db.Customers
                                              where c.Id == id
                                              select new
                                              {
                                                  Id = c.Id,
                                                  Name = c.Name,
                                                  Address = c.Address,
                                                  Phone = c.Phone,
                                                  Vip = c.Vip == true ? "Có" : "Không"
                                              }).ToList();
                return;
            }
            dgvCustomerList.DataSource = (from c in db.Customers
                                          where c.Name.Contains(txtSearch.Text)
                                          select new
                                          {
                                              Id = c.Id,
                                              Name = c.Name,
                                              Address = c.Address,
                                              Phone = c.Phone,
                                              Vip = c.Vip == true ? "Có" : "Không"
                                          }).ToList();
        }

        private void btnReLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }
=======
        private void btnAdd_Click(object sender, EventArgs e)
        {

        }
>>>>>>> origin/login
    }
}

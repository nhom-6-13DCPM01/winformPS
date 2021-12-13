using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetShopWinform.DAO;
using System.Windows.Forms;

namespace PetShopWinform.BUS
{
    class Statistical_BUS
    {
        private Statistical_DAO danhSachThongKe;

        public Statistical_BUS() { danhSachThongKe = new Statistical_DAO(); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="view"></param>
        public void layDanhSachThongKe(DataGridView view)
        {
            view.DataSource = danhSachThongKe.layDanhSachThongKe();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="view"></param>
        /// <param name="ngayBatDau"></param>
        /// <param name="ngayKetThuc"></param>
        public void layDanhSachThongKeTheoNgay(DataGridView view, DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            view.DataSource = danhSachThongKe.locDanhSachTheoNgay(ngayBatDau, ngayKetThuc);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="view"></param>
        /// <param name="tuKhoa"></param>
        public void timHoaDon(DataGridView view, String tuKhoa)
        {
            view.DataSource = danhSachThongKe.timKiemDanhSach(tuKhoa);
        }
    }
}

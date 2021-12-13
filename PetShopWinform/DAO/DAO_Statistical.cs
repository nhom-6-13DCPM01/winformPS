﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetShopWinform.Model;

namespace PetShopWinform.DAO
{
    class DAO_Statistical
    {
        private PetshopWinformEntities DBPetShop;

        public DAO_Statistical() { DBPetShop = new PetshopWinformEntities(); }

        /// <summary>
        /// Lấy ra danh sách hóa đơn khách hàng từ database
        /// dynamic là kiểu dữ liệu không thể xác định và chỉ được xác định khi chương trình được thực thi
        /// </summary>
        /// <returns>danh sách cần để hiển thị thống kê</returns>
        public dynamic layDanhSachThongKe()
        {
            var danhSach = (from u in DBPetShop.Oders
                            select new
                            {
                                Id = u.Id,
                                Date = u.DateCreate,
                                Status = u.Status,
                                Total = u.OrderInfoes.Select(c => c.Total).Sum(),
                                Customer = u.Customer1.Name
                            }).ToList();
            return danhSach;
        }

        /// <summary>
        /// Dùng để lọc danh sách theo ngày đã chỉ  định từ ngày bắt đầu đến ngày kết thúc
        /// dynamic là kiểu dữ liệu không thể xác định và chỉ được xác định khi chương trình được thực thi
        /// </summary>
        /// <param name="ngayBatDau">Kiểu DateTime</param>
        /// <param name="ngayKetThuc">Kiểu DateTime</param>
        /// <returns>Danh sách đã lọc</returns>
        public dynamic locDanhSachTheoNgay(DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            var danhSach = (from u in DBPetShop.Oders
                            where ngayBatDau >= u.DateCreate && u.DateCreate <= ngayKetThuc
                            select new
                            {
                                Id = u.Id,
                                Date = u.DateCreate,
                                Status = u.Status,
                                Customer = u.Customer1.Name,
                                Total = u.OrderInfoes.Select(c => c.Total).Sum()
                            }).ToList();
            return danhSach;
        }

        /// <summary>
        /// Dùng để tìm ra hóa đơn cần thiết
        /// </summary>
        /// <param name="tuKhoa">Để tìm kiếm kiểu String</param>
        /// <returns>Danh sách đã tìm được</returns>
        public dynamic timKiemDanhSach(String tuKhoa)
        {
            var c = Convert.ToChar(tuKhoa.ElementAt(1));
            if (c >= 48 && c <= 57)
            {
                var danhSach = (from u in DBPetShop.Oders
                                where u.Id == Convert.ToInt32(tuKhoa)
                                select new
                                {
                                    Id = u.Id,
                                    Date = u.DateCreate,
                                    Status = u.Status,
                                    Customer = u.Customer1.Name,
                                    Total = u.OrderInfoes.Select(a => a.Total).Sum()
                                }).ToList();
                return danhSach;
            }
            else
            {
                var danhSach = (from u in DBPetShop.Oders
                                where u.Customer1.Name == tuKhoa
                                select new
                                {
                                    Id = u.Id,
                                    Date = u.DateCreate,
                                    Status = u.Status,
                                    Customer = u.Customer1.Name,
                                    Total = u.OrderInfoes.Select(b => b.Total).Sum()
                                }).ToList();
                return danhSach;
            }
        }
    }
}
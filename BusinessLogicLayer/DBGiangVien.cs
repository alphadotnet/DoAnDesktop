﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Data;
using System.Data.SqlClient;
using MySqlConnector;

namespace BusinessLogicLayer
{
    public class DBGiangVien
    {
        private DAL db;
        public DBGiangVien()
        {
            db = new DAL();
        }

        public void SinhVienConnect()
        {
            try
            {
                db.changeStrConnectToSinhVien();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void GiangVienConnect()
        {
            try
            {
                db.changeStrConnectToGiangVien();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public DataSet DSGiangVien()
        {
            try
            {
                return db.ExecuteQueryDataSet("NonP_DanhSachGV", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet ThongTinGV(string magv)
        {
            try
            {
                return db.ExecuteQueryDataSet($"CALL RTO_ThongTinGV('{magv}')", CommandType.Text);
            } 
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ThemGV(ref string err, string TenDangNhap, string MatKhau, string HoTenGV, string MaKhoa)
        {
            try
            {
                MySqlParameter[] parameters =
                {
                    new MySqlParameter("@TenDangNhap", TenDangNhap),
                    new MySqlParameter("@MatKhau", MatKhau),
                    new MySqlParameter("@HoTenGV", HoTenGV),
                    new MySqlParameter("@MaKhoa", MaKhoa)
                };
                return db.MyExecuteNonQuery($"CALL Re_ThemGiangVien('{TenDangNhap}','{MatKhau}','{HoTenGV}','{MaKhoa}')", CommandType.Text, ref err, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool XoaGV(ref string err, string magv)
        {
            try
            {
                return db.MyExecuteNonQuery("Re_XoaGiangVien", CommandType.StoredProcedure,
                ref err, new MySqlParameter("p_MaGV", magv));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

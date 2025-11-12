using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public enum ChucVu
    {
        TruongNhom,
        PhoNhom,
        CongNhanBac3,
        CongNhanBac2,
        CongNhanBac1,
        Khac
    }

    internal class CONGNHAN:Person
    {
        private string maCn;
        private ChucVu chucVu;

        public string MaCn
        {
            get { return maCn; }
            set { maCn = value; }
        }

        public ChucVu ChucVu
        {
            get { return chucVu; }
            set { chucVu = value; }
        }

        public CONGNHAN() : base() 
        {
        }

        public CONGNHAN(string hoTen, int tuoi, string diaChi, string maCn, ChucVu chucVu)
            : base(hoTen, tuoi, diaChi) 
        {
            this.maCn = maCn;
            this.chucVu = chucVu;
        }

        public double TinhLuong()
        {
            double heSoLuong;
            const double LUONG_CO_SO = 8000000;

            heSoLuong = this.chucVu switch
            {
                ChucVu.TruongNhom => 3.0,
                ChucVu.PhoNhom => 2.5,
                ChucVu.CongNhanBac3 => 2.0,
                ChucVu.CongNhanBac2 => 1.5,
                ChucVu.CongNhanBac1 => 1.2,
            };

            return heSoLuong * LUONG_CO_SO;
        }
    }
}

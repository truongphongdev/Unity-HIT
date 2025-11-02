
using System;
using System.Collections.Generic;
using System.Linq;

namespace BTVN
{
    struct SanPham
    {
        public string Ten { get; set; }
        public double Gia { get; set; }
        public int SoLuong { get; set; }
        public string DanhMuc { get; set; }

        public SanPham(string ten, double gia, int soLuong, string danhMuc)
        {
            Ten = ten;
            Gia = gia;
            SoLuong = soLuong;
            DanhMuc = danhMuc;
        }

        public double TinhDoanhThu()
        {
            return Gia * SoLuong;
        }

        public override string ToString()
        {
            return $"Tên: {Ten}, Giá: {Gia}, Số lượng: {SoLuong}, Danh mục: {DanhMuc}, Doanh thu: {TinhDoanhThu()}";
        }
    }

    internal class Program
    {
        // bai 1
        public static string Bai1(string a)
        {
            if (a.Length == 0) return "No";
            Stack<char> stack = new Stack<char>();

            foreach (char s in a)
            {
                if (s == '(' || s == '{' || s == '[')
                {
                    stack.Push(s);
                }
                else if (s == ')' || s == '}' || s == ']')
                {
                    if (stack.Count == 0)
                    {
                        return "No";
                    }

                    char c = stack.Pop();
                    if (
                        (s == ')' && c != '(') ||
                        (s == ']' && c != '[') ||
                        (s == '}' && c != '{')
                        )
                    {
                        return "No";
                    }
                }
            }
            return "Yes";
        }

        // bai 2
        public static string Bai2(string s)
        {
            char[] c = new char[s.Length];
            int n = s.Length;
            for (int i = 0; i < n; i++)
            {
                c[i] = s[i];

                int left = 0;
                int right = n - 1;
                while (left < right)
                {
                    char tmp = c[left];
                    c[left] = c[right];
                    c[right] = tmp;
                    left++;
                    right--;
                }
            }
            string a = new string(c);
            return a;
        }




        // bai 3


        // Cấu trúc dữ liệu chính
        public static Dictionary<string, SanPham> danhSachSanPham = new Dictionary<string, SanPham>();

        // Thêm hoặc cập nhật sản phẩm
        public static void ThemHoacCapNhatSanPham(string ma, string ten, double gia, int soLuong, string danhMuc)
        {
            if (danhSachSanPham.ContainsKey(ma))
            {
                SanPham sp = danhSachSanPham[ma];
                sp.SoLuong += soLuong; // cộng dồn số lượng bán
                danhSachSanPham[ma] = sp;
            }
            else
            {
                danhSachSanPham[ma] = new SanPham(ten, gia, soLuong, danhMuc);
            }
        }

        // Lấy thông tin sản phẩm theo mã
        public static SanPham? LaySanPhamTheoMa(string ma)
        {
            if (danhSachSanPham.ContainsKey(ma))
                return danhSachSanPham[ma];
            return null;
        }

        // Sản phẩm bán chạy nhất (theo số lượng)
        static SanPham SanPhamBanChayNhat()
        {
            return danhSachSanPham.Values.OrderByDescending(sp => sp.SoLuong).First();
        }

        // Sản phẩm bán chạy nhất theo danh mục
        static SanPham SanPhamBanChayTheoDanhMuc(string danhMuc)
        {
            var danhSach = danhSachSanPham.Values.Where(sp => sp.DanhMuc == danhMuc);
            return danhSach.OrderByDescending(sp => sp.SoLuong).First();
        }

        // Tổng doanh thu theo danh mục
        static double TongDoanhThuTheoDanhMuc(string danhMuc)
        {
            return danhSachSanPham.Values
                .Where(sp => sp.DanhMuc == danhMuc)
                .Sum(sp => sp.Gia * sp.SoLuong);
        }

        // Xuất báo cáo tổng hợp
        static void XuatBaoCao()
        {
            Console.WriteLine("=== DANH SÁCH SẢN PHẨM ===");
            foreach (var item in danhSachSanPham)
            {
                Console.WriteLine($"Mã: {item.Key} - {item.Value}");
            }
        }

        static void Main(string[] args)
        {
            ThemHoacCapNhatSanPham("SP01", "Laptop Dell", 1500, 10, "Điện tử");
            ThemHoacCapNhatSanPham("SP02", "Chuột Logitech", 25, 30, "Phụ kiện");
            ThemHoacCapNhatSanPham("SP03", "Bàn phím cơ", 80, 20, "Phụ kiện");
            ThemHoacCapNhatSanPham("SP01", "Laptop Dell", 1500, 5, "Điện tử"); 

            XuatBaoCao();

            Console.WriteLine("Sản phẩm bán chạy nhất:");
            Console.WriteLine(SanPhamBanChayNhat());

            Console.WriteLine("Sản phẩm bán chạy nhất (Phụ kiện):");
            Console.WriteLine(SanPhamBanChayTheoDanhMuc("Phụ kiện"));

            Console.WriteLine("Tổng doanh thu (Phụ kiện): " + TongDoanhThuTheoDanhMuc("Phụ kiện"));
        }
    }
}

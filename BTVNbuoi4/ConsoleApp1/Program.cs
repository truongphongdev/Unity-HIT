using System;
using System.Collections.Generic; // Bắt buộc để dùng List<>
using System.Linq; // Bắt buộc để dùng .OrderBy, .FirstOrDefault, .Any
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{

    internal class Program
    {        static List<CONGNHAN> danhSachCongNhan = new List<CONGNHAN>();

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            fakeData();

            bool dangChay = true;
            while (dangChay)
            {
                // 1. Tạo menu
                Console.WriteLine("\n======================================");
                Console.WriteLine("== CHƯƠNG TRÌNH QUẢN LÝ CÔNG NHÂN ==");
                Console.WriteLine("======================================");
                Console.WriteLine("1. Thêm công nhân mới");
                Console.WriteLine("2. Hiển thị danh sách công nhân");
                Console.WriteLine("3. Sắp xếp danh sách (theo Tên, Lương)");
                Console.WriteLine("4. Tìm công nhân theo Mã");
                Console.WriteLine("5. Thoát");
                Console.Write("Vui lòng chọn chức năng (1-5): ");

                string luaChon = Console.ReadLine();
                Console.Clear(); // Xóa màn hình cho gọn

                switch (luaChon)
                {
                    case "1":
                        ThemCongNhan();
                        break;
                    case "2":
                        Console.WriteLine("--- DANH SÁCH CÔNG NHÂN HIỆN TẠI ---");
                        // Gọi hàm hiển thị, truyền vào danh sách chính
                        HienThiDanhSach(danhSachCongNhan);
                        break;
                    case "3":
                        SapXepDanhSach();
                        break;
                    case "4":
                        TimCongNhanTheoMa();
                        break;
                    case "5":
                        dangChay = false; // Kết thúc vòng lặp
                        Console.WriteLine("Đã thoát chương trình. Tạm biệt!");
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng chọn lại từ 1 đến 5.");
                        break;
                }

                if (dangChay)
                {
                    Console.WriteLine("\n(Nhấn Enter để tiếp tục...)");
                    Console.ReadLine();
                }
            }
        }

        // 2. Chức năng Thêm (tách hàm)
        static void ThemCongNhan()
        {
            Console.WriteLine("--- 1. THÊM CÔNG NHÂN MỚI ---");

            // Nhập Mã CN và kiểm tra trùng lặp
            string maCn;
            while (true)
            {
                Console.Write("Nhập Mã công nhân: ");
                maCn = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(maCn))
                {
                    Console.WriteLine("Mã không được để trống!");
                    continue; // Quay lại vòng lặp
                }

                // Dùng LINQ .Any() để kiểm tra trùng mã (không phân biệt hoa thường)
                bool daTonTai = danhSachCongNhan.Any(cn => cn.MaCn.Equals(maCn, StringComparison.OrdinalIgnoreCase));

                if (daTonTai)
                {
                    Console.WriteLine("Lỗi: Mã công nhân này đã tồn tại. Vui lòng nhập mã khác.");
                }
                else
                {
                    break; // Mã hợp lệ, thoát vòng lặp
                }
            }

            // Nhập các thông tin còn lại
            Console.Write("Nhập Họ tên: ");
            string hoTen = Console.ReadLine();

            int tuoi;
            while (true)
            {
                Console.Write("Nhập Tuổi: ");
                if (int.TryParse(Console.ReadLine(), out tuoi) && tuoi > 0)
                {
                    break;
                }
                Console.WriteLine("Tuổi không hợp lệ, vui lòng nhập số nguyên dương.");
            }

            Console.Write("Nhập Địa chỉ: ");
            string diaChi = Console.ReadLine();

            // Nhập Chức vụ (dùng Enum)
            ChucVu chucVu;
            Console.WriteLine("Chọn Chức vụ:");
            // Hiển thị danh sách các chức vụ từ Enum
            foreach (var cv in Enum.GetValues(typeof(ChucVu)))
            {
                Console.WriteLine($"  {(int)cv}. {cv}");
            }
            while (true)
            {
                Console.Write("Nhập số tương ứng (0-5): ");
                // Thử chuyển đổi chuỗi nhập vào thành Enum
                if (Enum.TryParse(Console.ReadLine(), out chucVu) && Enum.IsDefined(typeof(ChucVu), chucVu))
                {
                    break; // Hợp lệ
                }
                Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng nhập một số từ 0 đến 5.");
            }

            // Tạo đối tượng và thêm vào danh sách
            CONGNHAN congNhanMoi = new CONGNHAN(hoTen, tuoi, diaChi, maCn, chucVu);
            danhSachCongNhan.Add(congNhanMoi);

            Console.WriteLine(">>> Đã thêm công nhân thành công! <<<");
        }

        // 3. Chức năng Hiển thị (tách hàm)
        // Nhận vào một List<CONGNHAN> để có thể hiển thị cả danh sách gốc
        // hoặc danh sách đã sắp xếp
        static void HienThiDanhSach(List<CONGNHAN> ds)
        {
            if (ds.Count == 0)
            {
                Console.WriteLine("Danh sách công nhân hiện đang rỗng.");
                return;
            }

            // In tiêu đề cột
            Console.WriteLine(new string('-', 100));
            Console.WriteLine(string.Format("{0,-8} | {1,-20} | {2,-5} | {3,-20} | {4,-15} | {5,15:N0}",
                "Mã CN", "Họ Tên", "Tuổi", "Địa Chỉ", "Chức Vụ", "Lương (VND)"));
            Console.WriteLine(new string('-', 100));

            // In thông tin từng công nhân
            foreach (var cn in ds)
            {
                Console.WriteLine(string.Format("{0,-8} | {1,-20} | {2,-5} | {3,-20} | {4,-15} | {5,15:N0}",
                    cn.MaCn,
                    cn.HoTen,
                    cn.Tuoi,
                    cn.DiaChi,
                    cn.ChucVu,
                    cn.TinhLuong() // Tính lương
                ));
            }
            Console.WriteLine(new string('-', 100));
        }

        // 4. Chức năng Sắp xếp (tách hàm)
        static void SapXepDanhSach()
        {
            Console.WriteLine("--- 3. DANH SÁCH SẮP XẾP (Theo Tên, sau đó theo Lương) ---");

            if (danhSachCongNhan.Count == 0)
            {
                Console.WriteLine("Danh sách rỗng, không có gì để sắp xếp.");
                return;
            }

            // Dùng LINQ để sắp xếp:
            // 1. OrderBy(cn => cn.HoTen) - Sắp xếp theo Tên (tăng dần)
            // 2. ThenBy(cn => cn.TinhLuong()) - Nếu Tên trùng, sắp xếp theo Lương (tăng dần)
            var danhSachDaSapXep = danhSachCongNhan
                .OrderBy(cn => cn.HoTen)
                .ThenBy(cn => cn.TinhLuong())
                .ToList(); // Chuyển kết quả thành một List mới

            // Gọi hàm HienThiDanhSach để hiển thị list đã sắp xếp
            HienThiDanhSach(danhSachDaSapXep);
        }

        // 5. Chức năng Tìm kiếm (tách hàm)
        static void TimCongNhanTheoMa()
        {
            Console.WriteLine("--- 4. TÌM CÔNG NHÂN THEO MÃ ---");
            Console.Write("Nhập Mã công nhân cần tìm: ");
            string maTimKiem = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(maTimKiem))
            {
                Console.WriteLine("Mã tìm kiếm không được để trống.");
                return;
            }

            // Dùng LINQ .FirstOrDefault() để tìm người đầu tiên khớp
            // (Không phân biệt hoa thường)
            CONGNHAN congNhanTimThay = danhSachCongNhan
                .FirstOrDefault(cn => cn.MaCn.Equals(maTimKiem, StringComparison.OrdinalIgnoreCase));

            // Kiểm tra kết quả
            if (congNhanTimThay != null) // Nếu tìm thấy (không phải null)
            {
                Console.WriteLine("Đã tìm thấy công nhân:");
                // Tái sử dụng hàm HienThiDanhSach
                // Bằng cách tạo một list mới chỉ chứa 1 công nhân đó
                HienThiDanhSach(new List<CONGNHAN> { congNhanTimThay });
            }
            else // Nếu là null
            {
                Console.WriteLine($"Không tìm thấy công nhân nào có mã là '{maTimKiem}'.");
            }
        }

        // Hàm tiện ích để thêm dữ liệu mẫu
        static void fakeData()
        {
            danhSachCongNhan.Add(new CONGNHAN("Trần Văn B", 30, "Hà Nội", "CN002", ChucVu.CongNhanBac3));
            danhSachCongNhan.Add(new CONGNHAN("Nguyễn Thị A", 25, "Đà Nẵng", "CN001", ChucVu.PhoNhom));
            danhSachCongNhan.Add(new CONGNHAN("Lê Văn C", 45, "TP.HCM", "CN003", ChucVu.TruongNhom));
            // Thêm người trùng tên để kiểm tra sắp xếp
            danhSachCongNhan.Add(new CONGNHAN("Nguyễn Thị A", 22, "Hà Nội", "CN004", ChucVu.CongNhanBac1));
        }
    }
}
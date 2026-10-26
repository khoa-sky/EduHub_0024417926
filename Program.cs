using System.Text;
using EduHub.Ch01.Domain;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("==========================================================");
Console.WriteLine("      EDUHUB - HỆ THỐNG QUẢN LÝ CÂU LẠC BỘ & SỰ KIỆN     ");
Console.WriteLine("==========================================================\n");

// 1. Nhập thông tin Câu lạc bộ
Console.WriteLine("--- [BƯỚC 1] NHẬP THÔNG TIN CÂU LẠC BỘ ---");
Console.Write("Nhập tên CLB: ");
string clubName = Console.ReadLine()?.Trim() ?? "";
if (string.IsNullOrWhiteSpace(clubName)) clubName = "Tech Club";

Console.Write("Nhập mô tả CLB: ");
string clubDesc = Console.ReadLine()?.Trim() ?? "";
if (string.IsNullOrWhiteSpace(clubDesc)) clubDesc = "CLB Lập trình & Đổi mới sáng tạo - DThU";

var club = Club.Create(clubName, clubDesc);
Console.WriteLine($"-> Đã tạo CLB: {club.Name} (Mã: {club.Id})\n");

// 2. Nhập danh sách thành viên
Console.WriteLine("--- [BƯỚC 2] NHẬP THÀNH VIÊN VÀO CLB ---");
Console.Write("Nhập số lượng thành viên muốn thêm: ");
if (!int.TryParse(Console.ReadLine(), out int memberCount) || memberCount <= 0)
{
    memberCount = 2;
}

var members = new List<Member>();
for (int i = 1; i <= memberCount; i++)
{
    Console.WriteLine($"\nThành viên #{i}:");
    Console.Write("  - Họ và tên: ");
    string name = Console.ReadLine()?.Trim() ?? "";
    if (string.IsNullOrWhiteSpace(name)) name = i == 1 ? "Nguyễn Văn An" : "Trần Thị Bình";

    Console.Write("  - Email: ");
    string email = Console.ReadLine()?.Trim() ?? "";
    if (string.IsNullOrWhiteSpace(email) || !email.Contains('@')) email = i == 1 ? "an.nguyen@dthu.edu.vn" : "binh.tran@dthu.edu.vn";

    Console.Write("  - Mã số sinh viên (MSSV): ");
    string code = Console.ReadLine()?.Trim() ?? "";
    if (string.IsNullOrWhiteSpace(code)) code = i == 1 ? "DH24001" : "DH24002";

    var m = Member.Create(name, email, code);
    club.AddMember(m);
    members.Add(m);
    Console.WriteLine($"  -> Đã thêm thành công: {m.Name} ({m.StudentCode})");
}

// 3. Nhập thông tin sự kiện
Console.WriteLine("\n--- [BƯỚC 3] TẠO SỰ KIỆN MỚI CHO CLB ---");
Console.Write("Nhập tên sự kiện: ");
string eventTitle = Console.ReadLine()?.Trim() ?? "";
if (string.IsNullOrWhiteSpace(eventTitle)) eventTitle = "Workshop Lập trình C# hướng đối tượng";

Console.Write("Nhập sức chứa tối đa (Capacity): ");
if (!int.TryParse(Console.ReadLine(), out int capacity) || capacity <= 0)
{
    capacity = 50;
}

var ev = Event.Create(club.Id, eventTitle, DateTime.Now.AddDays(7), capacity, false);
Console.WriteLine($"-> Đã tạo sự kiện: {ev.Title} | Sức chứa: {ev.Capacity} chỗ\n");

// 4. Đăng ký tham gia sự kiện
Console.WriteLine("--- [BƯỚC 4] ĐĂNG KÝ THAM GIA SỰ KIỆN ---");
var registrations = new List<Registration>();
foreach (var m in members)
{
    if (ev.HasCapacity(registrations.Count))
    {
        var reg = Registration.Create(m.Id, ev.Id);
        reg.Confirm(); // Ban tổ chức duyệt xác nhận
        registrations.Add(reg);
        Console.WriteLine($"-> Sinh viên {m.Name} ({m.StudentCode}) đã đăng ký thành công [Trạng thái: {reg.Status}]");
    }
}

// 5. Điểm danh sinh viên
Console.WriteLine("\n--- [BƯỚC 5] ĐIỂM DANH SỰ KIỆN (CHECK-IN) ---");
var checkIns = new List<CheckIn>();
if (registrations.Count > 0)
{
    Console.WriteLine($"Tiến hành điểm danh cho: {members[0].Name} ({members[0].StudentCode})");
    Console.Write("Chọn phương thức (1. Quét mã QR, 2. Thủ công) [Mặc định 1]: ");
    string choice = Console.ReadLine()?.Trim() ?? "1";
    string method = choice == "2" ? CheckInMethod.Manual : CheckInMethod.QR;

    var checkIn = CheckIn.Create(registrations[0], method);
    checkIns.Add(checkIn);
    Console.WriteLine($"-> {checkIn.Summary()}\n");
}

// 6. In báo cáo tổng kết
Console.WriteLine("==========================================================");
Console.WriteLine($"   BÁO CÁO THAM DỰ SỰ KIỆN: {ev.Title.ToUpper()}");
Console.WriteLine($"   Đơn vị: {club.Name} | Sức chứa: {ev.Capacity} chỗ");
Console.WriteLine($"   Số đăng ký: {registrations.Count} | Đã check-in: {checkIns.Count}");
Console.WriteLine("==========================================================");
Console.WriteLine("{0,-10} | {1,-18} | {2,-12} | {3}", "MSSV", "Họ tên", "Trạng thái", "Điểm danh");
Console.WriteLine(new string('-', 58));

for (int i = 0; i < members.Count; i++)
{
    var m = members[i];
    var reg = registrations.FirstOrDefault(r => r.MemberId == m.Id);
    var regStatus = reg != null ? reg.Status.ToString() : "Chưa đăng ký";
    var checkInRecord = reg != null ? checkIns.FirstOrDefault(c => c.RegistrationId == reg.Id) : null;
    var checkInText = checkInRecord != null ? $"Đã check-in ({checkInRecord.Method})" : "Chưa check-in";

    Console.WriteLine("{0,-10} | {1,-18} | {2,-12} | {3}", m.StudentCode, m.Name, regStatus, checkInText);
}

Console.WriteLine("==========================================================");
Console.WriteLine("Chương trình hoàn tất!");

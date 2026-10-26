namespace EduHub.Ch01.Domain;

/// <summary>
/// Trạng thái hoạt động của thành viên trong hệ thống.
/// </summary>
public enum MemberStatus
{
    Active = 1,
    Inactive = 2
}

/// <summary>
/// Đại diện cho thành viên sinh viên trong hệ thống EduHub.
/// </summary>
public class Member
{
    /// <summary>
    /// Mã định danh duy nhất của thành viên (Khóa chính).
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Họ và tên của thành viên.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Địa chỉ email sinh viên.
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// Mã số sinh viên (MSSV).
    /// </summary>
    public string StudentCode { get; private set; }

    /// <summary>
    /// Thời gian tham gia hệ thống.
    /// </summary>
    public DateTime JoinedAt { get; private set; }

    /// <summary>
    /// Trạng thái hoạt động của thành viên.
    /// </summary>
    public MemberStatus Status { get; private set; }

    /// <summary>
    /// Hàm khởi tạo riêng tư (private constructor) để bảo vệ tính đóng gói.
    /// </summary>
    private Member(Guid id, string name, string email, string studentCode, DateTime joinedAt, MemberStatus status)
    {
        Id = id;
        Name = name;
        Email = email;
        StudentCode = studentCode;
        JoinedAt = joinedAt;
        Status = status;
    }

    /// <summary>
    /// Factory method khởi tạo đối tượng Member và kiểm tra tính hợp lệ của dữ liệu đầu vào.
    /// </summary>
    /// <param name="name">Họ tên sinh viên.</param>
    /// <param name="email">Email sinh viên (bắt buộc chứa ký tự '@').</param>
    /// <param name="studentCode">Mã số sinh viên.</param>
    /// <returns>Đối tượng Member hợp lệ.</returns>
    public static Member Create(string name, string email, string studentCode)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Họ tên sinh viên không được để trống.", nameof(name));

        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            throw new ArgumentException("Email không hợp lệ (phải chứa ký tự '@').", nameof(email));

        if (string.IsNullOrWhiteSpace(studentCode))
            throw new ArgumentException("Mã số sinh viên không được để trống.", nameof(studentCode));

        return new Member(
            id: Guid.NewGuid(),
            name: name.Trim(),
            email: email.Trim().ToLowerInvariant(),
            studentCode: studentCode.Trim().ToUpperInvariant(),
            joinedAt: DateTime.Now,
            status: MemberStatus.Active
        );
    }

    /// <summary>
    /// Kiểm tra thành viên có đang ở trạng thái hoạt động hay không.
    /// </summary>
    public bool IsActive() => Status == MemberStatus.Active;

    /// <summary>
    /// Tạm dừng hoạt động của thành viên.
    /// </summary>
    public void Deactivate()
    {
        Status = MemberStatus.Inactive;
    }

    /// <summary>
    /// Kích hoạt lại quyền hoạt động của thành viên.
    /// </summary>
    public void Activate()
    {
        Status = MemberStatus.Active;
    }

    /// <summary>
    /// Trả về chuỗi đại diện cho thành viên.
    /// </summary>
    public override string ToString() => $"{Name} (MSSV: {StudentCode}) - {Email}";
}

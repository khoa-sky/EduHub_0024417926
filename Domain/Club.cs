namespace EduHub.Ch01.Domain;

/// <summary>
/// Đại diện cho Câu lạc bộ (CLB) sinh viên trong hệ thống EduHub.
/// </summary>
public class Club
{
    private readonly List<Member> _members = new();

    /// <summary>
    /// Mã định danh duy nhất của Câu lạc bộ.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Tên của Câu lạc bộ.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Mô tả hoạt động của Câu lạc bộ.
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// Thời gian thành lập Câu lạc bộ.
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// Danh sách thành viên trong CLB (dưới dạng chỉ đọc để bảo vệ dữ liệu nội bộ).
    /// </summary>
    public IReadOnlyList<Member> Members => _members.AsReadOnly();

    /// <summary>
    /// Hàm khởi tạo riêng tư (private constructor).
    /// </summary>
    private Club(Guid id, string name, string description, DateTime createdAt)
    {
        Id = id;
        Name = name;
        Description = description;
        CreatedAt = createdAt;
    }

    /// <summary>
    /// Factory method khởi tạo CLB và kiểm tra tính hợp lệ của dữ liệu.
    /// </summary>
    /// <param name="name">Tên câu lạc bộ (không được rỗng).</param>
    /// <param name="description">Mô tả về câu lạc bộ (không được rỗng).</param>
    /// <returns>Đối tượng Club hợp lệ.</returns>
    public static Club Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Tên câu lạc bộ không được để trống.", nameof(name));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Mô tả câu lạc bộ không được để trống.", nameof(description));

        return new Club(
            id: Guid.NewGuid(),
            name: name.Trim(),
            description: description.Trim(),
            createdAt: DateTime.Now
        );
    }

    /// <summary>
    /// Thêm thành viên vào Câu lạc bộ (ngăn chặn trùng MSSV).
    /// </summary>
    /// <param name="member">Thành viên cần thêm.</param>
    public void AddMember(Member member)
    {
        ArgumentNullException.ThrowIfNull(member);

        if (!member.IsActive())
            throw new InvalidOperationException($"Không thể thêm thành viên '{member.Name}' vì tài khoản đang bị tạm dừng.");

        if (_members.Any(m => m.StudentCode.Equals(member.StudentCode, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException($"Sinh viên '{member.Name}' (MSSV: {member.StudentCode}) đã là thành viên của CLB {Name}.");

        _members.Add(member);
    }

    /// <summary>
    /// Trả về tổng số lượng thành viên hiện tại của CLB.
    /// </summary>
    public int MemberCount() => _members.Count;

    /// <summary>
    /// Kiểm tra xem một sinh viên có thuộc CLB hay không.
    /// </summary>
    public bool HasMember(Guid memberId) => _members.Any(m => m.Id == memberId);

    /// <summary>
    /// Trả về chuỗi đại diện mô tả Câu lạc bộ.
    /// </summary>
    public override string ToString() => $"[CLB] {Name} - {Description} ({MemberCount()} thành viên)";
}

namespace EduHub.Ch01.Domain;

/// <summary>
/// Đại diện cho Sự kiện (workshop, hội thảo, cuộc thi) do Câu lạc bộ tổ chức.
/// </summary>
public class Event
{
    /// <summary>
    /// Mã định danh duy nhất của Sự kiện (Khóa chính).
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Khóa ngoại tham chiếu đến CLB tổ chức sự kiện.
    /// </summary>
    public Guid ClubId { get; private set; }

    /// <summary>
    /// Tiêu đề hoặc tên sự kiện.
    /// </summary>
    public string Title { get; private set; }

    /// <summary>
    /// Thời gian dự kiến tổ chức sự kiện.
    /// </summary>
    public DateTime Date { get; private set; }

    /// <summary>
    /// Sức chứa tối đa (số người tham dự tối đa).
    /// </summary>
    public int Capacity { get; private set; }

    /// <summary>
    /// Hình thức tổ chức: Trực tuyến (true) hoặc Trực tiếp (false).
    /// </summary>
    public bool IsOnline { get; private set; }

    /// <summary>
    /// Hàm khởi tạo riêng tư (private constructor).
    /// </summary>
    private Event(Guid id, Guid clubId, string title, DateTime date, int capacity, bool isOnline)
    {
        Id = id;
        ClubId = clubId;
        Title = title;
        Date = date;
        Capacity = capacity;
        IsOnline = isOnline;
    }

    /// <summary>
    /// Factory method khởi tạo Sự kiện với kiểm tra hợp lệ dữ liệu đầu vào.
    /// </summary>
    /// <param name="clubId">Mã CLB tổ chức (không được rỗng).</param>
    /// <param name="title">Tiêu đề sự kiện (không được rỗng).</param>
    /// <param name="date">Thời gian tổ chức.</param>
    /// <param name="capacity">Sức chứa tối đa (phải > 0).</param>
    /// <param name="isOnline">Tổ chức trực tuyến hay trực tiếp.</param>
    /// <returns>Đối tượng Event hợp lệ.</returns>
    public static Event Create(Guid clubId, string title, DateTime date, int capacity, bool isOnline = false)
    {
        if (clubId == Guid.Empty)
            throw new ArgumentException("Mã Câu lạc bộ (ClubId) không được để trống.", nameof(clubId));

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Tiêu đề sự kiện không được để trống.", nameof(title));

        if (capacity <= 0)
            throw new ArgumentOutOfRangeException(nameof(capacity), "Sức chứa sự kiện phải lớn hơn 0.");

        return new Event(
            id: Guid.NewGuid(),
            clubId: clubId,
            title: title.Trim(),
            date: date,
            capacity: capacity,
            isOnline: isOnline
        );
    }

    /// <summary>
    /// Kiểm tra xem sự kiện còn đủ chỗ trống cho người đăng ký mới hay không.
    /// </summary>
    /// <param name="registeredCount">Số lượng sinh viên hiện đã đăng ký.</param>
    /// <returns>True nếu còn chỗ, False nếu đã hết chỗ.</returns>
    public bool HasCapacity(int registeredCount) => registeredCount < Capacity;

    /// <summary>
    /// Trả về chuỗi đại diện mô tả Sự kiện.
    /// </summary>
    public override string ToString() => $"[Sự kiện] {Title} ({Date:dd/MM/yyyy HH:mm}) - Sức chứa: {Capacity} chỗ";
}

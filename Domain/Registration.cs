namespace EduHub.Ch01.Domain;

/// <summary>
/// Trạng thái của phiếu đăng ký tham dự sự kiện.
/// </summary>
public enum RegistrationStatus
{
    Pending = 1,
    Confirmed = 2,
    Cancelled = 3
}

/// <summary>
/// Đại diện cho Phiếu đăng ký tham dự sự kiện (bảng liên kết nhiều - nhiều giữa Member và Event).
/// </summary>
public class Registration
{
    /// <summary>
    /// Mã định danh duy nhất của Phiếu đăng ký (Khóa chính).
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Khóa ngoại tham chiếu đến thành viên đăng ký (MemberId).
    /// </summary>
    public Guid MemberId { get; private set; }

    /// <summary>
    /// Khóa ngoại tham chiếu đến sự kiện được đăng ký (EventId).
    /// </summary>
    public Guid EventId { get; private set; }

    /// <summary>
    /// Thời điểm sinh viên gửi yêu cầu đăng ký.
    /// </summary>
    public DateTime RegisteredAt { get; private set; }

    /// <summary>
    /// Trạng thái hiện tại của phiếu đăng ký.
    /// </summary>
    public RegistrationStatus Status { get; private set; }

    /// <summary>
    /// Hàm khởi tạo riêng tư (private constructor).
    /// </summary>
    private Registration(Guid id, Guid memberId, Guid eventId, DateTime registeredAt, RegistrationStatus status)
    {
        Id = id;
        MemberId = memberId;
        EventId = eventId;
        RegisteredAt = registeredAt;
        Status = status;
    }

    /// <summary>
    /// Factory method khởi tạo phiếu đăng ký mới với trạng thái ban đầu là Pending.
    /// </summary>
    /// <param name="memberId">Mã thành viên đăng ký (không được rỗng).</param>
    /// <param name="eventId">Mã sự kiện đăng ký (không được rỗng).</param>
    /// <returns>Đối tượng Registration mới.</returns>
    public static Registration Create(Guid memberId, Guid eventId)
    {
        if (memberId == Guid.Empty)
            throw new ArgumentException("Mã thành viên (MemberId) không được để trống.", nameof(memberId));

        if (eventId == Guid.Empty)
            throw new ArgumentException("Mã sự kiện (EventId) không được để trống.", nameof(eventId));

        return new Registration(
            id: Guid.NewGuid(),
            memberId: memberId,
            eventId: eventId,
            registeredAt: DateTime.Now,
            status: RegistrationStatus.Pending
        );
    }

    /// <summary>
    /// Ban tổ chức duyệt xác nhận phiếu đăng ký.
    /// </summary>
    public void Confirm()
    {
        if (Status != RegistrationStatus.Pending)
            throw new InvalidOperationException($"Không thể duyệt phiếu đang ở trạng thái {Status}.");

        Status = RegistrationStatus.Confirmed;
    }

    /// <summary>
    /// Huỷ phiếu đăng ký tham dự sự kiện.
    /// </summary>
    public void Cancel()
    {
        if (Status == RegistrationStatus.Cancelled)
            throw new InvalidOperationException("Phiếu đăng ký đã bị huỷ trước đó.");

        Status = RegistrationStatus.Cancelled;
    }

    /// <summary>
    /// Trả về chuỗi đại diện mô tả phiếu đăng ký.
    /// </summary>
    public override string ToString() => $"[Phiếu #{Id.ToString()[..8]}] Member: {MemberId.ToString()[..8]} | Event: {EventId.ToString()[..8]} | Status: {Status}";
}

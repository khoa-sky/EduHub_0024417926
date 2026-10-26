namespace EduHub.Ch01.Domain;

/// <summary>
/// Các hằng số định nghĩa hình thức điểm danh hợp lệ trong hệ thống.
/// </summary>
public static class CheckInMethod
{
    /// <summary>
    /// Điểm danh bằng quét mã QR.
    /// </summary>
    public const string QR = "QR";

    /// <summary>
    /// Điểm danh thủ công qua đối soát danh sách.
    /// </summary>
    public const string Manual = "Manual";
}

/// <summary>
/// Đại diện cho bản ghi Điểm danh (Check-in) của sinh viên tại sự kiện.
/// </summary>
public class CheckIn
{
    /// <summary>
    /// Mã định danh duy nhất của lượt điểm danh (Khóa chính).
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Khóa ngoại tham chiếu đến Phiếu đăng ký (RegistrationId).
    /// </summary>
    public Guid RegistrationId { get; private set; }

    /// <summary>
    /// Thời điểm sinh viên điểm danh thực tế.
    /// </summary>
    public DateTime CheckedInAt { get; private set; }

    /// <summary>
    /// Hình thức điểm danh (sử dụng hằng số CheckInMethod.QR hoặc CheckInMethod.Manual).
    /// </summary>
    public string Method { get; private set; }

    /// <summary>
    /// Hàm khởi tạo riêng tư (private constructor).
    /// </summary>
    private CheckIn(Guid id, Guid registrationId, DateTime checkedInAt, string method)
    {
        Id = id;
        RegistrationId = registrationId;
        CheckedInAt = checkedInAt;
        Method = method;
    }

    /// <summary>
    /// Factory method tạo bản ghi điểm danh với kiểm tra quy tắc nghiệp vụ: Chỉ phiếu Confirmed mới được phép check-in.
    /// </summary>
    /// <param name="registration">Phiếu đăng ký của sinh viên.</param>
    /// <param name="method">Hình thức điểm danh (mặc định là CheckInMethod.Manual).</param>
    /// <returns>Đối tượng CheckIn hợp lệ.</returns>
    public static CheckIn Create(Registration registration, string method = CheckInMethod.Manual)
    {
        ArgumentNullException.ThrowIfNull(registration);

        if (registration.Status != RegistrationStatus.Confirmed)
            throw new InvalidOperationException($"Chỉ phiếu đã duyệt (Confirmed) mới được phép check-in. Phiếu hiện tại: {registration.Status}.");

        var validMethod = string.IsNullOrWhiteSpace(method) ? CheckInMethod.Manual : method.Trim().ToUpperInvariant();

        return new CheckIn(
            id: Guid.NewGuid(),
            registrationId: registration.Id,
            checkedInAt: DateTime.Now,
            method: validMethod
        );
    }

    /// <summary>
    /// Trả về chuỗi tóm tắt thông tin điểm danh.
    /// </summary>
    public string Summary() => $"[CheckIn #{Id.ToString()[..8]}] Lúc {CheckedInAt:HH:mm:ss dd/MM/yyyy} qua [{Method}] (Phiếu #{RegistrationId.ToString()[..8]})";

    /// <summary>
    /// Trả về chuỗi đại diện cho lượt điểm danh.
    /// </summary>
    public override string ToString() => Summary();
}

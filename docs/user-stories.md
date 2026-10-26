# TÀI LIỆU USER STORIES — HỆ THỐNG EDUHUB (CH01)

Dựa trên sơ đồ lớp (Class Diagram) gồm 5 thực thể: **Club, Member, Event, Registration, CheckIn**, dưới đây là các câu chuyện người dùng (User Stories) cốt lõi của hệ thống:

---

### US01: Tạo Câu lạc bộ & Thêm thành viên (Club)
- **User Story:**
  > **Là** Ban chủ nhiệm,  
  > **tôi muốn** tạo câu lạc bộ và thêm sinh viên vào danh sách thành viên,  
  > **để** quản lý hoạt động và thành viên của CLB.
- **Tiêu chí nghiệm thu:**
  - Khởi tạo được CLB với tên và mô tả.
  - Thêm được thành viên sinh viên vào CLB.
  - Không cho phép thêm trùng mã số sinh viên (MSSV).

---

### US02: Quản lý thông tin sinh viên (Member)
- **User Story:**
  > **Là** sinh viên,  
  > **tôi muốn** có thông tin tài khoản (họ tên, email, MSSV) trên hệ thống,  
  > **để** tham gia các CLB và sự kiện trong trường.
- **Tiêu chí nghiệm thu:**
  - Lưu đầy đủ: họ tên, email, MSSV, ngày tham gia và trạng thái (`Active` / `Inactive`).
  - Có thể kiểm tra trạng thái hoạt động (`IsActive()`) hoặc tạm ngưng (`Deactivate()`).

---

### US03: Tạo sự kiện cho CLB (Event)
- **User Story:**
  > **Là** Ban chủ nhiệm CLB,  
  > **tôi muốn** tạo các sự kiện (workshop, hội thảo) với giới hạn số lượng chỗ,  
  > **để** sinh viên đăng ký tham gia.
- **Tiêu chí nghiệm thu:**
  - Sự kiện trực thuộc một CLB cụ thể (`ClubId`).
  - Có thông tin: tiêu đề, ngày diễn ra, hình thức (Online/Offline) và sức chứa tối đa (`Capacity`).
  - Cung cấp hàm `HasCapacity()` kiểm tra sự kiện còn chỗ trống hay không.

---

### US04: Đăng ký tham gia sự kiện (Registration)
- **User Story:**
  > **Là** sinh viên,  
  > **tôi muốn** gửi phiếu đăng ký tham dự một sự kiện và được ban tổ chức duyệt,  
  > **để** có tên trong danh sách tham gia chính thức.
- **Tiêu chí nghiệm thu:**
  - Phiếu đăng ký liên kết giữa sinh viên (`MemberId`) và sự kiện (`EventId`).
  - Trạng thái ban đầu là `Pending`.
  - Ban tổ chức có thể duyệt (`Confirm()`) hoặc huỷ (`Cancel()`) phiếu.

---

### US05: Điểm danh khi tham dự sự kiện (CheckIn)
- **User Story:**
  > **Là** ban tổ chức tại ngày diễn ra sự kiện,  
  > **tôi muốn** quét điểm danh (bằng mã QR hoặc đối soát thủ công) cho sinh viên đã đến,  
  > **để** tổng kết danh sách có mặt thực tế.
- **Tiêu chí nghiệm thu:**
  - Tạo bản ghi điểm danh lưu thời gian và hình thức (`Method = "QR"` hoặc `"Manual"`).
  - Bản ghi điểm danh gắn với đúng mã phiếu (`RegistrationId`).
  - Thống kê được ai đã check-in và ai chưa check-in.

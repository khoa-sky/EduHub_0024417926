# SƠ ĐỒ LỚP (CLASS DIAGRAM) — DOMAIN EDUHUB (CH01)

## 1. Bản vẽ Sơ đồ lớp (Trắng đen)

Hình vẽ sơ đồ lớp chuẩn UML (trắng đen, bố cục phân cấp trực quan):

![Sơ đồ lớp EduHub Ch01](class-diagram.svg)

> **Tệp đính kèm trong thư mục `docs/`:**
> - 📄 **Tệp thiết kế Draw.io:** [`class-diagram.drawio`](class-diagram.drawio) (mở chỉnh sửa trực tiếp trên [app.diagrams.net](https://app.diagrams.net) hoặc extension Draw.io trên VS Code)
> - 🖼️ **Ảnh vector:** [`class-diagram.svg`](class-diagram.svg)

---

## 2. Mã nguồn biểu đồ Mermaid (Class Diagram)

```mermaid
classDiagram
    direction TB

    class MemberStatus {
        <<enumeration>>
        Active = 1
        Inactive = 2
    }

    class RegistrationStatus {
        <<enumeration>>
        Pending = 1
        Confirmed = 2
        Cancelled = 3
    }

    class CheckInMethod {
        <<static class>>
        +string QR = "QR"
        +string Manual = "Manual"
    }

    class Member {
        +Guid Id : Guid
        +string Name : string
        +string Email : string
        +string StudentCode : string
        +DateTime JoinedAt : DateTime
        +MemberStatus Status : MemberStatus
        -Member(id, name, email, studentCode, joinedAt, status)
        +Create(name, email, studentCode)$ Member
        +IsActive() bool
        +Deactivate() void
        +Activate() void
        +ToString() string
    }

    class Club {
        +Guid Id : Guid
        +string Name : string
        +string Description : string
        +DateTime CreatedAt : DateTime
        +IReadOnlyList~Member~ Members
        -Club(id, name, description, createdAt)
        +Create(name, description)$ Club
        +AddMember(member) void
        +MemberCount() int
        +HasMember(memberId) bool
        +ToString() string
    }

    class Event {
        +Guid Id : Guid
        +Guid ClubId : Guid
        +string Title : string
        +DateTime Date : DateTime
        +int Capacity : int
        +bool IsOnline : bool
        -Event(id, clubId, title, date, capacity, isOnline)
        +Create(clubId, title, date, capacity, isOnline)$ Event
        +HasCapacity(registeredCount) bool
        +ToString() string
    }

    class Registration {
        +Guid Id : Guid
        +Guid MemberId : Guid
        +Guid EventId : Guid
        +DateTime RegisteredAt : DateTime
        +RegistrationStatus Status : RegistrationStatus
        -Registration(id, memberId, eventId, registeredAt, status)
        +Create(memberId, eventId)$ Registration
        +Confirm() void
        +Cancel() void
        +ToString() string
    }

    class CheckIn {
        +Guid Id : Guid
        +Guid RegistrationId : Guid
        +DateTime CheckedInAt : DateTime
        +string Method : string
        -CheckIn(id, registrationId, checkedInAt, method)
        +Create(registration, method)$ CheckIn
        +Summary() string
        +ToString() string
    }

    Club "1" o-- "*" Member : tập hợp thành viên
    Club "1" *-- "*" Event : sở hữu & tổ chức [FK: ClubId]
    Member "1" <-- "*" Registration : sinh viên đăng ký [FK: MemberId]
    Event "1" <-- "*" Registration : nhận đăng ký [FK: EventId]
    Registration "1" <-- "0..1" CheckIn : điểm danh [FK: RegistrationId]
    
    Member ..> MemberStatus : sử dụng
    Registration ..> RegistrationStatus : sử dụng
    CheckIn ..> CheckInMethod : sử dụng
```

---

## 3. Bảng phân tích quan hệ thực thể

| Quan hệ | Loại quan hệ | Bội số | Khóa ngoại (FK) | Ý nghĩa nghiệp vụ |
| :--- | :---: | :---: | :--- | :--- |
| **Club — Member** | Aggregation (`o--`) | `1 — *` | Quản lý qua `Members` | CLB tập hợp nhiều thành viên sinh viên. Sinh viên có vòng đời độc lập. |
| **Club — Event** | Composition (`*--`) | `1 — *` | `Event.ClubId` &rarr; `Club.Id` | Một CLB tổ chức nhiều sự kiện; mỗi sự kiện trực thuộc duy nhất một CLB. |
| **Member — Registration** | Association (`<--`) | `1 — *` | `Registration.MemberId` &rarr; `Member.Id` | Sinh viên tạo các phiếu đăng ký tham gia sự kiện. |
| **Event — Registration** | Association (`<--`) | `1 — *` | `Registration.EventId` &rarr; `Event.Id` | Sự kiện tiếp nhận các phiếu đăng ký cho đến khi đạt sức chứa. |
| **Registration — CheckIn** | Association (`<--`) | `1 — 0..1` | `CheckIn.RegistrationId` &rarr; `Registration.Id` | Mỗi phiếu đăng ký chỉ được quét điểm danh tối đa 1 lần duy nhất. |

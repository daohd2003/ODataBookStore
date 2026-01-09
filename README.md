# 📚 OData BookStore API

**ODataBookStore** là một dự án mẫu minh họa cách xây dựng RESTful API hỗ trợ chuẩn **OData v4** (Open Data Protocol) trên nền tảng **ASP.NET Core 8**. Dự án sử dụng **Entity Framework Core** với **In-Memory Database** để mô phỏng dữ liệu, giúp dễ dàng triển khai và kiểm thử các tính năng truy vấn mạnh mẽ của OData mà không cần cài đặt SQL Server.



---

## 🚀 Tính năng nổi bật

Dự án đã được cấu hình để hỗ trợ các tính năng truy vấn OData mạnh mẽ sau:

* **$select**: Chọn lọc các trường dữ liệu cụ thể cần trả về.
* **$filter**: Lọc dữ liệu theo điều kiện logic (So sánh, chuỗi, toán học...).
* **$orderby**: Sắp xếp dữ liệu tăng dần hoặc giảm dần.
* **$expand**: Truy vấn dữ liệu từ các bảng liên kết (Eager Loading).
* **$count**: Đếm tổng số bản ghi thỏa mãn điều kiện.
* **$top / $skip**: Hỗ trợ phân trang dữ liệu.
* **MaxTop**: Giới hạn số lượng bản ghi tối đa trả về (được cấu hình là 100).

---

## 🛠 Công nghệ sử dụng

| Thành phần | Công nghệ / Phiên bản |
| :--- | :--- |
| **Framework** | .NET 8.0 |
| **OData Library** | Microsoft.AspNetCore.OData (v9.3.1) |
| **Database** | EF Core In-Memory (v9.0.5) |
| **Documentation** | Swashbuckle / Swagger UI |
| **Language** | C# |

---

## ⚙️ Cài đặt và Hướng dẫn chạy

Do dự án sử dụng **In-Memory Database**, bạn không cần cấu hình chuỗi kết nối SQL Server. Dữ liệu mẫu sẽ tự động được khởi tạo mỗi khi ứng dụng chạy.

### 1. Yêu cầu tiên quyết
* [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* Visual Studio 2022 hoặc Visual Studio Code.

### 2. Clone và chạy dự án

**Sử dụng Visual Studio:**
1.  Mở file `ODataBookStore.sln`.
2.  Nhấn **F5** hoặc nút **Start** để chạy ứng dụng.

**Sử dụng Terminal / CLI:**
```bash
# Di chuyển vào thư mục dự án
cd daohd2003-odatabookstore

# Khôi phục các thư viện
dotnet restore

# Chạy ứng dụng
dotnet run
```

Sau khi chạy thành công, ứng dụng sẽ hoạt động tại:
* **HTTP:** `http://localhost:5219`
* **HTTPS:** `https://localhost:7213`
* **Swagger UI:** `https://localhost:7213/swagger`

---

## 📖 Hướng dẫn sử dụng API (OData Examples)

Dưới đây là các ví dụ truy vấn bạn có thể thử ngay trên trình duyệt hoặc Postman sau khi chạy ứng dụng.

**Endpoint gốc:** `/odata/`

### 1. Lấy danh sách Sách (Books)
* **Lấy tất cả sách:**
    `GET /odata/Books`
    
* **Lọc sách có giá nhỏ hơn 60 ($filter):**
    `GET /odata/Books?$filter=Price lt 60`
    
* **Chỉ lấy Tên sách và Tác giả ($select):**
    `GET /odata/Books?$select=Title,Author`
    
* **Lấy sách kèm thông tin Nhà xuất bản ($expand):**
    `GET /odata/Books?$expand=Press`
    
* **Sắp xếp theo giá giảm dần ($orderby):**
    `GET /odata/Books?$orderby=Price desc`

### 2. Lấy danh sách Nhà xuất bản (Presses)
* **Lấy tất cả nhà xuất bản:**
    `GET /odata/Presses`
    
* **Lấy nhà xuất bản có Category là 'Book':**
    `GET /odata/Presses?$filter=Category eq ODataBookStore.Enums.Category'Book'`

---

## 📂 Cấu trúc dự án

```text
daohd2003-odatabookstore/
├── Controllers/
│   ├── BooksController.cs      # Xử lý API cho Entity Book
│   └── PressesController.cs    # Xử lý API cho Entity Press (có seed data)
├── Enums/
│   └── Category.cs             # Enum phân loại sách/tạp chí
├── Models/
│   ├── Book.cs                 # Entity Sách
│   ├── Press.cs                # Entity Nhà xuất bản
│   ├── Address.cs              # Complex Type (địa chỉ)
│   ├── DataSource.cs           # Dữ liệu mẫu (Mock Data)
│   └── BookStoreContext.cs     # EF Core DbContext
├── Program.cs                  # Cấu hình Services, OData Route & EDM Model
├── ODataBookStore.csproj       # File cấu hình dự án .NET
└── appsettings.json            # Cấu hình ứng dụng
```

---

## 📝 Lưu ý
* Dữ liệu được lưu trong **RAM (In-Memory)**, vì vậy mọi thay đổi (Thêm/Sửa/Xóa) sẽ bị mất khi bạn tắt hoặc khởi động lại ứng dụng.
* Cấu hình EDM Model được định nghĩa trong file `Program.cs` thông qua phương thức `GetEdmModel()`.

---

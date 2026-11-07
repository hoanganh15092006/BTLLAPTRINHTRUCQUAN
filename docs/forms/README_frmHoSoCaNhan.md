# BÁO CÁO KỸ THUẬT & HƯỚNG DẪN HOẠT ĐỘNG: FORM HỒ SƠ CÁ NHÂN (frmHoSoCaNhan)

---

## 1. TỔNG QUAN VỀ FORM
- **Tên lớp (Class Name)**: `frmHoSoCaNhan`
- **Tập tin mã nguồn**:
  - Code-behind: [Forms/frmHoSoCaNhan.cs](file:///d:/HOCTRENLOP/LAPTRINHTRUCQUAN/BTLLAPTRINHTRUCQUAN/Forms/frmHoSoCaNhan.cs)
  - UI Designer: [Forms/frmHoSoCaNhan.Designer.cs](file:///d:/HOCTRENLOP/LAPTRINHTRUCQUAN/BTLLAPTRINHTRUCQUAN/Forms/frmHoSoCaNhan.Designer.cs)
- **Mục đích nghiệp vụ**: Cho phép người dùng đang đăng nhập xem và cập nhật hồ sơ cá nhân của mình (Số điện thoại, Địa chỉ, Ngày sinh, Giới tính), tự đổi mật khẩu tài khoản và xem lịch sử các giao dịch bán hàng do chính mình đã thực hiện.
- **Phân quyền truy cập (RBAC)**: Tất cả người dùng sau khi đăng nhập (`Admin`, `ThuKho`, `BanHang`).

---

## 2. CÔNG NGHỆ, MÃ HÓA & BẢO VỆ MẬT KHẨU
1. **Kiến trúc quản lý hồ sơ tích hợp**:
   - Tự động nhận diện người dùng hiện tại qua biến toàn cục `Functions.CurrentUsername` và `Functions.CurrentMaNV`.
   - Kết nối 2 bảng `tblTaiKhoan` và `NhanVien` để hiển thị đồng bộ thông tin tài khoản và thông tin nhân sự.
2. **Bảo mật đổi mật khẩu 3 bước an toàn**:
   - Bước 1: Yêu cầu nhập mật khẩu hiện tại (Mật khẩu cũ).
   - Bước 2: Kiểm tra mật khẩu cũ qua hàm `SecurityHelper.VerifyPassword(oldPass, storedHash, storedSalt)`.
   - Bước 3: Nếu hợp lệ, sinh Salt ngẫu nhiên mới, băm mật khẩu mới và cập nhật vào `tblTaiKhoan`.
3. **Mã hóa AES-256 hai chiều đối với thông tin liên hệ**:
   - Số điện thoại và Địa chỉ cá nhân được giải mã từ CSDL để hiển thị cho người dùng chỉnh sửa.
   - Khi lưu, hệ thống tự động kiểm tra định dạng Regex điện thoại (`^0[0-9]{9,10}$`) và mã hóa lại bằng `SecurityHelper.EncryptAES()` trước khi cập nhật bảng `NhanVien`.

---

## 3. CÁCH HOẠT ĐỘNG & QUY TRÌNH NGHIỆP VỤ (LIFECYCLE & WORKFLOW)

### 3.1. Sơ đồ quy trình đổi mật khẩu
```mermaid
flowchart TD
    A([Người dùng nhập: Mật khẩu cũ, Mật khẩu mới, Xác nhận]) --> B{Nhấn nút [Đổi Mật Khẩu]}
    B --> C{Kiểm tra trống & Mật khẩu mới >= 6 ký tự?}
    C -- Không đạt --> D[Báo lỗi: Mật khẩu mới phải có ít nhất 6 ký tự!]
    C -- Đạt --> E{Mật khẩu mới == Xác nhận?}
    E -- Không khớp --> F[Báo lỗi: Mật khẩu xác nhận không khớp!]
    E -- Khớp --> G[Truy vấn MatKhauHash và Salt hiện tại từ tblTaiKhoan]
    G --> H{VerifyPassword: Mật khẩu cũ chính xác?}
    H -- Sai --> I[Báo lỗi: Mật khẩu cũ không chính xác!]
    H -- Đúng --> J[Sinh Salt mới & Băm mật khẩu mới: HashPassword]
    J --> K[UPDATE tblTaiKhoan SET MatKhauHash = @h, Salt = @s WHERE TenDangNhap = @u]
    K --> L[Ghi Audit Log & Hiển thị thông báo đổi mật khẩu thành công]
```

---

## 4. CHI TIẾT MÃ NGUỒN & PHƯƠNG THỨC CHÍNH

### 4.1. Bảng danh sách phương thức
| Phương thức | Phạm vi | Chức năng chi tiết |
|:---|:---|:---|
| `frmHoSoCaNhan_Load` | `private void` | Nạp thông tin tài khoản, giải mã SĐT/Địa chỉ nhân viên, nạp lịch sử giao dịch cá nhân |
| `LoadUserProfile` | `private void` | Truy vấn chi tiết hồ sơ từ bảng `tblTaiKhoan` và `NhanVien`, hiển thị lên các TextBox |
| `LoadUserHistory` | `private void` | Nạp danh sách các hóa đơn bán do chính nhân viên này lập từ `tblHoaDonBan` |
| `btnLuuThongTin_Click` | `private void` | Validate SĐT, mã hóa AES và cập nhật thông tin cá nhân vào bảng `NhanVien` |
| `btnDoiMatKhau_Click` | `private void` | Kiểm tra mật khẩu cũ, thẩm định mật khẩu mới, băm mật khẩu và cập nhật CSDL |

### 4.2. Khối mã đổi mật khẩu bảo mật
```csharp
string sql = "SELECT MatKhauHash, Salt FROM tblTaiKhoan WHERE TenDangNhap = @u";
string storedHash = "", storedSalt = "";

using (SqlCommand cmd = new SqlCommand(sql, Functions.Conn))
{
    cmd.Parameters.AddWithValue("@u", Functions.CurrentUsername);
    using (SqlDataReader reader = cmd.ExecuteReader())
    {
        if (reader.Read())
        {
            storedHash = reader["MatKhauHash"].ToString();
            storedSalt = reader["Salt"].ToString();
        }
    }
}

if (!SecurityHelper.VerifyPassword(txtMatKhauCu.Text, storedHash, storedSalt))
{
    MessageBox.Show("Mật khẩu hiện tại không chính xác!", "Lỗi Xác Thực", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    txtMatKhauCu.Focus();
    return;
}

string newSalt;
string newHash = SecurityHelper.HashPassword(txtMatKhauMoi.Text, out newSalt);

string updateSql = "UPDATE tblTaiKhoan SET MatKhauHash = @h, Salt = @s WHERE TenDangNhap = @u";
using (SqlCommand cmd = new SqlCommand(updateSql, Functions.Conn))
{
    cmd.Parameters.AddWithValue("@h", newHash);
    cmd.Parameters.AddWithValue("@s", newSalt);
    cmd.Parameters.AddWithValue("@u", Functions.CurrentUsername);
    cmd.ExecuteNonQuery();
}
Functions.LogAction("Đổi mật khẩu", "Người dùng [" + Functions.CurrentUsername + "] đã đổi mật khẩu thành công");
```

---

## 5. ẢNH CHỤP MINH CHỨNG GIAO DIỆN
- **Chế độ Sáng (Light Mode)**: `Screenshots/frmHoSoCaNhan_Light.png`
- **Chế độ Tối (Dark Mode)**: `Screenshots/frmHoSoCaNhan_Dark.png`
- **Giao diện chi tiết hồ sơ & lịch sử**: `Screenshots/Verified/12_HoSoCaNhan_View.png`

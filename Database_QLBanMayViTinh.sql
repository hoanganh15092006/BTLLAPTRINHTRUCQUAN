-- =========================================================================
-- ĐỒ ÁN / BÀI TẬP LỚN LẬP TRÌNH TRỰC QUAN C# WINFORMS
-- ĐỀ TÀI: QUẢN LÝ CỬA HÀNG BÁN MÁY VI TÍNH
-- CƠ SỞ DỮ LIỆU: QLBanMayViTinh (Chuẩn hóa 3NF - 21 Bảng dữ liệu)
-- ĐÁP ỨNG TRỌN VẸN 9 YÊU CẦU NGHIỆP VỤ & TRIGGER TỰ ĐỘNG HÓA
-- =========================================================================

USE master;
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = N'QLBanMayViTinh')
BEGIN
    ALTER DATABASE QLBanMayViTinh SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE QLBanMayViTinh;
END
GO

CREATE DATABASE QLBanMayViTinh;
GO

USE QLBanMayViTinh;
GO

-- =========================================================================
-- 1. TẠO 14 BẢNG DANH MỤC LINH KIỆN VÀ THÔNG SỐ KỸ THUẬT
-- =========================================================================

-- 1. Bảng Loại máy (Để bàn, xách tay, Workstation...)
CREATE TABLE LoaiMay (
    MaLoai NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenLoai NVARCHAR(100) NOT NULL
);

-- 2. Bảng Chíp (CPU)
CREATE TABLE Chip (
    MaChip NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenChip NVARCHAR(100) NOT NULL
);

-- 3. Bảng Ổ cứng
CREATE TABLE OCung (
    MaOCung NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenOCung NVARCHAR(100) NOT NULL
);

-- 4. Bảng Dung lượng ổ cứng
CREATE TABLE DungLuong (
    MaDungLuong NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenDungLuong NVARCHAR(50) NOT NULL
);

-- 5. Bảng Tốc độ xử lý / Bus / Read-Write
CREATE TABLE TocDo (
    MaTocDo NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenTocDo NVARCHAR(50) NOT NULL
);

-- 6. Bảng Ổ đĩa quang CD/DVD
CREATE TABLE OCD (
    MaOCD NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenOCD NVARCHAR(100) NOT NULL
);

-- 7. Bảng Màn hình
CREATE TABLE ManHinh (
    MaManHinh NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenManHinh NVARCHAR(100) NOT NULL
);

-- 8. Bảng Cỡ màn hình
CREATE TABLE CoMH (
    MaCoMH NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenCoMH NVARCHAR(50) NOT NULL
);

-- 9. Bảng Chuột
CREATE TABLE Chuot (
    MaChuot NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenChuot NVARCHAR(100) NOT NULL
);

-- 10. Bảng Bàn phím
CREATE TABLE BanPhim (
    MaBanPhim NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenBanPhim NVARCHAR(100) NOT NULL
);

-- 11. Bảng Ổ USB / Cổng giao tiếp
CREATE TABLE USB (
    MaUSB NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenUSB NVARCHAR(100) NOT NULL,
    DungLuong NVARCHAR(50) NULL
);

-- 12. Bảng RAM
CREATE TABLE RAM (
    MaRAM NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenRAM NVARCHAR(100) NOT NULL
);

-- 13. Bảng Loa âm thanh
CREATE TABLE Loa (
    MaLoa NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenLoa NVARCHAR(100) NOT NULL
);

-- 14. Bảng Hãng sản xuất
CREATE TABLE HangSanXuat (
    MaHangSX NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenHangSX NVARCHAR(100) NOT NULL
);

-- =========================================================================
-- 2. CỤM BẢNG ĐỐI TÁC VÀ NHÂN SỰ
-- =========================================================================

-- 15. Bảng Nhân viên
CREATE TABLE NhanVien (
    MaNV NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenNV NVARCHAR(100) NOT NULL,
    NgaySinh DATE NULL,
    GioiTinh NVARCHAR(10) NULL,
    DiaChi NVARCHAR(250) NULL,
    DienThoai NVARCHAR(20) NULL
);

-- 16. Bảng Khách hàng
CREATE TABLE KhachHang (
    MaKhach NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenKhach NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(250) NULL,
    DienThoai NVARCHAR(20) NULL
);

-- 17. Bảng Nhà cung cấp
CREATE TABLE NhaCungCap (
    MaNCC NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenNCC NVARCHAR(150) NOT NULL,
    DiaChi NVARCHAR(250) NULL,
    DienThoai NVARCHAR(20) NULL
);

-- =========================================================================
-- 3. BẢNG TRUNG TÂM: MÁY VI TÍNH (LIÊN KẾT 14 BẢNG LINH KIỆN)
-- =========================================================================

-- 18. Bảng Máy vi tính
CREATE TABLE MayViTinh (
    MaMayVT NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenMayVT NVARCHAR(150) NOT NULL,
    MaLoai NVARCHAR(20) NOT NULL,
    MaChip NVARCHAR(20) NOT NULL,
    MaOCung NVARCHAR(20) NOT NULL,
    MaDungLuong NVARCHAR(20) NOT NULL,
    MaTocDo NVARCHAR(20) NOT NULL,
    MaOCD NVARCHAR(20) NOT NULL,
    MaManHinh NVARCHAR(20) NOT NULL,
    MaCoMH NVARCHAR(20) NOT NULL,
    MaChuot NVARCHAR(20) NOT NULL,
    MaBanPhim NVARCHAR(20) NOT NULL,
    MaUSB NVARCHAR(20) NOT NULL,
    MaRAM NVARCHAR(20) NOT NULL,
    MaLoa NVARCHAR(20) NOT NULL,
    MaHangSX NVARCHAR(20) NOT NULL,
    GiaNhap DECIMAL(18,0) NOT NULL DEFAULT 0,
    GiaBan DECIMAL(18,0) NOT NULL DEFAULT 0,
    ThoiGianBH INT NOT NULL DEFAULT 12,
    SoLuong INT NOT NULL DEFAULT 0,
    Anh NVARCHAR(255) NULL,
    GhiChu NVARCHAR(500) NULL,

    -- Ràng buộc toàn vẹn khóa ngoại (FK)
    CONSTRAINT FK_MVT_Loai FOREIGN KEY (MaLoai) REFERENCES LoaiMay(MaLoai),
    CONSTRAINT FK_MVT_Chip FOREIGN KEY (MaChip) REFERENCES Chip(MaChip),
    CONSTRAINT FK_MVT_OCung FOREIGN KEY (MaOCung) REFERENCES OCung(MaOCung),
    CONSTRAINT FK_MVT_DungLuong FOREIGN KEY (MaDungLuong) REFERENCES DungLuong(MaDungLuong),
    CONSTRAINT FK_MVT_TocDo FOREIGN KEY (MaTocDo) REFERENCES TocDo(MaTocDo),
    CONSTRAINT FK_MVT_OCD FOREIGN KEY (MaOCD) REFERENCES OCD(MaOCD),
    CONSTRAINT FK_MVT_ManHinh FOREIGN KEY (MaManHinh) REFERENCES ManHinh(MaManHinh),
    CONSTRAINT FK_MVT_CoMH FOREIGN KEY (MaCoMH) REFERENCES CoMH(MaCoMH),
    CONSTRAINT FK_MVT_Chuot FOREIGN KEY (MaChuot) REFERENCES Chuot(MaChuot),
    CONSTRAINT FK_MVT_BanPhim FOREIGN KEY (MaBanPhim) REFERENCES BanPhim(MaBanPhim),
    CONSTRAINT FK_MVT_USB FOREIGN KEY (MaUSB) REFERENCES USB(MaUSB),
    CONSTRAINT FK_MVT_RAM FOREIGN KEY (MaRAM) REFERENCES RAM(MaRAM),
    CONSTRAINT FK_MVT_Loa FOREIGN KEY (MaLoa) REFERENCES Loa(MaLoa),
    CONSTRAINT FK_MVT_HangSX FOREIGN KEY (MaHangSX) REFERENCES HangSanXuat(MaHangSX)
);

-- =========================================================================
-- 4. CỤM BẢNG GIAO DỊCH (HÓA ĐƠN NHẬP & HÓA ĐƠN BÁN)
-- =========================================================================

-- 19. Bảng Hóa đơn nhập
CREATE TABLE HoaDonNhap (
    MaHDN NVARCHAR(30) NOT NULL PRIMARY KEY,
    NgayNhap DATETIME NOT NULL DEFAULT GETDATE(),
    MaNV NVARCHAR(20) NOT NULL,
    MaNCC NVARCHAR(20) NOT NULL,
    TongTien DECIMAL(18,0) NOT NULL DEFAULT 0,

    CONSTRAINT FK_HDN_NV FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),
    CONSTRAINT FK_HDN_NCC FOREIGN KEY (MaNCC) REFERENCES NhaCungCap(MaNCC)
);

-- 20. Bảng Chi tiết Hóa đơn nhập
CREATE TABLE ChiTietHDN (
    MaHDN NVARCHAR(30) NOT NULL,
    MaMayVT NVARCHAR(20) NOT NULL,
    SoLuong INT NOT NULL CHECK (SoLuong > 0),
    DonGia DECIMAL(18,0) NOT NULL CHECK (DonGia >= 0),
    ThanhTien DECIMAL(18,0) NOT NULL DEFAULT 0,

    PRIMARY KEY (MaHDN, MaMayVT),
    CONSTRAINT FK_CTHDN_HDN FOREIGN KEY (MaHDN) REFERENCES HoaDonNhap(MaHDN) ON DELETE CASCADE,
    CONSTRAINT FK_CTHDN_MVT FOREIGN KEY (MaMayVT) REFERENCES MayViTinh(MaMayVT)
);

-- 21. Bảng Hóa đơn bán
CREATE TABLE HoaDonBan (
    MaHDB NVARCHAR(30) NOT NULL PRIMARY KEY,
    NgayBan DATETIME NOT NULL DEFAULT GETDATE(),
    MaNV NVARCHAR(20) NOT NULL,
    MaKhach NVARCHAR(20) NOT NULL,
    TongTien DECIMAL(18,0) NOT NULL DEFAULT 0,

    CONSTRAINT FK_HDB_NV FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),
    CONSTRAINT FK_HDB_KH FOREIGN KEY (MaKhach) REFERENCES KhachHang(MaKhach)
);

-- 22. Bảng Chi tiết Hóa đơn bán
CREATE TABLE ChiTietHDB (
    MaHDB NVARCHAR(30) NOT NULL,
    MaMayVT NVARCHAR(20) NOT NULL,
    SoLuong INT NOT NULL CHECK (SoLuong > 0),
    DonGia DECIMAL(18,0) NOT NULL CHECK (DonGia >= 0),
    ThanhTien DECIMAL(18,0) NOT NULL DEFAULT 0,

    PRIMARY KEY (MaHDB, MaMayVT),
    CONSTRAINT FK_CTHDB_HDB FOREIGN KEY (MaHDB) REFERENCES HoaDonBan(MaHDB) ON DELETE CASCADE,
    CONSTRAINT FK_CTHDB_MVT FOREIGN KEY (MaMayVT) REFERENCES MayViTinh(MaMayVT)
);
GO

-- =========================================================================
-- 5. TRIGGER TỰ ĐỘNG HÓA YÊU CẦU 1, 2, 3 TRÊN ChiTietHDN & ChiTietHDB
-- =========================================================================

-- Trigger trên ChiTietHDN: Cập nhật Số lượng, Giá nhập, Giá bán = 110% * Giá nhập, Tổng tiền HDN
CREATE OR ALTER TRIGGER trg_ChiTietHDN_AUID
ON ChiTietHDN
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        -- 1. Cập nhật Thành tiền = SoLuong * DonGia cho các dòng vừa thêm/sửa
        IF EXISTS (SELECT 1 FROM inserted)
        BEGIN
            UPDATE ct
            SET ct.ThanhTien = ct.SoLuong * ct.DonGia
            FROM ChiTietHDN ct
            INNER JOIN inserted i ON ct.MaHDN = i.MaHDN AND ct.MaMayVT = i.MaMayVT;
        END

        -- 2. Tự động cộng/trừ số lượng tồn kho (Yêu cầu 1)
        --    Tự động cập nhật Giá nhập (Yêu cầu 2)
        --    Tự động tính Giá bán = 110% * Giá nhập (Yêu cầu 3)
        ;WITH Diff AS (
            SELECT 
                COALESCE(i.MaMayVT, d.MaMayVT) AS MaMayVT,
                COALESCE(i.SoLuong, 0) - COALESCE(d.SoLuong, 0) AS DeltaSL,
                i.DonGia AS DonGiaMoi
            FROM inserted i
            FULL OUTER JOIN deleted d ON i.MaHDN = d.MaHDN AND i.MaMayVT = d.MaMayVT
        )
        UPDATE m
        SET 
            m.SoLuong = m.SoLuong + df.DeltaSL,
            m.GiaNhap = CASE WHEN df.DonGiaMoi IS NOT NULL THEN df.DonGiaMoi ELSE m.GiaNhap END,
            m.GiaBan = CASE WHEN df.DonGiaMoi IS NOT NULL THEN ROUND(df.DonGiaMoi * 1.10, 0) ELSE m.GiaBan END
        FROM MayViTinh m
        INNER JOIN Diff df ON m.MaMayVT = df.MaMayVT;

        -- 3. Cập nhật lại Tổng tiền trên bảng HoaDonNhap
        DECLARE @ListHDN TABLE (MaHDN NVARCHAR(30));
        INSERT INTO @ListHDN
        SELECT DISTINCT MaHDN FROM inserted
        UNION
        SELECT DISTINCT MaHDN FROM deleted;

        UPDATE hdn
        SET hdn.TongTien = ISNULL((SELECT SUM(ct.ThanhTien) FROM ChiTietHDN ct WHERE ct.MaHDN = hdn.MaHDN), 0)
        FROM HoaDonNhap hdn
        INNER JOIN @ListHDN l ON hdn.MaHDN = l.MaHDN;

    END TRY
    BEGIN CATCH
        DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrMsg, 16, 1);
        ROLLBACK TRANSACTION;
    END CATCH
END;
GO

-- Trigger trên ChiTietHDB: Giảm tồn kho, Ngăn chặn xuất âm, Cập nhật Tổng tiền HDB
CREATE OR ALTER TRIGGER trg_ChiTietHDB_AUID
ON ChiTietHDB
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        -- 1. Cập nhật Thành tiền cho các dòng vừa thêm/sửa
        IF EXISTS (SELECT 1 FROM inserted)
        BEGIN
            UPDATE ct
            SET ct.ThanhTien = ct.SoLuong * ct.DonGia
            FROM ChiTietHDB ct
            INNER JOIN inserted i ON ct.MaHDB = i.MaHDB AND ct.MaMayVT = i.MaMayVT;
        END

        -- 2. Kiểm tra tồn kho trước khi trừ (chống xuất kho âm)
        ;WITH Diff AS (
            SELECT 
                COALESCE(i.MaMayVT, d.MaMayVT) AS MaMayVT,
                COALESCE(i.SoLuong, 0) - COALESCE(d.SoLuong, 0) AS DeltaSL
            FROM inserted i
            FULL OUTER JOIN deleted d ON i.MaHDB = d.MaHDB AND i.MaMayVT = d.MaMayVT
        )
        SELECT df.MaMayVT, m.TenMayVT, m.SoLuong, df.DeltaSL
        INTO #CheckStock
        FROM Diff df
        INNER JOIN MayViTinh m ON df.MaMayVT = m.MaMayVT
        WHERE m.SoLuong < df.DeltaSL;

        IF EXISTS (SELECT 1 FROM #CheckStock)
        BEGIN
            DECLARE @TenSP NVARCHAR(150), @SLTon INT, @SLCan INT;
            SELECT TOP 1 @TenSP = TenMayVT, @SLTon = SoLuong, @SLCan = DeltaSL FROM #CheckStock;
            DROP TABLE #CheckStock;
            RAISERROR(N'Lỗi xuất kho: Sản phẩm [%s] chỉ còn tồn %d cái, không đủ để xuất %d cái!', 16, 1, @TenSP, @SLTon, @SLCan);
            ROLLBACK TRANSACTION;
            RETURN;
        END
        DROP TABLE #CheckStock;

        -- 3. Trừ số lượng tồn kho (Yêu cầu 1)
        ;WITH Diff AS (
            SELECT 
                COALESCE(i.MaMayVT, d.MaMayVT) AS MaMayVT,
                COALESCE(i.SoLuong, 0) - COALESCE(d.SoLuong, 0) AS DeltaSL
            FROM inserted i
            FULL OUTER JOIN deleted d ON i.MaHDB = d.MaHDB AND i.MaMayVT = d.MaMayVT
        )
        UPDATE m
        SET m.SoLuong = m.SoLuong - df.DeltaSL
        FROM MayViTinh m
        INNER JOIN Diff df ON m.MaMayVT = df.MaMayVT;

        -- 4. Cập nhật lại Tổng tiền trên bảng HoaDonBan
        DECLARE @ListHDB TABLE (MaHDB NVARCHAR(30));
        INSERT INTO @ListHDB
        SELECT DISTINCT MaHDB FROM inserted
        UNION
        SELECT DISTINCT MaHDB FROM deleted;

        UPDATE hdb
        SET hdb.TongTien = ISNULL((SELECT SUM(ct.ThanhTien) FROM ChiTietHDB ct WHERE ct.MaHDB = hdb.MaHDB), 0)
        FROM HoaDonBan hdb
        INNER JOIN @ListHDB l ON hdb.MaHDB = l.MaHDB;

    END TRY
    BEGIN CATCH
        DECLARE @ErrMsgHDB NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrMsgHDB, 16, 1);
        ROLLBACK TRANSACTION;
    END CATCH
END;
GO

-- =========================================================================
-- 6. INSERT DỮ LIỆU MẪU ĐẦY ĐỦ CHO 21 BẢNG (CHẠY THỬ F5 ĐƯỢC NGAY)
-- =========================================================================

-- 1. LoaiMay
INSERT INTO LoaiMay (MaLoai, TenLoai) VALUES 
(N'LM01', N'Máy tính xách tay (Laptop)'),
(N'LM02', N'Máy tính để bàn Gaming (PC Gaming)'),
(N'LM03', N'Máy tính để bàn Văn phòng (All-in-One)'),
(N'LM04', N'Máy trạm đồ họa chuyên nghiệp (Workstation)');

-- 2. Chip
INSERT INTO Chip (MaChip, TenChip) VALUES 
(N'CPU01', N'Intel Core i5 13400F (10 Cores, up to 4.6 GHz)'),
(N'CPU02', N'Intel Core i7 13700K (16 Cores, up to 5.4 GHz)'),
(N'CPU03', N'AMD Ryzen 5 7600X (6 Cores, up to 5.3 GHz)'),
(N'CPU04', N'AMD Ryzen 7 7800X3D (8 Cores, 3D V-Cache)');

-- 3. OCung
INSERT INTO OCung (MaOCung, TenOCung) VALUES 
(N'OC01', N'SSD M.2 NVMe PCIe Gen4 x4'),
(N'OC02', N'SSD 2.5 inch SATA III'),
(N'OC03', N'HDD 3.5 inch Western Digital Blue');

-- 4. DungLuong
INSERT INTO DungLuong (MaDungLuong, TenDungLuong) VALUES 
(N'DL01', N'512 GB'),
(N'DL02', N'1024 GB (1 TB)'),
(N'DL03', N'2048 GB (2 TB)');

-- 5. TocDo
INSERT INTO TocDo (MaTocDo, TenTocDo) VALUES 
(N'TD01', N'3500 MB/s (PCIe 3.0)'),
(N'TD02', N'7000 MB/s (PCIe 4.0 Siêu tốc)'),
(N'TD03', N'7200 RPM (HDD Standard)');

-- 6. OCD
INSERT INTO OCD (MaOCD, TenOCD) VALUES 
(N'CD01', N'Không trang bị ổ CD'),
(N'CD02', N'Ổ đĩa DVD-RW ghi đọc gắn ngoài');

-- 7. ManHinh
INSERT INTO ManHinh (MaManHinh, TenManHinh) VALUES 
(N'MH01', N'Màn hình IPS Full HD 144Hz'),
(N'MH02', N'Màn hình IPS 2K QHD 165Hz Chống chói'),
(N'MH03', N'Màn hình OLED 4K UHD 120Hz Chuẩn màu');

-- 8. CoMH
INSERT INTO CoMH (MaCoMH, TenCoMH) VALUES 
(N'CO01', N'14.0 inch'),
(N'CO02', N'15.6 inch'),
(N'CO03', N'24.0 inch (Desktop)'),
(N'CO04', N'27.0 inch (Desktop)');

-- 9. Chuot
INSERT INTO Chuot (MaChuot, TenChuot) VALUES 
(N'CH01', N'Chuột không dây Logitech Silent M331'),
(N'CH02', N'Chuột Gaming Razer DeathAdder Essential'),
(N'CH03', N'Chuột quang văn phòng có dây Genius');

-- 10. BanPhim
INSERT INTO BanPhim (MaBanPhim, TenBanPhim) VALUES 
(N'BP01', N'Bàn phím cơ DareU EK87 Red Switch RGB'),
(N'BP02', N'Bàn phím cơ không dây Asus ROG Azoth'),
(N'BP03', N'Bàn phím cao su văn phòng Logitech K120');

-- 11. USB
INSERT INTO USB (MaUSB, TenUSB, DungLuong) VALUES 
(N'USB01', N'Kingston DataTraveler Exodia USB 3.2', N'64 GB'),
(N'USB02', N'SanDisk Ultra Flair USB 3.0 Vỏ kim loại', N'128 GB'),
(N'USB03', N'Samsung Type-C Flash Drive', N'256 GB');

-- 12. RAM
INSERT INTO RAM (MaRAM, TenRAM) VALUES 
(N'RAM01', N'16GB (2x8GB) DDR4 3200MHz Kingston Fury'),
(N'RAM02', N'32GB (2x16GB) DDR5 6000MHz Corsair Vengeance'),
(N'RAM03', N'64GB (2x32GB) DDR5 6400MHz G.Skill Trident Z5');

-- 13. Loa
INSERT INTO Loa (MaLoa, TenLoa) VALUES 
(N'LOA01', N'Loa tích hợp sẵn Stereo Waves MaxxAudio'),
(N'LOA02', N'Loa để bàn Microlab M-108 2.1 Siêu trầm'),
(N'LOA03', N'Loa Soundbar Gaming Edifier G2000 RGB');

-- 14. HangSanXuat
INSERT INTO HangSanXuat (MaHangSX, TenHangSX) VALUES 
(N'HSX01', N'ASUS ROG / TUF Gaming'),
(N'HSX02', N'DELL Technologies'),
(N'HSX03', N'MSI Gaming Series'),
(N'HSX04', N'HP (Hewlett-Packard)');

-- 15. NhanVien
INSERT INTO NhanVien (MaNV, TenNV, NgaySinh, GioiTinh, DiaChi, DienThoai) VALUES 
(N'NV01', N'Nguyễn Hoàng Long', '1995-04-12', N'Nam', N'128 Cầu Giấy, Hà Nội', '0912345678'),
(N'NV02', N'Trần Thị Mai Phương', '1998-08-20', N'Nữ', N'45 Giải Phóng, Hai Bà Trưng, Hà Nội', '0987654321'),
(N'NV03', N'Lê Minh Tuấn', '1994-11-05', N'Nam', N'78 Nguyễn Trãi, Thanh Xuân, Hà Nội', '0903112233'),
(N'NV04', N'Phạm Thu Hà', '1999-02-14', N'Nữ', N'12 Kim Mã, Ba Đình, Hà Nội', '0936445566'),
(N'NV05', N'Đỗ Quang Huy', '1997-07-28', N'Nam', N'90 Hoàng Mai, Hà Nội', '0945889900');

-- 16. KhachHang
INSERT INTO KhachHang (MaKhach, TenKhach, DiaChi, DienThoai) VALUES 
(N'KH01', N'Công ty CP Công Nghệ NextTech', N'Tầng 5 Keangnam Landmark, Nam Từ Liêm', '0243123456'),
(N'KH02', N'Vũ Đình Khang', N'15 Hoàng Quốc Việt, Cầu Giấy, Hà Nội', '0977889901'),
(N'KH03', N'Hoàng Yến Nhi', N'22 Tây Sơn, Đống Đa, Hà Nội', '0911223344'),
(N'KH04', N'Trần Đức Anh', N'36 Lê Văn Lương, Cầu Giấy, Hà Nội', '0988776655');

-- 17. NhaCungCap
INSERT INTO NhaCungCap (MaNCC, TenNCC, DiaChi, DienThoai) VALUES 
(N'NCC01', N'Tổng Kho Máy Tính Viễn Sơn', N'Số 175 Phố Vọng, Hai Bà Trưng, Hà Nội', '0243888999'),
(N'NCC02', N'Công ty Phân Phối Synnex FPT', N'Tòa nhà FPT Cầu Giấy, Hà Nội', '0247300666'),
(N'NCC03', N'Công ty TNHH Phân Phối Dầu Khí PSD', N'Tầng 6 Petrovietnam Tower, Hà Nội', '0243933221');

-- 18. MayViTinh (Khởi tạo sẵn 4 máy ban đầu: Số lượng = 0, Giá nhập = 0, Giá bán = 0 để kiểm tra trigger nhập hàng tự động)
INSERT INTO MayViTinh (MaMayVT, TenMayVT, MaLoai, MaChip, MaOCung, MaDungLuong, MaTocDo, MaOCD, MaManHinh, MaCoMH, MaChuot, MaBanPhim, MaUSB, MaRAM, MaLoa, MaHangSX, GiaNhap, GiaBan, ThoiGianBH, SoLuong, Anh, GhiChu) VALUES 
(N'MVT01', N'Laptop ASUS ROG Strix G16', N'LM01', N'CPU02', N'OC01', N'DL02', N'TD02', N'CD01', N'MH02', N'CO02', N'CH02', N'BP02', N'USB02', N'RAM02', N'LOA01', N'HSX01', 0, 0, 24, 0, N'asus_rog.jpg', N'Laptop Gaming cao cấp 2024'),
(N'MVT02', N'Laptop Dell XPS 15 9530', N'LM01', N'CPU02', N'OC01', N'DL02', N'TD02', N'CD01', N'MH03', N'CO02', N'CH01', N'BP03', N'USB03', N'RAM02', N'LOA01', N'HSX02', 0, 0, 12, 0, N'dell_xps.jpg', N'Dòng doanh nhân mỏng nhẹ sang trọng'),
(N'MVT03', N'PC Gaming MSI Dragon Core', N'LM02', N'CPU01', N'OC01', N'DL01', N'TD01', N'CD01', N'MH01', N'CO03', N'CH02', N'BP01', N'USB01', N'RAM01', N'LOA03', N'HSX03', 0, 0, 36, 0, N'msi_gaming.jpg', N'PC Gaming hiệu năng cao giá tốt'),
(N'MVT04', N'Máy trạm HP Z4 Workstation', N'LM04', N'CPU04', N'OC01', N'DL03', N'TD02', N'CD02', N'MH03', N'CO04', N'CH01', N'BP01', N'USB03', N'RAM03', N'LOA02', N'HSX04', 0, 0, 36, 0, N'hp_workstation.jpg', N'Chuyên render kiến trúc, 3D max');

-- 19 & 20: Nhập Hóa đơn nhập mẫu (Kích hoạt Trigger để tự sinh Số lượng, Giá nhập và Giá bán = 110% * Giá nhập)
INSERT INTO HoaDonNhap (MaHDN, NgayNhap, MaNV, MaNCC, TongTien) VALUES 
(N'HDN001', '2026-01-15 09:30:00', N'NV01', N'NCC01', 0),
(N'HDN002', '2026-02-20 14:15:00', N'NV02', N'NCC02', 0),
(N'HDN003', '2026-03-10 10:00:00', N'NV03', N'NCC03', 0);

-- Chi tiết hóa đơn nhập:
-- MVT01: Nhập 10 cái, Đơn giá 30,000,000 -> Giá bán tự động cập nhật = 33,000,000
-- MVT02: Nhập 8 cái, Đơn giá 40,000,000 -> Giá bán tự động cập nhật = 44,000,000
-- MVT03: Nhập 15 cái, Đơn giá 18,000,000 -> Giá bán tự động cập nhật = 19,800,000
-- MVT04: Nhập 5 cái, Đơn giá 50,000,000 -> Giá bán tự động cập nhật = 55,000,000
INSERT INTO ChiTietHDN (MaHDN, MaMayVT, SoLuong, DonGia, ThanhTien) VALUES 
(N'HDN001', N'MVT01', 10, 30000000, 0),
(N'HDN001', N'MVT02', 8, 40000000, 0),
(N'HDN002', N'MVT03', 15, 18000000, 0),
(N'HDN003', N'MVT04', 5, 50000000, 0);

-- 21 & 22: Nhập Hóa đơn bán mẫu (Kích hoạt Trigger để trừ tồn kho và tính doanh thu bán hàng)
INSERT INTO HoaDonBan (MaHDB, NgayBan, MaNV, MaKhach, TongTien) VALUES 
(N'HDB001', '2026-01-20 10:30:00', N'NV01', N'KH01', 0),
(N'HDB002', '2026-02-25 15:00:00', N'NV01', N'KH02', 0),
(N'HDB003', '2026-03-15 16:45:00', N'NV02', N'KH03', 0),
(N'HDB004', '2026-03-22 11:20:00', N'NV03', N'KH04', 0);

-- Chi tiết hóa đơn bán:
-- HDB001: Bán 2 cái MVT01 (đơn giá 33,000,000) và 1 cái MVT02 (đơn giá 44,000,000)
-- HDB002: Bán 3 cái MVT03 (đơn giá 19,800,000)
-- HDB003: Bán 1 cái MVT01 (đơn giá 33,000,000)
-- HDB004: Bán 1 cái MVT04 (đơn giá 55,000,000)
INSERT INTO ChiTietHDB (MaHDB, MaMayVT, SoLuong, DonGia, ThanhTien) VALUES 
(N'HDB001', N'MVT01', 2, 33000000, 0),
(N'HDB001', N'MVT02', 1, 44000000, 0),
(N'HDB002', N'MVT03', 3, 19800000, 0),
(N'HDB003', N'MVT01', 1, 33000000, 0),
(N'HDB004', N'MVT04', 1, 55000000, 0);
GO

-- Kiểm tra kết quả tự động cập nhật của Trigger sau khi insert dữ liệu mẫu:
PRINT N'=== KIỂM TRA TRẠNG THÁI BẢNG MÁY VI TÍNH SAU TRIGGER ===';
SELECT MaMayVT, TenMayVT, SoLuong, GiaNhap, GiaBan FROM MayViTinh;
PRINT N'=== KIỂM TRA TỔNG TIỀN HÓA ĐƠN NHẬP ===';
SELECT MaHDN, NgayNhap, TongTien FROM HoaDonNhap;
PRINT N'=== KIỂM TRA TỔNG TIỀN HÓA ĐƠN BÁN ===';
SELECT MaHDB, NgayBan, TongTien FROM HoaDonBan;
GO

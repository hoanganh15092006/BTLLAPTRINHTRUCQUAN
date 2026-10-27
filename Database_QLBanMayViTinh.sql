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

CREATE TABLE LoaiMay (
    MaLoai NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenLoai NVARCHAR(100) NOT NULL
);

CREATE TABLE Chip (
    MaChip NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenChip NVARCHAR(100) NOT NULL
);

CREATE TABLE OCung (
    MaOCung NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenOCung NVARCHAR(100) NOT NULL
);

CREATE TABLE DungLuong (
    MaDungLuong NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenDungLuong NVARCHAR(50) NOT NULL
);

CREATE TABLE TocDo (
    MaTocDo NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenTocDo NVARCHAR(50) NOT NULL
);

CREATE TABLE OCD (
    MaOCD NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenOCD NVARCHAR(100) NOT NULL
);

CREATE TABLE ManHinh (
    MaManHinh NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenManHinh NVARCHAR(100) NOT NULL
);

CREATE TABLE CoMH (
    MaCoMH NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenCoMH NVARCHAR(50) NOT NULL
);

CREATE TABLE Chuot (
    MaChuot NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenChuot NVARCHAR(100) NOT NULL
);

CREATE TABLE BanPhim (
    MaBanPhim NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenBanPhim NVARCHAR(100) NOT NULL
);

CREATE TABLE USB (
    MaUSB NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenUSB NVARCHAR(100) NOT NULL,
    DungLuong NVARCHAR(50) NULL
);

CREATE TABLE RAM (
    MaRAM NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenRAM NVARCHAR(100) NOT NULL
);

CREATE TABLE Loa (
    MaLoa NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenLoa NVARCHAR(100) NOT NULL
);

CREATE TABLE HangSanXuat (
    MaHangSX NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenHangSX NVARCHAR(100) NOT NULL
);

CREATE TABLE NhanVien (
    MaNV NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenNV NVARCHAR(100) NOT NULL,
    NgaySinh DATE NULL,
    GioiTinh NVARCHAR(10) NULL,
    DiaChi NVARCHAR(250) NULL,
    DienThoai NVARCHAR(20) NULL
);

CREATE TABLE KhachHang (
    MaKhach NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenKhach NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(250) NULL,
    DienThoai NVARCHAR(20) NULL
);

CREATE TABLE NhaCungCap (
    MaNCC NVARCHAR(20) NOT NULL PRIMARY KEY,
    TenNCC NVARCHAR(150) NOT NULL,
    DiaChi NVARCHAR(250) NULL,
    DienThoai NVARCHAR(20) NULL
);

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

CREATE TABLE HoaDonNhap (
    MaHDN NVARCHAR(30) NOT NULL PRIMARY KEY,
    NgayNhap DATETIME NOT NULL DEFAULT GETDATE(),
    MaNV NVARCHAR(20) NOT NULL,
    MaNCC NVARCHAR(20) NOT NULL,
    TongTien DECIMAL(18,0) NOT NULL DEFAULT 0,

    CONSTRAINT FK_HDN_NV FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),
    CONSTRAINT FK_HDN_NCC FOREIGN KEY (MaNCC) REFERENCES NhaCungCap(MaNCC)
);

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

CREATE TABLE HoaDonBan (
    MaHDB NVARCHAR(30) NOT NULL PRIMARY KEY,
    NgayBan DATETIME NOT NULL DEFAULT GETDATE(),
    MaNV NVARCHAR(20) NOT NULL,
    MaKhach NVARCHAR(20) NOT NULL,
    TongTien DECIMAL(18,0) NOT NULL DEFAULT 0,

    CONSTRAINT FK_HDB_NV FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),
    CONSTRAINT FK_HDB_KH FOREIGN KEY (MaKhach) REFERENCES KhachHang(MaKhach)
);

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

CREATE TABLE tblTaiKhoan (
    TenDangNhap NVARCHAR(50) NOT NULL PRIMARY KEY,
    MatKhauHash NVARCHAR(255) NOT NULL,
    Salt NVARCHAR(100) NOT NULL,
    MaNV NVARCHAR(20) NULL,
    VaiTro NVARCHAR(50) NOT NULL DEFAULT 'BanHang',
    TrangThai NVARCHAR(50) NOT NULL DEFAULT N'Hoạt động',
    NgayTao DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_TK_NV FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);

CREATE TABLE tblNhatKyHoatDong (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TenDangNhap NVARCHAR(50) NULL,
    HanhDong NVARCHAR(100) NOT NULL,
    ThoiGian DATETIME NOT NULL DEFAULT GETDATE(),
    ChiTiet NVARCHAR(500) NULL
);

CREATE TABLE tblCauHinhHeThong (
    KeyName NVARCHAR(100) NOT NULL PRIMARY KEY,
    KeyValue NVARCHAR(1000) NULL
);
GO

CREATE OR ALTER TRIGGER trg_ChiTietHDN_AUID
ON ChiTietHDN
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF EXISTS (SELECT 1 FROM inserted)
        BEGIN
            UPDATE ct
            SET ct.ThanhTien = ct.SoLuong * ct.DonGia
            FROM ChiTietHDN ct
            INNER JOIN inserted i ON ct.MaHDN = i.MaHDN AND ct.MaMayVT = i.MaMayVT;
        END

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

CREATE OR ALTER TRIGGER trg_ChiTietHDB_AUID
ON ChiTietHDB
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF EXISTS (SELECT 1 FROM inserted)
        BEGIN
            UPDATE ct
            SET ct.ThanhTien = ct.SoLuong * ct.DonGia
            FROM ChiTietHDB ct
            INNER JOIN inserted i ON ct.MaHDB = i.MaHDB AND ct.MaMayVT = i.MaMayVT;
        END

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

INSERT INTO LoaiMay (MaLoai, TenLoai) VALUES 
(N'LM01', N'Máy tính xách tay (Laptop)'),
(N'LM02', N'Máy tính để bàn Gaming (PC Gaming)'),
(N'LM03', N'Máy tính để bàn Văn phòng (All-in-One)'),
(N'LM04', N'Máy trạm đồ họa chuyên nghiệp (Workstation)');

INSERT INTO Chip (MaChip, TenChip) VALUES 
(N'CPU01', N'Intel Core i5 13400F (10 Cores, up to 4.6 GHz)'),
(N'CPU02', N'Intel Core i7 13700K (16 Cores, up to 5.4 GHz)'),
(N'CPU03', N'AMD Ryzen 5 7600X (6 Cores, up to 5.3 GHz)'),
(N'CPU04', N'AMD Ryzen 7 7800X3D (8 Cores, 3D V-Cache)');

INSERT INTO OCung (MaOCung, TenOCung) VALUES 
(N'OC01', N'SSD M.2 NVMe PCIe Gen4 x4'),
(N'OC02', N'SSD 2.5 inch SATA III'),
(N'OC03', N'HDD 3.5 inch Western Digital Blue');

INSERT INTO DungLuong (MaDungLuong, TenDungLuong) VALUES 
(N'DL01', N'512 GB'),
(N'DL02', N'1024 GB (1 TB)'),
(N'DL03', N'2048 GB (2 TB)');

INSERT INTO TocDo (MaTocDo, TenTocDo) VALUES 
(N'TD01', N'3500 MB/s (PCIe 3.0)'),
(N'TD02', N'7000 MB/s (PCIe 4.0 Siêu tốc)'),
(N'TD03', N'7200 RPM (HDD Standard)');

INSERT INTO OCD (MaOCD, TenOCD) VALUES 
(N'CD01', N'Không trang bị ổ CD'),
(N'CD02', N'Ổ đĩa DVD-RW ghi đọc gắn ngoài');

INSERT INTO ManHinh (MaManHinh, TenManHinh) VALUES 
(N'MH01', N'Màn hình IPS Full HD 144Hz'),
(N'MH02', N'Màn hình IPS 2K QHD 165Hz Chống chói'),
(N'MH03', N'Màn hình OLED 4K UHD 120Hz Chuẩn màu');

INSERT INTO CoMH (MaCoMH, TenCoMH) VALUES 
(N'CO01', N'14.0 inch'),
(N'CO02', N'15.6 inch'),
(N'CO03', N'24.0 inch (Desktop)'),
(N'CO04', N'27.0 inch (Desktop)');

INSERT INTO Chuot (MaChuot, TenChuot) VALUES 
(N'CH01', N'Chuột không dây Logitech Silent M331'),
(N'CH02', N'Chuột Gaming Razer DeathAdder Essential'),
(N'CH03', N'Chuột quang văn phòng có dây Genius');

INSERT INTO BanPhim (MaBanPhim, TenBanPhim) VALUES 
(N'BP01', N'Bàn phím cơ DareU EK87 Red Switch RGB'),
(N'BP02', N'Bàn phím cơ không dây Asus ROG Azoth'),
(N'BP03', N'Bàn phím cao su văn phòng Logitech K120');

INSERT INTO USB (MaUSB, TenUSB, DungLuong) VALUES 
(N'USB01', N'Kingston DataTraveler Exodia USB 3.2', N'64 GB'),
(N'USB02', N'SanDisk Ultra Flair USB 3.0 Vỏ kim loại', N'128 GB'),
(N'USB03', N'Samsung Type-C Flash Drive', N'256 GB');

INSERT INTO RAM (MaRAM, TenRAM) VALUES 
(N'RAM01', N'16GB (2x8GB) DDR4 3200MHz Kingston Fury'),
(N'RAM02', N'32GB (2x16GB) DDR5 6000MHz Corsair Vengeance'),
(N'RAM03', N'64GB (2x32GB) DDR5 6400MHz G.Skill Trident Z5');

INSERT INTO Loa (MaLoa, TenLoa) VALUES 
(N'LOA01', N'Loa tích hợp sẵn Stereo Waves MaxxAudio'),
(N'LOA02', N'Loa để bàn Microlab M-108 2.1 Siêu trầm'),
(N'LOA03', N'Loa Soundbar Gaming Edifier G2000 RGB');

INSERT INTO HangSanXuat (MaHangSX, TenHangSX) VALUES 
(N'HSX01', N'ASUS ROG / TUF Gaming'),
(N'HSX02', N'DELL Technologies'),
(N'HSX03', N'MSI Gaming Series'),
(N'HSX04', N'HP (Hewlett-Packard)');

INSERT INTO NhanVien (MaNV, TenNV, NgaySinh, GioiTinh, DiaChi, DienThoai) VALUES 
(N'NV01', N'Nguyễn Hoàng Long', '1995-04-12', N'Nam', N'128 Cầu Giấy, Hà Nội', '0912345678'),
(N'NV02', N'Trần Thị Mai Phương', '1998-08-20', N'Nữ', N'45 Giải Phóng, Hai Bà Trưng, Hà Nội', '0987654321'),
(N'NV03', N'Lê Minh Tuấn', '1994-11-05', N'Nam', N'78 Nguyễn Trãi, Thanh Xuân, Hà Nội', '0903112233'),
(N'NV04', N'Phạm Thu Hà', '1999-02-14', N'Nữ', N'12 Kim Mã, Ba Đình, Hà Nội', '0936445566'),
(N'NV05', N'Đỗ Quang Huy', '1997-07-28', N'Nam', N'90 Hoàng Mai, Hà Nội', '0945889900');

INSERT INTO KhachHang (MaKhach, TenKhach, DiaChi, DienThoai) VALUES 
(N'KH01', N'Công ty CP Công Nghệ NextTech', N'Tầng 5 Keangnam Landmark, Nam Từ Liêm', '0243123456'),
(N'KH02', N'Vũ Đình Khang', N'15 Hoàng Quốc Việt, Cầu Giấy, Hà Nội', '0977889901'),
(N'KH03', N'Hoàng Yến Nhi', N'22 Tây Sơn, Đống Đa, Hà Nội', '0911223344'),
(N'KH04', N'Trần Đức Anh', N'36 Lê Văn Lương, Cầu Giấy, Hà Nội', '0988776655');

INSERT INTO NhaCungCap (MaNCC, TenNCC, DiaChi, DienThoai) VALUES 
(N'NCC01', N'Tổng Kho Máy Tính Viễn Sơn', N'Số 175 Phố Vọng, Hai Bà Trưng, Hà Nội', '0243888999'),
(N'NCC02', N'Công ty Phân Phối Synnex FPT', N'Tòa nhà FPT Cầu Giấy, Hà Nội', '0247300666'),
(N'NCC03', N'Công ty TNHH Phân Phối Dầu Khí PSD', N'Tầng 6 Petrovietnam Tower, Hà Nội', '0243933221');

INSERT INTO MayViTinh (MaMayVT, TenMayVT, MaLoai, MaChip, MaOCung, MaDungLuong, MaTocDo, MaOCD, MaManHinh, MaCoMH, MaChuot, MaBanPhim, MaUSB, MaRAM, MaLoa, MaHangSX, GiaNhap, GiaBan, ThoiGianBH, SoLuong, Anh, GhiChu) VALUES 
(N'MVT01', N'Laptop ASUS ROG Strix G16', N'LM01', N'CPU02', N'OC01', N'DL02', N'TD02', N'CD01', N'MH02', N'CO02', N'CH02', N'BP02', N'USB02', N'RAM02', N'LOA01', N'HSX01', 0, 0, 24, 0, N'asus_rog.jpg', N'Laptop Gaming cao cấp 2024'),
(N'MVT02', N'Laptop Dell XPS 15 9530', N'LM01', N'CPU02', N'OC01', N'DL02', N'TD02', N'CD01', N'MH03', N'CO02', N'CH01', N'BP03', N'USB03', N'RAM02', N'LOA01', N'HSX02', 0, 0, 12, 0, N'dell_xps.jpg', N'Dòng doanh nhân mỏng nhẹ sang trọng'),
(N'MVT03', N'PC Gaming MSI Dragon Core', N'LM02', N'CPU01', N'OC01', N'DL01', N'TD01', N'CD01', N'MH01', N'CO03', N'CH02', N'BP01', N'USB01', N'RAM01', N'LOA03', N'HSX03', 0, 0, 36, 0, N'msi_gaming.jpg', N'PC Gaming hiệu năng cao giá tốt'),
(N'MVT04', N'Máy trạm HP Z4 Workstation', N'LM04', N'CPU04', N'OC01', N'DL03', N'TD02', N'CD02', N'MH03', N'CO04', N'CH01', N'BP01', N'USB03', N'RAM03', N'LOA02', N'HSX04', 0, 0, 36, 0, N'hp_workstation.jpg', N'Chuyên render kiến trúc, 3D max');

INSERT INTO HoaDonNhap (MaHDN, NgayNhap, MaNV, MaNCC, TongTien) VALUES 
(N'HDN001', '2026-01-15 09:30:00', N'NV01', N'NCC01', 0),
(N'HDN002', '2026-02-20 14:15:00', N'NV02', N'NCC02', 0),
(N'HDN003', '2026-03-10 10:00:00', N'NV03', N'NCC03', 0);

INSERT INTO ChiTietHDN (MaHDN, MaMayVT, SoLuong, DonGia, ThanhTien) VALUES 
(N'HDN001', N'MVT01', 10, 30000000, 0),
(N'HDN001', N'MVT02', 8, 40000000, 0),
(N'HDN002', N'MVT03', 15, 18000000, 0),
(N'HDN003', N'MVT04', 5, 50000000, 0);

INSERT INTO HoaDonBan (MaHDB, NgayBan, MaNV, MaKhach, TongTien) VALUES 
(N'HDB001', '2026-01-20 10:30:00', N'NV01', N'KH01', 0),
(N'HDB002', '2026-02-25 15:00:00', N'NV01', N'KH02', 0),
(N'HDB003', '2026-03-15 16:45:00', N'NV02', N'KH03', 0),
(N'HDB004', '2026-03-22 11:20:00', N'NV03', N'KH04', 0);

INSERT INTO ChiTietHDB (MaHDB, MaMayVT, SoLuong, DonGia, ThanhTien) VALUES 
(N'HDB001', N'MVT01', 2, 33000000, 0),
(N'HDB001', N'MVT02', 1, 44000000, 0),
(N'HDB002', N'MVT03', 3, 19800000, 0),
(N'HDB003', N'MVT01', 1, 33000000, 0),
(N'HDB004', N'MVT04', 1, 55000000, 0);
GO

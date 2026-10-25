using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace QLBanMayViTinh.Classes
{
    /// <summary>
    /// Class hỗ trợ đồng bộ giao diện Modern Flat Dashboard sang trọng,
    /// tự động bật DoubleBuffered chống giật lag cho DataGridView,
    /// hiệu ứng Hover cho các nút chức năng và định dạng tiền tệ chuyên nghiệp.
    /// </summary>
    public static class UIThemeHelper
    {
        // Bảng màu hiện đại Modern Flat
        public static readonly Color ColorSidebar = Color.FromArgb(24, 24, 27);         // #18181B - Tối thanh lịch
        public static readonly Color ColorSidebarHover = Color.FromArgb(39, 39, 42);    // #27272A
        public static readonly Color ColorSidebarActive = Color.FromArgb(79, 70, 229);   // #4F46E5 - Indigo Active
        public static readonly Color ColorBackground = Color.FromArgb(244, 244, 245);   // #F4F4F5 - Trắng xám dịu mắt
        public static readonly Color ColorHeaderGrid = Color.FromArgb(30, 41, 59);      // #1E293B - Xanh than đậm
        public static readonly Color ColorRowEven = Color.White;
        public static readonly Color ColorRowOdd = Color.FromArgb(248, 250, 252);       // #F8FAFC
        public static readonly Color ColorRowSelected = Color.FromArgb(224, 231, 255);  // #E0E7FF

        // Bảng màu nút bấm chức năng (Modern Flat Buttons)
        public static readonly Color BtnThemColor = Color.FromArgb(16, 185, 129);   // #10B981 - Xanh lá Emerald
        public static readonly Color BtnSuaColor = Color.FromArgb(245, 158, 11);    // #F59E0B - Vàng Amber
        public static readonly Color BtnXoaColor = Color.FromArgb(239, 68, 68);     // #EF4444 - Đỏ Rose
        public static readonly Color BtnLuuColor = Color.FromArgb(59, 130, 246);    // #3B82F6 - Xanh Blue
        public static readonly Color BtnHuyColor = Color.FromArgb(107, 114, 128);   // #6B7280 - Xám Cool Gray
        public static readonly Color BtnTimColor = Color.FromArgb(99, 102, 241);    // #6366F1 - Tím Indigo
        public static readonly Color BtnInExcelColor = Color.FromArgb(5, 150, 105); // #059669 - Xanh ngọc Excel

        /// <summary>
        /// Bật tính năng DoubleBuffered cho DataGridView bằng Reflection để cuộn mượt, không nhấp nháy
        /// </summary>
        public static void SetDoubleBuffered(Control control)
        {
            typeof(Control).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, control, new object[] { true });
        }

        /// <summary>
        /// Trang trí DataGridView chuẩn mực, hiện đại: Header nền tối chữ trắng, dòng so le, căn giữa
        /// </summary>
        public static void StyleDataGridView(DataGridView dgv)
        {
            if (dgv == null) return;
            SetDoubleBuffered(dgv);

            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(226, 232, 240);

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = ColorHeaderGrid;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersHeight = 38;

            dgv.RowHeadersVisible = false;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(30, 41, 59);
            dgv.DefaultCellStyle.BackColor = ColorRowEven;
            dgv.DefaultCellStyle.SelectionBackColor = ColorRowSelected;
            dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);
            dgv.DefaultCellStyle.Padding = new Padding(4, 2, 4, 2);

            dgv.AlternatingRowsDefaultCellStyle.BackColor = ColorRowOdd;
            dgv.RowTemplate.Height = 32;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AllowUserToResizeRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        /// <summary>
        /// Tạo style Flat button bo viền đẹp và tự động gán hiệu ứng Hover
        /// </summary>
        public static void StyleButton(Button btn, Color backColor, Color foreColor)
        {
            if (btn == null) return;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = backColor;
            btn.ForeColor = foreColor;
            btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;

            // Hiệu ứng Hover đổi màu nhẹ
            btn.MouseEnter += (s, e) =>
            {
                btn.BackColor = ControlPaint.Light(backColor, 0.15f);
            };
            btn.MouseLeave += (s, e) =>
            {
                btn.BackColor = backColor;
            };
        }

        /// <summary>
        /// Định dạng hiển thị tiền tệ VNĐ cho các cột chỉ định trong DataGridView
        /// </summary>
        public static void FormatCurrencyColumns(DataGridView dgv, params string[] columnNames)
        {
            foreach (var col in columnNames)
            {
                if (dgv.Columns.Contains(col))
                {
                    dgv.Columns[col].DefaultCellStyle.Format = "#,##0 VNĐ";
                    dgv.Columns[col].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
        }
    }
}

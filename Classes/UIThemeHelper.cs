using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;

namespace QLBanMayViTinh.Classes
{

    public static class UIThemeHelper
    {

        public static readonly Color ColorBackground = Color.FromArgb(241, 245, 249);
        public static readonly Color ColorCard = Color.White;
        public static readonly Color ColorBorder = Color.FromArgb(226, 232, 240);
        public static readonly Color ColorBorderFocus = Color.FromArgb(99, 102, 241);

        public static readonly Color ColorTextPrimary = Color.FromArgb(15, 23, 42);
        public static readonly Color ColorTextSecondary = Color.FromArgb(100, 116, 139);
        public static readonly Color ColorTextMuted = Color.FromArgb(148, 163, 184);

        public static readonly Color ColorSidebar = Color.FromArgb(15, 23, 42);
        public static readonly Color ColorSidebarLogo = Color.FromArgb(10, 15, 30);
        public static readonly Color ColorSidebarHover = Color.FromArgb(30, 41, 59);
        public static readonly Color ColorSidebarActive = Color.FromArgb(79, 70, 229);
        public static readonly Color ColorSidebarText = Color.FromArgb(241, 245, 249);
        public static readonly Color ColorSidebarMuted = Color.FromArgb(148, 163, 184);

        public static readonly Color ColorPrimary = Color.FromArgb(79, 70, 229);
        public static readonly Color ColorPrimaryHover = Color.FromArgb(67, 56, 202);
        public static readonly Color ColorPrimaryLight = Color.FromArgb(238, 242, 255);

        public static readonly Color BtnThemColor = Color.FromArgb(16, 185, 129);
        public static readonly Color BtnSuaColor = Color.FromArgb(245, 158, 11);
        public static readonly Color BtnXoaColor = Color.FromArgb(239, 68, 68);
        public static readonly Color BtnLuuColor = Color.FromArgb(59, 130, 246);
        public static readonly Color BtnHuyColor = Color.FromArgb(100, 116, 139);
        public static readonly Color BtnTimColor = Color.FromArgb(99, 102, 241);
        public static readonly Color BtnInExcelColor = Color.FromArgb(5, 150, 105);
        public static readonly Color BtnDongColor = Color.FromArgb(71, 85, 105);

        public static readonly Color ColorHeaderGrid = Color.FromArgb(30, 41, 59);
        public static readonly Color ColorRowEven = Color.White;
        public static readonly Color ColorRowOdd = Color.FromArgb(248, 250, 252);
        public static readonly Color ColorRowSelected = Color.FromArgb(224, 231, 255);
        public static readonly Color ColorRowSelectedText = Color.FromArgb(30, 27, 75);

        public static void SetDoubleBuffered(Control control)
        {
            if (control == null) return;
            try
            {
                typeof(Control).InvokeMember("DoubleBuffered",
                    BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                    null, control, new object[] { true });
            }
            catch { }
        }

        public static void StyleCard(Panel pnl, Color? borderColor = null)
        {
            if (pnl == null) return;
            SetDoubleBuffered(pnl);
            pnl.BackColor = ColorCard;
            Color bColor = borderColor ?? ColorBorder;

            pnl.Paint += (s, e) =>
            {
                using (Pen pen = new Pen(bColor, 1))
                {
                    Rectangle rect = new Rectangle(0, 0, pnl.Width - 1, pnl.Height - 1);
                    e.Graphics.DrawRectangle(pen, rect);
                }
            };
        }

        public static void StyleButton(Button btn, Color backColor, Color foreColor)
        {
            if (btn == null) return;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = backColor;
            btn.ForeColor = foreColor;
            btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            btn.Height = Math.Max(btn.Height, 36);

            btn.MouseEnter += (s, e) =>
            {
                btn.BackColor = ControlPaint.Light(backColor, 0.15f);
            };
            btn.MouseLeave += (s, e) =>
            {
                btn.BackColor = backColor;
            };
        }

        public static void StylePrimaryButton(Button btn) => StyleButton(btn, ColorPrimary, Color.White);
        public static void StyleSuccessButton(Button btn) => StyleButton(btn, BtnThemColor, Color.White);
        public static void StyleWarningButton(Button btn) => StyleButton(btn, BtnSuaColor, Color.White);
        public static void StyleDangerButton(Button btn) => StyleButton(btn, BtnXoaColor, Color.White);
        public static void StyleSecondaryButton(Button btn) => StyleButton(btn, BtnHuyColor, Color.White);

        public static void StyleTextBox(TextBox txt)
        {
            if (txt == null) return;
            txt.BorderStyle = BorderStyle.FixedSingle;
            txt.Font = new Font("Segoe UI", 9.5F);
            txt.ForeColor = ColorTextPrimary;
            txt.BackColor = Color.White;
        }

        public static void StyleComboBox(ComboBox cbo)
        {
            if (cbo == null) return;
            cbo.FlatStyle = FlatStyle.Flat;
            cbo.Font = new Font("Segoe UI", 9.5F);
            cbo.ForeColor = ColorTextPrimary;
            cbo.BackColor = Color.White;
            cbo.DropDownHeight = 220;
        }

        public static void StyleDataGridView(DataGridView dgv)
        {
            if (dgv == null) return;
            SetDoubleBuffered(dgv);

            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(241, 245, 249);

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = ColorHeaderGrid;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersHeight = 38;

            dgv.RowHeadersVisible = false;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgv.DefaultCellStyle.ForeColor = ColorTextPrimary;
            dgv.DefaultCellStyle.BackColor = ColorRowEven;
            dgv.DefaultCellStyle.SelectionBackColor = ColorRowSelected;
            dgv.DefaultCellStyle.SelectionForeColor = ColorRowSelectedText;
            dgv.DefaultCellStyle.Padding = new Padding(6, 2, 6, 2);

            dgv.AlternatingRowsDefaultCellStyle.BackColor = ColorRowOdd;
            dgv.RowTemplate.Height = 32;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AllowUserToResizeRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

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

        public static Panel CreateKpiCard(string title, string value, string subtitle, Color accentColor, string iconEmoji, int width = 230, int height = 80)
        {
            Panel card = new Panel();
            card.Size = new Size(width, height);
            card.BackColor = ColorCard;
            SetDoubleBuffered(card);

            card.Paint += (s, e) =>
            {
                using (Pen pen = new Pen(ColorBorder, 1))
                {
                    e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, card.Width - 1, card.Height - 1));
                }
                using (SolidBrush brush = new SolidBrush(accentColor))
                {
                    e.Graphics.FillRectangle(brush, new Rectangle(0, 0, 5, card.Height));
                }
            };

            Label lblIcon = new Label();
            lblIcon.Text = iconEmoji;
            lblIcon.Font = new Font("Segoe UI", 18F);
            lblIcon.Location = new Point(12, 18);
            lblIcon.Size = new Size(42, 42);
            lblIcon.TextAlign = ContentAlignment.MiddleCenter;

            Label lblTitle = new Label();
            lblTitle.Text = title.ToUpper();
            lblTitle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblTitle.ForeColor = ColorTextSecondary;
            lblTitle.Location = new Point(58, 12);
            lblTitle.Size = new Size(width - 65, 16);

            Label lblVal = new Label();
            lblVal.Text = value;
            lblVal.Font = new Font("Segoe UI", 13.5F, FontStyle.Bold);
            lblVal.ForeColor = ColorTextPrimary;
            lblVal.Location = new Point(58, 28);
            lblVal.Size = new Size(width - 65, 26);

            Label lblSub = new Label();
            lblSub.Text = subtitle;
            lblSub.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            lblSub.ForeColor = ColorTextMuted;
            lblSub.Location = new Point(58, 54);
            lblSub.Size = new Size(width - 65, 18);

            card.Controls.Add(lblIcon);
            card.Controls.Add(lblTitle);
            card.Controls.Add(lblVal);
            card.Controls.Add(lblSub);

            return card;
        }
    }
}

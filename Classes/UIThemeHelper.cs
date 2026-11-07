using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace QLBanMayViTinh.Classes
{
    public static class UIThemeHelper
    {
        private static bool isDarkMode = false;

        public static bool IsDarkMode
        {
            get => isDarkMode;
            set
            {
                if (isDarkMode != value)
                {
                    isDarkMode = value;
                    OnThemeChanged?.Invoke();
                }
            }
        }

        public static event Action OnThemeChanged;

        public static Color ColorBackground => IsDarkMode ? Color.FromArgb(15, 23, 42) : Color.FromArgb(241, 245, 249);
        public static Color ColorCard => IsDarkMode ? Color.FromArgb(30, 41, 59) : Color.White;
        public static Color ColorBorder => IsDarkMode ? Color.FromArgb(100, 116, 139) : Color.FromArgb(148, 163, 184);
        public static Color ColorBorderFocus => Color.FromArgb(79, 70, 229);
        public static Color ColorBorderHover => IsDarkMode ? Color.FromArgb(148, 163, 184) : Color.FromArgb(100, 116, 139);

        public static Color ColorTextPrimary => IsDarkMode ? Color.FromArgb(248, 250, 252) : Color.FromArgb(15, 23, 42);
        public static Color ColorTextSecondary => IsDarkMode ? Color.FromArgb(203, 213, 225) : Color.FromArgb(100, 116, 139);
        public static Color ColorTextMuted => Color.FromArgb(148, 163, 184);

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

        public static Color ColorHeaderGrid => IsDarkMode ? Color.FromArgb(15, 23, 42) : Color.FromArgb(30, 41, 59);
        public static Color ColorRowEven => IsDarkMode ? Color.FromArgb(30, 41, 59) : Color.White;
        public static Color ColorRowOdd => IsDarkMode ? Color.FromArgb(22, 32, 50) : Color.FromArgb(248, 250, 252);
        public static Color ColorRowSelected => IsDarkMode ? Color.FromArgb(67, 56, 202) : Color.FromArgb(224, 231, 255);
        public static Color ColorRowSelectedText => IsDarkMode ? Color.White : Color.FromArgb(30, 27, 75);

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

        public static GraphicsPath GetRoundedRectanglePath(Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            if (bounds.Width <= 0 || bounds.Height <= 0)
            {
                return path;
            }
            if (radius <= 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            int diameter = radius * 2;
            if (diameter > bounds.Width) diameter = bounds.Width;
            if (diameter > bounds.Height) diameter = bounds.Height;

            Rectangle arc = new Rectangle(bounds.Location, new Size(diameter, diameter));
            path.AddArc(arc, 180, 90);
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();
            return path;
        }

        public static void ApplyRoundedRegion(Control control, int radius)
        {
            if (control == null || control.Width <= 0 || control.Height <= 0) return;
            using (GraphicsPath path = GetRoundedRectanglePath(new Rectangle(0, 0, control.Width, control.Height), radius))
            {
                Region old = control.Region;
                control.Region = new Region(path);
                if (old != null)
                {
                    old.Dispose();
                }
            }
        }

        public static void MakeRoundedControl(Control control, int radius, Color? borderColor = null)
        {
            if (control == null) return;
            SetDoubleBuffered(control);

            if (control.Width > 0 && control.Height > 0)
            {
                ApplyRoundedRegion(control, radius);
            }

            string tagStr = control.Tag != null ? control.Tag.ToString() : "";
            if (tagStr.Contains("ThemeRounded"))
            {
                return;
            }
            control.Tag = (string.IsNullOrEmpty(tagStr) ? "" : tagStr + ";") + "ThemeRounded";

            control.Resize += (s, e) =>
            {
                if (control.Width > 0 && control.Height > 0)
                {
                    ApplyRoundedRegion(control, radius);
                }
            };

            control.Paint += (s, e) =>
            {
                if (e.Graphics == null || control.Width <= 1 || control.Height <= 1) return;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Color bColor = borderColor ?? ColorBorder;
                using (Pen pen = new Pen(bColor, 1.2f))
                using (GraphicsPath path = GetRoundedRectanglePath(new Rectangle(0, 0, control.Width - 1, control.Height - 1), radius))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            };
        }

        public static void StyleCard(Panel pnl, Color? borderColor = null, int radius = 8)
        {
            if (pnl == null) return;
            SetDoubleBuffered(pnl);
            pnl.BackColor = ColorCard;
            MakeRoundedControl(pnl, radius, borderColor);
        }

        public static void StyleHeroCard(Panel pnl, int radius = 10)
        {
            if (pnl == null) return;
            SetDoubleBuffered(pnl);
            pnl.BackColor = IsDarkMode ? Color.FromArgb(15, 23, 42) : Color.FromArgb(30, 41, 59);
            MakeRoundedControl(pnl, radius, Color.FromArgb(99, 102, 241));
        }

        public static void StyleButton(Button btn, Color backColor, Color foreColor, int radius = 8)
        {
            if (btn == null) return;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = backColor;
            btn.ForeColor = foreColor;
            btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            if (btn.Name != "btnChonAnh") btn.Height = Math.Max(btn.Height, 36);

            btn.Padding = new Padding(6, 0, 6, 0);

            if (btn.Width > 0 && !string.IsNullOrEmpty(btn.Text) && btn.Name != "btnChonAnh")
            {
                int minW = TextRenderer.MeasureText(btn.Text, btn.Font).Width + 14;
                if (btn.Width < minW) btn.Width = minW;
            }

            MakeRoundedControl(btn, radius);

            string tagStr = btn.Tag != null ? btn.Tag.ToString() : "";
            if (!tagStr.Contains("BtnHover"))
            {
                btn.Tag = (string.IsNullOrEmpty(tagStr) ? "" : tagStr + ";") + "BtnHover";
                btn.MouseEnter += (s, e) =>
                {
                    btn.BackColor = ControlPaint.Light(btn.BackColor, 0.15f);
                };
                btn.MouseLeave += (s, e) =>
                {
                    btn.BackColor = backColor;
                };
            }
        }

        public static void StylePrimaryButton(Button btn) => StyleButton(btn, ColorPrimary, Color.White, 8);
        public static void StyleSuccessButton(Button btn) => StyleButton(btn, BtnThemColor, Color.White, 8);
        public static void StyleWarningButton(Button btn) => StyleButton(btn, BtnSuaColor, Color.White, 8);
        public static void StyleDangerButton(Button btn) => StyleButton(btn, BtnXoaColor, Color.White, 8);
        public static void StyleSecondaryButton(Button btn) => StyleButton(btn, BtnHuyColor, Color.White, 8);

        public static void StyleTextBox(TextBox txt, int radius = 6)
        {
            if (txt == null) return;
            txt.BorderStyle = BorderStyle.FixedSingle;
            txt.Font = new Font("Segoe UI", 9.5F);
            txt.ForeColor = ColorTextPrimary;
            if (txt.ReadOnly || !txt.Enabled)
            {
                txt.BackColor = IsDarkMode ? Color.FromArgb(30, 41, 59) : Color.FromArgb(241, 245, 249);
            }
            else
            {
                txt.BackColor = IsDarkMode ? Color.FromArgb(40, 53, 72) : Color.White;
            }

            string tagStr = txt.Tag != null ? txt.Tag.ToString() : "";
            if (!tagStr.Contains("ThemeTxtBorder"))
            {
                txt.Tag = (string.IsNullOrEmpty(tagStr) ? "" : tagStr + ";") + "ThemeTxtBorder";
                new ControlBorderPainter(txt);
                AttachParentBorder(txt);
            }
        }

        public static void StyleComboBox(ComboBox cbo, int radius = 6)
        {
            if (cbo == null) return;
            cbo.FlatStyle = FlatStyle.Flat;
            cbo.Font = new Font("Segoe UI", 9.5F);
            cbo.ForeColor = ColorTextPrimary;
            cbo.BackColor = IsDarkMode ? Color.FromArgb(40, 53, 72) : Color.White;
            cbo.DropDownHeight = 220;

            string tagStr = cbo.Tag != null ? cbo.Tag.ToString() : "";
            if (!tagStr.Contains("ThemeCboBorder"))
            {
                cbo.Tag = (string.IsNullOrEmpty(tagStr) ? "" : tagStr + ";") + "ThemeCboBorder";
                new ControlBorderPainter(cbo);
                AttachParentBorder(cbo);
            }
        }

        public static void StyleDateTimePicker(DateTimePicker dtp)
        {
            if (dtp == null) return;
            dtp.Font = new Font("Segoe UI", 9.5F);
            dtp.CalendarForeColor = ColorTextPrimary;
            dtp.CalendarMonthBackground = ColorCard;
            dtp.CalendarTitleBackColor = ColorPrimary;
            dtp.CalendarTitleForeColor = Color.White;

            string tagStr = dtp.Tag != null ? dtp.Tag.ToString() : "";
            if (!tagStr.Contains("ThemeDtpBorder"))
            {
                dtp.Tag = (string.IsNullOrEmpty(tagStr) ? "" : tagStr + ";") + "ThemeDtpBorder";
                new ControlBorderPainter(dtp);
                AttachParentBorder(dtp);
            }
        }

        public static void StyleRadioButton(RadioButton rdo)
        {
            if (rdo == null) return;
            SetDoubleBuffered(rdo);
            rdo.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            rdo.Cursor = Cursors.Hand;
            rdo.ForeColor = rdo.Checked ? ColorPrimary : ColorTextPrimary;

            if (rdo.AutoSize)
            {
                Size pref = rdo.PreferredSize;
                rdo.AutoSize = false;
                rdo.Size = new Size(pref.Width + 18, Math.Max(pref.Height + 8, 30));
            }
            rdo.Padding = new Padding(8, 2, 8, 2);

            string tagStr = rdo.Tag != null ? rdo.Tag.ToString() : "";
            if (!tagStr.Contains("ThemeRdoStyled"))
            {
                rdo.Tag = (string.IsNullOrEmpty(tagStr) ? "" : tagStr + ";") + "ThemeRdoStyled";

                rdo.CheckedChanged += (s, e) =>
                {
                    rdo.ForeColor = rdo.Checked ? ColorPrimary : ColorTextPrimary;
                    rdo.Invalidate();
                    rdo.Parent?.Invalidate();
                };
                rdo.MouseEnter += (s, e) => { rdo.Invalidate(); rdo.Parent?.Invalidate(); };
                rdo.MouseLeave += (s, e) => { rdo.Invalidate(); rdo.Parent?.Invalidate(); };

                rdo.Paint += (s, e) =>
                {
                    if (e.Graphics == null || rdo.Width <= 1 || rdo.Height <= 1) return;
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                    bool isChecked = rdo.Checked;
                    Color borderCol = isChecked ? ColorPrimary : ColorBorder;
                    Rectangle rect = new Rectangle(0, 0, rdo.Width - 1, rdo.Height - 1);
                    float pw = isChecked ? 2f : 1.2f;

                    using (Pen pen = new Pen(borderCol, pw))
                    using (GraphicsPath p = GetRoundedRectanglePath(rect, 6))
                    {
                        e.Graphics.DrawPath(pen, p);
                    }
                };

                AttachParentBorder(rdo);
            }
        }

        public static void StyleCheckBox(CheckBox chk)
        {
            if (chk == null) return;
            SetDoubleBuffered(chk);
            chk.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            chk.Cursor = Cursors.Hand;
            chk.ForeColor = chk.Checked ? ColorPrimary : ColorTextPrimary;

            if (chk.AutoSize)
            {
                Size pref = chk.PreferredSize;
                chk.AutoSize = false;
                chk.Size = new Size(pref.Width + 16, Math.Max(pref.Height + 6, 28));
            }
            chk.Padding = new Padding(6, 2, 6, 2);

            string tagStr = chk.Tag != null ? chk.Tag.ToString() : "";
            if (!tagStr.Contains("ThemeChkStyled"))
            {
                chk.Tag = (string.IsNullOrEmpty(tagStr) ? "" : tagStr + ";") + "ThemeChkStyled";

                chk.CheckedChanged += (s, e) =>
                {
                    chk.ForeColor = chk.Checked ? ColorPrimary : ColorTextPrimary;
                    chk.Invalidate();
                    chk.Parent?.Invalidate();
                };
                chk.MouseEnter += (s, e) => { chk.Invalidate(); chk.Parent?.Invalidate(); };
                chk.MouseLeave += (s, e) => { chk.Invalidate(); chk.Parent?.Invalidate(); };

                chk.Paint += (s, e) =>
                {
                    if (e.Graphics == null || chk.Width <= 1 || chk.Height <= 1) return;
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    Color borderCol = chk.Checked ? ColorPrimary : ColorBorder;
                    Rectangle rect = new Rectangle(0, 0, chk.Width - 1, chk.Height - 1);
                    float pw = chk.Checked ? 1.8f : 1.2f;
                    using (Pen pen = new Pen(borderCol, pw))
                    using (GraphicsPath p = GetRoundedRectanglePath(rect, 4))
                    {
                        e.Graphics.DrawPath(pen, p);
                    }
                };

                AttachParentBorder(chk);
            }
        }

        public static void StyleTabControl(TabControl tab)
        {
            if (tab == null) return;
            tab.DrawMode = TabDrawMode.OwnerDrawFixed;
            tab.ItemSize = new Size(275, 38);
            tab.SizeMode = TabSizeMode.Fixed;

            string tagStr = tab.Tag != null ? tab.Tag.ToString() : "";
            if (tagStr.Contains("ThemeTabStyled")) return;
            tab.Tag = (string.IsNullOrEmpty(tagStr) ? "" : tagStr + ";") + "ThemeTabStyled";

            tab.DrawItem += (s, e) =>
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle tabRect = tab.GetTabRect(e.Index);
                bool isSelected = (tab.SelectedIndex == e.Index);

                Color tabBg = isSelected ? ColorCard : (IsDarkMode ? Color.FromArgb(22, 32, 50) : Color.FromArgb(241, 245, 249));
                Color tabText = isSelected ? ColorPrimary : ColorTextSecondary;
                Color borderCol = isSelected ? ColorPrimary : ColorBorder;

                using (SolidBrush b = new SolidBrush(tabBg))
                {
                    g.FillRectangle(b, tabRect);
                }

                using (Pen p = new Pen(borderCol, isSelected ? 2f : 1.2f))
                {
                    g.DrawRectangle(p, tabRect.X, tabRect.Y, tabRect.Width - 1, tabRect.Height - 1);
                }

                if (isSelected)
                {
                    using (SolidBrush barBrush = new SolidBrush(ColorPrimary))
                    {
                        g.FillRectangle(barBrush, tabRect.X + 2, tabRect.Bottom - 4, tabRect.Width - 4, 3);
                    }
                }

                string text = tab.TabPages[e.Index].Text;
                using (Font f = new Font("Segoe UI", 9.5F, isSelected ? FontStyle.Bold : FontStyle.Regular))
                using (SolidBrush tb = new SolidBrush(tabText))
                using (StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                {
                    g.DrawString(text, f, tb, tabRect, sf);
                }
            };

            tab.SelectedIndexChanged += (s, e) => tab.Invalidate();
        }

        private static void AttachParentBorder(Control control)
        {
            if (control == null) return;
            Action attach = () =>
            {
                Control p = control.Parent;
                if (p == null) return;
                string pTag = p.Tag != null ? p.Tag.ToString() : "";
                if (!pTag.Contains("ThemeParentBorderHooked"))
                {
                    p.Tag = (string.IsNullOrEmpty(pTag) ? "" : pTag + ";") + "ThemeParentBorderHooked";
                    p.Paint += (s, e) =>
                    {
                        if (e.Graphics == null) return;
                        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        foreach (Control child in p.Controls)
                        {
                            if (child is ComboBox || child is TextBox || child is DateTimePicker)
                            {
                                Color bc = child.Focused ? ColorBorderFocus : ColorBorder;
                                float pw = child.Focused ? 2f : 1.2f;
                                using (Pen pen = new Pen(bc, pw))
                                using (GraphicsPath gp = GetRoundedRectanglePath(new Rectangle(child.Left - 1, child.Top - 1, child.Width + 1, child.Height + 1), 4))
                                {
                                    e.Graphics.DrawPath(pen, gp);
                                }
                            }
                            else if (child is RadioButton rdo)
                            {
                                Color bc = rdo.Checked ? ColorPrimary : ColorBorder;
                                Color bg = rdo.Checked ? (IsDarkMode ? Color.FromArgb(30, 41, 59) : Color.FromArgb(238, 242, 255)) : (IsDarkMode ? ColorCard : Color.White);
                                Rectangle r = new Rectangle(rdo.Left, rdo.Top, rdo.Width, rdo.Height);
                                float pw = rdo.Checked ? 2f : 1.2f;
                                using (SolidBrush b = new SolidBrush(bg))
                                using (GraphicsPath gp = GetRoundedRectanglePath(r, 6))
                                {
                                    e.Graphics.FillPath(b, gp);
                                }
                                using (Pen pen = new Pen(bc, pw))
                                using (GraphicsPath gp = GetRoundedRectanglePath(r, 6))
                                {
                                    e.Graphics.DrawPath(pen, gp);
                                }
                            }
                            else if (child is CheckBox chk)
                            {
                                Color bc = chk.Checked ? ColorPrimary : ColorBorder;
                                Color bg = chk.Checked ? (IsDarkMode ? Color.FromArgb(30, 41, 59) : Color.FromArgb(238, 242, 255)) : (IsDarkMode ? ColorCard : Color.White);
                                Rectangle r = new Rectangle(chk.Left, chk.Top, chk.Width, chk.Height);
                                float pw = chk.Checked ? 2f : 1.2f;
                                using (SolidBrush b = new SolidBrush(bg))
                                using (GraphicsPath gp = GetRoundedRectanglePath(r, 4))
                                {
                                    e.Graphics.FillPath(b, gp);
                                }
                                using (Pen pen = new Pen(bc, pw))
                                using (GraphicsPath gp = GetRoundedRectanglePath(r, 4))
                                {
                                    e.Graphics.DrawPath(pen, gp);
                                }
                            }
                        }
                    };
                }
            };

            if (control.Parent != null) attach();
            else control.ParentChanged += (s, e) => attach();

            control.GotFocus += (s, e) => { control.Invalidate(); control.Parent?.Invalidate(new Rectangle(control.Left - 3, control.Top - 3, control.Width + 6, control.Height + 6)); };
            control.LostFocus += (s, e) => { control.Invalidate(); control.Parent?.Invalidate(new Rectangle(control.Left - 3, control.Top - 3, control.Width + 6, control.Height + 6)); };
            control.MouseEnter += (s, e) => { control.Invalidate(); control.Parent?.Invalidate(new Rectangle(control.Left - 3, control.Top - 3, control.Width + 6, control.Height + 6)); };
            control.MouseLeave += (s, e) => { control.Invalidate(); control.Parent?.Invalidate(new Rectangle(control.Left - 3, control.Top - 3, control.Width + 6, control.Height + 6)); };
        }

        public static void StyleDataGridView(DataGridView dgv)
        {
            if (dgv == null) return;
            SetDoubleBuffered(dgv);

            dgv.BackgroundColor = ColorCard;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = ColorBorder;

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
            dgv.RowTemplate.Height = 34;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AllowUserToResizeRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            string tagStr = dgv.Tag != null ? dgv.Tag.ToString() : "";
            if (!tagStr.Contains("ThemeDgvRowBorder"))
            {
                dgv.Tag = (string.IsNullOrEmpty(tagStr) ? "" : tagStr + ";") + "ThemeDgvRowBorder";
                dgv.RowPostPaint += (s, e) =>
                {
                    if (e.RowIndex >= 0 && e.RowIndex < dgv.Rows.Count)
                    {
                        if (dgv.Rows[e.RowIndex].Selected)
                        {
                            int w = dgv.ClientSize.Width - 1;
                            Rectangle r = new Rectangle(0, e.RowBounds.Top, w, e.RowBounds.Height - 1);
                            using (Pen p = new Pen(ColorPrimary, 2f))
                            {
                                e.Graphics.DrawRectangle(p, r);
                            }
                        }
                    }
                };
            }
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

        public static Panel CreateKpiCard(string title, string value, string subtitle, Color accentColor, string iconEmoji, int width = 230, int height = 80, int radius = 8)
        {
            Panel card = new Panel();
            card.Size = new Size(width, height);
            card.BackColor = ColorCard;
            SetDoubleBuffered(card);
            MakeRoundedControl(card, radius);

            card.Paint += (s, e) =>
            {
                if (e.Graphics == null || card.Width <= 1 || card.Height <= 1) return;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (Pen pen = new Pen(ColorBorder, 1))
                using (GraphicsPath path = GetRoundedRectanglePath(new Rectangle(0, 0, card.Width - 1, card.Height - 1), radius))
                {
                    e.Graphics.DrawPath(pen, path);
                }
                using (SolidBrush brush = new SolidBrush(accentColor))
                {
                    e.Graphics.FillRectangle(brush, new Rectangle(0, 0, 5, card.Height));
                }
            };

            int textX = string.IsNullOrEmpty(iconEmoji) ? 18 : 58;
            int textW = string.IsNullOrEmpty(iconEmoji) ? width - 26 : width - 65;

            if (!string.IsNullOrEmpty(iconEmoji))
            {
                Label lblIcon = new Label();
                lblIcon.Text = iconEmoji;
                lblIcon.Font = new Font("Segoe UI", 18F);
                lblIcon.Location = new Point(10, 14);
                lblIcon.Size = new Size(48, 48);
                lblIcon.TextAlign = ContentAlignment.MiddleCenter;
                card.Controls.Add(lblIcon);
            }

            Label lblTitle = new Label();
            lblTitle.Text = title.ToUpper();
            lblTitle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblTitle.ForeColor = ColorTextSecondary;
            lblTitle.Location = new Point(textX, 12);
            lblTitle.Size = new Size(textW, 16);

            Label lblVal = new Label();
            lblVal.Text = value;
            lblVal.Font = new Font("Segoe UI", 13.5F, FontStyle.Bold);
            lblVal.ForeColor = ColorTextPrimary;
            lblVal.Location = new Point(textX, 28);
            lblVal.Size = new Size(textW, 26);

            Label lblSub = new Label();
            lblSub.Text = subtitle;
            lblSub.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            lblSub.ForeColor = ColorTextMuted;
            lblSub.Location = new Point(textX, 54);
            lblSub.Size = new Size(textW, 18);

            card.Controls.Add(lblTitle);
            card.Controls.Add(lblVal);
            card.Controls.Add(lblSub);

            return card;
        }

        public static void ApplyTheme(Control root)
        {
            if (root == null) return;

            if (root is Form frm)
            {
                frm.BackColor = ColorBackground;
                frm.ForeColor = ColorTextPrimary;
            }
            else if (root is Panel pnl)
            {
                if (pnl.Name == "pnlSidebar" || pnl.Name == "pnlLogo" || pnl.Name == "pnlUserCard")
                {
                    pnl.BackColor = pnl.Name == "pnlLogo" ? ColorSidebarLogo : ColorSidebar;
                }
                else if (pnl.Name == "pnlHeader" || pnl.Name == "pnlTop")
                {
                    pnl.BackColor = IsDarkMode ? Color.FromArgb(30, 41, 59) : Color.White;
                }
                else if (pnl.Name == "pnlContent" || pnl.Name == "pnlBentoDashboard" || pnl.Name == "pnlCards" || pnl.Name == "pnlKpiBar")
                {
                    pnl.BackColor = ColorBackground;
                }
                else if (pnl.Name == "pnlBentoHero")
                {
                    pnl.BackColor = IsDarkMode ? Color.FromArgb(15, 23, 42) : Color.FromArgb(30, 41, 59);
                }
                else
                {
                    pnl.BackColor = ColorCard;
                    pnl.ForeColor = ColorTextPrimary;
                }
            }
            else if (root is Label lbl)
            {
                Control parent = lbl.Parent;
                bool isSidebar = parent != null && (parent.Name == "pnlSidebar" || parent.Name == "pnlLogo" || parent.Name == "pnlUserCard");
                if (isSidebar)
                {
                    if (lbl.Name.StartsWith("lblCat"))
                    {
                        lbl.ForeColor = ColorSidebarMuted;
                    }
                    else
                    {
                        lbl.ForeColor = ColorSidebarText;
                    }
                }
                else if (lbl.Name == "lblHeroRevenue" || lbl.Name == "lblHeroProfit" || lbl.Name == "lblHeroTrend" || lbl.Name == "lblHeroBadge" || lbl.Name == "lblHeroTag")
                {
                }
                else if (lbl.Name == "lblDbStatus" || lbl.Name == "lblStatus" || lbl.Name == "lblUserStatus")
                {
                }
                else
                {
                    if (lbl.ForeColor == Color.FromArgb(100, 116, 139) ||
                        lbl.ForeColor == Color.FromArgb(148, 163, 184) ||
                        lbl.ForeColor == Color.FromArgb(203, 213, 225) ||
                        lbl.ForeColor == Color.FromArgb(71, 85, 105) ||
                        lbl.Name == "lblBangChu")
                    {
                        lbl.ForeColor = ColorTextSecondary;
                    }
                    else
                    {
                        lbl.ForeColor = ColorTextPrimary;
                    }
                }
            }
            else if (root is TextBox txt)
            {
                StyleTextBox(txt);
            }
            else if (root is ComboBox cbo)
            {
                StyleComboBox(cbo);
            }
            else if (root is DataGridView dgv)
            {
                StyleDataGridView(dgv);
            }
            else if (root is DateTimePicker dtp)
            {
                StyleDateTimePicker(dtp);
            }
            else if (root is RadioButton rdo)
            {
                StyleRadioButton(rdo);
            }
            else if (root is CheckBox chk)
            {
                StyleCheckBox(chk);
            }
            else if (root is LinkLabel lnk)
            {
                lnk.LinkColor = IsDarkMode ? Color.FromArgb(165, 180, 252) : Color.FromArgb(79, 70, 229);
            }
            else if (root is GroupBox grp)
            {
                grp.BackColor = ColorCard;
                grp.ForeColor = ColorTextPrimary;
            }
            else if (root is TabControl tab)
            {
                StyleTabControl(tab);
            }
            else if (root is TabPage page)
            {
                page.BackColor = ColorCard;
                page.ForeColor = ColorTextPrimary;
            }
            else if (root is Guna2Button gBtn)
            {
                gBtn.BorderRadius = 8;
            }
            else if (root is Guna2Panel gPnl)
            {
                gPnl.BorderRadius = 8;
                gPnl.BorderThickness = 1;
                gPnl.BorderColor = ColorBorder;
                gPnl.FillColor = ColorCard;
            }
            else if (root is Guna2TextBox gTxt)
            {
                gTxt.BorderRadius = 6;
                gTxt.BorderThickness = 1;
                gTxt.BorderColor = ColorBorder;
                gTxt.FocusedState.BorderColor = ColorBorderFocus;
                gTxt.FillColor = IsDarkMode ? Color.FromArgb(40, 53, 72) : Color.White;
                gTxt.ForeColor = ColorTextPrimary;
            }
            else if (root is Guna2ComboBox gCbo)
            {
                gCbo.BorderRadius = 6;
                gCbo.BorderThickness = 1;
                gCbo.BorderColor = ColorBorder;
                gCbo.FocusedState.BorderColor = ColorBorderFocus;
                gCbo.FillColor = IsDarkMode ? Color.FromArgb(40, 53, 72) : Color.White;
                gCbo.ForeColor = ColorTextPrimary;
            }

            foreach (Control child in root.Controls)
            {
                ApplyTheme(child);
            }
        }

        public static void SaveThemeToDatabase(bool isDark)
        {
            try
            {
                if (Functions.Conn != null && Functions.Conn.State == ConnectionState.Open)
                {
                    string val = isDark ? "Dark" : "Light";
                    string sql = string.Format(
                        "IF EXISTS (SELECT 1 FROM tblCauHinhHeThong WHERE KeyName = 'ThemeMode') " +
                        "UPDATE tblCauHinhHeThong SET KeyValue = '{0}' WHERE KeyName = 'ThemeMode' " +
                        "ELSE INSERT INTO tblCauHinhHeThong (KeyName, KeyValue) VALUES ('ThemeMode', '{0}'); " +
                        "IF EXISTS (SELECT 1 FROM tblCauHinhHeThong WHERE KeyName = 'GiaoDien') " +
                        "UPDATE tblCauHinhHeThong SET KeyValue = '{0}' WHERE KeyName = 'GiaoDien' " +
                        "ELSE INSERT INTO tblCauHinhHeThong (KeyName, KeyValue) VALUES ('GiaoDien', '{0}');", val);
                    Functions.RunSQL(sql, false);
                }
            }
            catch { }
        }

        public static bool LoadThemeFromDatabase()
        {
            try
            {
                if (Functions.Conn != null && Functions.Conn.State == ConnectionState.Open)
                {
                    string val = Functions.GetFieldValues("SELECT TOP 1 KeyValue FROM tblCauHinhHeThong WHERE KeyName = 'ThemeMode' OR KeyName = 'GiaoDien'");
                    if (!string.IsNullOrEmpty(val))
                    {
                        IsDarkMode = val.Equals("Dark", StringComparison.OrdinalIgnoreCase);
                        return IsDarkMode;
                    }
                }
            }
            catch { }
            return false;
        }
    }

    public class ControlBorderPainter : NativeWindow
    {
        private readonly Control control;
        private bool isHovered = false;

        public ControlBorderPainter(Control ctrl)
        {
            control = ctrl;
            if (control.IsHandleCreated)
            {
                AssignHandle(control.Handle);
            }
            control.HandleCreated += (s, e) => AssignHandle(control.Handle);
            control.HandleDestroyed += (s, e) => ReleaseHandle();
            control.MouseEnter += (s, e) => { isHovered = true; control.Invalidate(); };
            control.MouseLeave += (s, e) => { isHovered = false; control.Invalidate(); };
            control.GotFocus += (s, e) => { control.Invalidate(); };
            control.LostFocus += (s, e) => { control.Invalidate(); };
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0x000F)
            {
                if (control != null && control.Width > 1 && control.Height > 1)
                {
                    try
                    {
                        using (Graphics g = Graphics.FromHwnd(control.Handle))
                        {
                            Color bc = control.Focused ? UIThemeHelper.ColorBorderFocus :
                                       (isHovered ? UIThemeHelper.ColorBorderHover : UIThemeHelper.ColorBorder);
                            float pw = control.Focused ? 2f : 1.5f;
                            using (Pen p = new Pen(bc, pw))
                            {
                                g.DrawRectangle(p, 0, 0, control.Width - 1, control.Height - 1);
                            }
                        }
                    }
                    catch { }
                }
            }
        }
    }
}

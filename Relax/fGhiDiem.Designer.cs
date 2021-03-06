namespace Relax
{
    partial class fGhiDiem
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tMaKH = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.tDiem = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.tTien = new DevExpress.XtraEditors.CalcEdit();
            this.tSothe = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.tMaKH.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tDiem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tTien.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tSothe.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tMaKH
            // 
            this.tMaKH.Location = new System.Drawing.Point(83, 100);
            this.tMaKH.Name = "tMaKH";
            this.tMaKH.Properties.ReadOnly = true;
            this.tMaKH.Size = new System.Drawing.Size(166, 20);
            this.tMaKH.TabIndex = 17;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(24, 103);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(49, 13);
            this.labelControl3.TabIndex = 16;
            this.labelControl3.Text = "Mã khách:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(25, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(37, 13);
            this.labelControl1.TabIndex = 15;
            this.labelControl1.Text = "Số điểm";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(102, 136);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 3;
            this.simpleButton1.Text = "Ghi";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // tDiem
            // 
            this.tDiem.EnterMoveNextControl = true;
            this.tDiem.Location = new System.Drawing.Point(83, 12);
            this.tDiem.Name = "tDiem";
            this.tDiem.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tDiem.Properties.DisplayFormat.FormatString = "### ### ##0";
            this.tDiem.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.tDiem.Properties.EditFormat.FormatString = "### ### ##0";
            this.tDiem.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.tDiem.Properties.Mask.EditMask = "### ### ##0";
            this.tDiem.Size = new System.Drawing.Size(166, 20);
            this.tDiem.TabIndex = 13;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(26, 43);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(33, 13);
            this.labelControl2.TabIndex = 19;
            this.labelControl2.Text = "Số tiền";
            // 
            // tTien
            // 
            this.tTien.EnterMoveNextControl = true;
            this.tTien.Location = new System.Drawing.Point(83, 40);
            this.tTien.Name = "tTien";
            this.tTien.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tTien.Properties.DisplayFormat.FormatString = "### ### ##0";
            this.tTien.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.tTien.Properties.EditFormat.FormatString = "### ### ##0";
            this.tTien.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.tTien.Properties.Mask.EditMask = "### ### ##0";
            this.tTien.Size = new System.Drawing.Size(166, 20);
            this.tTien.TabIndex = 18;
            // 
            // tSothe
            // 
            this.tSothe.Location = new System.Drawing.Point(83, 67);
            this.tSothe.Name = "tSothe";
            this.tSothe.Size = new System.Drawing.Size(100, 20);
            this.tSothe.TabIndex = 2;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(25, 70);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(31, 13);
            this.labelControl4.TabIndex = 21;
            this.labelControl4.Text = "Số thẻ";
            // 
            // fGhiDiem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 170);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.tSothe);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.tTien);
            this.Controls.Add(this.tMaKH);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.tDiem);
            this.Name = "fGhiDiem";
            this.Text = "fGhiDiem";
            ((System.ComponentModel.ISupportInitialize)(this.tMaKH.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tDiem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tTien.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tSothe.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit tMaKH;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.CalcEdit tDiem;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.CalcEdit tTien;
        private DevExpress.XtraEditors.TextEdit tSothe;
        private DevExpress.XtraEditors.LabelControl labelControl4;
    }
}
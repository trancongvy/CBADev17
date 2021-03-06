namespace Relax
{
    partial class fDoixu
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
            this.tDiem = new DevExpress.XtraEditors.CalcEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.tMaKH = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.tXu = new DevExpress.XtraEditors.TextEdit();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            this.tSoThe = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.tDiem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tMaKH.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tXu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tSoThe.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tDiem
            // 
            this.tDiem.EnterMoveNextControl = true;
            this.tDiem.Location = new System.Drawing.Point(74, 30);
            this.tDiem.Name = "tDiem";
            this.tDiem.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tDiem.Properties.DisplayFormat.FormatString = "### ### ##0";
            this.tDiem.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.tDiem.Properties.EditFormat.FormatString = "### ### ##0";
            this.tDiem.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.tDiem.Properties.Mask.EditMask = "### ### ##0";
            this.tDiem.Size = new System.Drawing.Size(166, 20);
            this.tDiem.TabIndex = 0;
            this.tDiem.EditValueChanged += new System.EventHandler(this.tTien_EditValueChanged);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(109, 167);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 3;
            this.simpleButton1.Text = "Ghi";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 33);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 13);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "Tiền:";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 131);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(49, 13);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "Mã khách:";
            // 
            // tMaKH
            // 
            this.tMaKH.Location = new System.Drawing.Point(74, 128);
            this.tMaKH.Name = "tMaKH";
            this.tMaKH.Properties.ReadOnly = true;
            this.tMaKH.Size = new System.Drawing.Size(166, 20);
            this.tMaKH.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Số xu";
            // 
            // tXu
            // 
            this.tXu.Location = new System.Drawing.Point(74, 63);
            this.tXu.Name = "tXu";
            this.tXu.Properties.ReadOnly = true;
            this.tXu.Size = new System.Drawing.Size(166, 20);
            this.tXu.TabIndex = 11;
            // 
            // checkEdit1
            // 
            this.checkEdit1.Location = new System.Drawing.Point(174, 94);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.checkEdit1.Properties.Appearance.Options.UseFont = true;
            this.checkEdit1.Properties.Caption = "Ghi thẻ";
            this.checkEdit1.Properties.ReadOnly = true;
            this.checkEdit1.Size = new System.Drawing.Size(75, 23);
            this.checkEdit1.TabIndex = 2;
            // 
            // tSoThe
            // 
            this.tSoThe.Location = new System.Drawing.Point(74, 97);
            this.tSoThe.Name = "tSoThe";
            this.tSoThe.Size = new System.Drawing.Size(94, 20);
            this.tSoThe.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 100);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(52, 13);
            this.labelControl2.TabIndex = 14;
            this.labelControl2.Text = "Đọc số thẻ";
            // 
            // fDoixu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 199);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.tSoThe);
            this.Controls.Add(this.checkEdit1);
            this.Controls.Add(this.tXu);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tMaKH);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.tDiem);
            this.KeyPreview = true;
            this.Name = "fDoixu";
            this.Text = "Đổi xu";
            ((System.ComponentModel.ISupportInitialize)(this.tDiem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tMaKH.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tXu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tSoThe.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.CalcEdit tDiem;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit tMaKH;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit tXu;
        private DevExpress.XtraEditors.CheckEdit checkEdit1;
        private DevExpress.XtraEditors.TextEdit tSoThe;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}
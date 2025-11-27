namespace Unidad4NoSQL
{
    partial class frmUnidad4
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            groupBox1 = new GroupBox();
            panel2 = new Panel();
            txtCorreoSecundario = new TextBox();
            label4 = new Label();
            panel1 = new Panel();
            txtCorreoPrincipal = new TextBox();
            label3 = new Label();
            btnGuardar = new Button();
            groupBox1.SuspendLayout();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Corbel", 16.2F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(12, 215, 253);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(187, 35);
            label1.TabIndex = 0;
            label1.Text = "No duplicados";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Corbel", 12F);
            label2.ForeColor = Color.White;
            label2.Location = new Point(13, 51);
            label2.Name = "label2";
            label2.Size = new Size(710, 24);
            label2.TabIndex = 1;
            label2.Text = "Correo electrónico donde validamos que no ingrese un dato duplicado en el registro";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(panel2);
            groupBox1.Controls.Add(panel1);
            groupBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox1.ForeColor = SystemColors.ButtonHighlight;
            groupBox1.Location = new Point(22, 90);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(701, 137);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Correo electrónico";
            // 
            // panel2
            // 
            panel2.Controls.Add(txtCorreoSecundario);
            panel2.Controls.Add(label4);
            panel2.Location = new Point(357, 28);
            panel2.Name = "panel2";
            panel2.Size = new Size(319, 91);
            panel2.TabIndex = 1;
            // 
            // txtCorreoSecundario
            // 
            txtCorreoSecundario.Location = new Point(25, 36);
            txtCorreoSecundario.Name = "txtCorreoSecundario";
            txtCorreoSecundario.Size = new Size(276, 27);
            txtCorreoSecundario.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 0);
            label4.Name = "label4";
            label4.Size = new Size(143, 20);
            label4.TabIndex = 0;
            label4.Text = "Correo secundario*";
            // 
            // panel1
            // 
            panel1.Controls.Add(txtCorreoPrincipal);
            panel1.Controls.Add(label3);
            panel1.Location = new Point(20, 28);
            panel1.Name = "panel1";
            panel1.Size = new Size(308, 91);
            panel1.TabIndex = 0;
            // 
            // txtCorreoPrincipal
            // 
            txtCorreoPrincipal.Location = new Point(21, 36);
            txtCorreoPrincipal.Name = "txtCorreoPrincipal";
            txtCorreoPrincipal.Size = new Size(271, 27);
            txtCorreoPrincipal.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(0, 0);
            label3.Name = "label3";
            label3.Size = new Size(127, 20);
            label3.TabIndex = 0;
            label3.Text = "Correo Principal*";
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.FromArgb(128, 255, 128);
            btnGuardar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnGuardar.ForeColor = Color.Blue;
            btnGuardar.Location = new Point(581, 258);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(133, 48);
            btnGuardar.TabIndex = 3;
            btnGuardar.Text = "AGREGAR";
            btnGuardar.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // frmUnidad4
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(37, 41, 47);
            ClientSize = new Size(750, 333);
            Controls.Add(btnGuardar);
            Controls.Add(groupBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "frmUnidad4";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "UNIDAD 4 (NO DUPLICADOS)";
            groupBox1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private GroupBox groupBox1;
        private Panel panel2;
        private TextBox txtCorreoSecundario;
        private Label label4;
        private Panel panel1;
        private TextBox txtCorreoPrincipal;
        private Label label3;
        private Button btnGuardar;
    }
}

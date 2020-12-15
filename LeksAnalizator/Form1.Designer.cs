namespace LeksAnalizator
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.keyWordGrid1 = new System.Windows.Forms.DataGridView();
            this.keyID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.keyWord = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.litteralsGrid = new System.Windows.Forms.DataGridView();
            this.litID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.litteralL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.identifierGrid = new System.Windows.Forms.DataGridView();
            this.identID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.identWord = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.logGrid = new System.Windows.Forms.DataGridView();
            this.logID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tablesId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.word = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rowNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.constantGrid = new System.Windows.Forms.DataGridView();
            this.constId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.constant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.resultLabel = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.keyWordGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.litteralsGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.identifierGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.constantGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(198, 217);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 388);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(1026, 39);
            this.button1.TabIndex = 1;
            this.button1.Text = "Start analize";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // keyWordGrid1
            // 
            this.keyWordGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.keyWordGrid1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.keyID,
            this.keyWord});
            this.keyWordGrid1.Location = new System.Drawing.Point(216, 29);
            this.keyWordGrid1.Name = "keyWordGrid1";
            this.keyWordGrid1.RowHeadersVisible = false;
            this.keyWordGrid1.Size = new System.Drawing.Size(168, 353);
            this.keyWordGrid1.TabIndex = 2;
            // 
            // keyID
            // 
            this.keyID.HeaderText = "No";
            this.keyID.Name = "keyID";
            this.keyID.Width = 30;
            // 
            // keyWord
            // 
            this.keyWord.HeaderText = "Key Word";
            this.keyWord.Name = "keyWord";
            this.keyWord.Width = 137;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(216, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Key words";
            // 
            // litteralsGrid
            // 
            this.litteralsGrid.AllowUserToAddRows = false;
            this.litteralsGrid.AllowUserToDeleteRows = false;
            this.litteralsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.litteralsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.litID,
            this.litteralL});
            this.litteralsGrid.Location = new System.Drawing.Point(558, 29);
            this.litteralsGrid.Name = "litteralsGrid";
            this.litteralsGrid.ReadOnly = true;
            this.litteralsGrid.RowHeadersVisible = false;
            this.litteralsGrid.Size = new System.Drawing.Size(198, 170);
            this.litteralsGrid.TabIndex = 4;
            // 
            // litID
            // 
            this.litID.HeaderText = "No";
            this.litID.Name = "litID";
            this.litID.ReadOnly = true;
            this.litID.Width = 60;
            // 
            // litteralL
            // 
            this.litteralL.HeaderText = "Delimeter";
            this.litteralL.Name = "litteralL";
            this.litteralL.ReadOnly = true;
            this.litteralL.Width = 132;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(555, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Delimeters";
            // 
            // identifierGrid
            // 
            this.identifierGrid.AllowUserToAddRows = false;
            this.identifierGrid.AllowUserToDeleteRows = false;
            this.identifierGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.identifierGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.identID,
            this.identWord});
            this.identifierGrid.Location = new System.Drawing.Point(390, 29);
            this.identifierGrid.Name = "identifierGrid";
            this.identifierGrid.ReadOnly = true;
            this.identifierGrid.RowHeadersVisible = false;
            this.identifierGrid.Size = new System.Drawing.Size(162, 353);
            this.identifierGrid.TabIndex = 6;
            // 
            // identID
            // 
            this.identID.HeaderText = "No";
            this.identID.Name = "identID";
            this.identID.ReadOnly = true;
            this.identID.Width = 30;
            // 
            // identWord
            // 
            this.identWord.HeaderText = "Identifier";
            this.identWord.Name = "identWord";
            this.identWord.ReadOnly = true;
            this.identWord.Width = 131;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(387, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "ID";
            // 
            // logGrid
            // 
            this.logGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.logGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.logID,
            this.tablesId,
            this.word,
            this.tableType,
            this.rowNumber});
            this.logGrid.Location = new System.Drawing.Point(762, 29);
            this.logGrid.Name = "logGrid";
            this.logGrid.RowHeadersVisible = false;
            this.logGrid.Size = new System.Drawing.Size(280, 353);
            this.logGrid.TabIndex = 8;
            // 
            // logID
            // 
            this.logID.HeaderText = "No";
            this.logID.Name = "logID";
            this.logID.Width = 30;
            // 
            // tablesId
            // 
            this.tablesId.HeaderText = "No in table";
            this.tablesId.Name = "tablesId";
            this.tablesId.Width = 50;
            // 
            // word
            // 
            this.word.HeaderText = "Lexeme";
            this.word.Name = "word";
            this.word.Width = 90;
            // 
            // tableType
            // 
            this.tableType.HeaderText = "Type";
            this.tableType.Name = "tableType";
            this.tableType.Width = 70;
            // 
            // rowNumber
            // 
            this.rowNumber.HeaderText = "Row";
            this.rowNumber.Name = "rowNumber";
            this.rowNumber.Width = 40;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(759, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Log";
            // 
            // constantGrid
            // 
            this.constantGrid.AllowUserToAddRows = false;
            this.constantGrid.AllowUserToDeleteRows = false;
            this.constantGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.constantGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.constId,
            this.constant});
            this.constantGrid.Location = new System.Drawing.Point(558, 232);
            this.constantGrid.Name = "constantGrid";
            this.constantGrid.ReadOnly = true;
            this.constantGrid.RowHeadersVisible = false;
            this.constantGrid.Size = new System.Drawing.Size(198, 150);
            this.constantGrid.TabIndex = 10;
            // 
            // constId
            // 
            this.constId.HeaderText = "No";
            this.constId.Name = "constId";
            this.constId.ReadOnly = true;
            this.constId.Width = 30;
            // 
            // constant
            // 
            this.constant.HeaderText = "Const";
            this.constant.Name = "constant";
            this.constant.ReadOnly = true;
            this.constant.Width = 167;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(558, 216);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Const";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 248);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Result";
            // 
            // resultLabel
            // 
            this.resultLabel.AutoSize = true;
            this.resultLabel.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.resultLabel.Location = new System.Drawing.Point(12, 271);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(66, 13);
            this.resultLabel.TabIndex = 13;
            this.resultLabel.Text = "Not analized";
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.textBox2.Location = new System.Drawing.Point(12, 478);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(344, 44);
            this.textBox2.TabIndex = 14;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 537);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(344, 46);
            this.button2.TabIndex = 15;
            this.button2.Text = "Parse";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(370, 481);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Result";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(370, 509);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "label8";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 647);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.resultLabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.constantGrid);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.logGrid);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.identifierGrid);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.litteralsGrid);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.keyWordGrid1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Проектирование компиляторов. Сенчев Антон";
            ((System.ComponentModel.ISupportInitialize)(this.keyWordGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.litteralsGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.identifierGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.constantGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView keyWordGrid1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView litteralsGrid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView identifierGrid;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView logGrid;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView constantGrid;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label resultLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn keyID;
        private System.Windows.Forms.DataGridViewTextBoxColumn keyWord;
        private System.Windows.Forms.DataGridViewTextBoxColumn litID;
        private System.Windows.Forms.DataGridViewTextBoxColumn litteralL;
        private System.Windows.Forms.DataGridViewTextBoxColumn identID;
        private System.Windows.Forms.DataGridViewTextBoxColumn identWord;
        private System.Windows.Forms.DataGridViewTextBoxColumn logID;
        private System.Windows.Forms.DataGridViewTextBoxColumn tablesId;
        private System.Windows.Forms.DataGridViewTextBoxColumn word;
        private System.Windows.Forms.DataGridViewTextBoxColumn tableType;
        private System.Windows.Forms.DataGridViewTextBoxColumn rowNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn constId;
        private System.Windows.Forms.DataGridViewTextBoxColumn constant;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}


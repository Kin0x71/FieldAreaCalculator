
namespace FieldAreaCalculator
{
	partial class MainForm
	{
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.cbIsPolygon = new System.Windows.Forms.CheckBox();
			this.labelResult = new System.Windows.Forms.Label();
			this.labelInfo = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// cbIsPolygon
			// 
			this.cbIsPolygon.AutoSize = true;
			this.cbIsPolygon.Checked = true;
			this.cbIsPolygon.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbIsPolygon.Location = new System.Drawing.Point(12, 8);
			this.cbIsPolygon.Name = "cbIsPolygon";
			this.cbIsPolygon.Size = new System.Drawing.Size(64, 17);
			this.cbIsPolygon.TabIndex = 0;
			this.cbIsPolygon.Text = "Polygon";
			this.cbIsPolygon.UseVisualStyleBackColor = true;
			this.cbIsPolygon.CheckStateChanged += new System.EventHandler(this.cbIsPolygon_CheckStateChanged);
			// 
			// labelResult
			// 
			this.labelResult.AutoSize = true;
			this.labelResult.Location = new System.Drawing.Point(81, 9);
			this.labelResult.Name = "labelResult";
			this.labelResult.Size = new System.Drawing.Size(79, 13);
			this.labelResult.TabIndex = 2;
			this.labelResult.Text = "Xxxxxxxxxxxxxx";
			// 
			// labelInfo
			// 
			this.labelInfo.AutoSize = true;
			this.labelInfo.Location = new System.Drawing.Point(9, 30);
			this.labelInfo.Name = "labelInfo";
			this.labelInfo.Size = new System.Drawing.Size(159, 13);
			this.labelInfo.TabIndex = 3;
			this.labelInfo.Text = "Xxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(650, 536);
			this.Controls.Add(this.labelInfo);
			this.Controls.Add(this.labelResult);
			this.Controls.Add(this.cbIsPolygon);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "MainForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox cbIsPolygon;
		private System.Windows.Forms.Label labelResult;
		private System.Windows.Forms.Label labelInfo;
	}
}


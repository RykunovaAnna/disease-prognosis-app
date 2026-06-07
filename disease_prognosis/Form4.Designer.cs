namespace disease_prognosis
{
    partial class Form4
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form4));
            this.lblResult = new System.Windows.Forms.Label();
            this.lblResultInfo = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblProbabilityInfo = new System.Windows.Forms.Label();
            this.lblProbability = new System.Windows.Forms.Label();
            this.lblRiskLevelInfo = new System.Windows.Forms.Label();
            this.lblRiskLevel = new System.Windows.Forms.Label();
            this.lblRecommendationInfo = new System.Windows.Forms.Label();
            this.lblRecommendation = new System.Windows.Forms.Label();
            this.btnNewPatient = new System.Windows.Forms.Button();
            this.btnTopFactors = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblResult.Location = new System.Drawing.Point(33, 21);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(79, 18);
            this.lblResult.TabIndex = 4;
            this.lblResult.Text = "Прогноз:";
            // 
            // lblResultInfo
            // 
            this.lblResultInfo.AutoSize = true;
            this.lblResultInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblResultInfo.Location = new System.Drawing.Point(332, 21);
            this.lblResultInfo.Name = "lblResultInfo";
            this.lblResultInfo.Size = new System.Drawing.Size(71, 18);
            this.lblResultInfo.TabIndex = 5;
            this.lblResultInfo.Text = "Прогноз:";
            this.lblResultInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSave.Location = new System.Drawing.Point(242, 275);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(181, 40);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblProbabilityInfo
            // 
            this.lblProbabilityInfo.AutoSize = true;
            this.lblProbabilityInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblProbabilityInfo.Location = new System.Drawing.Point(332, 73);
            this.lblProbabilityInfo.Name = "lblProbabilityInfo";
            this.lblProbabilityInfo.Size = new System.Drawing.Size(178, 18);
            this.lblProbabilityInfo.TabIndex = 10;
            this.lblProbabilityInfo.Text = "Вероятность патологии:";
            this.lblProbabilityInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblProbability
            // 
            this.lblProbability.AutoSize = true;
            this.lblProbability.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblProbability.Location = new System.Drawing.Point(33, 73);
            this.lblProbability.Name = "lblProbability";
            this.lblProbability.Size = new System.Drawing.Size(200, 18);
            this.lblProbability.TabIndex = 9;
            this.lblProbability.Text = "Вероятность патологии:";
            // 
            // lblRiskLevelInfo
            // 
            this.lblRiskLevelInfo.AutoSize = true;
            this.lblRiskLevelInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblRiskLevelInfo.Location = new System.Drawing.Point(332, 129);
            this.lblRiskLevelInfo.Name = "lblRiskLevelInfo";
            this.lblRiskLevelInfo.Size = new System.Drawing.Size(115, 18);
            this.lblRiskLevelInfo.TabIndex = 12;
            this.lblRiskLevelInfo.Text = "Уровень риска:";
            this.lblRiskLevelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRiskLevel
            // 
            this.lblRiskLevel.AutoSize = true;
            this.lblRiskLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblRiskLevel.Location = new System.Drawing.Point(33, 129);
            this.lblRiskLevel.Name = "lblRiskLevel";
            this.lblRiskLevel.Size = new System.Drawing.Size(129, 18);
            this.lblRiskLevel.TabIndex = 11;
            this.lblRiskLevel.Text = "Уровень риска:";
            // 
            // lblRecommendationInfo
            // 
            this.lblRecommendationInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblRecommendationInfo.Location = new System.Drawing.Point(332, 176);
            this.lblRecommendationInfo.Name = "lblRecommendationInfo";
            this.lblRecommendationInfo.Size = new System.Drawing.Size(305, 79);
            this.lblRecommendationInfo.TabIndex = 14;
            this.lblRecommendationInfo.Text = "Рекомендации:";
            // 
            // lblRecommendation
            // 
            this.lblRecommendation.AutoSize = true;
            this.lblRecommendation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblRecommendation.Location = new System.Drawing.Point(33, 191);
            this.lblRecommendation.Name = "lblRecommendation";
            this.lblRecommendation.Size = new System.Drawing.Size(128, 18);
            this.lblRecommendation.TabIndex = 13;
            this.lblRecommendation.Text = "Рекомендации:";
            // 
            // btnNewPatient
            // 
            this.btnNewPatient.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnNewPatient.Location = new System.Drawing.Point(461, 275);
            this.btnNewPatient.Name = "btnNewPatient";
            this.btnNewPatient.Size = new System.Drawing.Size(192, 40);
            this.btnNewPatient.TabIndex = 15;
            this.btnNewPatient.Text = "Новый прогноз";
            this.btnNewPatient.UseVisualStyleBackColor = true;
            this.btnNewPatient.Click += new System.EventHandler(this.btnNewPatient_Click);
            // 
            // btnTopFactors
            // 
            this.btnTopFactors.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnTopFactors.Location = new System.Drawing.Point(24, 275);
            this.btnTopFactors.Name = "btnTopFactors";
            this.btnTopFactors.Size = new System.Drawing.Size(178, 40);
            this.btnTopFactors.TabIndex = 16;
            this.btnTopFactors.Text = "Интерпретировать";
            this.btnTopFactors.UseVisualStyleBackColor = true;
            this.btnTopFactors.Click += new System.EventHandler(this.btnTopFactors_Click);
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 335);
            this.Controls.Add(this.btnTopFactors);
            this.Controls.Add(this.btnNewPatient);
            this.Controls.Add(this.lblRecommendationInfo);
            this.Controls.Add(this.lblRecommendation);
            this.Controls.Add(this.lblRiskLevelInfo);
            this.Controls.Add(this.lblRiskLevel);
            this.Controls.Add(this.lblProbabilityInfo);
            this.Controls.Add(this.lblProbability);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblResultInfo);
            this.Controls.Add(this.lblResult);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form4";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Результат прогнозирования";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form4_FormClosing);
            this.Load += new System.EventHandler(this.Form4_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblResultInfo;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblProbabilityInfo;
        private System.Windows.Forms.Label lblProbability;
        private System.Windows.Forms.Label lblRiskLevelInfo;
        private System.Windows.Forms.Label lblRiskLevel;
        private System.Windows.Forms.Label lblRecommendationInfo;
        private System.Windows.Forms.Label lblRecommendation;
        private System.Windows.Forms.Button btnNewPatient;
        private System.Windows.Forms.Button btnTopFactors;
    }
}
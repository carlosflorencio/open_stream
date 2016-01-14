namespace Stream
{
    partial class StreamView
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
            this.pinned_team = new System.Windows.Forms.Label();
            this.team = new System.Windows.Forms.TextBox();
            this.link_list = new System.Windows.Forms.ListView();
            this.link = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // pinned_team
            // 
            this.pinned_team.AutoSize = true;
            this.pinned_team.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pinned_team.Location = new System.Drawing.Point(12, 9);
            this.pinned_team.Name = "pinned_team";
            this.pinned_team.Size = new System.Drawing.Size(42, 13);
            this.pinned_team.TabIndex = 0;
            this.pinned_team.Text = "Team:";
            // 
            // team
            // 
            this.team.Location = new System.Drawing.Point(60, 6);
            this.team.Name = "team";
            this.team.Size = new System.Drawing.Size(212, 20);
            this.team.TabIndex = 1;
            this.team.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.team_KeyPress);
            // 
            // link_list
            // 
            this.link_list.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.link_list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.link});
            this.link_list.FullRowSelect = true;
            this.link_list.Location = new System.Drawing.Point(15, 32);
            this.link_list.MultiSelect = false;
            this.link_list.Name = "link_list";
            this.link_list.Size = new System.Drawing.Size(257, 217);
            this.link_list.TabIndex = 2;
            this.link_list.UseCompatibleStateImageBehavior = false;
            this.link_list.View = System.Windows.Forms.View.List;
            this.link_list.DoubleClick += new System.EventHandler(this.link_list_DoubleClick);
            // 
            // link
            // 
            this.link.Text = "Link";
            this.link.Width = 241;
            // 
            // StreamView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.link_list);
            this.Controls.Add(this.team);
            this.Controls.Add(this.pinned_team);
            this.Name = "StreamView";
            this.Text = "Stream";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label pinned_team;
        private System.Windows.Forms.TextBox team;
        private System.Windows.Forms.ListView link_list;
        private System.Windows.Forms.ColumnHeader link;
    }
}
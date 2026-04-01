using System.ComponentModel;

namespace FlightBookingSystem.Resources.Views;

partial class LoginView
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

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
        LoginButton = new System.Windows.Forms.Button();
        UsernameField = new System.Windows.Forms.TextBox();
        PasswordField = new System.Windows.Forms.TextBox();
        label1 = new System.Windows.Forms.Label();
        SuspendLayout();
        // 
        // LoginButton
        // 
        LoginButton.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
        LoginButton.Location = new System.Drawing.Point(52, 175);
        LoginButton.Name = "LoginButton";
        LoginButton.Size = new System.Drawing.Size(98, 23);
        LoginButton.TabIndex = 0;
        LoginButton.Text = "Login";
        LoginButton.UseVisualStyleBackColor = true;
        // 
        // UsernameField
        // 
        UsernameField.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
        UsernameField.Location = new System.Drawing.Point(3, 69);
        UsernameField.Name = "UsernameField";
        UsernameField.Size = new System.Drawing.Size(200, 23);
        UsernameField.TabIndex = 1;
        UsernameField.Text = "Username";
        // 
        // PasswordField
        // 
        PasswordField.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
        PasswordField.Location = new System.Drawing.Point(3, 114);
        PasswordField.Name = "PasswordField";
        PasswordField.Size = new System.Drawing.Size(200, 23);
        PasswordField.TabIndex = 2;
        PasswordField.Text = "Password";
        PasswordField.UseSystemPasswordChar = true;
        // 
        // label1
        // 
        label1.Location = new System.Drawing.Point(50, 9);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(100, 23);
        label1.TabIndex = 3;
        label1.Text = "Login";
        label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // LoginView
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.SystemColors.Control;
        ClientSize = new System.Drawing.Size(204, 210);
        Controls.Add(label1);
        Controls.Add(PasswordField);
        Controls.Add(UsernameField);
        Controls.Add(LoginButton);
        Location = new System.Drawing.Point(15, 15);
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.Label label1;

    private System.Windows.Forms.TextBox PasswordField;

    private System.Windows.Forms.Button LoginButton;
    private System.Windows.Forms.TextBox UsernameField;

    #endregion
}
using System.ComponentModel;

namespace FlightBookingSystem.Resources.Views;

partial class MainView
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
        tabControl1 = new System.Windows.Forms.TabControl();
        tabPage1 = new System.Windows.Forms.TabPage();
        splitContainer1 = new System.Windows.Forms.SplitContainer();
        FlightsTable = new System.Windows.Forms.DataGridView();
        label2 = new System.Windows.Forms.Label();
        BookFlightButton = new System.Windows.Forms.Button();
        SearchTable = new System.Windows.Forms.DataGridView();
        ClearButton = new System.Windows.Forms.Button();
        SearchButton = new System.Windows.Forms.Button();
        dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
        SearchDestionationField = new System.Windows.Forms.TextBox();
        label3 = new System.Windows.Forms.Label();
        label1 = new System.Windows.Forms.Label();
        tabPage2 = new System.Windows.Forms.TabPage();
        ModifyToursitNamesButton = new System.Windows.Forms.Button();
        BookingsTable = new System.Windows.Forms.DataGridView();
        label4 = new System.Windows.Forms.Label();
        LogoutButton = new System.Windows.Forms.Button();
        tabControl1.SuspendLayout();
        tabPage1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
        splitContainer1.Panel1.SuspendLayout();
        splitContainer1.Panel2.SuspendLayout();
        splitContainer1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)FlightsTable).BeginInit();
        ((System.ComponentModel.ISupportInitialize)SearchTable).BeginInit();
        tabPage2.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)BookingsTable).BeginInit();
        SuspendLayout();
        // 
        // tabControl1
        // 
        tabControl1.Controls.Add(tabPage1);
        tabControl1.Controls.Add(tabPage2);
        tabControl1.Location = new System.Drawing.Point(0, 28);
        tabControl1.Name = "tabControl1";
        tabControl1.Padding = new System.Drawing.Point(0, 0);
        tabControl1.SelectedIndex = 0;
        tabControl1.Size = new System.Drawing.Size(784, 533);
        tabControl1.TabIndex = 0;
        // 
        // tabPage1
        // 
        tabPage1.Controls.Add(splitContainer1);
        tabPage1.Controls.Add(label1);
        tabPage1.Location = new System.Drawing.Point(4, 24);
        tabPage1.Name = "tabPage1";
        tabPage1.Padding = new System.Windows.Forms.Padding(3);
        tabPage1.Size = new System.Drawing.Size(776, 505);
        tabPage1.TabIndex = 0;
        tabPage1.Text = "Dashboard & Booking";
        tabPage1.UseVisualStyleBackColor = true;
        // 
        // splitContainer1
        // 
        splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
        splitContainer1.Location = new System.Drawing.Point(3, 3);
        splitContainer1.Name = "splitContainer1";
        splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
        // 
        // splitContainer1.Panel1
        // 
        splitContainer1.Panel1.Controls.Add(FlightsTable);
        splitContainer1.Panel1.Controls.Add(label2);
        // 
        // splitContainer1.Panel2
        // 
        splitContainer1.Panel2.Controls.Add(BookFlightButton);
        splitContainer1.Panel2.Controls.Add(SearchTable);
        splitContainer1.Panel2.Controls.Add(ClearButton);
        splitContainer1.Panel2.Controls.Add(SearchButton);
        splitContainer1.Panel2.Controls.Add(dateTimePicker1);
        splitContainer1.Panel2.Controls.Add(SearchDestionationField);
        splitContainer1.Panel2.Controls.Add(label3);
        splitContainer1.Size = new System.Drawing.Size(770, 499);
        splitContainer1.SplitterDistance = 207;
        splitContainer1.TabIndex = 1;
        // 
        // FlightsTable
        // 
        FlightsTable.Location = new System.Drawing.Point(5, 30);
        FlightsTable.Name = "FlightsTable";
        FlightsTable.ReadOnly = true;
        FlightsTable.Size = new System.Drawing.Size(760, 174);
        FlightsTable.TabIndex = 1;
        // 
        // label2
        // 
        label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        label2.Location = new System.Drawing.Point(5, 4);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(127, 23);
        label2.TabIndex = 0;
        label2.Text = "All Available Flights";
        label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // BookFlightButton
        // 
        BookFlightButton.Location = new System.Drawing.Point(5, 260);
        BookFlightButton.Name = "BookFlightButton";
        BookFlightButton.Size = new System.Drawing.Size(132, 23);
        BookFlightButton.TabIndex = 6;
        BookFlightButton.Text = "Book Selected Flight";
        BookFlightButton.UseVisualStyleBackColor = true;
        // 
        // SearchTable
        // 
        SearchTable.Location = new System.Drawing.Point(5, 64);
        SearchTable.Name = "SearchTable";
        SearchTable.ReadOnly = true;
        SearchTable.Size = new System.Drawing.Size(760, 190);
        SearchTable.TabIndex = 5;
        // 
        // ClearButton
        // 
        ClearButton.Location = new System.Drawing.Point(419, 35);
        ClearButton.Name = "ClearButton";
        ClearButton.Size = new System.Drawing.Size(75, 23);
        ClearButton.TabIndex = 4;
        ClearButton.Text = "Clear";
        ClearButton.UseVisualStyleBackColor = true;
        // 
        // SearchButton
        // 
        SearchButton.Location = new System.Drawing.Point(339, 35);
        SearchButton.Name = "SearchButton";
        SearchButton.Size = new System.Drawing.Size(74, 23);
        SearchButton.TabIndex = 3;
        SearchButton.Text = "Search";
        SearchButton.UseVisualStyleBackColor = true;
        // 
        // dateTimePicker1
        // 
        dateTimePicker1.Location = new System.Drawing.Point(123, 35);
        dateTimePicker1.Name = "dateTimePicker1";
        dateTimePicker1.Size = new System.Drawing.Size(200, 23);
        dateTimePicker1.TabIndex = 2;
        // 
        // SearchDestionationField
        // 
        SearchDestionationField.Location = new System.Drawing.Point(5, 35);
        SearchDestionationField.Name = "SearchDestionationField";
        SearchDestionationField.Size = new System.Drawing.Size(112, 23);
        SearchDestionationField.TabIndex = 1;
        SearchDestionationField.Text = "Destination...";
        // 
        // label3
        // 
        label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        label3.Location = new System.Drawing.Point(5, 9);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(100, 23);
        label3.TabIndex = 0;
        label3.Text = "Search Flights";
        label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // label1
        // 
        label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        label1.Location = new System.Drawing.Point(3, 3);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(132, 23);
        label1.TabIndex = 0;
        label1.Text = "All Available Flights";
        label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // tabPage2
        // 
        tabPage2.Controls.Add(ModifyToursitNamesButton);
        tabPage2.Controls.Add(BookingsTable);
        tabPage2.Controls.Add(label4);
        tabPage2.Location = new System.Drawing.Point(4, 24);
        tabPage2.Name = "tabPage2";
        tabPage2.Padding = new System.Windows.Forms.Padding(3);
        tabPage2.Size = new System.Drawing.Size(776, 505);
        tabPage2.TabIndex = 1;
        tabPage2.Text = "Manage Bookings";
        tabPage2.UseVisualStyleBackColor = true;
        // 
        // ModifyToursitNamesButton
        // 
        ModifyToursitNamesButton.Location = new System.Drawing.Point(8, 476);
        ModifyToursitNamesButton.Name = "ModifyToursitNamesButton";
        ModifyToursitNamesButton.Size = new System.Drawing.Size(137, 23);
        ModifyToursitNamesButton.TabIndex = 2;
        ModifyToursitNamesButton.Text = "Modify Tourist Names";
        ModifyToursitNamesButton.UseVisualStyleBackColor = true;
        // 
        // BookingsTable
        // 
        BookingsTable.Location = new System.Drawing.Point(8, 29);
        BookingsTable.Name = "BookingsTable";
        BookingsTable.ReadOnly = true;
        BookingsTable.Size = new System.Drawing.Size(760, 444);
        BookingsTable.TabIndex = 1;
        // 
        // label4
        // 
        label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        label4.Location = new System.Drawing.Point(8, 3);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(100, 23);
        label4.TabIndex = 0;
        label4.Text = "All Bookings";
        label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // LogoutButton
        // 
        LogoutButton.Location = new System.Drawing.Point(697, 8);
        LogoutButton.Name = "LogoutButton";
        LogoutButton.Size = new System.Drawing.Size(75, 23);
        LogoutButton.TabIndex = 1;
        LogoutButton.Text = "Logout";
        LogoutButton.UseVisualStyleBackColor = true;
        // 
        // MainView
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(784, 561);
        Controls.Add(LogoutButton);
        Controls.Add(tabControl1);
        Text = "MainView";
        tabControl1.ResumeLayout(false);
        tabPage1.ResumeLayout(false);
        splitContainer1.Panel1.ResumeLayout(false);
        splitContainer1.Panel2.ResumeLayout(false);
        splitContainer1.Panel2.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
        splitContainer1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)FlightsTable).EndInit();
        ((System.ComponentModel.ISupportInitialize)SearchTable).EndInit();
        tabPage2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)BookingsTable).EndInit();
        ResumeLayout(false);
    }

    private System.Windows.Forms.Button ModifyToursitNamesButton;

    private System.Windows.Forms.DataGridView BookingsTable;

    private System.Windows.Forms.Label label4;

    private System.Windows.Forms.Button BookFlightButton;

    private System.Windows.Forms.Button LogoutButton;

    private System.Windows.Forms.Button SearchButton;
    private System.Windows.Forms.Button ClearButton;
    private System.Windows.Forms.DataGridView SearchTable;

    private System.Windows.Forms.DateTimePicker dateTimePicker1;

    private System.Windows.Forms.TextBox SearchDestionationField;

    private System.Windows.Forms.Label label3;

    private System.Windows.Forms.DataGridView FlightsTable;

    private System.Windows.Forms.Label label2;

    private System.Windows.Forms.SplitContainer splitContainer1;

    private System.Windows.Forms.Label label1;

    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;

    #endregion
}
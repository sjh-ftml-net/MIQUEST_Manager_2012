namespace MIQUEST_Query_Manager
{
    partial class HomeForm
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
            this.ifTbInputPath = new System.Windows.Forms.TextBox();
            this.ifBtRun = new System.Windows.Forms.Button();
            this.ifBtQuit = new System.Windows.Forms.Button();
            this.ifBtSelectInputPath = new System.Windows.Forms.Button();
            this.ifBtSelectOutputPath = new System.Windows.Forms.Button();
            this.ifTbOutputPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ifTbStatus = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ifProgressBar = new System.Windows.Forms.ProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.ifCbClearFolder = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ifTbQuerySetName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ifTbQuerySetDescription = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.ifCbSystemType = new System.Windows.Forms.ComboBox();
            this.ifDtQueryEndDate = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.ifTbExportPeriod = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.ifTbLTCExportPeriod = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.ifCbFileSplit = new System.Windows.Forms.CheckBox();
            this.ifTbPracticePopulation = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ifBtSuggestSplits = new System.Windows.Forms.Button();
            this.ifTbExportSplit = new System.Windows.Forms.TextBox();
            this.ifCbFileSplitWeeksMonths = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.ifCbFileSplitWeeksMonthsLTC = new System.Windows.Forms.ComboBox();
            this.ifTbExportSplitLTC = new System.Windows.Forms.TextBox();
            this.ifCbScheduled = new System.Windows.Forms.CheckBox();
            this.ifDtScheduledStart = new System.Windows.Forms.DateTimePicker();
            this.label16 = new System.Windows.Forms.Label();
            this.ifDtScheduledEnd = new System.Windows.Forms.DateTimePicker();
            this.label17 = new System.Windows.Forms.Label();
            this.ifCbScheduledWeekMonth = new System.Windows.Forms.ComboBox();
            this.ifTbScheduledPeriod = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.ifCbRemote = new System.Windows.Forms.CheckBox();
            this.ifTbPracticeCode = new System.Windows.Forms.TextBox();
            this.ifCbMedia = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.ifCbReferralGen = new System.Windows.Forms.CheckBox();
            this.ifCbEncounterGen = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // ifTbInputPath
            // 
            this.ifTbInputPath.Location = new System.Drawing.Point(152, 120);
            this.ifTbInputPath.Name = "ifTbInputPath";
            this.ifTbInputPath.Size = new System.Drawing.Size(547, 20);
            this.ifTbInputPath.TabIndex = 0;
            // 
            // ifBtRun
            // 
            this.ifBtRun.Location = new System.Drawing.Point(571, 551);
            this.ifBtRun.Name = "ifBtRun";
            this.ifBtRun.Size = new System.Drawing.Size(75, 23);
            this.ifBtRun.TabIndex = 1;
            this.ifBtRun.Text = "Write HQL";
            this.ifBtRun.UseVisualStyleBackColor = true;
            this.ifBtRun.Click += new System.EventHandler(this.ifBtRun_Click);
            // 
            // ifBtQuit
            // 
            this.ifBtQuit.Location = new System.Drawing.Point(652, 551);
            this.ifBtQuit.Name = "ifBtQuit";
            this.ifBtQuit.Size = new System.Drawing.Size(75, 23);
            this.ifBtQuit.TabIndex = 2;
            this.ifBtQuit.Text = "Quit";
            this.ifBtQuit.UseVisualStyleBackColor = true;
            this.ifBtQuit.Click += new System.EventHandler(this.ifBtQuit_Click);
            // 
            // ifBtSelectInputPath
            // 
            this.ifBtSelectInputPath.Location = new System.Drawing.Point(700, 120);
            this.ifBtSelectInputPath.Name = "ifBtSelectInputPath";
            this.ifBtSelectInputPath.Size = new System.Drawing.Size(27, 20);
            this.ifBtSelectInputPath.TabIndex = 3;
            this.ifBtSelectInputPath.Text = "...";
            this.ifBtSelectInputPath.UseVisualStyleBackColor = true;
            this.ifBtSelectInputPath.Click += new System.EventHandler(this.ifBtSelectInputPath_Click);
            // 
            // ifBtSelectOutputPath
            // 
            this.ifBtSelectOutputPath.Location = new System.Drawing.Point(700, 147);
            this.ifBtSelectOutputPath.Name = "ifBtSelectOutputPath";
            this.ifBtSelectOutputPath.Size = new System.Drawing.Size(27, 20);
            this.ifBtSelectOutputPath.TabIndex = 4;
            this.ifBtSelectOutputPath.Text = "...";
            this.ifBtSelectOutputPath.UseVisualStyleBackColor = true;
            this.ifBtSelectOutputPath.Click += new System.EventHandler(this.ifBtSelectOutputPath_Click);
            // 
            // ifTbOutputPath
            // 
            this.ifTbOutputPath.Location = new System.Drawing.Point(152, 147);
            this.ifTbOutputPath.Name = "ifTbOutputPath";
            this.ifTbOutputPath.Size = new System.Drawing.Size(547, 20);
            this.ifTbOutputPath.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(63, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Template Folder";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(85, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "HQL Folder";
            // 
            // ifTbStatus
            // 
            this.ifTbStatus.Enabled = false;
            this.ifTbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ifTbStatus.Location = new System.Drawing.Point(56, 500);
            this.ifTbStatus.Name = "ifTbStatus";
            this.ifTbStatus.Size = new System.Drawing.Size(671, 20);
            this.ifTbStatus.TabIndex = 8;
            this.ifTbStatus.Text = "Idle";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 499);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Status";
            // 
            // ifProgressBar
            // 
            this.ifProgressBar.AccessibleRole = System.Windows.Forms.AccessibleRole.ProgressBar;
            this.ifProgressBar.Location = new System.Drawing.Point(16, 526);
            this.ifProgressBar.Name = "ifProgressBar";
            this.ifProgressBar.Size = new System.Drawing.Size(711, 13);
            this.ifProgressBar.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(209, 23);
            this.label4.TabIndex = 11;
            this.label4.Text = "MIQUEST Query Manager";
            // 
            // ifCbClearFolder
            // 
            this.ifCbClearFolder.AutoSize = true;
            this.ifCbClearFolder.Location = new System.Drawing.Point(152, 62);
            this.ifCbClearFolder.Name = "ifCbClearFolder";
            this.ifCbClearFolder.Size = new System.Drawing.Size(191, 17);
            this.ifCbClearFolder.TabIndex = 12;
            this.ifCbClearFolder.Text = "Clear HQL folder before processing";
            this.ifCbClearFolder.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(677, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Version1.0";
            // 
            // ifTbQuerySetName
            // 
            this.ifTbQuerySetName.Location = new System.Drawing.Point(152, 85);
            this.ifTbQuerySetName.MaxLength = 8;
            this.ifTbQuerySetName.Name = "ifTbQuerySetName";
            this.ifTbQuerySetName.Size = new System.Drawing.Size(68, 20);
            this.ifTbQuerySetName.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Name and Description";
            // 
            // ifTbQuerySetDescription
            // 
            this.ifTbQuerySetDescription.Location = new System.Drawing.Point(226, 85);
            this.ifTbQuerySetDescription.Name = "ifTbQuerySetDescription";
            this.ifTbQuerySetDescription.Size = new System.Drawing.Size(501, 20);
            this.ifTbQuerySetDescription.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(78, 407);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "System Type";
            // 
            // ifCbSystemType
            // 
            this.ifCbSystemType.FormattingEnabled = true;
            this.ifCbSystemType.Items.AddRange(new object[] {
            "TPP SystmOne",
            "Other"});
            this.ifCbSystemType.Location = new System.Drawing.Point(152, 404);
            this.ifCbSystemType.Name = "ifCbSystemType";
            this.ifCbSystemType.Size = new System.Drawing.Size(200, 21);
            this.ifCbSystemType.TabIndex = 20;
            this.ifCbSystemType.Text = "TPP SystmOne";
            // 
            // ifDtQueryEndDate
            // 
            this.ifDtQueryEndDate.Location = new System.Drawing.Point(152, 250);
            this.ifDtQueryEndDate.Name = "ifDtQueryEndDate";
            this.ifDtQueryEndDate.Size = new System.Drawing.Size(200, 20);
            this.ifDtQueryEndDate.TabIndex = 21;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(61, 256);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Query END date";
            // 
            // ifTbExportPeriod
            // 
            this.ifTbExportPeriod.Location = new System.Drawing.Point(152, 276);
            this.ifTbExportPeriod.Name = "ifTbExportPeriod";
            this.ifTbExportPeriod.Size = new System.Drawing.Size(200, 20);
            this.ifTbExportPeriod.TabIndex = 23;
            this.ifTbExportPeriod.Text = "12";
            this.ifTbExportPeriod.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ifTbExportPeriod_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(77, 279);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "Export Period";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(53, 305);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(93, 13);
            this.label11.TabIndex = 26;
            this.label11.Text = "LTC Export Period";
            // 
            // ifTbLTCExportPeriod
            // 
            this.ifTbLTCExportPeriod.Location = new System.Drawing.Point(152, 302);
            this.ifTbLTCExportPeriod.Name = "ifTbLTCExportPeriod";
            this.ifTbLTCExportPeriod.Size = new System.Drawing.Size(200, 20);
            this.ifTbLTCExportPeriod.TabIndex = 25;
            this.ifTbLTCExportPeriod.Text = "60";
            this.ifTbLTCExportPeriod.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ifTbLTCExportPeriod_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(354, 280);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(37, 13);
            this.label12.TabIndex = 27;
            this.label12.Text = "Month";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(354, 306);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(37, 13);
            this.label13.TabIndex = 28;
            this.label13.Text = "Month";
            // 
            // ifCbFileSplit
            // 
            this.ifCbFileSplit.AutoSize = true;
            this.ifCbFileSplit.Location = new System.Drawing.Point(152, 355);
            this.ifCbFileSplit.Name = "ifCbFileSplit";
            this.ifCbFileSplit.Size = new System.Drawing.Size(133, 17);
            this.ifCbFileSplit.TabIndex = 31;
            this.ifCbFileSplit.Text = "Report File Period Split";
            this.ifCbFileSplit.UseVisualStyleBackColor = true;
            this.ifCbFileSplit.CheckedChanged += new System.EventHandler(this.ifCbFileSplit_CheckedChanged);
            // 
            // ifTbPracticePopulation
            // 
            this.ifTbPracticePopulation.Location = new System.Drawing.Point(152, 378);
            this.ifTbPracticePopulation.Name = "ifTbPracticePopulation";
            this.ifTbPracticePopulation.Size = new System.Drawing.Size(133, 20);
            this.ifTbPracticePopulation.TabIndex = 32;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(47, 381);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 13);
            this.label7.TabIndex = 33;
            this.label7.Text = "Practice Population";
            // 
            // ifBtSuggestSplits
            // 
            this.ifBtSuggestSplits.Location = new System.Drawing.Point(291, 374);
            this.ifBtSuggestSplits.Name = "ifBtSuggestSplits";
            this.ifBtSuggestSplits.Size = new System.Drawing.Size(61, 26);
            this.ifBtSuggestSplits.TabIndex = 34;
            this.ifBtSuggestSplits.Text = "Suggest";
            this.ifBtSuggestSplits.UseVisualStyleBackColor = true;
            this.ifBtSuggestSplits.Click += new System.EventHandler(this.ifBtSuggestSplits_Click);
            // 
            // ifTbExportSplit
            // 
            this.ifTbExportSplit.Location = new System.Drawing.Point(152, 431);
            this.ifTbExportSplit.Name = "ifTbExportSplit";
            this.ifTbExportSplit.Size = new System.Drawing.Size(133, 20);
            this.ifTbExportSplit.TabIndex = 35;
            this.ifTbExportSplit.Text = "6";
            this.ifTbExportSplit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ifTbExportSplit_KeyPress);
            // 
            // ifCbFileSplitWeeksMonths
            // 
            this.ifCbFileSplitWeeksMonths.AutoCompleteCustomSource.AddRange(new string[] {
            "Weeks",
            "Months"});
            this.ifCbFileSplitWeeksMonths.FormattingEnabled = true;
            this.ifCbFileSplitWeeksMonths.Items.AddRange(new object[] {
            "Month",
            "Week"});
            this.ifCbFileSplitWeeksMonths.Location = new System.Drawing.Point(291, 430);
            this.ifCbFileSplitWeeksMonths.Name = "ifCbFileSplitWeeksMonths";
            this.ifCbFileSplitWeeksMonths.Size = new System.Drawing.Size(57, 21);
            this.ifCbFileSplitWeeksMonths.TabIndex = 36;
            this.ifCbFileSplitWeeksMonths.Text = "Month";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(67, 434);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(79, 13);
            this.label14.TabIndex = 37;
            this.label14.Text = "Export File Split";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(77, 458);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(69, 13);
            this.label15.TabIndex = 40;
            this.label15.Text = "LTC File Split";
            // 
            // ifCbFileSplitWeeksMonthsLTC
            // 
            this.ifCbFileSplitWeeksMonthsLTC.AutoCompleteCustomSource.AddRange(new string[] {
            "Weeks",
            "Months"});
            this.ifCbFileSplitWeeksMonthsLTC.FormattingEnabled = true;
            this.ifCbFileSplitWeeksMonthsLTC.Items.AddRange(new object[] {
            "Month",
            "Week"});
            this.ifCbFileSplitWeeksMonthsLTC.Location = new System.Drawing.Point(291, 457);
            this.ifCbFileSplitWeeksMonthsLTC.Name = "ifCbFileSplitWeeksMonthsLTC";
            this.ifCbFileSplitWeeksMonthsLTC.Size = new System.Drawing.Size(57, 21);
            this.ifCbFileSplitWeeksMonthsLTC.TabIndex = 39;
            this.ifCbFileSplitWeeksMonthsLTC.Text = "Month";
            // 
            // ifTbExportSplitLTC
            // 
            this.ifTbExportSplitLTC.Location = new System.Drawing.Point(152, 457);
            this.ifTbExportSplitLTC.Name = "ifTbExportSplitLTC";
            this.ifTbExportSplitLTC.Size = new System.Drawing.Size(133, 20);
            this.ifTbExportSplitLTC.TabIndex = 38;
            this.ifTbExportSplitLTC.Text = "24";
            this.ifTbExportSplitLTC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ifTbExportSplitLTC_KeyPress);
            // 
            // ifCbScheduled
            // 
            this.ifCbScheduled.AutoSize = true;
            this.ifCbScheduled.Location = new System.Drawing.Point(524, 227);
            this.ifCbScheduled.Name = "ifCbScheduled";
            this.ifCbScheduled.Size = new System.Drawing.Size(108, 17);
            this.ifCbScheduled.TabIndex = 41;
            this.ifCbScheduled.Text = "Scheduled Query";
            this.ifCbScheduled.UseVisualStyleBackColor = true;
            this.ifCbScheduled.CheckedChanged += new System.EventHandler(this.ifCbScheduled_CheckedChanged);
            // 
            // ifDtScheduledStart
            // 
            this.ifDtScheduledStart.Location = new System.Drawing.Point(524, 250);
            this.ifDtScheduledStart.Name = "ifDtScheduledStart";
            this.ifDtScheduledStart.Size = new System.Drawing.Size(200, 20);
            this.ifDtScheduledStart.TabIndex = 42;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(489, 256);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(29, 13);
            this.label16.TabIndex = 43;
            this.label16.Text = "Start";
            // 
            // ifDtScheduledEnd
            // 
            this.ifDtScheduledEnd.Location = new System.Drawing.Point(524, 276);
            this.ifDtScheduledEnd.Name = "ifDtScheduledEnd";
            this.ifDtScheduledEnd.Size = new System.Drawing.Size(200, 20);
            this.ifDtScheduledEnd.TabIndex = 44;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(481, 305);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(37, 13);
            this.label17.TabIndex = 47;
            this.label17.Text = "Period";
            // 
            // ifCbScheduledWeekMonth
            // 
            this.ifCbScheduledWeekMonth.AutoCompleteCustomSource.AddRange(new string[] {
            "Weeks",
            "Months"});
            this.ifCbScheduledWeekMonth.FormattingEnabled = true;
            this.ifCbScheduledWeekMonth.Items.AddRange(new object[] {
            "Month",
            "Week"});
            this.ifCbScheduledWeekMonth.Location = new System.Drawing.Point(663, 302);
            this.ifCbScheduledWeekMonth.Name = "ifCbScheduledWeekMonth";
            this.ifCbScheduledWeekMonth.Size = new System.Drawing.Size(57, 21);
            this.ifCbScheduledWeekMonth.TabIndex = 46;
            this.ifCbScheduledWeekMonth.Text = "Month";
            // 
            // ifTbScheduledPeriod
            // 
            this.ifTbScheduledPeriod.Location = new System.Drawing.Point(524, 302);
            this.ifTbScheduledPeriod.Name = "ifTbScheduledPeriod";
            this.ifTbScheduledPeriod.Size = new System.Drawing.Size(133, 20);
            this.ifTbScheduledPeriod.TabIndex = 45;
            this.ifTbScheduledPeriod.Text = "1";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(492, 279);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(26, 13);
            this.label18.TabIndex = 48;
            this.label18.Text = "End";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(444, 381);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(74, 13);
            this.label21.TabIndex = 51;
            this.label21.Text = "Practice Code";
            // 
            // ifCbRemote
            // 
            this.ifCbRemote.AutoSize = true;
            this.ifCbRemote.Location = new System.Drawing.Point(524, 355);
            this.ifCbRemote.Name = "ifCbRemote";
            this.ifCbRemote.Size = new System.Drawing.Size(94, 17);
            this.ifCbRemote.TabIndex = 49;
            this.ifCbRemote.Text = "Remote Query";
            this.ifCbRemote.UseVisualStyleBackColor = true;
            this.ifCbRemote.CheckedChanged += new System.EventHandler(this.ifCbRemote_CheckedChanged);
            // 
            // ifTbPracticeCode
            // 
            this.ifTbPracticeCode.Location = new System.Drawing.Point(524, 378);
            this.ifTbPracticeCode.Name = "ifTbPracticeCode";
            this.ifTbPracticeCode.Size = new System.Drawing.Size(196, 20);
            this.ifTbPracticeCode.TabIndex = 57;
            // 
            // ifCbMedia
            // 
            this.ifCbMedia.FormattingEnabled = true;
            this.ifCbMedia.Items.AddRange(new object[] {
            "Disk",
            "Network"});
            this.ifCbMedia.Location = new System.Drawing.Point(524, 404);
            this.ifCbMedia.Name = "ifCbMedia";
            this.ifCbMedia.Size = new System.Drawing.Size(196, 21);
            this.ifCbMedia.TabIndex = 58;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(482, 407);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(36, 13);
            this.label19.TabIndex = 59;
            this.label19.Text = "Media";
            // 
            // ifCbReferralGen
            // 
            this.ifCbReferralGen.AutoSize = true;
            this.ifCbReferralGen.Location = new System.Drawing.Point(152, 227);
            this.ifCbReferralGen.Name = "ifCbReferralGen";
            this.ifCbReferralGen.Size = new System.Drawing.Size(167, 17);
            this.ifCbReferralGen.TabIndex = 60;
            this.ifCbReferralGen.Text = "Generate REFERRAL queries";
            this.ifCbReferralGen.UseVisualStyleBackColor = true;
            // 
            // ifCbEncounterGen
            // 
            this.ifCbEncounterGen.AutoSize = true;
            this.ifCbEncounterGen.Checked = true;
            this.ifCbEncounterGen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ifCbEncounterGen.Location = new System.Drawing.Point(152, 204);
            this.ifCbEncounterGen.Name = "ifCbEncounterGen";
            this.ifCbEncounterGen.Size = new System.Drawing.Size(178, 17);
            this.ifCbEncounterGen.TabIndex = 61;
            this.ifCbEncounterGen.Text = "Generate ENCOUNTER queries";
            this.ifCbEncounterGen.UseVisualStyleBackColor = true;
            // 
            // HomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 628);
            this.Controls.Add(this.ifCbEncounterGen);
            this.Controls.Add(this.ifCbReferralGen);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.ifCbMedia);
            this.Controls.Add(this.ifTbPracticeCode);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.ifCbRemote);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.ifCbScheduledWeekMonth);
            this.Controls.Add(this.ifTbScheduledPeriod);
            this.Controls.Add(this.ifDtScheduledEnd);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.ifDtScheduledStart);
            this.Controls.Add(this.ifCbScheduled);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.ifCbFileSplitWeeksMonthsLTC);
            this.Controls.Add(this.ifTbExportSplitLTC);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.ifCbFileSplitWeeksMonths);
            this.Controls.Add(this.ifTbExportSplit);
            this.Controls.Add(this.ifBtSuggestSplits);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.ifTbPracticePopulation);
            this.Controls.Add(this.ifCbFileSplit);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.ifTbLTCExportPeriod);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.ifTbExportPeriod);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.ifDtQueryEndDate);
            this.Controls.Add(this.ifCbSystemType);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.ifTbQuerySetDescription);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ifTbQuerySetName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ifCbClearFolder);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ifProgressBar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ifTbStatus);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ifTbOutputPath);
            this.Controls.Add(this.ifBtSelectOutputPath);
            this.Controls.Add(this.ifBtSelectInputPath);
            this.Controls.Add(this.ifBtQuit);
            this.Controls.Add(this.ifBtRun);
            this.Controls.Add(this.ifTbInputPath);
            this.Name = "HomeForm";
            this.Text = "MIQUEST Query Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HomeForm_FormClosing);
            this.Load += new System.EventHandler(this.HomeForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ifTbInputPath;
        private System.Windows.Forms.Button ifBtRun;
        private System.Windows.Forms.Button ifBtQuit;
        private System.Windows.Forms.Button ifBtSelectInputPath;
        private System.Windows.Forms.Button ifBtSelectOutputPath;
        private System.Windows.Forms.TextBox ifTbOutputPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ifTbStatus;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ProgressBar ifProgressBar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox ifCbClearFolder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox ifTbQuerySetName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox ifTbQuerySetDescription;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.ComboBox ifCbSystemType;
        private System.Windows.Forms.DateTimePicker ifDtQueryEndDate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox ifTbExportPeriod;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox ifTbLTCExportPeriod;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox ifTbPracticePopulation;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button ifBtSuggestSplits;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        public System.Windows.Forms.CheckBox ifCbFileSplit;
        public System.Windows.Forms.TextBox ifTbExportSplit;
        public System.Windows.Forms.ComboBox ifCbFileSplitWeeksMonths;
        public System.Windows.Forms.ComboBox ifCbFileSplitWeeksMonthsLTC;
        public System.Windows.Forms.TextBox ifTbExportSplitLTC;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        public System.Windows.Forms.ComboBox ifCbScheduledWeekMonth;
        public System.Windows.Forms.TextBox ifTbScheduledPeriod;
        private System.Windows.Forms.Label label18;
        public System.Windows.Forms.CheckBox ifCbScheduled;
        public System.Windows.Forms.DateTimePicker ifDtScheduledStart;
        public System.Windows.Forms.DateTimePicker ifDtScheduledEnd;
        private System.Windows.Forms.Label label21;
        public System.Windows.Forms.CheckBox ifCbRemote;
        private System.Windows.Forms.Label label19;
        public System.Windows.Forms.TextBox ifTbPracticeCode;
        public System.Windows.Forms.ComboBox ifCbMedia;
        public System.Windows.Forms.CheckBox ifCbReferralGen;
        public System.Windows.Forms.CheckBox ifCbEncounterGen;
    }
}


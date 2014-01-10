﻿namespace TemplateEditor
{
    partial class TemplateRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public TemplateRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tab1 = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.btnSetings = this.Factory.CreateRibbonButton();
            this.group2 = this.Factory.CreateRibbonGroup();
            this.btnAddNew = this.Factory.CreateRibbonButton();
            this.group3 = this.Factory.CreateRibbonGroup();
            this.btnEdit = this.Factory.CreateRibbonButton();
            this.group5 = this.Factory.CreateRibbonGroup();
            this.btnSave = this.Factory.CreateRibbonButton();
            this.group6 = this.Factory.CreateRibbonGroup();
            this.btnPreview = this.Factory.CreateRibbonButton();
            this.group4 = this.Factory.CreateRibbonGroup();
            this.btnLogIn = this.Factory.CreateRibbonButton();
            this.group7 = this.Factory.CreateRibbonGroup();
            this.btnTemplate = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.group1.SuspendLayout();
            this.group2.SuspendLayout();
            this.group3.SuspendLayout();
            this.group5.SuspendLayout();
            this.group6.SuspendLayout();
            this.group4.SuspendLayout();
            this.group7.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.group1);
            this.tab1.Groups.Add(this.group2);
            this.tab1.Groups.Add(this.group3);
            this.tab1.Groups.Add(this.group5);
            this.tab1.Groups.Add(this.group7);
            this.tab1.Groups.Add(this.group6);
            this.tab1.Groups.Add(this.group4);
            this.tab1.Label = "CATS";
            this.tab1.Name = "tab1";
            // 
            // group1
            // 
            this.group1.Items.Add(this.btnSetings);
            this.group1.Name = "group1";
            // 
            // btnSetings
            // 
            this.btnSetings.Label = "Setting";
            this.btnSetings.Name = "btnSetings";
            this.btnSetings.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button1_Click);
            // 
            // group2
            // 
            this.group2.Items.Add(this.btnAddNew);
            this.group2.Name = "group2";
            // 
            // btnAddNew
            // 
            this.btnAddNew.Label = "Add/EditTemplate";
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnAddNew_Click);
            // 
            // group3
            // 
            this.group3.Items.Add(this.btnEdit);
            this.group3.Name = "group3";
            // 
            // btnEdit
            // 
            this.btnEdit.Label = "Upload/Download Template";
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnEdit_Click);
            // 
            // group5
            // 
            this.group5.Items.Add(this.btnSave);
            this.group5.Name = "group5";
            // 
            // btnSave
            // 
            this.btnSave.Label = "Save Docment";
            this.btnSave.Name = "btnSave";
            this.btnSave.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnSave_Click_1);
            // 
            // group6
            // 
            this.group6.Items.Add(this.btnPreview);
            this.group6.Name = "group6";
            this.group6.Visible = false;
            // 
            // btnPreview
            // 
            this.btnPreview.Label = "Preview";
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Visible = false;
      
            // 
            // group4
            // 
            this.group4.Items.Add(this.btnLogIn);
            this.group4.Name = "group4";
            // 
            // btnLogIn
            // 
            this.btnLogIn.Label = "Log in to CATS";
            this.btnLogIn.Name = "btnLogIn";
            this.btnLogIn.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnLogIn_Click);
            // 
            // group7
            // 
            this.group7.Items.Add(this.btnTemplate);
            this.group7.Name = "group7";
            // 
            // btnTemplate
            // 
            this.btnTemplate.Label = "New Template";
            this.btnTemplate.Name = "btnTemplate";
            this.btnTemplate.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnTemplate_Click);
            // 
            // TemplateRibbon
            // 
            this.Name = "TemplateRibbon";
            this.RibbonType = "Microsoft.Word.Document";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.TemplateRibbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.group2.ResumeLayout(false);
            this.group2.PerformLayout();
            this.group3.ResumeLayout(false);
            this.group3.PerformLayout();
            this.group5.ResumeLayout(false);
            this.group5.PerformLayout();
            this.group6.ResumeLayout(false);
            this.group6.PerformLayout();
            this.group4.ResumeLayout(false);
            this.group4.PerformLayout();
            this.group7.ResumeLayout(false);
            this.group7.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSetings;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnAddNew;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group3;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnEdit;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group5;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSave;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group4;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnLogIn;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group6;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnPreview;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group7;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnTemplate;
    }

    partial class ThisRibbonCollection
    {
        internal TemplateRibbon TemplateRibbon
        {
            get { return this.GetRibbon<TemplateRibbon>(); }
        }
    }
}

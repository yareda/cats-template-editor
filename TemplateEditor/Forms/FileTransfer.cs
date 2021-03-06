﻿using System;
using System.Diagnostics;
using System.IO;
using System.ServiceModel;
using System.Windows.Forms;
using TemplateEditor.TemplateService;

namespace TemplateEditor.Forms
{
    public partial class FileTransfer : Form
    {
        private static readonly string Uri = Properties.Settings.Default.ServerUrl.ToString();
        EndpointAddress _address = new EndpointAddress(Uri);

        public FileTransfer()
        {
            InitializeComponent();
        }

        private void FileTransfer_Load(object sender, EventArgs e)
        {
            
            GetTemplateTypes();
            FillGrid();

        }

        private void FillGrid()
        {
            var templateType = (int) cmbTemplateTypes.SelectedValue;
            var client = new TemplateManagerClient("BasicHttpBinding_ITemplateManager", _address);
            var templates = client.GetTemplates(templateType);
            FileList.Items.Clear();
            foreach (var template in templates)
            {

                ListViewItem item = new ListViewItem(template.Name);
                item.SubItems.Add(template.FileName);
                FileList.Items.Add(item);
            }
            client.Close();
        }
       

        private void DownloadButton_Click(object sender, EventArgs e)
        {

            Download();
        }

       

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Delete Template?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (FileList.SelectedItems.Count == 0)
                {
                    MessageBox.Show("You must select a file to delete");
                }
                else
                {
                    string virtualPath = FileList.SelectedItems[0].SubItems[1].Text;

                    using (var client = new TemplateManagerClient("BasicHttpBinding_ITemplateManager", _address))
                    {
                        client.DeleteFile(virtualPath + ".dotx");
                        client.DeleteTemplate(virtualPath);
                        client.Close();
                    }
                    
                    FillGrid();
                }
            }

        }


        

        private void Download()
        {

            var filePath = string.Empty;
            if (FileList.SelectedItems.Count == 0)
            {
                MessageBox.Show("You must select a file to download");
            }
            else
            {
                var documentProcessorService = new DocumentProcessorService();
                string path = documentProcessorService.DownloadDocument(FileList);
                if (path!=string.Empty)
                {
                    this.Close();
                    this.Dispose();
                    try
                    {
                      Process.Start("WINWORD.EXE", path);
                      
                    }
                    catch (Exception)
                    {

                        Process.Start("WINWORD.EXE", path);
                    }
                    
                }
            }
        }

       


       

        private void GetTemplateTypes()
        {
            var client = new TemplateManagerClient("BasicHttpBinding_ITemplateManager", _address);
            cmbTemplateTypes.DataSource = null;
            cmbTemplateTypes.DataSource = client.GetTemplateTypes();
            cmbTemplateTypes.DisplayMember = "TemplateObject";
            cmbTemplateTypes.ValueMember = "TemplateTypeId";
            client.Close();
        }

       

       

        private void cmbTemplateTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               FillGrid();
            }catch
            {
                
            }
            
        }
    }
}

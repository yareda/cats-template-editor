using System;
using System.Diagnostics;
using System.IO;
using System.ServiceModel;
using System.Windows.Forms;
using TemplateEditor.TemplateService;

namespace TemplateEditor.Forms
{
    public partial class LoadTemplate : Form
    {
        private static readonly string Uri = Properties.Settings.Default.ServerUrl.ToString();
        EndpointAddress _address = new EndpointAddress(Uri);

        public LoadTemplate()
        {
            InitializeComponent();
        }

        private void LoadTemplate_Load(object sender, EventArgs e)
        {
            LoadTemplateTypes();
        }
        private void LoadTemplateTypes()
        {
           GetTemplateTypes();
            
        }

       
        private void cmbTemplateTypes_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                PopulateTemplates(int.Parse(cmbTemplateTypes.SelectedValue.ToString()));
                
            }
            catch (Exception)
            {


            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            
              

                // Strip off 'Root' from the full path
            string path = "storage/partial1.html";// item.SubItems[1].Text;

                // Ask where it should be saved
                SaveFileDialog dlg = new SaveFileDialog()
                {
                    RestoreDirectory = true,
                    OverwritePrompt = true,
                    Title = "Save as...",
                    FileName = Path.GetFileName(path)
                };

                dlg.ShowDialog(this);

                if (!string.IsNullOrEmpty(dlg.FileName))
                {
                    // Get the file from the server
                    using (FileStream output = new FileStream(dlg.FileName, FileMode.Create))
                    {
                        Stream downloadStream;

                        using (TemplateManagerClient client = new TemplateManagerClient("BasicHttpBinding_ITemplateManager", _address))
                        {
                            downloadStream = client.GetFile(path);
                        }

                        downloadStream.CopyTo(output);
                    }

                    Process.Start(dlg.FileName);
                
            }
           
        }


        private  void PopulateTemplates(int templetId)
        {

            var client = new TemplateManagerClient("BasicHttpBinding_ITemplateManager", _address);
            LstTemplates.DataSource = null;
            LstTemplates.DataSource = client.GetTemplates(templetId);
            LstTemplates.DisplayMember = "Name";
            LstTemplates.ValueMember = "TemplateId";
            client.Close();
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
    }
}

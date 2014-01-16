using System;
using System.ServiceModel;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using TemplateEditor.TemplateService;

namespace TemplateEditor.Forms
{
    public partial class ObjectList : Form
    {
        private static readonly string Uri = Properties.Settings.Default.ServerUrl.ToString();
        EndpointAddress _address = new EndpointAddress(Uri);

        public ObjectList()
        {
            InitializeComponent();
        }

        private void ObjectList_Load(object sender, EventArgs e)
        {
            LoadTemplateTypes();
        }

        private void LoadTemplateTypes()
        {

            var client = new TemplateManagerClient("BasicHttpBinding_ITemplateManager", _address);
            cmbTemplateTypes.DataSource = null;
            cmbTemplateTypes.DataSource = client.GetTemplateTypes();
            cmbTemplateTypes.DisplayMember = "TemplateObject";
            cmbTemplateTypes.ValueMember = "TemplateTypeId";
            client.Close();
           
        }

        private void GetTemplates(int templetTypeId)
        {
            var client = new TemplateManagerClient("BasicHttpBinding_ITemplateManager", _address);
            LstTemplates.DataSource = null;
            LstTemplates.DataSource = client.GetTemplates(templetTypeId);
            LstTemplates.DisplayMember = "Name";
            LstTemplates.ValueMember = "TemplateId";
            client.Close();
        }

        private void ShowFields(int templateId)
        {
            var client = new TemplateManagerClient("BasicHttpBinding_ITemplateManager", _address);

            LstFields.DataSource = null;
            LstFields.Items.Clear();
            LstFields.DataSource = client.GetFieldsByTemplateId(templateId);
            LstFields.DisplayMember = "Field";
            LstFields.ValueMember = "TemplateId";
            client.Close();
        }

        private void cmbTemplateTypes_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                GetTemplates(int.Parse(cmbTemplateTypes.SelectedValue.ToString()));
                LstFields.DataSource = null;
                LstFields.Items.Clear();
            }
            catch (Exception)
            {
                
                
            }
            
        }

        private void LstObjects_Click(object sender, EventArgs e)
        {
            try
            {
                ShowFields(int.Parse(LstTemplates.SelectedValue.ToString()));
            }
            catch (Exception)
            {
                
                
            }
           
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            _Application wordApp;
            try
            {
                wordApp = (_Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");
            }
            catch (Exception)
            {

                wordApp = new Microsoft.Office.Interop.Word.Application();
            }
     
            // insert merge field at the current cursor location
           

            Microsoft.Office.Interop.Word.Range selection = wordApp.Selection.Range;
         

            var doc = wordApp.ActiveDocument;
            doc.MailMerge.Fields.Add(selection,LstFields.Text);

            this.Close();
            this.Dispose();
        }
    }
}

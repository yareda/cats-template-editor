using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TemplateEditor.TemplateService;

namespace TemplateEditor.Forms
{
    public partial class TemplateType : Form
    {
        // add a delegate
        public delegate void TemplateTypeUpdateHandler(object sender,
        TemplateTypeEveArguments e);

        // add an event of the delegate type
        public event TemplateTypeUpdateHandler TemplateTypeUpdated;

        public int templateType;
        public string fileName;

        public TemplateType()
        {
            InitializeComponent();
            
        }
       
        private void TemplateType_Load(object sender, EventArgs e)
        {
            GetTemplateTypes();
        }

        private void GetTemplateTypes()
        {
            var client = new TemplateManagerClient();
            cmbTemplateTypes.DataSource = null;
            cmbTemplateTypes.DataSource = client.GetTemplateTypes();
            cmbTemplateTypes.DisplayMember = "TemplateObject";
            cmbTemplateTypes.ValueMember = "TemplateTypeId";
            client.Close();
        }

        private Boolean Validate()
        {
            if (int.Parse(cmbTemplateTypes.SelectedValue.ToString()) == -1)
                return false;
            return txtFileName.Text.Trim().Length >= 1;
        }


        private void Ok()
        {
            if (Validate())
            {
                templateType = int.Parse(cmbTemplateTypes.SelectedValue.ToString());
                fileName = txtFileName.Text.Trim();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            var templateType = int.Parse(cmbTemplateTypes.SelectedValue.ToString());
            var fileName = txtFileName.Text.Trim();

            var arguments = new TemplateTypeEveArguments(templateType,fileName);
            TemplateTypeUpdated(this, arguments);

           
           
            Dispose();
        }
        

    }
}

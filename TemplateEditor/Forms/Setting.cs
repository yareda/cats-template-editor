using System;
using System.Globalization;
using System.ServiceModel;
using System.Windows.Forms;
using TemplateEditor.TemplateService;

namespace TemplateEditor.Forms
{
    public partial class Setting : Form
    {
        public Setting()
        {
            InitializeComponent();
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            btnUseCurrent.Enabled = false;
            txtUrl.Text = Properties.Settings.Default.ServerUrl.ToString();
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            txtUrl.Text = Properties.Settings.Default.ServerUrl.ToString();
        }

        private void btnUseCurrent_Click(object sender, EventArgs e)
        {

          if (MessageBox.Show(text: "This will overwrite the default url path to the server. Continue?", caption: "Warning", buttons: MessageBoxButtons.YesNo, icon: MessageBoxIcon.Warning)  == DialogResult.Yes)
          {
              Properties.Settings.Default["ServerUrl"] = txtUrl.Text;
              Properties.Settings.Default.Save();
          }
            
        }

        private void txtUrl_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtUrl_KeyUp(object sender, KeyEventArgs e)
        {
            btnUseCurrent.Enabled = txtUrl.Text != Properties.Settings.Default["ServerUrl"].ToString();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
             string Uri = Properties.Settings.Default.ServerUrl.ToString(CultureInfo.InvariantCulture);

             EndpointAddress _address = new EndpointAddress(Uri);
            try
            {
                var client = new TemplateManagerClient("BasicHttpBinding_ITemplateManager", _address);
                client.DoWork();
                MessageBox.Show("Connection successful","Server Connection",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch (Exception)
            {

                MessageBox.Show("Unable to connect to the server", "Server connection", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            
        }
    }
}

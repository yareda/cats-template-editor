using System;
using System.ServiceModel;
using System.Windows.Forms;
using TemplateEditor.TemplateService;

namespace TemplateEditor.Forms
{
    public partial class Login : Form
    {
        private static readonly string Uri = Properties.Settings.Default.ServerUrl.ToString();
        EndpointAddress _address = new EndpointAddress(Uri);


        private Boolean loggedIn=false;

        public Boolean IsLoggedIn
        {
            get { return loggedIn; } 
        }

        public Login()
        {
            InitializeComponent();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            TemplateManagerClient client = null;
            try
            {
                client = new TemplateManagerClient("BasicHttpBinding_ITemplateManager", _address);
            }
            catch (Exception)
            {

                MessageBox.Show("Unable to connect to the Server. Please check the server URL", "Server Connection",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
             


            if (txtUserName.Text.Trim().Length < 1 )
            {
                MessageBox.Show("User Name is Empty", "Login Erorr", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUserName.Focus();
                return;
            }
            if (txtPassword.Text.Trim().Length < 1)
            {
                MessageBox.Show("Password is Empty", "Login Erorr", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }
            
            try
            {
                loggedIn = client.Authenticate(txtUserName.Text, txtPassword.Text);
                this.Close();
                this.Dispose();
                EnableRibbon();
            }
            catch
            {
                DisableRibbon();
                MessageBox.Show("User name or password is incorrect", "Login Erorr", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void DisableRibbon()
        {
            Globals.Ribbons.TemplateRibbon.btnSave.Enabled = false;
            Globals.Ribbons.TemplateRibbon.btnAddNew.Enabled = false;
            Globals.Ribbons.TemplateRibbon.btnEdit.Enabled = false;
            //Globals.Ribbons.TemplateRibbon.btnSetings.Enabled = false;
            Globals.Ribbons.TemplateRibbon.btnTemplate.Enabled = false;
            
        }

        private void EnableRibbon()
        {
            Globals.Ribbons.TemplateRibbon.btnSave.Enabled = true;
            Globals.Ribbons.TemplateRibbon.btnAddNew.Enabled = true;
            Globals.Ribbons.TemplateRibbon.btnEdit.Enabled = true;
            //Globals.Ribbons.TemplateRibbon.btnSetings.Enabled = true;
            Globals.Ribbons.TemplateRibbon.group4.Visible = false;
            Globals.Ribbons.TemplateRibbon.btnTemplate.Enabled = true;
           
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void btnOk_KeyDown(object sender, KeyEventArgs e)
        {

        }

       

      
    }
}

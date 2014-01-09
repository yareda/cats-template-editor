using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Tools.Ribbon;
using TemplateEditor.Forms;
using TemplateEditor.TemplateService;

namespace TemplateEditor
{
    public partial class TemplateRibbon
    {
        private void TemplateRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            DisableRibbon();
            //InitLoginForm();
        }

        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            var settingDialog = new Setting();
            settingDialog.ShowDialog();
        }

        private void btnEdit_Click(object sender, RibbonControlEventArgs e)
        {
            var load = new FileTransfer();
            load.ShowDialog();
        }

        private void btnAddNew_Click(object sender, RibbonControlEventArgs e)
        {
            var editTemp = new ObjectList();
            editTemp.ShowDialog();
        }

        private void btnSave_Click(object sender, RibbonControlEventArgs e)
        {
            //Microsoft.Office.Interop.Word.Application oWord = new Application();

            //oWord.Visible = true;

            //var oDoc = oWord.Documents.Add();

        }

        private void btnLogIn_Click(object sender, RibbonControlEventArgs e)
        {
            var login = new Login();
            login.ShowDialog();
        }

        private void DisableRibbon()
        {
            btnSave.Enabled = false;
            btnAddNew.Enabled = false;
            btnEdit.Enabled = false;
            btnSetings.Enabled = false;

        }

        private void EnableRibbon()
        {
            btnSave.Enabled = true;
            btnAddNew.Enabled = true;
            btnEdit.Enabled = true;
            btnSetings.Enabled = true;
            btnLogIn.Visible = false;
        }

        public void InitLoginForm()
        {
            var login = new Login();
            login.ShowDialog();

            if (login.IsLoggedIn)
            {
                EnableRibbon();
            }
            else
            {
                DisableRibbon();
            }

        }

        private void btnSave_Click_1(object sender, RibbonControlEventArgs e)
        {
            IDocumentProcessorService documentProcessorService = new DocumentProcessorService();


            _Application wordApp;
            try
            {
                wordApp = (_Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");
            }
            catch (Exception)
            {

                wordApp = new Microsoft.Office.Interop.Word.Application();
            }

            string newFileName;
            var aDoc = wordApp.ActiveDocument;
            string fileName = aDoc.Name;

            int dotPosition = fileName.IndexOf(".", 1, System.StringComparison.Ordinal);

            switch (dotPosition)
            {
                case -1:
                    {
                        // Ask where it should be saved
                        var dlg = new SaveFileDialog()
                        {
                            RestoreDirectory = true,
                            OverwritePrompt = true,
                            Title = "Enter the file name of the template",
                        };
                        dlg.ShowDialog();

                        fileName = Path.GetFileName(dlg.FileName);
                    }
                    break;
                default:
                    fileName = fileName.Substring(0, aDoc.Name.Length - (aDoc.Name.Length - dotPosition));
                    fileName = Properties.Settings.Default.DefaultPath.ToString() + fileName;
                    break;
            }

            if (fileName.Trim() == string.Empty)
                return;
            aDoc.SaveAs(fileName, WdSaveFormat.wdFormatXMLTemplate);
            aDoc.Close();
            documentProcessorService.UploadDocument(1, fileName + ".dotx");
            documentProcessorService.DeleteDocument(fileName + ".dotx");
            InsertToLetterTemplate(fileName ,1);
            InsertToTemplate(fileName, 1);
            MessageBox.Show("Template is Uploaded.", "Saving", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

      

        private void InsertToLetterTemplate(string name, int templateType)
        {
          
            var letter = new LetterTemplate()
            {
                Name = name,
                FileName = name,
                TemplateType = templateType
            };

            var client = new TemplateManagerClient();
            client.InsertToLetterTemplate(letter);

        }


        private void InsertToTemplate(string name, int templateType)
        {

            var letter = new TemplateEditor.TemplateService.Template()
            {
                Name = name,
                FileName = name,
                TemplateType = templateType
            };

            var client = new TemplateManagerClient();
            client.InsertToTemplate(letter);

        }

        private void btnPreview_Click(object sender, RibbonControlEventArgs e)
        {

            saveTemporarly();

        }

        private void saveTemporarly()
        {
            IDocumentProcessorService documentProcessorService = new DocumentProcessorService();


            _Application wordApp;
            try
            {
                wordApp = (_Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");
            }
            catch (Exception)
            {

                wordApp = new Microsoft.Office.Interop.Word.Application();
            }

           
            var aDoc = wordApp.ActiveDocument;
            var fileName = aDoc.Name;
            int dotPosition = fileName.IndexOf(".", 1, System.StringComparison.Ordinal);

            switch (dotPosition)
            {
                case -1:
                    {
                        
                        fileName = Guid.NewGuid().ToString();
                        aDoc.SaveAs(fileName, WdSaveFormat.wdFormatTemplate);
                        aDoc.Close();
                        documentProcessorService.UploadDocument(1, fileName + ".dot");
                        documentProcessorService.DeleteDocument(fileName + ".dot");
                    }
                    break;
                default:
                    fileName = fileName.Substring(0, aDoc.Name.Length - (aDoc.Name.Length - dotPosition));
                    fileName = Properties.Settings.Default.DefaultPath.ToString() + fileName;
                    aDoc.SaveAs(fileName, WdSaveFormat.wdFormatTemplate);
                    aDoc.Close();
                    documentProcessorService.UploadDocument(1, fileName + ".dot");
                    documentProcessorService.DeleteDocument(fileName + ".dot");

                    break;
            }

            //var docFile = documentProcessorService.PreviewDoc(1,fileName + ".dot");
            //Process.Start("WINWORD.EXE", docFile + "dotx");
            
        }
    }
}

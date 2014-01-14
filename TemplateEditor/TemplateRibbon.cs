using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Tools.Ribbon;
using TemplateEditor.Forms;
using TemplateEditor.TemplateService;
using TemplateType = TemplateEditor.Forms.TemplateType;

namespace TemplateEditor
{
    public partial class TemplateRibbon
    {
        public int tTemplateType;
        public string newFileName;

        private void TemplateRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            DisableRibbon();
            GetTemplateTypes();

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
            btnTemplate.Enabled = false;
            

        }

        private void EnableRibbon()
        {
            btnSave.Enabled = true;
            btnAddNew.Enabled = true;
            btnEdit.Enabled = true;
            btnSetings.Enabled = true;
            btnLogIn.Visible = false;
            btnTemplate.Enabled = false;
           
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


        // handles the event from frmId
        private void TemplateType_ButtonClicked(object sender, TemplateTypeEveArguments e)
        {
            // update the forms values from the event args
            newFileName = e.fFileName;
            tTemplateType = e.tTemplateType;

        }

        private void btnSave_Click_1(object sender, RibbonControlEventArgs e)
        {

            try
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
           

            

            string fileName = aDoc.Name;

             int dotPosition = fileName.IndexOf(".", 1, System.StringComparison.Ordinal);
             fileName = fileName.Substring(0, aDoc.Name.Length - (aDoc.Name.Length - dotPosition));

            if (StringIsGuid(fileName));
            {
                var templateType = new TemplateType();
                templateType.TemplateTypeUpdated +=
                    new TemplateType.TemplateTypeUpdateHandler(TemplateType_ButtonClicked);
              
                templateType.ShowDialog();
                fileName = newFileName;
            }
          

            //switch (dotPosition)
            //{
            //    case -1:
            //        {
            //             Ask where it should be saved
            //            var dlg = new SaveFileDialog()
            //            {
            //                RestoreDirectory = true,
            //                OverwritePrompt = true,
            //                Title = "Enter the file name of the template",

            //            };
            //            dlg.ShowDialog();

            //            fileName = Path.GetFileName(dlg.FileName);
            //        }
            //        break;
            //    default:
            //        fileName = fileName.Substring(0, aDoc.Name.Length - (aDoc.Name.Length - dotPosition));
                  
            //        break;
            //}


           
            //OBJECT OF MISSING "NULL VALUE"
            Object oMissing = Type.Missing;

            if (fileName.Trim() == string.Empty)
                return;

           


            Object oSaveAsFile = (Object)fileName;

            aDoc.SaveAs(oSaveAsFile,WdSaveFormat.wdFormatXMLTemplate,  ref oMissing, ref oMissing,
                            ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                            ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                            ref oMissing, ref oMissing);

            aDoc.Close();
            documentProcessorService.UploadDocument(tTemplateType, fileName + ".dotx");
            documentProcessorService.DeleteDocument(fileName + ".dotx");
            InsertToLetterTemplate(fileName, tTemplateType);
            InsertToTemplate(fileName, tTemplateType);
            MessageBox.Show("Template is Uploaded.", "Saving", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("There was a problem in saving the template.", "Saving", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }


        }

        public bool StringIsGuid(string sString)
        {
            bool bResult = true;
            try
            {
                
                new Guid(sString);
                
            }
            catch
            {
                bResult = false;

            }
            return bResult;
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

       
        

        private void btnTemplate_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {


                DocumentProcessorService documentProcessorService = new DocumentProcessorService();

                _Document aDoc = new Document();
                Object oMissing = Type.Missing;

                aDoc.Activate();
                Object oSaveAsFile = (Object) Guid.NewGuid().ToString();

                //Insert a paragraph at the beginning of the document.

                Paragraph oPara1;
                oPara1 = aDoc.Content.Paragraphs.Add(ref oMissing);
                oPara1.Range.Text = "New Template. You can create a new template from this document.";
                oPara1.Range.Font.Bold = 1;
                oPara1.Format.SpaceAfter = 24; //24 pt spacing after paragraph.
                oPara1.Range.InsertParagraphAfter();


                aDoc.SaveAs(oSaveAsFile, WdSaveFormat.wdFormatXMLTemplate, ref oMissing, ref oMissing,
                            ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                            ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                            ref oMissing, ref oMissing);

                aDoc.Close();
                documentProcessorService.UploadDocument(1, oSaveAsFile + ".dotx");

                var filePath = Properties.Settings.Default.TemplatePath.ToString(CultureInfo.InvariantCulture) +
                               oSaveAsFile;
                if (!string.IsNullOrEmpty(filePath))
                {
                    //Get the file from the server
                    using (var output = new FileStream(filePath, FileMode.Create))
                    {
                        Stream downloadStream;

                        using (var client = new TemplateManagerClient())
                        {
                            downloadStream = client.GetFile(oSaveAsFile.ToString());
                        }

                        downloadStream.CopyTo(output);
                        downloadStream.Close();
                    }
                    Process.Start("WINWORD.EXE", filePath + ".DOTX");
                }

            }
            catch (Exception)
            {
                MessageBox.Show("There was a problem in creating a new template.", "Creating new Template", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void GetTemplateTypes()
        {
            var client = new TemplateManagerClient();
            var templateTypes = client.GetTemplateTypes();

            foreach (var templateType in templateTypes)
            {
                RibbonDropDownItem item = this.Factory.CreateRibbonDropDownItem();
                item.Label = templateType.TemplateObject.ToString();
                
               // cmbTemplateType.Items.Add(item);
            }
           
            client.Close();
        }
        
            
    }

}

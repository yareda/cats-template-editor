using System.IO;
using System.Windows.Forms;
using TemplateEditor.TemplateService;

namespace TemplateEditor
{
   public class DocumentProcessorService:IDocumentProcessorService
    {
       public void UploadDocument(int templateType, string filepath)
       {
           if (!string.IsNullOrEmpty(filepath))
           {
               string virtualPath =  Path.GetFileName(filepath);

               using (Stream uploadStream = new FileStream(filepath, FileMode.Open))
               {
                   using (var client = new TemplateManagerClient())
                   {
                      client.PutFile(new FileUploadMessage() { VirtualPath = virtualPath, DataStream = uploadStream }.VirtualPath, uploadStream);
                   }
               }

               
           }
       }



       public string DownloadDocument(ListView fileList)
       {
           var filePath = string.Empty;
           
               ListViewItem item = fileList.SelectedItems[0];

               // Strip off 'Root' from the full path
               var path = item.SubItems[1].Text;


               filePath = Properties.Settings.Default.TemplatePath.ToString() + path;
               if (!string.IsNullOrEmpty(filePath))
               {
                    //Get the file from the server
                   using (var output = new FileStream(filePath, FileMode.Create))
                   {
                       Stream downloadStream;

                       using (var client = new TemplateManagerClient())
                       {
                           downloadStream = client.GetFile(path);
                       }

                       downloadStream.CopyTo(output);
                       downloadStream.Close();
                   }
                   return filePath;

               }
               return string.Empty;
           
           
       }

       public void DeleteDocument(string path)
       {
           if (File.Exists(path))
           {

               File.Delete(path);
           }
       }

      


      

       public string PreviewDoc(string fileName)
       {
           using (var client = new TemplateManagerClient())
           {
               var docFile = client.PreviewTemplate(filePath: fileName);
               return docFile;
           }
       }
    }
}

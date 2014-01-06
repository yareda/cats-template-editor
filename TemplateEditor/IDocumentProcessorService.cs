using System.Windows.Forms;

namespace TemplateEditor
{
    public interface IDocumentProcessorService
    {
        void UploadDocument(int templateType, string path);
        string DownloadDocument(ListView fileList);
        void DeleteDocument(string path);

    }
}

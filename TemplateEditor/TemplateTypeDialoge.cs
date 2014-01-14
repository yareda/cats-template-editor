using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TemplateEditor
{
   public class TemplateTypeEveArguments: EventArgs
   {
       private int _templateType;
       private string _fileName;

       public TemplateTypeEveArguments(int templateType, string fileName)
       {
           _templateType = templateType;
           _fileName = fileName;
       }

       public int tTemplateType
       {
           get { return _templateType; }
       }
       public string fFileName
       {
           get { return _fileName; }
       }
   }
}

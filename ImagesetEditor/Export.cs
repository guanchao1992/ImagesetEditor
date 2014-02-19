using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ImageSetEditor
{
    class TextExport : IImagesetExport
    {
        #region Fields

        private string m_fileName;

        #endregion Fields

        #region Methods

        public void OnExportBegin()
        {
            FileStream file = new FileStream(m_fileName, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(file);
        }

        public void OnExportImage(IImage image)
        {
        }

        public string OnExportEnd()
        {
            return null;
        }

        #endregion Methods
        
        #region Constructors

        public TextExport(string fileName)
        {
            m_fileName = fileName;
        }

        #endregion Constructors
    }

    class XmlExport : IImagesetExport
    {
        #region Fields

        private string m_fileName;

        #endregion Fields

        #region Methods

        public void OnExportBegin()
        {
        }

        public void OnExportImage(IImage image)
        {
        }

        public string OnExportEnd()
        {
            return null;
        }

        #endregion Methods

        #region Constructors

        public XmlExport(string fileName)
        {
            m_fileName = fileName;
        }

        #endregion Constructors
    }
}

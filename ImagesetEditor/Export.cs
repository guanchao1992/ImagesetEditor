using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace ImageSetEditor
{
    class ProjectExport : IImagesetExport
    {
        #region Fields

        private string m_fileName;

        private XmlDocument m_xml;

        private XmlNode m_root;

        #endregion Fields

        #region Methods

        public void OnExportBegin(ICanvas canvas)
        {
            m_xml = new XmlDocument();
            m_root = m_xml.CreateElement("Imageset");
            m_xml.AppendChild(m_root);

            XmlAttribute width = m_root.Attributes.Append(m_xml.CreateAttribute("Width"));

            width.Value = canvas.Size.Width.ToString();

            XmlAttribute height = m_root.Attributes.Append(m_xml.CreateAttribute("Height"));

            height.Value = canvas.Size.Height.ToString();
        }

        public void OnExportImage(IImage image)
        {
            XmlNode imageNode = m_root.AppendChild(m_xml.CreateElement("Image"));

            XmlNode name = imageNode.Attributes.Append(m_xml.CreateAttribute("Name"));

            name.Value = image.Name;

            XmlAttribute xPos = imageNode.Attributes.Append(m_xml.CreateAttribute("XPos"));

            xPos.Value = image.Position.X.ToString();

            XmlAttribute yPos = imageNode.Attributes.Append(m_xml.CreateAttribute("YPos"));

            yPos.Value = image.Position.Y.ToString();

            XmlAttribute width = imageNode.Attributes.Append(m_xml.CreateAttribute("Width"));

            width.Value = image.Size.Width.ToString();

            XmlAttribute height = imageNode.Attributes.Append(m_xml.CreateAttribute("Height"));

            height.Value = image.Size.Height.ToString();
        }

        public string OnExportEnd()
        {
            FileStream file = new FileStream(m_fileName, FileMode.OpenOrCreate);

            StreamWriter sw = new StreamWriter(file);

            m_xml.Save(sw);

            sw.Close();

            file.Close();

            return null;
        }

        #endregion Methods

        #region Constructors

        public ProjectExport(string fileName)
        {
            m_fileName = fileName;
        }

        #endregion Constructors
    }

    class TextExport : IImagesetExport
    {
        #region Fields

        private string m_fileName;

        private FileStream m_file;

        private StreamWriter m_sw;

        #endregion Fields

        #region Methods

        public void OnExportBegin(ICanvas canvas)
        {
            m_file = new FileStream(m_fileName, FileMode.OpenOrCreate);
            m_sw = new StreamWriter(m_file);
        }

        public void OnExportImage(IImage image)
        {
            m_sw.WriteLine(
                "\"" + image.Name + "\" " + 
                image.Position.X + "," + image.Position.Y + " " +
                image.Size.Width + "," + image.Size.Height
                );
        }

        public string OnExportEnd()
        {
            m_sw.Close();

            m_file.Close();

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

        private XmlDocument m_xml;

        private XmlNode m_root;

        #endregion Fields

        #region Methods

        public void OnExportBegin(ICanvas canvas)
        {
            m_xml = new XmlDocument();
            m_root = m_xml.CreateElement("Imageset");
            m_xml.AppendChild(m_root);
        }

        public void OnExportImage(IImage image)
        {
            XmlNode imageNode = m_root.AppendChild(m_xml.CreateElement("Image"));

            XmlNode name = imageNode.Attributes.Append(m_xml.CreateAttribute("Name"));

            name.Value = image.Name;

            XmlNode xPos = imageNode.Attributes.Append(m_xml.CreateAttribute("XPos"));

            xPos.Value = image.Position.X.ToString();

            XmlNode yPos = imageNode.Attributes.Append(m_xml.CreateAttribute("YPos"));

            yPos.Value = image.Position.Y.ToString();

            XmlNode width = imageNode.Attributes.Append(m_xml.CreateAttribute("Width"));

            width.Value = image.Size.Width.ToString();

            XmlNode height = imageNode.Attributes.Append(m_xml.CreateAttribute("Height"));

            height.Value = image.Size.Height.ToString();
        }

        public string OnExportEnd()
        {
            FileStream file = new FileStream(m_fileName, FileMode.OpenOrCreate);

            StreamWriter sw = new StreamWriter(file);

            m_xml.Save(sw);

            sw.Close();

            file.Close();

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

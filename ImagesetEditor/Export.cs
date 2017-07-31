using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace ImagesetEditor
{
    class ProjectExport : IImagesetExport
    {
        #region Fields

        private string m_fileName;

        private string m_folder;

        private XmlDocument m_xml;

        private XmlNode m_root;

        private MainForm m_form;

        #endregion Fields

        #region Methods

        public void OnExportBegin(ICanvas canvas)
        {
            m_xml = new XmlDocument();

            m_root = m_xml.CreateElement("ImagesetProject");

            m_xml.AppendChild(m_root);

            XmlAttribute projectVersion = m_root.Attributes.Append(m_xml.CreateAttribute("ProjectVersion"));

            projectVersion.Value = MainForm.GetProjectVersion();

            XmlAttribute version = m_root.Attributes.Append(m_xml.CreateAttribute("Version"));

            version.Value = MainForm.GetVersion();

            XmlAttribute width = m_root.Attributes.Append(m_xml.CreateAttribute("Width"));

            width.Value = canvas.Size.Width.ToString();

            XmlAttribute height = m_root.Attributes.Append(m_xml.CreateAttribute("Height"));

            height.Value = canvas.Size.Height.ToString();

            XmlAttribute rimView = m_root.Attributes.Append(m_xml.CreateAttribute("RimView"));

            rimView.Value = m_form.EditControl.RimView.ToString();

            XmlAttribute background = m_root.Attributes.Append(m_xml.CreateAttribute("Background"));

            background.Value = 
                canvas.ColorWorkSpace.R.ToString() + "," +
                canvas.ColorWorkSpace.G.ToString() + "," +
                canvas.ColorWorkSpace.B.ToString();

            XmlAttribute lastExportFile = m_root.Attributes.Append(m_xml.CreateAttribute("LastExportFile"));

            lastExportFile.Value = m_form.LastExportFile;

            XmlAttribute lastExportFilterIndex = m_root.Attributes.Append(m_xml.CreateAttribute("LastExportFilterIndex"));

            lastExportFilterIndex.Value = m_form.LastExportFilterIndex.ToString();
        }

        public string OnExportEnd()
        {
            if (File.Exists(m_fileName))
            {
                File.Delete(m_fileName);
            }

            FileStream file = new FileStream(m_fileName, FileMode.OpenOrCreate);

            StreamWriter sw = new StreamWriter(file);

            m_xml.Save(sw);

            sw.Close();

            file.Close();

            return null;
        }

        public void OnExportGroupBegin(GroupExpression expression)
        {
            XmlNode groupNode = m_root.AppendChild(m_xml.CreateElement("Group"));

            XmlNode exp = groupNode.Attributes.Append(m_xml.CreateAttribute("Expression"));

            exp.Value = expression.Expression;
        }

        public void OnExportGroupEnd()
        {
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

            XmlAttribute source = imageNode.Attributes.Append(m_xml.CreateAttribute("Source"));

            source.Value = image.FilePath.Replace(m_folder, "$(ProjectDir)");
        }

        public static string GetFolder(string fileName)
        {
            string folder = "";

            List<string> path = new List<string>(fileName.Split('\\'));

            if (path.Count() > 2)
            {
                path.RemoveAt(path.Count() - 1);
            }

            foreach (string s in path)
            {
                folder = folder + s + "\\";
            }

            return folder;
        }

        #endregion Methods

        #region Constructors

        public ProjectExport(string fileName, MainForm form)
        {
            m_fileName = fileName;

            m_form = form;

            m_folder = GetFolder(fileName);
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
            if (File.Exists(m_fileName))
            {
                File.Delete(m_fileName);
            }

            m_file = new FileStream(m_fileName, FileMode.OpenOrCreate);
            m_sw = new StreamWriter(m_file);
        }

        public string OnExportEnd()
        {
            m_sw.Close();

            m_file.Close();

            return m_fileName.Split('.').First() + ".png";
        }

        public void OnExportGroupBegin(GroupExpression expression)
        {
            m_sw.WriteLine("\"" + expression.Name + "\"");
        }

        public void OnExportGroupEnd()
        {

        }

        public void OnExportImage(IImage image)
        {
            m_sw.WriteLine(
                "    \"" + image.Name + "\" " + 
                image.Position.X + "," + image.Position.Y + " " +
                image.Size.Width + "," + image.Size.Height
                );
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

        public string OnExportEnd()
        {
            if (File.Exists(m_fileName))
            {
                File.Delete(m_fileName);
            }

            FileStream tFile = new FileStream(m_fileName, FileMode.Create, FileAccess.Write, FileShare.Read);

            try
            {
                XmlWriterSettings tSetting = new XmlWriterSettings();
                tSetting.CloseOutput = false;
                tSetting.Encoding = Encoding.UTF8;
                tSetting.Indent = true;
                tSetting.IndentChars = "  ";
                tSetting.NewLineOnAttributes = false;
                tSetting.OmitXmlDeclaration = true;

                XmlWriter tWritter = XmlWriter.Create(tFile, tSetting);

                m_xml.Save(tWritter);
            }
            catch
            {
                tFile.Close();

                throw;
            }

            tFile.Close();

            return m_fileName.Split('.').First() + ".png"; ;
        }

        public void OnExportGroupBegin(GroupExpression expression)
        {
            XmlNode groupNode = m_root.AppendChild(m_xml.CreateElement("Group"));

            XmlNode name = groupNode.Attributes.Append(m_xml.CreateAttribute("Name"));

            name.Value = expression.Name;

            foreach (KeyValuePair<string, string> item in expression.Data)
            {
                XmlNode attr = groupNode.Attributes.Append(m_xml.CreateAttribute(item.Key));

                attr.Value = item.Value;
            }
        }

        public void OnExportGroupEnd()
        {
        }
        private void setXmlAttribute(XmlDocument xml, XmlNode imageNode, string attribute, string value)
        {
            MainForm.setXmlAttribute(xml, imageNode, attribute, value);
        }

        public void OnExportImage(IImage image)
        {
            //XmlNodeList xnl = m_xml.GetElementsByTagName(image.Name);
            XmlNode imageNode = m_root.AppendChild(m_xml.CreateElement("Image"));

            XmlNode name = imageNode.Attributes.Append(m_xml.CreateAttribute("Name"));

            name.Value = image.Name;
            /*
            XmlNode imageNode = null;
            if (xnl.Count >0)
            {
                imageNode = m_root.AppendChild(m_xml.CreateElement("Image"));
                XmlNode name = imageNode.Attributes.Append(m_xml.CreateAttribute("Name"));
                name.Value = image.Name;
            }
            else
            {
                imageNode = xnl[0];
            }
            */

            setXmlAttribute(m_xml, imageNode, "XPos", image.Position.X.ToString());
            setXmlAttribute(m_xml, imageNode, "YPos", image.Position.Y.ToString());
            setXmlAttribute(m_xml, imageNode, "Width", image.Size.Width.ToString());
            setXmlAttribute(m_xml, imageNode, "Height", image.Size.Height.ToString());
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

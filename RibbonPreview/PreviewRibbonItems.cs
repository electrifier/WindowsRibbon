using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Linq;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using RibbonGenerator;

namespace RibbonPreview
{
    class PreviewRibbonItems
    {
        private Action<bool> buildActionEnabled;
        private Action<bool> previewActionEnabled;
        private Action<string> setText;
        private string uiccXsdPath = @"..\..\UICC.xsd";

        public static PreviewRibbonItems Instance = new PreviewRibbonItems();

        private PreviewRibbonItems()
        {
        }

        internal RibbonParser Parser { get; private set; }

        public void SetActions(Action<bool> buildActionEnabled, Action<bool> previewActionEnabled, Action<string> setText)
        {
            this.buildActionEnabled = buildActionEnabled;
            this.previewActionEnabled = previewActionEnabled;
            this.setText = setText;
            buildActionEnabled(false);
            previewActionEnabled(false);
        }

        public void SetRibbonXmlFile(string path)
        {
            string validateMsg;
            bool buildEnabled = false;
            bool previewEnabled = false;
            if (File.Exists(path))
            {
                validateMsg = Validation(path);
                bool validate = (validateMsg == "" ? true : false);
                if (validate)
                {
                    XmlRibbonFile = path;
                    buildEnabled = true;
                    previewEnabled = CheckRibbonResource(path);
                }
                else
                {
                    setText(validateMsg);
                    MessageBox.Show("Xml is not valid", "XML Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            buildActionEnabled(buildEnabled);
            previewActionEnabled(previewEnabled);
        }

        private bool CheckRibbonResource(string path)
        {
            bool previewEnabled = false;
            string ribbonResourceFileName = Path.ChangeExtension(path, "ribbon");
            if (File.Exists(ribbonResourceFileName))
            {
                SetRibbonResourceName(ribbonResourceFileName);
                previewEnabled = true;
                Parser = new RibbonParser(path);
            }
            return previewEnabled;
        }

        private void SetRibbonResourceName(string path)
        {
            RibbonResourceName = "file://" + path;
        }

        /// <summary>
        /// MainForm sets the name, PreviewForm uses it for Ribbon.ResourceName
        /// ResourceName starts with "file://". It's a file based resource
        /// </summary>
        public string RibbonResourceName { get; private set; }

        public string XmlRibbonFile { get; private set; }


        private string Validation(string path)
        {
            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add("http://schemas.microsoft.com/windows/2009/Ribbon", uiccXsdPath);

            XDocument doc = XDocument.Load(path);
            string msg = "";
            doc.Validate(schemas, (o, e) =>
            {
                msg += e.Message + Environment.NewLine;
            });
            return msg;
            //Console.WriteLine(msg == "" ? "Document is valid" : "Document invalid: " + msg);
        }

        //private void Validation2(string filename)
        //{
        //    // Set the validation settings.
        //    XmlReaderSettings settings = new XmlReaderSettings();
        //    settings.ValidationType = ValidationType.Schema;
        //    settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
        //    settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;
        //    settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
        //    settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);
        //    settings.Schemas.Add("http://schemas.microsoft.com/windows/2009/Ribbon", uiccXsdPath);

        //    // Create the XmlReader object.
        //    XmlReader reader = XmlReader.Create(filename, settings);

        //    // Parse the file. 
        //    while (reader.Read()) ;

        //}
        //// Display any warnings or errors.
        //private static void ValidationCallBack(object sender, ValidationEventArgs args)
        //{
        //    if (args.Severity == XmlSeverityType.Warning)
        //        Console.WriteLine("\tWarning: Matching schema not found.  No validation occurred." + args.Message);
        //    else
        //        Console.WriteLine("\tValidation error: " + args.Message);

        //}

        public void BuildRibbonFile()
        {
            MessageOutput message = null;
            try
            {
                string path = XmlRibbonFile;
                string content = File.ReadAllText(path);
                Manager manager = new Manager(message = new MessageOutput(), path, content);

                var targets = manager.Targets;
                foreach (var target in targets)
                {
                    var buffer = manager.CreateRibbon(target);
                    File.WriteAllBytes(target.RibbonFilename, buffer);
                }
                bool previewEnabled = CheckRibbonResource(path);
                previewActionEnabled(previewEnabled);

                // create the C# file RibbonItems.Designer.cs
                if (previewEnabled)
                    new CSharpCodeBuilder().Execute(path, Parser);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (message != null)
                {
                    string allMsg = message.GetString();
                    setText(allMsg);
                    BuildLogFile(allMsg);
                    message.Close();
                }
            }
        }

        private void BuildLogFile(string logging)
        {
            string fileName = XmlRibbonFile;
            StringReader sr = new StringReader(logging);
            StreamWriter sw = File.CreateText(Path.ChangeExtension(fileName, "log"));
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                sw.WriteLine(line);
            }
            sw.Close();
            sr.Close();
        }
    }
}

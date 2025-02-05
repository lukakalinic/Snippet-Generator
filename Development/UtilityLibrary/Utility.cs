﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using ScintillaNET;

namespace UtilityLibrary
{
    public static class Utility
    {

        public static string CreateXML(Scintilla scintillaBox, RichTextBox descriptionRichTextBox, TextBox titleTextBox, TextBox authorTextBox, RadioButton expansionSnippetRadioButton, RadioButton surroundWithRadioButton)
        {
            XNamespace xn = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet";

            var xdoc = new XDocument(
                       new XDeclaration("1.0", "utf-8", "yes"),
                       new XElement(xn + "CodeSnippets", new XAttribute("xmlns", "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet"),
                           new XElement("CodeSnippet", new XAttribute("Format", "1.0.0"),
                               new XElement("Header",
                                   new XElement("Title", titleTextBox.Text),
                                   new XElement("Shortcuts"),
                                   new XElement("Description", descriptionRichTextBox.Text),
                                   new XElement("Author", authorTextBox.Text),
                                   new XElement("SnippetTypes",
                                       new XElement("SnippetType", expansionSnippetRadioButton.Checked ? expansionSnippetRadioButton.Text : surroundWithRadioButton.Text))),
                               new XElement("Snippet",
                                   new XElement("Code", new XAttribute("Language", "XML"), new XCData(scintillaBox.Text))))));

            var wr = new StringWriter();
            xdoc.Save(wr);

            return wr.ToString();
        }

        public static void SaveFile(string filePath, string snippet)
        {
            StreamWriter sw = new StreamWriter(filePath);

            sw.Write(snippet);

            sw.Close();
        }

        public static bool IsBrace(int c)
        {
            switch (c)
            {
                case '(':
                case ')':
                case '[':
                case ']':
                    return true;
            }

            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace DependencyGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string _folder;
        StringBuilder _builder;

        public MainWindow()
        {
            InitializeComponent();
            _builder = new StringBuilder();

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {   
            if (!String.IsNullOrEmpty(textBox2.Text))
            {
                _folder = textBox2.Text;
                GenerateXML();
            }
            else
            {
                var dialog = new System.Windows.Forms.FolderBrowserDialog();
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    _folder = dialog.SelectedPath;
                    GenerateXML();
                }
            }
        }

        private void GenerateXML()
        {
            string[] filePaths = Directory.GetFiles(_folder);
            string dirName = new DirectoryInfo(_folder).Name;
            foreach (var file in filePaths)
            {
                FileInfo fInfo = new FileInfo(file);

                
                if (fInfo.Extension.ToLower().Contains("dll"))
                {
                    // Build the link includes
                    _builder.Append(part1);
                    _builder.Append(dirName + "\\" + fInfo.Name);
                    _builder.Append(part2);
                    _builder.Append(fInfo.Name);
                    _builder.Append(part3);
                    _builder.AppendLine();
                    // Build the file includes
                    _builder.Append(part4);
                    _builder.Append(dirName + "\\" + fInfo.Name);
                    _builder.Append(part5);
                    _builder.AppendLine();
                }
            }

            textBox1.Text = _builder.ToString();
        }



        string part1 = "<Content Include=\"";
        string part2 = "\"><Link>";
        string part3 = "</Link><CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory></Content>";
        string part4 = "<Content Include=\"";
        string part5 = "\" />";
        string part6;
    }
}

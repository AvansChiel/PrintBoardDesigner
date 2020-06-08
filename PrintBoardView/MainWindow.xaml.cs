using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using PrintBoardDesigner;

namespace PrintBoardView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainController mainController;
        public MainWindow()
        {
            InitializeComponent();
            mainController = new MainController();
        }

        private void OpenCircuit_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();

            Nullable<bool> result = openFileDlg.ShowDialog();
            if (result == true)
            {
                FileNameTextBox.Text = openFileDlg.FileName;
            }
        }

        private void StartCircuit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mainController.BuildCircuit(FileNameTextBox.Text);
                CircuitWindow circuitWindow = new CircuitWindow(mainController);
                this.Close();
            }
            catch (Exception exception)
            {
                FileNameTextBox.Text = exception.Message;
            }
        }

    }
}

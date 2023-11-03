using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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

namespace CameraRecordingFetchApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> outputFiles;
        public MainWindow()
        {
            InitializeComponent();
            outputFiles = new List<string>();
            PopulateTrainingCentres();
        }
        private void PopulateTrainingCentres()
        {
            cbTrainingCentre.Items.Add("Training Centre A");
            cbTrainingCentre.Items.Add("Training Centre B");
            cbTrainingCentre.Items.Add("Training Centre C");
        }

        private List<string> GetOutputFiles()
        {
            return outputFiles;
        }

        private void btnFetch_Click(object sender, RoutedEventArgs e, List<string> outputFiles)
        {
            string? trainingCentre = cbTrainingCentre.SelectedItem?.ToString();
            DateTime dateFrom = dpDateFrom.SelectedDate ?? DateTime.MinValue;
            _ = dpDateTo.SelectedDate
                ?? DateTime.MaxValue;
            outputFiles = new List<string>
            {
                "Recording1.mp4",
                "Recording2.mp4",
                "Recording3.mp4",
            };
            lstOutputFiles.Items.Clear();
            foreach (string file in outputFiles)
            {
                lstOutputFiles.Items.Add(new ListBoxItem { Content = file });
            }

        }
        private async void BtnDownload_Click(object sender, RoutedEventArgs e)
        {
            string downloadFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Downloads";

            if (!Directory.Exists(downloadFolder))
                Directory.CreateDirectory(downloadFolder);

            progressBar.Visibility = Visibility.Visible;
            progressBar.Value = 0;

            await Task.Run(() =>
            {
                int totalFiles = outputFiles.Count;
                int currentFile = 0;

                foreach (string file in outputFiles)
                {
                    string sourceFilePath = System.IO.Path.Combine("C:\\Centre-recordings", file); // Replace with the actual source file path
                    string destinationFilePath = System.IO.Path.Combine(downloadFolder, file);

                    File.Copy(sourceFilePath, destinationFilePath, true);

                    currentFile++;
                    int progressPercentage = (int)((double)currentFile / totalFiles * 100);

                }
            });

            progressBar.Visibility = Visibility.Hidden;
            MessageBox.Show("Files downloaded successfully!");
        }

        private static void UpdateProgressBar(int progressPercentage)
        {
            throw new NotImplementedException();
        }
        private void btnProgress_click(object sender, RoutedEventArgs e)
        {
            int progressPercentage = 0;
            UpdateProgressBar(progressPercentage);

            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
            worker.DoWork += Worker_DoWork;
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerAsync();
        }

        private void Worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i <= 100; i++)
            {
                ((BackgroundWorker)sender).ReportProgress(i);
                System.Threading.Thread.Sleep(100);
            }
        }
    }
}

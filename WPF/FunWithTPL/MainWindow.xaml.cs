using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;
namespace FunWithTPL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private CancellationTokenSource cancelToken = new CancellationTokenSource();
        private void cmdCancel_Click(Object sender, EventArgs e)
        {
            cancelToken.Cancel();
        }

        private void cmdProcess_Click(Object sender, EventArgs e)
        {
            Task.Factory.StartNew(()=>ProcessFiles());
        }

        private void ProcessFiles()
        {
            ParallelOptions parOpts = new ParallelOptions();
            parOpts.CancellationToken = cancelToken.Token;
            parOpts.MaxDegreeOfParallelism = System.Environment.ProcessorCount;
            string[] files = Directory.GetFiles(@".\Images","*.jpg", SearchOption.AllDirectories);
            string newDir = @".\InvertedImages";
            Directory.CreateDirectory(newDir);
            try{
                Parallel.ForEach(files, parOpts, currentFile =>
                {
                    parOpts.CancellationToken.ThrowIfCancellationRequested();
                    string filename = Path.GetFileName(currentFile);
                    using(Bitmap bitmap = new Bitmap(currentFile))
                    {
                        bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        bitmap.Save(Path.Combine(newDir,filename));
                    }
                    //this.Title = $"Processing {filename} on thread {Thread.CurrentThread.ManagedThreadId}";
                    this.Dispatcher.Invoke((Action)delegate
                    {
                        this.Title = $"Processing {filename} on thread {Thread.CurrentThread.ManagedThreadId}";
                    });
                });
                this.Dispatcher.Invoke((Action)delegate{
                    this.Title = "Done!";
                });
            }
            catch (Exception ex)
            {
                var random = new Random();
                this.Dispatcher.Invoke((Action)delegate{
                    this.Title = ex.Message + $"{random.Next()}";
                    cancelToken.Dispose();
                    cancelToken = new CancellationTokenSource();
                });
            }
  
        }
    }
}

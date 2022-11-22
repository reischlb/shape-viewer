using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ArchiBeadando
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri(@"magyarorszag.png", UriKind.Relative));
            myCanvas.Height = ib.ImageSource.Height;
            myCanvas.Width = ib.ImageSource.Width;
            
            myCanvas.Background = ib;
            myCanvas.InvalidateVisual();
            int a = 1;


        }
    }
}

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
using ShapeOperations;

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

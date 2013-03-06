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
using Microsoft.Surface.Presentation.Controls;
using System.Diagnostics;
using TabletopComputing.Managers;
using TabletopComputing.Views;

namespace TabletopComputing.Controls
{
    /// <summary>
    /// Interaction logic for BeatTag.xaml
    /// </summary>
    public partial class BeatTag : TagVisualization
    {

        private PaintingView _paintingView;
        int sample = -1;

        public BeatTag()
        {
            Loaded += new RoutedEventHandler(OnLoaded);
            Unloaded += new RoutedEventHandler(OnUnloaded);

            InitializeComponent();
        }

        void OnLoaded(object sender, RoutedEventArgs e)
        {
            // Add to registry
            TagVisualizationManager.Instance.AddTagviz(this);

            // Get PaintingView
            SurfaceWindow1 mainWindow = App.Current.MainWindow as SurfaceWindow1;
            Debug.Assert(mainWindow != null);
            _paintingView = mainWindow.ShellView.PaintingView;

            tbSampleName.Text = _paintingView.GetSampleFromID(this.VisualizedTag.Value);

            //_paintingView.addRemoveBeat("kick2.wav");
        }

        void OnUnloaded(object sender, RoutedEventArgs e)
        {
            // Remove from registry
            //_paintingView.addRemoveBeat("kick2.wav");
            TagVisualizationManager.Instance.RemoveTagviz(this);
        }
    }
}

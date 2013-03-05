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
using TabletopComputing.Controls;
using System.ComponentModel;
using Microsoft.Surface.Presentation.Input;
using Microsoft.Surface.Presentation.Controls;
using TabletopComputing.Managers;
using TabletopComputing.Properties;
using System.Windows.Ink;
using IrrKlang;
using System.Windows.Threading;
using System.Diagnostics;

namespace TabletopComputing.Views
{
    /// <summary>
    /// Interaction logic for PaintingView.xaml
    /// </summary>
    public partial class PaintingView : UserControl, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged interface

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region Life cycle

        //Constants
        const int NROFBEATS = 2;
        const int BPM = 128;

        String BEATSPATH = "Song/";

        int BEATCONSTANT;

        DispatcherTimer dispatcherTimer;
        ISoundEngine engine;
        List<string> soundList;
        Stopwatch stopwatch;


        public PaintingView()
        {
            InitializeComponent();

            engine = new ISoundEngine();
            soundList = new List<string>();
            BEATCONSTANT = 1000 * 60 * 8 * NROFBEATS / BPM;

            stopwatch = new Stopwatch();
            stopwatch.Start();

            //  DispatcherTimer setup
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Start();

        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

        }

        public void addRemoveBeat(string beatName)
        {
            beatName = BEATSPATH + beatName;
            if (!soundList.Contains(beatName))
            {
                soundList.Add(beatName);
            }
            else
            {
                soundList.Remove(beatName);
            }
        }


        bool first = true;

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {

            // Forcing the CommandManager to raise the RequerySuggested event
            var time = stopwatch.ElapsedMilliseconds;

            if (((time) >= (BEATCONSTANT)) || first)
            {

                if (first)
                    first = false;

                stopwatch.Reset();
                stopwatch.Start();

                foreach (string sound in soundList)
                {
                    engine.Play2D(sound);
                }
            }

            CommandManager.InvalidateRequerySuggested();
        }


        #endregion


    }
}

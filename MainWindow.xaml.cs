using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Diagnostics;
using IrrKlang;
using System.Windows.Threading;


namespace AudioTest
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
    /// 
	public partial class MainWindow : Window
	{

        //Colors
        String ACTIVECOLOR = "#FFBF00";
        String DISABLEDCOLOR = "#7373D9";
        String BEATCOLOR = "#FFDC73";
        String BACKGROUNDCOLOR = "#242424";
        String BEATGREY = "#343434";

        String BEATSPATH = "Song2/";
		
		Storyboard gameLoop;
        List<string> soundList;
        int BPM = 128;
        int NrOfBeats = 2;
        int BEATCONSTANT;
        Stopwatch stopwatch;     
        ISoundEngine engine;
        DispatcherTimer dispatcherTimer;
				
		public MainWindow()
		{
			this.InitializeComponent();

            // start up irrKlang
            engine = new ISoundEngine();

            soundList = new List<string>();

            stopwatch = new Stopwatch();
            stopwatch.Start();
            BEATCONSTANT = 1000 * 60 * 8 * NrOfBeats / BPM;


            // play a sound file
            //engine.Play2D("beat3.wav");
            addRemoveBeat(btnKick, "kick2.wav");

            //  DispatcherTimer setup
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            //dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
			// Insert code required on object creation below this point.
		}


		
        bool first = true;

        //  System.Windows.Threading.DispatcherTimer.Tick handler 
        // 
        //  Updates the current seconds display and calls 
        //  InvalidateRequerySuggested on the CommandManager to force  
        //  the Command to raise the CanExecuteChanged event. 
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            var time = stopwatch.ElapsedMilliseconds;

            //Color beat sections

            double p = (double)time / (double)BEATCONSTANT;

            if (p >= 0 && p < 0.25)
                colorBeatSection(1);
            else if (p >= 0.25 && p < 0.5)
                colorBeatSection(2);
            else if (p >= 0.5 && p < 0.75)
                colorBeatSection(3);
            else if (p >= 0.75 && p < 1)
                colorBeatSection(4);

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

            // Forcing the CommandManager to raise the RequerySuggested event
            CommandManager.InvalidateRequerySuggested();
        }

        private void colorBeatSection(int beat)
        {
            var bc = new BrushConverter();
            if (beat == 1)
            {
                recBeat1.Fill = (Brush)bc.ConvertFrom(BEATCOLOR);
                recBeat2.Fill = (Brush)bc.ConvertFrom(BACKGROUNDCOLOR);
                recBeat3.Fill = (Brush)bc.ConvertFrom(BACKGROUNDCOLOR);
                recBeat4.Fill = (Brush)bc.ConvertFrom(BACKGROUNDCOLOR);
            }
            else if (beat == 2)
            {
                recBeat1.Fill = (Brush)bc.ConvertFrom(BEATGREY);
                recBeat2.Fill = (Brush)bc.ConvertFrom(BEATCOLOR);
                recBeat3.Fill = (Brush)bc.ConvertFrom(BACKGROUNDCOLOR);
                recBeat4.Fill = (Brush)bc.ConvertFrom(BACKGROUNDCOLOR);
            }
            else if (beat == 3)
            {
                recBeat1.Fill = (Brush)bc.ConvertFrom(BEATGREY);
                recBeat2.Fill = (Brush)bc.ConvertFrom(BEATGREY);
                recBeat3.Fill = (Brush)bc.ConvertFrom(BEATCOLOR);
                recBeat4.Fill = (Brush)bc.ConvertFrom(BACKGROUNDCOLOR);
            }
            else if (beat == 4)
            {
                recBeat1.Fill = (Brush)bc.ConvertFrom(BEATGREY);
                recBeat2.Fill = (Brush)bc.ConvertFrom(BEATGREY);
                recBeat3.Fill = (Brush)bc.ConvertFrom(BEATGREY);
                recBeat4.Fill = (Brush)bc.ConvertFrom(BEATCOLOR);
            }
        }

        private void addRemoveBeat(object sender, string beatName)
        {
            var bc = new BrushConverter();
            var btn = sender as Button;
            beatName = BEATSPATH + beatName;
            if (!soundList.Contains(beatName))
            {
                soundList.Add(beatName);
                btn.Background = (Brush)bc.ConvertFrom(ACTIVECOLOR);
            }
            else
            {
                soundList.Remove(beatName);
                btn.Background = (Brush)bc.ConvertFrom(DISABLEDCOLOR);
            }
        }

        private void btnKickClick(object sender, System.Windows.RoutedEventArgs e)
        {
        	// TODO: Add event handler implementation here.
            addRemoveBeat(sender, "kick2.wav");
        }

        private void btnSnareClick(object sender, System.Windows.RoutedEventArgs e)
        {
        	// TODO: Add event handler implementation here.
            addRemoveBeat(sender, "snare2.wav");
        }

        private void btnHihatClick(object sender, System.Windows.RoutedEventArgs e)
        {
        	// TODO: Add event handler implementation here.
            addRemoveBeat(sender, "hihat_open2.wav");
        }

        private void btnPianoClick(object sender, System.Windows.RoutedEventArgs e)
        {
        	// TODO: Add event handler implementation here.
            addRemoveBeat(sender, "piano2.wav");
        }

        private void btnBassClick(object sender, System.Windows.RoutedEventArgs e)
        {
        	// TODO: Add event handler implementation here.
            addRemoveBeat(sender, "bass2.wav");
        }

        private void btnLeadClick(object sender, System.Windows.RoutedEventArgs e)
        {
        	// TODO: Add event handler implementation here.
            addRemoveBeat(sender, "lead2.wav");
        }

        private void btnHihatClosedClick(object sender, System.Windows.RoutedEventArgs e)
        {
        	// TODO: Add event handler implementation here.
            addRemoveBeat(sender, "hihat_closed2.wav");
        }

        private void btnWnoiseClick(object sender, System.Windows.RoutedEventArgs e)
        {
        	// TODO: Add event handler implementation here.
            addRemoveBeat(sender, "wnoise2.wav");
        }

        private void btnCrashClick(object sender, System.Windows.RoutedEventArgs e)
        {
        	// TODO: Add event handler implementation here.
            addRemoveBeat(sender, "crash2.wav");
        }
		
	}
}
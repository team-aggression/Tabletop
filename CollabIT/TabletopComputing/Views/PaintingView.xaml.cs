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
        Dictionary<long, string> beatTags;
        Stopwatch stopwatch;
        List<long> registeredTags;


        public PaintingView()
        {
            InitializeComponent();

            engine = new ISoundEngine();
            soundList = new List<string>();
            registeredTags = new List<long>();
            beatTags = new Dictionary<long, string>();
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

        public string GetSampleFromID(long id) {
            if (beatTags.ContainsKey(id))
                return beatTags[id];
            else return "NO sample";
        }

        public void addBeatTag(int id, string sample)
        {
            if(!beatTags.Keys.Contains(id)) {
                beatTags.Add(id, sample);
			}
            else {
                
            }
        }

        public void RegisterTag(long id)
        {
            if (!registeredTags.Contains(id))
                registeredTags.Add(id);
        }

        public void Unregistertag(long id)
        {
            if (registeredTags.Contains(id))
            {
                registeredTags.Remove(id);
            }
        }

        private void EnableDisableSample(string sample, bool enabled)
        {
            if (sample.Equals("kick2.wav"))
            {
                tagvisKick.IsEnabled = enabled;
            }
            else if (sample.Equals("lead2.wav"))
            {
                //.IsEnabled = enabled;
            }
            else if (sample.Equals("snare2.wav"))
            {
                tgSnare.IsEnabled = enabled;
            }
            else if (sample.Equals("piano2.wav"))
            {
                tgPiano.IsEnabled = enabled;
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

                foreach (long tagid in registeredTags)
                {
                    engine.Play2D("Song/" + beatTags[tagid]);
                }

            }

            CommandManager.InvalidateRequerySuggested();
        }

        private void TagEnter(object sender, Microsoft.Surface.Presentation.Controls.TagVisualizerEventArgs e)
        {
            
        	var id = e.TagVisualization.VisualizedTag.Value;
			var tagviz = sender as TagVisualizer;
            if (tagviz != null)
            {
                if (tagviz.Content.Equals("Kick"))
                {
                    AddBeatTag(id,"kick2");
                }
                else if (tagviz.Content.Equals("Piano"))
                {
                    AddBeatTag(id, "piano2");
                }
                else if (tagviz.Content.Equals("Snare"))
                {
                    AddBeatTag(id, "snare2");
                }
                else if (tagviz.Content.Equals("Open Hi-hat"))
                {
                    AddBeatTag(id, "hihat_open2");
                }
                else if (tagviz.Content.Equals("Closed Hi-hat"))
                {
                    AddBeatTag(id, "hihat_closed2");
                }
                else if (tagviz.Content.Equals("Wnoise"))
                {
                    AddBeatTag(id, "wnoise2");
                }
                else if (tagviz.Content.Equals("Lead"))
                {
                    AddBeatTag(id, "lead2");
                }
                tagviz.IsEnabled = false;
            }
			// TODO: Add event handler implementation here.
        }

        private void AddBeatTag(long id, string sample)
        {
            if (!beatTags.ContainsKey(id))
            {
                beatTags.Add(id, sample + ".wav");
            }
            else
            {
                EnableDisableSample(beatTags[id], true);
                beatTags[id] = sample + ".wav";
            }
            EnableDisableSample(beatTags[id], false);
        }

        private void TagAddedToLoop(object sender, Microsoft.Surface.Presentation.Controls.TagVisualizerEventArgs e)
        {
			var id = e.TagVisualization.VisualizedTag.Value;
            //if(beatTags.ContainsKey(id)) {
            //    string beat = beatTags[id];
            //    addRemoveBeat(beat);
            //}
            RegisterTag(id);
        	// TODO: Add event handler implementation here.
        }

        private void TagRemovedFromLoop(object sender, Microsoft.Surface.Presentation.Controls.TagVisualizerEventArgs e)
        {
        	// TODO: Add event handler implementation here.
			var id = e.TagVisualization.VisualizedTag.Value;
            Unregistertag(id);
        }


        #endregion


    }
}

﻿using System;
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
using System.Windows.Media.Animation;

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

        public int BEATCONSTANT;

        DispatcherTimer dispatcherTimer;
        ISoundEngine engine;
        List<string> soundList;
        Dictionary<long, string> beatTags;
        Stopwatch stopwatch;
        List<long> registeredTags;
        Storyboard sb;


        public PaintingView()
        {
            InitializeComponent();

            engine = new ISoundEngine();
            soundList = new List<string>();
            registeredTags = new List<long>();
            beatTags = new Dictionary<long, string>();
            BEATCONSTANT = 1000 * 60 * 8 * NROFBEATS / BPM;

            sb = this.FindResource("CenterGrow") as Storyboard;
            sb.Duration = new Duration(TimeSpan.FromMilliseconds(BEATCONSTANT / 16));
            
            Storyboard.SetTarget(sb, this.ellipse);

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
                if(beatTags.ContainsKey(id))
                    EnableDisableSample(beatTags[id],true);
                registeredTags.Remove(id);
            }
        }

        private void EnableDisableSample(string sample, bool enabled)
        {
            if (sample.Equals("kick2.wav"))
            {
                tagvisKick.IsEnabled = enabled;
                ColorSample(tagvisKick, enabled);
            }
            else if (sample.Equals("lead2.wav"))
            {
                tgLead.IsEnabled = enabled;
                ColorSample(tgLead, enabled);
            }
            else if (sample.Equals("snare2.wav"))
            {
                tgSnare.IsEnabled = enabled;
                ColorSample(tgSnare, enabled);
            }
            else if (sample.Equals("piano2.wav"))
            {
                tgPiano.IsEnabled = enabled;
                ColorSample(tgPiano, enabled);
            }
            else if (sample.Equals("hihat_open2.wav"))
            {
                tgOpenHH.IsEnabled = enabled;
                ColorSample(tgOpenHH, enabled);
            }
            else if (sample.Equals("hihat_closed2.wav"))
            {
                tgClosedHH.IsEnabled = enabled;
                ColorSample(tgClosedHH, enabled);
            }
            else if (sample.Equals("wnoise2.wav"))
            {
                tgWNoise.IsEnabled = enabled;
                ColorSample(tgWNoise, enabled);
            }
            else if (sample.Equals("crash2.wav"))
            {
                tgCrash.IsEnabled = enabled;
                ColorSample(tgCrash, enabled);
            }
            else if (sample.Equals("bass2.wav"))
            {
                tgBass.IsEnabled = enabled;
                ColorSample(tgBass, enabled);
            }

        }

        private void ColorSample(TagVisualizer tg, bool enabled)
        {
            var bc = new BrushConverter();
            if (enabled)
            {
                tg.Background = (Brush)bc.ConvertFrom("#EBEBEB");
                //tg.Foreground = (Brush)bc.ConvertFrom("#3E454C");
                
            }
            else
            {
                tg.Background = (Brush)bc.ConvertFrom("#2185C5");
                //tg.Foreground = (Brush)bc.ConvertFrom("#3E454C");
            }
        }

        bool first = true;

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {

            // Forcing the CommandManager to raise the RequerySuggested event
            var time = stopwatch.ElapsedMilliseconds;

            float p = (float)time / (float)BEATCONSTANT;

            if (p >= 0 && p < 0.25)
                tbBeatCounter.Text = "1";
            else if (p >= 0.25 && p < 0.5)
                tbBeatCounter.Text = "2";
            else if (p >= 0.50 && p < 0.75)
                tbBeatCounter.Text = "3";
            else if (p >= 0.75 && p < 1.0)
                tbBeatCounter.Text = "4";

            if (((time) >= (BEATCONSTANT)) || first)
            {

                if (first)
                {
                    first = false;
                    sb.Begin();
                }

                //Random random = new Random();
               // int randomNumber = random.Next(0, 16777215);

                //string hexValue = randomNumber.ToString("X");

                //var bc = new BrushConverter();
                //grad1.Color = (Color)ColorConverter.ConvertFromString("#" + hexValue);
                //grad2.Color = (Color)ColorConverter.ConvertFromString("#" + hexValue);

                stopwatch.Reset();
                stopwatch.Start();

                foreach (long tagid in registeredTags)
                {
                    if(beatTags.ContainsKey(tagid))
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
                else if (tagviz.Content.Equals("Open HH"))
                {
                    AddBeatTag(id, "hihat_open2");
                }
                else if (tagviz.Content.Equals("Closed HH"))
                {
                    AddBeatTag(id, "hihat_closed2");
                }
                else if (tagviz.Content.Equals("WNoise"))
                {
                    AddBeatTag(id, "wnoise2");
                }
                else if (tagviz.Content.Equals("Lead"))
                {
                    AddBeatTag(id, "lead2");
                }
                else if (tagviz.Content.Equals("Crash"))
                {
                    AddBeatTag(id, "crash2");
                }
                else if (tagviz.Content.Equals("Bass"))
                {
                    AddBeatTag(id, "bass2");
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

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
using System.Collections.ObjectModel;
using System.IO;
using TabletopComputing.Properties;
using System.Diagnostics;
using System.ComponentModel;

namespace TabletopComputing.Views
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : UserControl, INotifyPropertyChanged
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

        #region Types

        public enum ContentTypeType
        {
            MiscItems,
            PhotosFolder,
            Painting
        }

        #endregion

        #region CLR Properties

        #region ContentType

        private ContentTypeType m_ContentType = ContentTypeType.Painting;
        public ContentTypeType ContentType
        {
            get { return m_ContentType; }
            set
            {
                if (value != m_ContentType)
                {
                    m_ContentType = value;

                    OnPropertyChanged("ContentType");
                }
            }
        }

        #endregion

        #region OtherContentTypeText

        private string m_OtherContentTypeText;
        public string OtherContentTypeText
        {
            get { return m_OtherContentTypeText; }
            set
            {
                if (value != m_OtherContentTypeText)
                {
                    m_OtherContentTypeText = value;
                    OnPropertyChanged("OtherContentTypeText");
                }
            }
        }

        #endregion

        #endregion

        #region Life cycle

        public ShellView()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

        }

        #endregion


        #region Event driven

        private void OnSoundMediaElementMediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            Debug.WriteLine("Couldn't play sound. Error: " + e.ErrorException.ToString());
        }

        #endregion


        #region Event driven

        private void OnGoToMiscItemsButtonClick(object sender, RoutedEventArgs e)
        {
            ContentType = ContentTypeType.MiscItems;
        }

        private void OnGoToPhotosFolderButtonClick(object sender, RoutedEventArgs e)
        {
            ContentType = ContentTypeType.PhotosFolder;
        }

        private void OnGoToPaintingButtonClick(object sender, RoutedEventArgs e)
        {
            ContentType = ContentTypeType.Painting;
        }

        #endregion
    }
}

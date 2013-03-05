using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Surface.Presentation.Controls;

namespace CollaBeat.Managers
{
    public class TagVisualizationManager
    {
        #region CLR Properties

        #region Instance

        private static TagVisualizationManager _instance;
        public static TagVisualizationManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TagVisualizationManager();
                }

                return _instance;
            }
        }

        #endregion

        #endregion

        #region Fields

        private Dictionary<long, TagVisualization> _vizs = new Dictionary<long, TagVisualization>();

        #endregion

        #region Operations

        public void AddTagviz(TagVisualization tagviz)
        {
            long tagValue = tagviz.VisualizedTag.Value;
            _vizs.Remove(tagValue);
            _vizs.Add(tagValue, tagviz);
        }

        public void RemoveTagviz(TagVisualization tagviz)
        {
            long tagValue = tagviz.VisualizedTag.Value;
            _vizs.Remove(tagValue);
        }

        public void Clear()
        {
            _vizs.Clear();
        }

        public bool TryGetTagviz(long tagValue, out TagVisualization tagviz)
        {
            return _vizs.TryGetValue(tagValue, out tagviz);
        }

        #endregion
    }
}

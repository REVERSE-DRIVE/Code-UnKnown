#if UNITY_EDITOR
using System;
using UnityEngine;

namespace ButtonAttribute
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class InspectorButtonAttribute : PropertyAttribute
    {
        public string label;
        public bool isToolTipActive;
        public string toolTip;
        public int space;
        
        public InspectorButtonAttribute(string label = null, int space = 0, bool isToolTipActive = false, string tooltip = null)
        {
            this.label = label;
            this.isToolTipActive = isToolTipActive;
            this.toolTip = tooltip;
            this.space = space;
        }
    }
}
#endif
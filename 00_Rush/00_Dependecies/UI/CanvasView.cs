using Rush;
using Unity.VisualScripting;
using UnityEngine;

namespace Rush
{
    [System.Serializable]
    public class CanvasViewField
    {
        [SerializeField]
        private Canvas m_Canvas;

        public RenderMode GetRenderMode()
        {
            return m_Canvas.renderMode;
        }
    }
    public partial class CanvasView : View
    {
        [SerializeField]
        private CanvasViewField m_CanvasViewField;

        public RenderMode GetRenderMode()
        {
            return m_CanvasViewField.GetRenderMode();
        }
    }
}


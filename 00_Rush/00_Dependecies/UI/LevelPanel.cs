using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace Rush
{
    
    public partial class LevelPanel : PanelView
    {
        [SerializeField]
        private List<LevelView> m_LevelViews = new();

        protected override void Start()
        {
            base.Start();
            //GameSingleton.Instance.AddOnRefrech(InitInternal);

        }
        private void OnDestroy()
        {
            //GameSingleton.Instance.RemoveOnRefresh(InitInternal);
        }
        private LevelView GetLevelView(LevelDefinition defi)
        {
            LevelView match = m_LevelViews.Find(x => x.LevelDefinition == defi);
            return match;
        }
        public void Init(List<Level> levels)
        {
            InitInternal(levels);
        }
        private void InitInternal(List<Level> levels)
        {
            foreach (var level in levels)
            {
                GetLevelView(level.Definition).Init(level);
            }
        }
        protected override void ShowInternal(float overideDelay = 0)
        {
            base.ShowInternal(overideDelay);
            //GameSingleton.Instance.Refresh();
            InitInternal(GameSingleton.Instance.Levels);
        }
    }
}


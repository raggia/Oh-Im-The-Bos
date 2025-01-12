using System.Collections.Generic;
using UnityEngine;

namespace Rush
{
    public class OfficePanel : PanelView
    {
        [SerializeField]
        private StaffView m_StaffViewPrefab;
        [SerializeField]
        private Transform m_StaffSpawn;
        [SerializeField, ReadOnly]
        private List<StaffView> m_StaffViews = new();

        protected override void Start()
        {
            base.Start();
            Init(GameSingleton.Instance.GetStaffDefinitions());
        }
        private void Init(List<StaffDefinition> defis)
        {
            foreach (StaffDefinition def in defis)
            {
                ShowStaff(def);
            }
        }
        private StaffView GetStaffView(StaffDefinition defi)
        {
            StaffView match = m_StaffViews.Find(x => x.Defi == defi);
            return match;
        }

        private StaffView SpawnStaffView(StaffDefinition defi)
        {
            StaffView staffView = Instantiate(m_StaffViewPrefab, m_StaffSpawn, false);
            staffView.Init(defi);
            return staffView;
        }
        private void ShowStaff(StaffDefinition defi)
        {
/*            foreach (var staff in m_StaffViews)
            {
                StaffView view = GetStaffView(staff.Defi);
                m_StaffViews.Remove(staff);
                Destroy(view.gameObject);
            }*/
            m_StaffViews.Add(SpawnStaffView(defi));
            GetStaffView(defi).Show();
        }
    }
}


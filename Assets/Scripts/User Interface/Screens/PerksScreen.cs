using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    public sealed class PerksScreen : ScreenObserver
    {
        #region Editor fields
        [SerializeField] private Button _getPoints = null;
        [SerializeField] private Button _forgetAll = null;
        [SerializeField] private TextMeshProUGUI _availablePoints = null;
        [SerializeField] private List<SkillNode> _skillNodes = null;
        #endregion

        #region Properties
        public override UIScreen Screen => UIScreen.Perks;
        #endregion

        #region Overridden methods
        public override void Setup()
        {
            UICore.MVCApplication.PerksView.SetupNodes(_skillNodes);

            base.Setup();
        }

        public override void Activate()
        {
            UICore.MVCApplication.PerksView.UpdateNodes();

            _getPoints.onClick.AddListener(AddPoint);
            _forgetAll.onClick.AddListener(ForgetAll);

            UICore.MVCApplication.PerksView.OnUpdateSkillPoints += OnAddSkillPoint;            

            base.Activate();
        }

        public override void Deactivate()
        {
            _getPoints.onClick.RemoveListener(AddPoint);
            _forgetAll.onClick.RemoveListener(ForgetAll);

            UICore.MVCApplication.PerksView.OnUpdateSkillPoints -= OnAddSkillPoint;

            base.Deactivate();
        }
        #endregion

        #region Event handlers
        private void AddPoint()
        {
            UICore.MVCApplication.PerksView.AddOneSkillPoint();
        }

        private void ForgetAll()
        {
            UICore.MVCApplication.PerksView.RestoreAllSkillPoints();
            UICore.MVCApplication.PerksView.UpdateNodes();
        }

        private void OnAddSkillPoint(ushort points)
        {
            _availablePoints.text = points.ToString();
        }
        #endregion
    }
}
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    public sealed class PerksScreen : ScreenObserver
    {
        #region Editor fields
        [SerializeField] private Button _closeWindow = null;
        [SerializeField] private Button _forget = null;
        [SerializeField] private Button _forgetAll = null;
        [SerializeField] private Button _getPoints = null;
        [SerializeField] private Button _learn = null;
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
            _closeWindow.onClick.AddListener(OnClickCloseWindow);
            _forget.onClick.AddListener(OnClickForget);
            _forgetAll.onClick.AddListener(OnClickForgetAll);
            _getPoints.onClick.AddListener(OnClickAddPoint);
            _learn.onClick.AddListener(OnClickLearn);

            UICore.MVCApplication.PerksView.OnUpdateSkillPoints += OnAddSkillPoint;

            UICore.MVCApplication.PerksView.UpdateSkillPointsView();
            UICore.MVCApplication.PerksView.UpdateNodes();
            UICore.MVCApplication.PerksView.UpdateNodesPrice();
            UICore.MVCApplication.PerksView.ActivateNodes();

            base.Activate();
        }

        public override void Deactivate()
        {
            _closeWindow.onClick.RemoveListener(OnClickCloseWindow);
            _forget.onClick.RemoveListener(OnClickForget);
            _forgetAll.onClick.RemoveListener(OnClickForgetAll);
            _getPoints.onClick.RemoveListener(OnClickAddPoint);
            _learn.onClick.RemoveListener(OnClickLearn);

            UICore.MVCApplication.PerksView.OnUpdateSkillPoints -= OnAddSkillPoint;

            UICore.MVCApplication.PerksView.DeactivateNodes();

            base.Deactivate();
        }
        #endregion

        #region Event handlers
        private void OnClickAddPoint() => UICore.MVCApplication.PerksView.AddOneSkillPoint();

        private void OnClickForgetAll()
        {
            UICore.MVCApplication.PerksView.RestoreAllSkillPoints();
            UICore.MVCApplication.PerksView.UpdateNodes();
        }

        private void OnClickCloseWindow() => UICore.OpenScreen(UIScreen.Player);

        private void OnAddSkillPoint(ushort points) => _availablePoints.text = points.ToString();

        private void OnClickLearn() => UICore.MVCApplication.PerksView.LearnSkill();

        private void OnClickForget() => UICore.MVCApplication.PerksView.ForgetSkill();
        #endregion
    }
}
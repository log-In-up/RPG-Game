using GameData;
using System.Collections.Generic;
using UserInterface;

namespace MVC
{
    public sealed class PerksView
    {
        #region Fields
        private PerksController _perksController = null;
        private PerksModel _perksModel = null;
        private List<SkillNode> _skillNodes = null;
        #endregion

        #region Events
        public delegate void PerksPointsDelegate(ushort points);
        public event PerksPointsDelegate OnUpdateSkillPoints;
        #endregion

        public PerksView(PerksController perksController, PerksModel perksModel)
        {
            _perksController = perksController;
            _perksModel = perksModel;
        }

        #region Public methods
        internal void SetupNodes(List<SkillNode> skillNodes) => _skillNodes = skillNodes;

        internal void AddOneSkillPoint()
        {
            _perksController.AddOneSkillPoint();
            OnUpdateSkillPoints?.Invoke(_perksModel.SkillPoints);
        }

        internal void RestoreAllSkillPoints()
        {
            _perksController.RestoreAllSkillPoints();
            OnUpdateSkillPoints?.Invoke(_perksModel.SkillPoints);

            _perksController.ForgetAllSkills();
        }

        internal void UpdateNodes()
        {
            foreach (SkillNode skillNode in _skillNodes)
            {
                bool value = _perksModel.IsSkillUnlocked(skillNode.SkillType);

                skillNode.UpdateLockStatus(value);
            }
        }

        internal void UpdateNodesPrice()
        {
            foreach (SkillNode skillNode in _skillNodes)
            {
                SkillData skillData = _perksModel.SkillList.GetSkill(skillNode.SkillType);

                ushort price = skillData.UnlockCost;
                skillNode.UpdateSkillCost(price);
            }
        }

        internal void ActivateNodes()
        {
            foreach (SkillNode skillNode in _skillNodes)
            {
                skillNode.OnNodeSelection += OnNodeSelection;
            }
        }

        internal void DeactivateNodes()
        {
            foreach (SkillNode skillNode in _skillNodes)
            {
                skillNode.OnNodeSelection -= OnNodeSelection;
            }
        }

        internal void LearnSkill()
        {
            _perksController.LearnSkill();
            OnUpdateSkillPoints?.Invoke(_perksModel.SkillPoints);

            UpdateNodes();
        }

        internal void ForgetSkill()
        {
            _perksController.ForgetSkill();
            OnUpdateSkillPoints?.Invoke(_perksModel.SkillPoints);

            UpdateNodes();
        }

        internal void UpdateSkillPointsView() => OnUpdateSkillPoints?.Invoke(_perksModel.SkillPoints);
        #endregion

        #region Methods
        private void OnNodeSelection(SkillNode skillNode) => _perksController.SetSelectedNode(skillNode);
        #endregion
    }
}
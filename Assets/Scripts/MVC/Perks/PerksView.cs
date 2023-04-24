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
            foreach (SkillNode node in _skillNodes)
            {
                bool value = _perksModel.IsSkillUnlocked(node.SkillType);

                node.UpdateLockStatus(value);
            }
        }
        #endregion
    }
}
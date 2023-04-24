using SkillTree;

namespace MVC
{
    public sealed class PerksController
    {
        #region Fields
        private PerksModel _skillTreeModel;
        #endregion

        public PerksController(PerksModel skillTreeModel)
        {
            _skillTreeModel = skillTreeModel;
        }

        #region Public methods
        internal void UnlockSkill(SkillType skillType)
        {
            if (!_skillTreeModel.IsSkillUnlocked(skillType))
            {
                _skillTreeModel.UnlockSkill(skillType);
            }
        }

        internal void LockSkill(SkillType skillType)
        {
            if (_skillTreeModel.IsSkillUnlocked(skillType))
            {
                _skillTreeModel.LockSkill(skillType);
            }
        }

        internal void AddOneSkillPoint() => _skillTreeModel.AddOneSkillPoint();

        internal void RestoreAllSkillPoints() => _skillTreeModel.RestoreAllSkillPoints();

        internal void ForgetAllSkills()
        {
            _skillTreeModel.ClearSkills();
        }
        #endregion
    }
}
using SkillTree;
using System.Collections.Generic;

namespace MVC
{
    public sealed class PerksModel
    {
        #region Fields
        private ushort _skillPoints, _totalSkillPoints;
        private List<SkillType> _unlockedSkills = null;
        #endregion

        #region Events
        public delegate void PerksDelegate(SkillType skillType);
        public delegate void ClearPerksDelegate();

        public event PerksDelegate OnUnlockSkill, OnLockSkill;
        public event ClearPerksDelegate OnLockAllSkills;
        #endregion

        #region Properties
        public ushort SkillPoints { get => _skillPoints; set => _skillPoints = value; }
        #endregion

        public PerksModel()
        {
            _unlockedSkills = new List<SkillType> { SkillType.Base };

            _skillPoints = _totalSkillPoints = 0;
        }

        #region Public methods
        public void AddOneSkillPoint()
        {
            _skillPoints++;
            _totalSkillPoints++;
        }

        internal void UnlockSkill(SkillType skillType)
        {
            _unlockedSkills.Add(skillType);

            OnUnlockSkill(skillType);
        }

        internal void LockSkill(SkillType skillType)
        {
            _unlockedSkills.Remove(skillType);

            OnLockSkill(skillType);
        }

        internal void ClearSkills()
        {
            _unlockedSkills.Clear();
            _unlockedSkills.Add(SkillType.Base);

            OnLockAllSkills?.Invoke();
        }

        internal void RestoreAllSkillPoints()
        {
            _skillPoints = _totalSkillPoints;
        }

        internal bool IsSkillUnlocked(SkillType skillType) => _unlockedSkills.Contains(skillType);
        #endregion
    }
}
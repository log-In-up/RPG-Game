using GameData;
using SkillTree;
using System.Collections.Generic;
using System.Linq;

namespace MVC
{
    public sealed class PerksModel
    {
        #region Fields
        private SkillList _skills;

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
        public ushort SkillPoints => _skillPoints;
        public SkillList SkillList => _skills;
        public List<SkillType> UnlockedSkills => _unlockedSkills;
        #endregion

        public PerksModel(SkillList skillList)
        {
            _skills = skillList;

            _unlockedSkills = new List<SkillType> { SkillType.Base };

            _skillPoints = _totalSkillPoints = 0;
        }

        #region Public methods
        internal void AddOneSkillPoint()
        {
            _skillPoints++;
            _totalSkillPoints++;
        }

        internal bool SkillPointsCanWithdrawn(ushort value)
        {
            int totalSkillPoints = _skillPoints;

            return (totalSkillPoints -= value) >= 0;
        }

        internal void WithdrawSkillPoints(ushort value) => _skillPoints -= value;

        internal void ReturnSkillPoints(ushort value) => _skillPoints += value;

        internal void UnlockSkill(SkillType skillType)
        {
            _unlockedSkills.Add(skillType);

            OnUnlockSkill?.Invoke(skillType);
        }

        internal void LockSkill(SkillType skillType)
        {
            _unlockedSkills.Remove(skillType);

            OnLockSkill?.Invoke(skillType);
        }

        internal void ClearSkills()
        {
            _unlockedSkills.Clear();
            _unlockedSkills.Add(SkillType.Base);

            OnLockAllSkills?.Invoke();
        }

        internal void RestoreAllSkillPoints() => _skillPoints = _totalSkillPoints;

        internal bool IsSkillUnlocked(SkillType skillType) => _unlockedSkills.Contains(skillType);

        internal bool SkillCanBeAdded(List<SkillType> skills) => _unlockedSkills.Any(unlockedSkill => skills.Any(skill => skill == unlockedSkill));
        #endregion
    }
}
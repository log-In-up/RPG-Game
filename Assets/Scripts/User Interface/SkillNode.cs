using SkillTree;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UserInterface
{
    [DisallowMultipleComponent]
    public sealed class SkillNode : MonoBehaviour, IPointerClickHandler
    {
        #region Editor fields
        [SerializeField] private Color _activated, _deactivated;
        [SerializeField] private TextMeshProUGUI _requiredSkillpoints = null;
        [SerializeField] private SkillType _skillType;
        [SerializeField] private Image _image = null;
        #endregion

        #region Properties
        public SkillType SkillType => _skillType;
        #endregion

        #region Events
        public delegate void NodeClickDelegate(SkillNode skillNode);
        public event NodeClickDelegate OnNodeSelection;
        #endregion

        #region Public methods
        internal void UpdateLockStatus(bool value)
        {
            _image.color = value ? _activated : _deactivated;
            _requiredSkillpoints.enabled = !value;
        }

        internal void UpdateSkillCost(ushort value) => _requiredSkillpoints.text = value.ToString();
        #endregion

        #region Interface realization
        public void OnPointerClick(PointerEventData eventData) => OnNodeSelection?.Invoke(this);
        #endregion
    }
}
using UnityEngine;

namespace GameData
{
    [CreateAssetMenu(fileName = "Player Data", menuName = "Game Data/Player/Data", order = 1)]
    public sealed class PlayerData : ScriptableObject
    {
        #region Editor fields
        [field: SerializeField, Min(0.0f)] public float MovementSpeed { get; private set; }
        [field: SerializeField, Min(0.0f)] public float SmoothTime { get; private set; }
        [field: SerializeField, Min(0.0f)] public float MaxHealth { get; private set; }
        [field: SerializeField, Min(0.0f)] public float MaxMana { get; private set; }
        #endregion
    }
}
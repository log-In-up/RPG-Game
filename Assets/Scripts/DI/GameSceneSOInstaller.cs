using GameData;
using UnityEngine;
using Zenject;

namespace DI
{
    [CreateAssetMenu(fileName = "GameScene SOInstaller", menuName = "Installers/GameScene SOInstaller")]
    public sealed class GameSceneSOInstaller : ScriptableObjectInstaller<GameSceneSOInstaller>
    {
        #region Editor fields
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private SkillList _skills;
        #endregion

        public override void InstallBindings()
        {
            BindFromInstanceObject(_playerData);
            BindFromInstanceObject(_skills);
        }

        #region Bind Methods
        private void BindFromInstanceObject<T>(T instance)
        {
            Container.Bind<T>().FromInstance(instance).AsSingle().NonLazy();
        }
        #endregion
    }
}
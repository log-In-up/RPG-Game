using MVC;
using Player;
using UnityEngine;
using Zenject;

namespace DI
{
    public sealed class GameSceneInstaller : MonoInstaller
    {
        #region Editor fields
        [SerializeField] private FixedJoystick _dynamicJoystick = null;
        [SerializeField] private PlayerBehaviour _playerBehaviour = null;
        [SerializeField] private MVCApplication _mvcApplication = null;
        #endregion

        #region Overridden methods
        public override void InstallBindings()
        {
            BindFromInstanceObject(_dynamicJoystick);
            BindFromInstanceObject(_playerBehaviour);
            BindFromInstanceObject(_mvcApplication);
        }
        #endregion

        #region Methods
        private void BindFromInstanceObject<T>(T instance)
        {
            Container.Bind<T>().FromInstance(instance).AsSingle().NonLazy();
        }
        #endregion
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserInterface
{
    public sealed class PerksScreen : ScreenObserver
    {
        #region Properties
        public override UIScreen Screen => UIScreen.Perks;
        #endregion

        #region Overridden methods
        public override void Activate()
        {


            base.Activate();
        }

        public override void Deactivate()
        {


            base.Deactivate();
        }
        #endregion
    }
}
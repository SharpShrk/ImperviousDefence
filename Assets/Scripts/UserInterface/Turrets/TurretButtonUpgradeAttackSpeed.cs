using UnityEngine.Events;

namespace TurretsUI
{
    public class TurretButtonUpgradeAttackSpeed : TurretButtonUpgrade
    {
        public event UnityAction ButtonUpgradeAttackSpeedPressed;

        protected override void OnButtonClick()
        {
            ButtonUpgradeAttackSpeedPressed?.Invoke();
        }
    }
}
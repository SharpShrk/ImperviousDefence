using UnityEngine.Events;

namespace TurretsUI
{
    public class TurretButtonUpgradeDamage : TurretButtonUpgrade
    {
        public event UnityAction ButtonUpgradeDamagePressed;

        protected override void OnButtonClick()
        {
            ButtonUpgradeDamagePressed?.Invoke();
        }
    }
}
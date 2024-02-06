using UnityEngine.Events;

namespace UserInterface
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
namespace TurretScripts
{
    public class TurretDamage : TurretStats
    {
        private Turret _turret;

        private void OnEnable()
        {
            _turret = GetComponent<Turret>();
        }

        public override void Upgrade()
        {
            Value += UpdateValue;
            Level++;

            _turret.SetUpgradeDamage(Value);
        }
    }
}
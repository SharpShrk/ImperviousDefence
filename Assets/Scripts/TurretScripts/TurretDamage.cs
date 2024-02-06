namespace TurretScripts
{
    public class TurretDamage : TurretStats
    {
        private Turret _turret;

        public bool IsMaxLevel { get; private set; }

        private void OnEnable()
        {
            IsMaxLevel = false;
            _turret = GetComponent<Turret>();
        }

        public override void Upgrade()
        {
            Value += UpdateValue;
            Level++;

            _turret.SetUpgradeDamage(Value);

            if (Level == MaxLevel)
            {
                IsMaxLevel = true;
            }
        }
    }
}
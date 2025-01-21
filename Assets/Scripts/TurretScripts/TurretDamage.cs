namespace TurretScripts
{
    public class TurretDamage : TurretStats
    {
        public override void Upgrade()
        {
            Value += UpdateValue;
            Level++;

            Turret.SetUpgradeDamage(Value);
        }
    }
}
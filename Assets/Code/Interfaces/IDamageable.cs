/// <summary>
/// Interface naudoti objektams kurios galima zaloti
/// </summary>
public interface IDamageable
{
    void Damage(DamagePack dPack);
    void Death();
}
public class DamagePack
{
    public float damageAmount;
}

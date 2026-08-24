public interface IAttackInputSource
{
    bool AttackPressed { get; }

    void ConsumeAttack();
}
public enum PlayerStates
{
    Idle,
    Walk,
    Melee
}

public enum EnemyStates
{
    Idle,
    Patrol,
    Follow,
    Attack,
    Chase,
    Evade,
    Die
}

public class LittleKinghtAnim
{
    public const string  Velocity = "Velocity";
    public const string  Moving = "Moving";
}
public class EnemyAnimationParameters
{
    public const string  Velocity = "Velocity"; // float
    public const string  Proximity = "Proximity"; // float
    public const string  InView = "InView"; // bool
    
}
    


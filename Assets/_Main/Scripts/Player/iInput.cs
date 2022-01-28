public interface iInput
{    
    float GetH { get; }
    float GetV { get; }
    bool IsMoving();
    public bool IsAttackMelee();
    void UpdateInputs();
}
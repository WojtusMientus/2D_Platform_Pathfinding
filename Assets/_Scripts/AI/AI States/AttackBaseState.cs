
public class AttackState : AIBaseState
{
    
    #region METHODS
    
    public override void EnterState()
    {
        controller.Attack();
    }
    
    #endregion
    
}


public class MoveState : AIBaseState
{
    
    #region METHODS
    
    public override void EnterState()
    {
        controller.SetupMove();
    }
    
    public override void UpdateState()
    {
        controller.Move();
    }
    
    #endregion
    
}

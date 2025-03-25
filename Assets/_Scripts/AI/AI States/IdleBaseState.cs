
public class IdleState: AIBaseState
{
    
    #region METHODS
    
    public override void EnterState()
    {
        controller.SetupIdle();
    }

    public override void UpdateState()
    {   
        controller.Idle();
    }   
    
    #endregion
    
}

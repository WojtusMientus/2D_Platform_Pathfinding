
public class ChaseState : AIBaseState
{
    
    #region METHODS
    
    public override void EnterState()
    {
        controller.SetUpChase();
    }

    public override void UpdateState()
    {
        controller.Chase();
    }
    
    #endregion
    
}

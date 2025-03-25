
public class AIBaseState
{
    
    #region VARIABLES
    
    protected BaseEnemyController controller;
    
    #endregion
    
    #region METHODS
    
    public void InitializeState(BaseEnemyController baseEnemyController)
    {
        controller = baseEnemyController;
    }

    public virtual void EnterState() { }

    public virtual void UpdateState() { }
    
    #endregion
    
}


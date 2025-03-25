
public class BaseEnemyController : AIStateMachine
{
    
    #region METHODS
    
    public virtual void Move() { }
    
    public virtual void Attack() { }

    public virtual void Chase() { }
    
    public virtual void Idle() { }

    
    
    public virtual void SetupIdle() { }
    
    public virtual void SetupMove() { }

    public virtual void SetUpChase() { }
    
    #endregion
    
}
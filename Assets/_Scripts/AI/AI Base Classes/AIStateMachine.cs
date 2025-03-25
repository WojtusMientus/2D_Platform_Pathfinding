using UnityEngine;

public class AIStateMachine : MonoBehaviour
{
    
    #region VARIABLES
    
    protected AIBaseState currentState;
    
    #endregion
    
    #region METHODS
    
    protected void SwitchState(AIBaseState newBaseState)
    {
        currentState = newBaseState;
        currentState.EnterState();
    }

    protected virtual void InitializeStates() { }
    
    #endregion
    
}


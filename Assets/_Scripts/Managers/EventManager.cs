using UnityEngine.Events;

public static class EventManager
{
    public static event UnityAction<int> OnPlayerPlatformHit;
    
    #region METHODS
    
    public static void RaiseOnPlayerPlatformHit(int playerID)
    {
        OnPlayerPlatformHit?.Invoke(playerID);
    }
    
    #endregion
    
}

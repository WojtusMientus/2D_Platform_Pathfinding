using UnityEditor;
using UnityEngine;

public static class Globals
{
    
    #region VARIABLES

    public static readonly string PLATFORM_TAG = "Platform";
    public static readonly string PLAYER_TAG = "Player";
    
    #endregion
    
    #region METHODS
    
# if UNITY_EDITOR
    public static void SetColor(Color color)
    {
        Gizmos.color = color;
        Handles.color = color;
    }
#endif
    
    #endregion
    
}
using UnityEngine;

public class DebugDrawer : MonoBehaviour
{
    
    #region VARIABLES
    
    [Space(10)]
    [Header("Colors and Size")]
    
    public Color drawingColor = Color.yellow;
    
    [Min(0f)]
    public float nodeRadius = 0.025f;
    public float connectionLineThickness = 1f;

    [Space(10f)] public bool drawDebug;
    
    #endregion
    
}

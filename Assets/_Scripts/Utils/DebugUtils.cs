#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

public static class DebugUtils
{
    #region VARIABLES
    
    private static readonly float arrowHeadLength = 0.25f;
    private static readonly float arrowHeadAngle = 20.0f;
    
    #endregion
    
    #region METHODS
    
    public static void DrawArrow(Vector3 startPosition, Vector3 endPosition, DebugDrawer debugDrawer)
    {
        Vector3 directionToArrowEndPosition = (endPosition - startPosition).normalized;
            
        Vector3 arrowStartPosition = startPosition + directionToArrowEndPosition * debugDrawer.nodeRadius;
        Vector3 arrowEndPosition = endPosition - directionToArrowEndPosition * debugDrawer.nodeRadius;
        
        Globals.SetColor(debugDrawer.drawingColor);
        
        Handles.DrawLine(arrowStartPosition, arrowEndPosition, debugDrawer.connectionLineThickness);
        Vector3 direction = (arrowStartPosition - arrowEndPosition).normalized;
        
        Vector3 relativeRight = Quaternion.AngleAxis(arrowHeadAngle, Vector3.forward) * direction;
        Handles.DrawLine(arrowEndPosition, arrowEndPosition + relativeRight * arrowHeadLength, debugDrawer.connectionLineThickness);
        
        Vector3 relativeLeft = Quaternion.AngleAxis(-arrowHeadAngle, Vector3.forward) * direction;
        Handles.DrawLine(arrowEndPosition, arrowEndPosition + relativeLeft * arrowHeadLength, debugDrawer.connectionLineThickness);
    }
    
    #endregion
    
}

#endif
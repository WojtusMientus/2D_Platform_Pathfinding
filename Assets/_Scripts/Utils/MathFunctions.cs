using UnityEngine;

public static class MathFunctions
{
    
    #region VARIABLES
    
    public const float G = 9.81f * 1.5f;
    
    private static readonly float jumpUpHeightPeak = .33f;
    private static readonly float jumpDownHeightPeak = .5f;

    #endregion
    
    #region NOTES_FOR_CALCULATIONS
    
    // v_y = sqrt(2 * g * (h_peak - start_y))
    // T up = v_y / g
    // T down = sqrt( (2 * (h_peak - y_2))/ g)
    // Final T = T up + t down
    // v_x = (x_2 - x_1) / T
    
    #endregion
    
    #region METHODS
    
    public static Vector3 CalculateProjectileVelocityUp(Vector3 startingPosition, Vector3 endingPosition)
    {
        float initialYVelocity = CalculateInitialVerticalVelocity(startingPosition.y, endingPosition.y, jumpUpHeightPeak);
        
        float timeDurationUp = CalculateTimeUp(initialYVelocity);
        float timeDurationDown = CalculateTimeDown(endingPosition.y, endingPosition.y, jumpUpHeightPeak);
        
        float combinedTimeDuration = timeDurationUp + timeDurationDown;
        
        float initialXVelocity = CalculateInitialHorizontalVelocity(startingPosition.x, endingPosition.x, combinedTimeDuration);

        return new Vector3(initialXVelocity, initialYVelocity, 0f);
    }

    public static Vector3 CalculateProjectileVelocityDown(Vector3 startingPosition, Vector3 endingPosition)
    {
        float initialYVelocity = CalculateInitialVerticalVelocity(startingPosition.y, startingPosition.y, jumpDownHeightPeak);
        
        float timeDurationUp = CalculateTimeUp(initialYVelocity);
        float timeDurationDown = CalculateTimeDown(endingPosition.y, startingPosition.y, jumpDownHeightPeak);
        
        float combinedTimeDuration = timeDurationUp + timeDurationDown;
        
        float initialXVelocity = CalculateInitialHorizontalVelocity(startingPosition.x, endingPosition.x, combinedTimeDuration);

        return new Vector3(initialXVelocity, initialYVelocity, 0f);
    }

    private static float CalculateInitialVerticalVelocity(float startingYPoint, float maxHeightPoint, float peakHeight)
    {
        return Mathf.Sqrt(2f * G * ((maxHeightPoint + peakHeight) - startingYPoint));
    }

    private static float CalculateTimeUp(float initialVerticalVelocity)
    {
        return initialVerticalVelocity / G;
    }

    private static float CalculateTimeDown(float endingYPoint, float maxHeightPoint, float peakHeight)
    {
        return Mathf.Sqrt((2f * ((maxHeightPoint + peakHeight) - endingYPoint)) / G);
    }

    private static float CalculateInitialHorizontalVelocity(float startingXPoint, float endingXPoint, float jumpTime)
    {
        return (endingXPoint - startingXPoint) / jumpTime;
    }

    public static float Normalize(this float value)
    {
        return value / Mathf.Abs(value);
    }
    
    #endregion
    
}

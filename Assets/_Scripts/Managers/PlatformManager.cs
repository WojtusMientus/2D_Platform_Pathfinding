using System.Linq;
using UnityEngine;

[DefaultExecutionOrder(2)]
public class PlatformManager : MonoBehaviour
{
    
    #region VARIABLES
    
    public Platform[] platforms;

    #endregion
    
    private void Awake()
    {
        platforms = GameObject.FindGameObjectsWithTag(Globals.PLATFORM_TAG).Where(obj => obj.GetComponent<Platform>() != null).Select(obj => obj.GetComponent<Platform>()).ToArray();

        SetUpPlatforms();
        
        EnemyManager.Instance.GetStartingPlayerPlatform();
    }

    
    #region METHODS
    
    private void SetUpPlatforms()
    {
        for (int i = 0; i < platforms.Length; i++)
            platforms[i].SetUpPlatform(i);
        
        foreach (Platform platform in platforms)
            platform.RecalculateAllNodeConnections();
    }
    
    #endregion
    
}
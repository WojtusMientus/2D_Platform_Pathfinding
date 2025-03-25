using UnityEngine;

[DefaultExecutionOrder(-10)]
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    
    #region VARIABLES
    
    public GameObject player;
    public int currentPlayerPlatformID;

    #endregion
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        
        player = GameObject.FindGameObjectWithTag(Globals.PLAYER_TAG);
    }
    
    private void OnEnable()
    {
        EventManager.OnPlayerPlatformHit += SetPlayerPlatformID;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerPlatformHit -= SetPlayerPlatformID;
    }
    
    
    #region METHODS
    
    public void GetStartingPlayerPlatform()
    {
        if (Physics.Raycast(player.transform.position, Vector3.down, out RaycastHit hit))
            if (hit.collider.gameObject.CompareTag(Globals.PLATFORM_TAG))
                SetPlayerPlatformID(hit.collider.gameObject.GetComponent<Platform>().PlatformID);
    }
    
    private void SetPlayerPlatformID(int playerPlatformID)
    {
        currentPlayerPlatformID = playerPlatformID;
    }
    
    #endregion
    
}

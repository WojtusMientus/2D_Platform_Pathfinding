using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[DefaultExecutionOrder(10)]
public class FindPathTest : MonoBehaviour
{
    [SerializeField] private GameObject startPoint;
    [SerializeField] private GameObject endPoint;
    [SerializeField] private DebugDrawer nodeDrawer;
    [SerializeField] private DebugDrawer helperDrawer;
    [SerializeField] private PlatformManager platformManager;

    private int endPlatformID;
    private int startPlatformID;
    private List<Node> calculatedPath;

    private bool canDraw;
    
    private void Awake()
    {
        calculatedPath = new List<Node>();
    }

    private void Update()
    {
        if (Time.realtimeSinceStartup > 1)
             canDraw = true;
    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(endPoint.transform.position, Vector3.down, out RaycastHit hit, 1000f))
            endPlatformID = hit.collider.gameObject.GetComponent<Platform>().PlatformID;

        if (Physics.Raycast(startPoint.transform.position, Vector3.down, out RaycastHit hit2, 1000f))
            startPlatformID = hit2.collider.gameObject.GetComponent<Platform>().PlatformID;
        
        _ = FindPath();
    }

    private async Task FindPath()
    {
        calculatedPath = await Pathfinding.Instance.FindPathAsync(startPoint.transform.position, endPlatformID, endPoint.transform.position);
    }
    
# if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (canDraw && calculatedPath != null && calculatedPath.Count != 0 )
        {
            if (Event.current.type == EventType.Repaint)
            {
                float yStartPosition = platformManager.platforms[startPlatformID].transform.position.y + 0.3f + .25f;
            
                Vector3 newStartPosition = new Vector3(startPoint.transform.position.x, yStartPosition, startPoint.transform.position.z);
            
                DebugUtils.DrawArrow(startPoint.transform.position, newStartPosition, helperDrawer);
                DebugUtils.DrawArrow(newStartPosition, calculatedPath[0].Position, helperDrawer);
            
                for (int i = 1; i < calculatedPath.Count; i++)
                    DebugUtils.DrawArrow(calculatedPath[i - 1].Position, calculatedPath[i].Position, nodeDrawer);
            
                float yEndPosition = platformManager.platforms[endPlatformID].transform.position.y + 0.3f + .25f;
            
                Vector3 newEndPosition = new Vector3(endPoint.transform.position.x, yEndPosition, endPoint.transform.position.z);
            
                DebugUtils.DrawArrow(calculatedPath[calculatedPath.Count - 1].Position, newEndPosition, helperDrawer);
                DebugUtils.DrawArrow(newEndPosition, endPoint.transform.position, helperDrawer);
            }
        }
    }
#endif
}
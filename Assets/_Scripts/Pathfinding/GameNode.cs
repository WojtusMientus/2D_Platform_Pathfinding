using System;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(0)]
[Serializable]
public class GameNode : MonoBehaviour
{
    
    #region PROPERTIES
    [HideInInspector] public List<NodeConnection> Connections => connections;

    #endregion
    
    
    #region VARIABLES
    
    public Node node;
    
    [SerializeField] private DebugDrawer nodeDebugger;

    [SerializeField] private List<NodeConnection> connections = new List<NodeConnection>();
    
    public int ParentPlatformID;
    
    #endregion
    
    private void Awake()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1000f))
            if (hit.collider.TryGetComponent(out Platform hitPlatform))
                hitPlatform.AddNode(this);
    }
    
    
    #region METHODS
    
    public void SetUpInternalNodePosition()
    {
        node = new Node(transform.position, ParentPlatformID);
    }
    
    public void AddConnection(NodeConnection connection)
    {
        connections.Add(connection);
    }

    public void RecalculateConnections()
    {
        foreach (NodeConnection connection in connections)
        {
            connection.CalculateStartingRawConnectionLength(this);
            connection.CalculateStartingWeightMultiplier(1f);
        }
    }
    
    #endregion
    
# if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (nodeDebugger == null || !nodeDebugger.drawDebug)
            return;
        
        foreach (NodeConnection connection in connections)
        {
            if (connection.GameNode == null)
                continue;
            
            DebugUtils.DrawArrow(transform.position, connection.GameNode.transform.position, nodeDebugger);
        }
        Gizmos.color = new Color(255, 255, 0, 0.5f);
        Gizmos.DrawSphere(transform.position, nodeDebugger.nodeRadius);
    }
    
#endif

}

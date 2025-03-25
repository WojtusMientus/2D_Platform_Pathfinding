using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class Platform : MonoBehaviour
{
    
    #region PROPERTIES
    
    public int PlatformID { get; private set; }
    public List<GameNode> PlatformNodes = new List<GameNode>();
    
    #endregion
    
    #region VARIABLES
    
    [Space(10)]
    [SerializeField] private float nodeHeight = .5f;
    
    [Space(10)]
    [SerializeField] private bool snapLeftNodeToEdge;
    [SerializeField] private bool snapRightNodeToEdge;

    private readonly float distanceFromTheEdge = 0.15f;
    
    #endregion
    
    private void Awake()
    {
        LevelNodes();
    }
    
    #region METHODS
    
    private void LevelNodes()
    {
        foreach (GameNode gameNode in PlatformNodes)
        {
            float newYPosition = transform.position.y + (transform.localScale.y / 2) + nodeHeight;
            gameNode.transform.position = new Vector3(gameNode.transform.position.x, newYPosition, gameNode.transform.position.z);
        }
    }
    public void AddNode(GameNode node)
    {
        PlatformNodes.Add(node);
    }

    public void SetUpPlatform(int id)
    {
        PlatformID = id;
        SortPlatformNodes();
        SnapToPlatformEdges();
        ConnectAllNodesOnTheSamePlatform();
        SetAllChildrenNodeParentPlatformIDAndInternalPosition();
    }
    
    private void SortPlatformNodes()
    {
        PlatformNodes.Sort((x,y) => x.transform.position.x.CompareTo(y.transform.position.x));
    }

    private void SnapToPlatformEdges()
    {
        if (PlatformNodes.Count < 1)
            return;
        
        if (transform.localScale.x > transform.localScale.z)
            SnapToPlatformEdges(transform.localScale.x);
        else
            SnapToPlatformEdges(transform.localScale.z);
    }
    
    private void SnapToPlatformEdges(float localScaleValue)
    {
        if (snapLeftNodeToEdge)
        {
            float newXPosition = transform.position.x - (localScaleValue / 2) + distanceFromTheEdge;
            PlatformNodes[0].transform.position = new Vector3(newXPosition, PlatformNodes[0].transform.position.y, PlatformNodes[0].transform.position.z);
        }
        
        if (snapRightNodeToEdge)
        {
            float newXPosition = transform.position.x + (localScaleValue / 2) - distanceFromTheEdge;
            PlatformNodes[^1].transform.position = new Vector3(newXPosition, PlatformNodes[^1].transform.position.y, PlatformNodes[^1].transform.position.z);
        }
    }
    
    private void ConnectAllNodesOnTheSamePlatform()
    {
        for (int i = 0; i < PlatformNodes.Count - 1; i++)
        {
            PlatformNodes[i].AddConnection(new NodeConnection(PlatformNodes[i + 1]));
            PlatformNodes[i + 1].AddConnection(new NodeConnection(PlatformNodes[i]));
        }
    }
    
    public void RecalculateAllNodeConnections()
    {
        foreach (GameNode gameNode in PlatformNodes)
            gameNode.RecalculateConnections();
    }
    
    private void SetAllChildrenNodeParentPlatformIDAndInternalPosition()
    {
        foreach (GameNode gameNode in PlatformNodes)
        {
            gameNode.ParentPlatformID = PlatformID;
            gameNode.SetUpInternalNodePosition();
        }
    }
    
    #endregion
    
}
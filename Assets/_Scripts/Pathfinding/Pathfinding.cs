using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{

    public static Pathfinding Instance;

    #region VARIABLES
    
    private Heap openSet;
    private HashSet<Node> closedSet;
    
    private readonly float heuristicsMultiplier = 1f;
    private readonly int heapSize = 100;

    private Node startingNode;
    private Vector3 startPathfindingPosition;
    private Vector3 endPathfindingPosition;
    
    #endregion
    
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        
        openSet = new Heap(heapSize);
        closedSet = new HashSet<Node>();
    }
    
    
    #region METHODS
    
    public async Task<List<Node>> FindPathAsync(Vector3 startPosition, int endPlatformID, Vector3 endPosition)
    {
        ClearHelperDataStructures();
        
        startPathfindingPosition = startPosition;
        endPathfindingPosition = endPosition;
        startingNode = new Node(startPosition);
        
        closedSet.Add(startingNode);

        InitializeStartingPlatform();

        return await CalculatePathToEndPlatform(endPlatformID);
    }

    private async Task<List<Node>> CalculatePathToEndPlatform(int endPlatformID)
    {
        while (openSet.Count > 0)
        {
            GameNode currentNode = openSet.RemoveFirst();
            closedSet.Add(currentNode.node);
            
            if (currentNode.ParentPlatformID == endPlatformID)
                return RetraceFoundPath(currentNode.node);
            
            CheckNeighbourNodes(currentNode);
            
            await Task.Yield();
        }

        return null;
    }

    private void ClearHelperDataStructures()
    {
        openSet.ClearHeap();
        closedSet.Clear();
    }

    private void InitializeStartingPlatform()
    {
        Platform startPlatform = GetStartingPlatform(startPathfindingPosition);
        
        if (startPlatform == null)
            return;
        
        foreach (GameNode gameNode in startPlatform!.PlatformNodes)
        {
            float distanceToStartingNode = CalculateStartingDistance(gameNode.transform.position);
            float heuristics = CalculateStartingHeuristics();
            
            gameNode.node.DistanceToPreviousClosestNode = distanceToStartingNode;
            gameNode.node.HeuristicCost = heuristics;
            gameNode.node.ParentNode = startingNode;
            
            openSet.Add(gameNode);
        }
    }

    private Platform GetStartingPlatform(Vector3 startPosition)
    {
        if (Physics.Raycast(startPosition, Vector3.down, out RaycastHit hit, Mathf.Infinity))
            return hit.collider.gameObject.GetComponentInParent<Platform>();
        
        return null;
    }
    
    private float CalculateStartingDistance(Vector3 endPosition)
    {
        return Vector3.Distance(startPathfindingPosition, endPosition);
    }
    
    private float CalculateStartingHeuristics()
    {
        return Vector3.Distance(startPathfindingPosition, endPathfindingPosition);
    }

    private void CheckNeighbourNodes(GameNode currentNode)
    {
        foreach (NodeConnection neighbourNode in currentNode.Connections)
        {
            if (closedSet.Contains(neighbourNode.GameNode.node))
                continue;

            AddNeighbourNodeToOpenSetAndUpdateItsValues(currentNode, neighbourNode);
        }
    }

    private void AddNeighbourNodeToOpenSetAndUpdateItsValues(GameNode currentNode, NodeConnection neighbourNode)
    {
        float newMovementCost = currentNode.node.DistanceToPreviousClosestNode + CalculateDistance(neighbourNode);

        if (newMovementCost < neighbourNode.GameNode.node.DistanceToPreviousClosestNode || !openSet.Contains(neighbourNode.GameNode))
        {
            neighbourNode.GameNode.node.DistanceToPreviousClosestNode = newMovementCost;
            neighbourNode.GameNode.node.HeuristicCost = CalculateHeuristics(neighbourNode.GameNode, endPathfindingPosition);
            neighbourNode.GameNode.node.ParentNode = currentNode.node;
                
            if (!openSet.Contains(neighbourNode.GameNode))
                openSet.Add(neighbourNode.GameNode);
        }
    }

    private float CalculateDistance(NodeConnection connection)
    {
        return connection.RawConnectionLength * connection.WeightMultiplier;
    }
    
    private float CalculateHeuristics(GameNode neighbour, Vector3 endPosition)
    {
        return heuristicsMultiplier * Vector3.Distance(neighbour.transform.position, endPosition);
    }

    private List<Node> RetraceFoundPath(Node currentNode)
    {
        List<Node> path = new List<Node>();
        
        while (currentNode != startingNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.ParentNode;
        }
        
        path.Reverse();
        
        return path;
    }
    
    #endregion
    
}

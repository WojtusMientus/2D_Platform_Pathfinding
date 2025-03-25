using System;
using UnityEngine;


public class Node: IComparable<Node>
{
    
    #region PROPERTIES
    
    public Vector3 Position { get; private set; }
    public Node ParentNode { get; set; }

    public float HeuristicCost { get; set; }
    public float DistanceToPreviousClosestNode { get; set; }
    
    public int ParentParentPlatformID { get; private set; }
    public int HeapIndex { get; set; }
    
    #endregion
    
    private float combinedCost => HeuristicCost + DistanceToPreviousClosestNode;
    

    public Node(Vector3 position, int parentPlatformID = 0)
    {
        Position = position;
        ParentParentPlatformID = parentPlatformID;
    }

    #region METHODS

    public int CompareTo(Node other)
    {
        int compareValue = combinedCost.CompareTo(other.combinedCost);
        
        if (compareValue == 0)
            compareValue = HeuristicCost.CompareTo(other.HeuristicCost);
        
        return compareValue;
    }

    #endregion

}

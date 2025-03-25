using System;
using UnityEngine;
using UnityEngine.Serialization;

public enum ConnectionMode
{
    Multiplication,
    Addition,
}

[Serializable]
public class NodeConnection
{
    
    #region VARIABLES
    
    [FormerlySerializedAs("gameNode")] [SerializeField] public GameNode GameNode;
    [FormerlySerializedAs("weightMultiplier")] [SerializeField] public float WeightMultiplier = 1f;
    [FormerlySerializedAs("connectionMode")] [SerializeField] public ConnectionMode ConnectionMode;
    
    [SerializeField] public float rawConnectionLength;
    public float RawConnectionLength => rawConnectionLength;

    #endregion
    
    public NodeConnection(GameNode endNodeConnection, float weightMultiplier = 1f)
    {
        GameNode = endNodeConnection;
        WeightMultiplier = weightMultiplier;
    }

    
    #region METHODS
    
    public void CalculateStartingRawConnectionLength(GameNode startingNodeConnection)
    {
        rawConnectionLength = Vector3.Distance(startingNodeConnection.transform.position, GameNode.transform.position);
    }

    public void CalculateStartingWeightMultiplier(float newWeightMultiplier)
    {
        if (WeightMultiplier == 0)
            WeightMultiplier = newWeightMultiplier;
    }
    
    #endregion
    
}

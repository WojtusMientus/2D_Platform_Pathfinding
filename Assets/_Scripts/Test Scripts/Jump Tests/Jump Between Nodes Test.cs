using UnityEngine;



public class JumpBetweenNodesTest: MonoBehaviour
{

    [SerializeField] private Rigidbody testRB;
    
    [SerializeField] private GameObject startingNode;
    [SerializeField] private GameObject endingNode;
    
    
    public void JumpUp()
    {
        testRB.gameObject.transform.position = startingNode.transform.position;
        
        Vector3 calculatedVelocity = MathFunctions.CalculateProjectileVelocityUp(startingNode.transform.position, endingNode.transform.position);
        
        testRB.linearVelocity = calculatedVelocity;
    }

    public void JumpDown()
    {
        testRB.gameObject.transform.position = endingNode.transform.position;
        
        Vector3 calculatedVelocity= MathFunctions.CalculateProjectileVelocityDown(endingNode.transform.position, startingNode.transform.position);
        
        testRB.linearVelocity = calculatedVelocity;
    }
}


public class Heap
{
    #region PROPERTIES
    public int Count => currentItemCount;

    #endregion
    
    #region VARIABLES
    
    private readonly GameNode[] nodes;
    
    private int currentItemCount;

    #endregion
    

    public Heap(int maxHeapSize)
    {
        nodes = new GameNode[maxHeapSize];
        currentItemCount = 0;
    }

    
    #region METHODS
    
    public void Add(GameNode gameNode)
    {
        gameNode.node.HeapIndex = currentItemCount;
        nodes[currentItemCount] = gameNode;

        SortUp(gameNode);
        
        currentItemCount++;
    }

    private void SortUp(GameNode gameNode)
    {
        int parentIndex = (gameNode.node.HeapIndex - 1) / 2;

        while (true)
        {
            GameNode parentNode = nodes[parentIndex];
            
            if (gameNode.node.CompareTo(parentNode.node) < 0)
                Swap(gameNode, parentNode);
            else
                break;
            
            parentIndex = (gameNode.node.HeapIndex - 1) / 2;
        }
    }

    private void Swap(GameNode firstNode, GameNode secondNode)
    {
        nodes[firstNode.node.HeapIndex] = secondNode;
        nodes[secondNode.node.HeapIndex] = firstNode;
        (firstNode.node.HeapIndex, secondNode.node.HeapIndex) = (secondNode.node.HeapIndex, firstNode.node.HeapIndex);
    }

    public GameNode RemoveFirst()
    {
        GameNode firstNode = nodes[0];

        currentItemCount--;
        nodes[0] = nodes[currentItemCount];
        nodes[0].node.HeapIndex = 0;
        
        SortDown(nodes[0]);
        
        return firstNode;
    }

    private void SortDown(GameNode gameNode)
    {
        while (true)
        {
            int leftChildIndex = gameNode.node.HeapIndex * 2 + 1;
            int rightChildIndex = gameNode.node.HeapIndex * 2 + 2;

            if (leftChildIndex < currentItemCount)
            {
                int swapIndex = leftChildIndex;
                
                if (rightChildIndex < currentItemCount)
                    if (nodes[leftChildIndex].node.CompareTo(nodes[rightChildIndex].node) > 0)
                        swapIndex = rightChildIndex;
                
                if (gameNode.node.CompareTo(nodes[swapIndex].node) > 0)
                    Swap(gameNode, nodes[swapIndex]);
                else 
                    return;
            }
            else
                return;
        }
    }

    public void ClearHeap()
    {
        for (int i = 0; i < currentItemCount; i++)
            nodes[i] = null;

        currentItemCount = 0;
    }
    
    public bool Contains(GameNode gameNode)
    {
        return Equals(nodes[gameNode.node.HeapIndex], gameNode);
    }
    
    #endregion
    
}




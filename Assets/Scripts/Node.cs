using UnityEngine;

public class Node : MonoBehaviour
{
    public string nodeName; 

    [SerializeField] private Node nextNode;

    public Sprite nodeImage;
    public string nodeText;
    [SerializeField] 

    ReferenceManager referenceManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        referenceManager = ReferenceManager.Instance;
    }

    public virtual void RunNode()
    { 
    
    }


    public Node getNextNode()
    { 
        return nextNode;
    }

}

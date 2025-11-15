using UnityEngine;

public class Node : MonoBehaviour
{
    public string nodeName; 

    [SerializeField] private Node nextNode;

    public Sprite nodeImage;
    public string nodeText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    

    public Node getNextNode()
    { 
        return nextNode;
    }

}

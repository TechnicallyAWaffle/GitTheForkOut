using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] private Node nextNode;

    public ChoiceScriptableObject choice1;
    public ChoiceScriptableObject choice2;
    public ChoiceScriptableObject choice3;
    public ChoiceScriptableObject choice4;

    public Sprite nodeImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void GetChoices()
    { 
        
    }

    public void DisplayNodeChoices()
    { 
        
    }

    public Node getNextNode()
    { 
        return nextNode;
    }

}

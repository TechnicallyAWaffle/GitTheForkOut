using System.Collections;
using TMPro;
using UnityEngine;

public class Node : MonoBehaviour
{
    public string nodeName; 
    [SerializeField] private Node nextNode;
    public Sprite nodeImage;
    public string nodeText;
    [SerializeField] private TextMeshProUGUI bottomTextBox;
    [SerializeField] private TextMeshProUGUI topTextBox;
    private bool useBottomTextBox = false;
 

    protected ReferenceManager referenceManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        referenceManager = ReferenceManager.Instance;
    }


    public virtual void RunNode()
    {
        DisplayNodeText();
    }

    private void DisplayNodeText()
    { 
        nodeText = useBottomTextBox ? bottomTextBox.text : topTextBox.text;
    }

    public virtual void NextNode(Node nextNode)
    {
        nextNode.RunNode();
    }

    public void ClearText()
    {
        bottomTextBox.text = "";
        topTextBox.text = "";
    }

    public Node getNextNode()
    { 
        return nextNode;
    }

}

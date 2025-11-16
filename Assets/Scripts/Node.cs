using System.Collections;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] public string nodeName; 
    [SerializeField] private Node nextNode;
    public string nodeText;
    [SerializeField] private TextMeshProUGUI bottomTextBox;
    [SerializeField] private TextMeshProUGUI topTextBox;
    [SerializeField] private bool useBottomTextBox = false;
 

    protected ReferenceManager referenceManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        referenceManager = ReferenceManager.Instance;
        nodeName = gameObject.name;
    }


    public virtual void RunNode()
    {
        Debug.Log("Running Node: " + nodeName);
        DisplayNodeText();
    }

    private void DisplayNodeText()
    {
        if (useBottomTextBox)
        {
            bottomTextBox.text = nodeText;
        }
        else
        {
            
            topTextBox.text = nodeText;
        }
    }

    public virtual bool CanGoToNextNode()
    {
        return true;
        //referenceManager.timelineManager.FindAndRunNextNode(nextNode);
    }

    public void ClearText()
    {
        bottomTextBox.text = "";
        topTextBox.text = "";
    }

    public Node GetNextNode()
    {
        return nextNode;
    }

}

using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class TimelineManager : MonoBehaviour
{

    ReferenceManager referenceManager;

    public List<Node> nodes = new();
    [SerializeField] private Node currentNode;
    [SerializeField] private Node lastChoiceNode;
    public LineRenderer lr;

    //Runtime Vars
    public float lineDrawtime;

    void Start()
    {
        referenceManager = ReferenceManager.Instance;
        //StartCoroutine(LineDraw());
        FindAndRunNextNode(nodes[0]);
        lr.positionCount = 1;
        lr.SetPosition(0, nodes[0].transform.position);
    }

    public void FindAndRunNextNode(Node nextNode)
    {
        referenceManager.currentChoiceObject.SetActive(false);
        if (nextNode)
        {
            if (currentNode)
            {
                currentNode.NextNode();
                currentNode.RunNode();
                StartCoroutine(DrawLineToNextNode(currentNode, nextNode));
            }
            else
                StartCoroutine(InitialNodeWait(nextNode));
        }
        else
        { 
            //no node so do some funny thing idk
        }

        currentNode = nextNode;
    }

    private IEnumerator InitialNodeWait(Node node)
    {
        node.RunNode();
        yield return new WaitForSeconds(1f);
        FindAndRunNextNode(node.GetNextNode());
    }

    public void RunChoiceNode(ChoiceNode node)
    {
        lastChoiceNode = node;
        UpdateCurrentChoiceData(node);
        referenceManager.currentChoiceObject.SetActive(true);
    }

    private void UpdateCurrentChoiceData(ChoiceNode node)
    {
        for (int index = 0; index < 4; index++)
        {
            if(node.nodeImage)
                referenceManager.nodeImage = node.nodeImage;
            if (node.choiceImages[index] && node.choiceTexts[index] != null)
            {
                referenceManager.choiceImages[index].sprite = node.choiceImages[index];
                referenceManager.choiceTexts[index].text = node.choiceTexts[index];
                referenceManager.choiceButtons[index].onClick.AddListener(() => FindAndRunNextNode(node.nextNodes[index]));
            }
                
        }
    }

    public void ClearButtonReferences()
    {
        for (int index = 0; index < 4; index++)
        {
            referenceManager.choiceButtons[index].onClick.RemoveAllListeners();
        }
    }

    private IEnumerator DrawLineToNextNode(Node currentNode, Node nextNode)
    {
        lr.positionCount += 1;
        float t = 0;

        Vector3[] positions = new Vector3[lr.positionCount];
        lr.GetPositions(positions);
        Vector3 lastPosition = currentNode.transform.position + new Vector3(0, 0, 1);
        Vector3 nextPosition = nextNode.transform.position + new Vector3(0, 0, 1);

        lr.SetPosition(lr.positionCount - 1, lastPosition);
        //Draw Line
        for (; t < lineDrawtime; t += Time.deltaTime)
        {
            lr.SetPosition(lr.positionCount-1, Vector3.Lerp(lastPosition, nextPosition, t / lineDrawtime));
            yield return null;
        }
        lr.SetPosition(lr.positionCount - 1, nextPosition);
        yield return new WaitForSeconds(0.5f);
        FindAndRunNextNode(currentNode.GetNextNode());
    }

    private void SetNextLinePosition()
    {
        int currentIndex = lr.positionCount++;
        //lr.SetPosition(currentIndex, );
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}

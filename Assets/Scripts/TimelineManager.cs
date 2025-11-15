using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TimelineManager : MonoBehaviour
{

    ReferenceManager referenceManager;

    public List<Node> nodes = new();
    [SerializeField] private Node currentNode;
    [SerializeField] private Node lastChoiceNode;
    LineRenderer lr;

    void Start()
    {
        referenceManager = ReferenceManager.Instance;
        //StartCoroutine(LineDraw());
        nodes[0].RunNode();
    }

    private void FindAndRunNextNode()
    {
        currentNode.getNextNode().RunNode();
    }

    public void RunChoiceNode(ChoiceNode node)
    {
        lastChoiceNode = node;
        UpdateCurrentChoiceData(node);
    }

    private void UpdateCurrentChoiceData(ChoiceNode node)
    {
        for (int index = 0; index <= 4; index++)
        {
            if(node.nodeImage)
                referenceManager.nodeImage = node.nodeImage;
            if (node.choiceImages[index] && node.choiceTexts[index] != null)
            {
                referenceManager.choiceImages[index] = node.choiceImages[index];
                referenceManager.choiceTexts[index].text = node.choiceTexts[index];
                referenceManager.choiceButtons[index].onClick.AddListener(() => node.NextNode(node.nextNodes[index]));
            }
                
        }
    }

    public void ClearButtonReferences()
    {
        for (int index = 0; index <= 4; index++)
        {
            referenceManager.choiceButtons[index].onClick.RemoveAllListeners();
        }
    }

    private IEnumerator LineDraw()
    {
        float t = 0;
        float time = 2;

        Vector3[] positions = new Vector3[lr.positionCount];
        lr.GetPositions(positions);
        int index = 0;
        Vector3 lastPosition = lr.GetPosition(index);
        Vector3 nextPosition = lr.GetPosition(index+1);
        foreach (Vector3 position in positions)
        {
            index++;
            
        }
      
        
        Vector3 newpos;
        for (; t < time; t += Time.deltaTime)
        {
            //newpos = Vector3.Lerp(1, 1, t / time);
            //lr.SetPosition(1, newpos);
            yield return null;
        }
        //lr.SetPosition(1, orig2);
        
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

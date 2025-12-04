using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class TimelineManager : MonoBehaviour
{

    ReferenceManager referenceManager;
    //Refs
    private Camera mainCamera;

    public List<Node> nodes = new();
    [SerializeField] private Node currentNode;
    [SerializeField] private Node lastChoiceNode;
    public LineRenderer lr;

    //Runtime Vars
    public float lineDrawtime;
    public float zoomSpeed;
    public float lerpDuration;
    public float defaultCameraSize;
    public float choiceCameraSize;
    public float cutsceneCameraSize;
    [SerializeField] private Node[] currentNextNodeChoices;

    void Start()
    {
        referenceManager = ReferenceManager.Instance;
        this.mainCamera = referenceManager.mainCamera;

        PopulateNodeListAndHideAllNodes();
        ShowNode(nodes[0]);

        //StartCoroutine(LineDraw());
        FindAndRunNextNode(nodes[0]);
        lr.positionCount = 1;
        lr.SetPosition(0, nodes[0].transform.position);
        referenceManager.mainCamera.orthographicSize = defaultCameraSize;
    }

    #region Node Visibility Stuff

    private void PopulateNodeListAndHideAllNodes()
    {
        foreach (Node node in referenceManager.nodeParentThatContainsAllNodes.transform.GetComponentsInChildren<Node>())
        {
            nodes.Add(node);
            node.gameObject.SetActive(false);
        }
    }

    private void ShowNode(Node node)
    {
        node.gameObject.SetActive(true);
    }


    #endregion


    public void FindAndRunNextNode(Node nextNode)
    {

        if (mainCamera.orthographicSize != defaultCameraSize)
            StartCoroutine(ZoomCamera(true, mainCamera.orthographicSize));

        referenceManager.currentChoiceObject.SetActive(false);
        referenceManager.audioSource.Stop();
  
        if (nextNode)
        {
            Debug.Log("Finding Node: " + nextNode.name);
            if (currentNode)
            {
                StartCoroutine(DrawLineToNextNode(currentNode, nextNode));
            }
            else
                StartCoroutine(InitialNodeWait(nextNode));
            currentNode = nextNode;
        }
        else
        { 
            //no node so do some funny thing idk
        }

        
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

        for (int i = 0; i < 4; i++)
        {
            if (referenceManager.choiceTexts[i].text == "")
            {
                referenceManager.choiceTexts[i].gameObject.SetActive(false);
            }
            else
            {
                referenceManager.choiceTexts[i].gameObject.SetActive(true);
            }
                
        }
        referenceManager.VideoCanvas.SetActive(false);
        StartCoroutine(ZoomCamera(false, choiceCameraSize));
    }

    public void RunCutsceneNode(CutsceneNode node)
    {
        for (int i = 0; i < 4; i++) referenceManager.choiceTexts[i].text = "";
        referenceManager.VideoCanvas.SetActive(true);
        StartCoroutine(ZoomCamera(false, cutsceneCameraSize));
    }


    public IEnumerator LerpCameraToNode(Node node)
    {
        referenceManager.currentChoiceObject.transform.position = node.transform.position + new Vector3(0,0,-3);

        float timer = 0f;
        float startTime = Time.time;
        while (timer < 1f)
        {
            yield return null;
            float elapsedTime = Time.time - startTime;
            float t = elapsedTime / lerpDuration;

            // Clamp t between 0 and 1 to prevent overshooting
            t = Mathf.Clamp01(t);

            referenceManager.mainCamera.transform.position = 
                Vector3.Lerp(referenceManager.mainCamera.transform.position, node.transform.position + new Vector3(0,0,-9), t);
        }
    }

    private IEnumerator ZoomCamera(bool isZoomOut, float cameraSize)
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("meow");
        float timer = 0f;

        while (timer < 1f)
        {
            timer += Time.deltaTime * zoomSpeed;

            if(isZoomOut)
                referenceManager.mainCamera.orthographicSize = Mathf.Lerp(cameraSize, defaultCameraSize, timer);
            else
                referenceManager.mainCamera.orthographicSize = Mathf.Lerp(defaultCameraSize, cameraSize, timer);
            yield return null; // Wait for the next frame
        }

        if(isZoomOut)
            referenceManager.mainCamera.fieldOfView = defaultCameraSize;
        else
            referenceManager.mainCamera.fieldOfView = cameraSize;
    }


    private void UpdateCurrentChoiceData(ChoiceNode node)
    {
        for (int index = 0; index < 4; index++)
        {
            if (node.nodeImage)
                referenceManager.nodeImage.sprite = node.nodeImage;
            if (node.choiceImages[index])
                referenceManager.choiceImages[index].sprite = node.choiceImages[index];
            if (node.choiceTexts[index] != null)
                referenceManager.choiceTexts[index].text = node.choiceTexts[index];
            if (node.nextNodes[index])
                currentNextNodeChoices[index] = node.nextNodes[index];
        }
    }

    public void NextNodeBasedOnChoice(int index)
    {
        FindAndRunNextNode(currentNextNodeChoices[index]);
    }

    private IEnumerator DrawLineToNextNode(Node currentNode, Node nextNode)
    {
        if (referenceManager.mainCamera.orthographicSize < 9)
            yield return new WaitForSeconds(0.75f);

        if (!nextNode.gameObject.activeSelf)
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
                lr.SetPosition(lr.positionCount - 1, Vector3.Lerp(lastPosition, nextPosition, t / lineDrawtime));
                yield return null;
            }
            lr.SetPosition(lr.positionCount - 1, nextPosition);
        }

        ShowNode(nextNode);
        StartCoroutine(LerpCameraToNode(nextNode));
        yield return new WaitForSeconds(1f);
        nextNode.RunNode();
        if (nextNode.CanGoToNextNode())
        {
            FindAndRunNextNode(currentNode.GetNextNode());
        }
            
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}

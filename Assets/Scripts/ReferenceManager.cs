using System.Data.SqlTypes;
using TMPro;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class ReferenceManager : MonoBehaviour
{
    public Camera mainCamera;
    public TimelineManager timelineManager;

    //Node Choices
    public GameObject currentChoiceObject;
    public Image choiceImage;
    public TextMeshProUGUI currentNodeChoiceText1;
    public TextMeshProUGUI currentNodeChoiceText2;
    public TextMeshProUGUI currentNodeChoiceText3;
    public TextMeshProUGUI currentNodeChoiceText4;

    public static ReferenceManager Instance;

    

    public static ReferenceManager GetInstance() { return Instance; }
    void Start()
    {
        Instance = this;
    }
}

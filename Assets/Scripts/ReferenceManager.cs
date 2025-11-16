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
    public Image nodeImage;
    public SpriteRenderer[] choiceImages;
    public TextMeshProUGUI[] choiceTexts;
    public Button[] choiceButtons;

    public static ReferenceManager Instance;

    

    public static ReferenceManager GetInstance() { return Instance; }
    void Awake()
    {
        Instance = this;
    }
}

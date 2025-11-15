using UnityEngine;
using UnityEngine.UI;

public class ChoiceNode : Node
{
    public Image[] choiceImages;
    [TextAreaAttribute]
    public string[] choiceTexts;
    public Node[] nextNodes;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public string[] GetChoiceTexts()
    {
        return choiceTexts;
    }

    public Image[] GetChoiceImages()
    {
        return choiceImages;
    }

    public Node[] GetNextNodes()
    {
        return nextNodes;
    }

    public void DisplayNodeChoices()
    {

    }

}

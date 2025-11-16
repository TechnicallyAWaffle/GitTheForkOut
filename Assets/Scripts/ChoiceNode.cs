using UnityEngine;
using UnityEngine.UI;

public class ChoiceNode : Node
{
    public Sprite[] choiceImages;
    [TextAreaAttribute]
    public string[] choiceTexts;
    public Node[] nextNodes;
    public Sprite nodeImage;

    protected override void Start()
    {    
        base.Start();
    }

    public override void RunNode()
    {
        base.RunNode();
        referenceManager.timelineManager.RunChoiceNode(this);
    }

    public override bool CanGoToNextNode()
    {
        return false;   
    }

    public string[] GetChoiceTexts()
    {
        return choiceTexts;
    }

    public Sprite[] GetChoiceImages()
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

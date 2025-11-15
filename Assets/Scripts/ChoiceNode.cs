using UnityEngine;
using UnityEngine.UI;

public class ChoiceNode : Node
{
    public Sprite[] choiceImages;
    [TextAreaAttribute]
    public string[] choiceTexts;
    public Button[] choiceButtons;
    public Node[] nextNodes;

    public override void RunNode()
    {
        referenceManager.timelineManager.RunChoiceNode(this);
        referenceManager.timelineManager.ClearButtonReferences();
    }

    public override void NextNode(Node nextNode)
    {
        base.NextNode(nextNode);
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

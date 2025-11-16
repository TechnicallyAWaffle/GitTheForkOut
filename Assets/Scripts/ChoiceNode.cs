using UnityEngine;
using UnityEngine.UI;

public class ChoiceNode : Node
{
    public Sprite[] choiceImages;
    [TextAreaAttribute]
    public string[] choiceTexts;
    public Node[] nextNodes;
    public Image nodeImage;

    protected override void Start()
    {    
        base.Start();
    }

    public override void RunNode()
    {
        base.RunNode();
        referenceManager.timelineManager.RunChoiceNode(this);
    }

    public override void NextNode()
    {
        base.NextNode();
        referenceManager.timelineManager.ClearButtonReferences();
        
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

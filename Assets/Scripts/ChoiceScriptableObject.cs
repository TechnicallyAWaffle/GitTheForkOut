using UnityEngine;

[CreateAssetMenu(fileName = "ChoiceScriptableObject", menuName = "Scriptable Objects/ChoiceScriptableObject")]
public class ChoiceScriptableObject : ScriptableObject
{
    public Node nextNode;
    public string choiceText;
    public Sprite choiceImage;
    public bool isCorrect;
}

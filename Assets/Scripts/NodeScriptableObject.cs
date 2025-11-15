using System.Buffers;
using UnityEngine;

[CreateAssetMenu(fileName = "NodeScriptableObject", menuName = "Scriptable Objects/NodeScriptableObject")]
public class NodeScriptableObject : ScriptableObject
{
    public Node node;
    public Node nextNode;

}

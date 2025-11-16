using UnityEngine;
using UnityEngine.Video;

public class CutsceneNode : Node
{
    public VideoPlayer player;
    public VideoClip clip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        player = referenceManager.mainCamera.gameObject.GetComponent<VideoPlayer>();
    }

    public override void RunNode()
    {
        base.RunNode();
        player.Play();
    }
}

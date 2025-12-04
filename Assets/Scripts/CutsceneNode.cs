using UnityEngine;
using UnityEngine.Video;
using static UnityEngine.ParticleSystem;

public class CutsceneNode : Node
{
    //public Sprite cutsceneCanvas;
    public VideoPlayer player;
    public VideoClip clip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        player = referenceManager.VideoRenderer.GetComponent<VideoPlayer>();

    }

    public override void RunNode()
    {
        base.RunNode();
        timelineManager.RunCutsceneNode(this);
        player.loopPointReached += EnterNextNode;
        player.clip = this.clip;
        referenceManager.currentChoiceObject.SetActive(true);
        player.Play();
    }
    public override bool CanGoToNextNode()
    {
        return false;
    }

    void EnterNextNode(VideoPlayer vp)
    {
        // Play the particle effect when the video reaches the end.  
        vp.clip = null;
        timelineManager.FindAndRunNextNode(nextNode);
    }
}

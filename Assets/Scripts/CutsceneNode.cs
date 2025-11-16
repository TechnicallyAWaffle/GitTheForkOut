using UnityEngine;
using UnityEngine.Video;

public class CutsceneNode : MonoBehaviour
{
    public VideoPlayer player;
    public VideoClip clip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject camera = GameObject.Find("Main Camera");
        player = camera.AddComponent<UnityEngine.Video.VideoPlayer>();
        player.playOnAwake = false;
        player.Play();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class BackgroundVideoSet : MonoBehaviour
{
    public VideoPlayer player;
    public string path;

    // Start is called before the first frame update
    void Start()
    {
        player.url = System.IO.Path.Combine(Application.streamingAssetsPath, path);
    }
}

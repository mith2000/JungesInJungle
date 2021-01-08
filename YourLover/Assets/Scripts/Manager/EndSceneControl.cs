using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneControl : MonoBehaviour
{
    private void Start()
    {
        AudioManager.GetInstance().Stop("GameBackgroundMusic");
        AudioManager.GetInstance().Play("EndSong", false);
    }
}

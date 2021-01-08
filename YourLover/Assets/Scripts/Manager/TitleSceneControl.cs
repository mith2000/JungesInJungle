using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneControl : MonoBehaviour
{
    void Start()
    {
        AudioManager.GetInstance().Play("TitleSong", false);
    }
}

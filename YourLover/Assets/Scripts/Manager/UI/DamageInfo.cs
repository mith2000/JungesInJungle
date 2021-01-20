using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamageInfo : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string damage;

    void Start()
    {
        text.text = damage;
        Destroy(gameObject, 0.5f);
    }
}

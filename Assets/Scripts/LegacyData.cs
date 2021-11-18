using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegacyData : MonoBehaviour
{
    public AudioClip audioFile;

    [Multiline(10)]
    [SerializeField]
    private string description;
    public string GetDescription()
    {
        return description;
    }
}

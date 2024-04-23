using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordsManager : MonoBehaviour
{
    public static WordsManager Instance { get; private set; }

    public Dictionary<string, string> dictElementos;

    public int numDict;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        dictElementos = new Dictionary<string, string>()
        {
            {"Hidrogeno","H"},
            {"Helio","He"},
            {"Litio","Li"},
            {"Berilio","Be"},
            {"Boro","B"},
        };
        numDict = dictElementos.Count;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject[] puzzleLevels;
    public GameObject fadePanel;

    int currentLevel = 0;

    void Awake()
    {
        instance = this;
        LoadLevel(currentLevel);
    }

    void LoadLevel(int index)
    {
        Instantiate(puzzleLevels[index], Vector3.zero, Quaternion.identity).tag = "Puzzle"; // Spawn puzzle
    }
}

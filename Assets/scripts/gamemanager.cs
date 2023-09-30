using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class gamemanager : MonoBehaviour
{
    public static gamemanager instance;
    private int score;
    public TextMeshProUGUI tmp;
    public Transform minenemy;
    public float mindistance;
    public int scorecount
    {
        get { return score; }
        set
        {
            score = value;
            upUI();
        }
    }
    private void Awake()
    {
        instance = this;
    }
    void upUI()
    {
        tmp.text = score.ToString();
    }
}
using UnityEngine;
using TMPro;
using System.Threading;

public class scoreManager : MonoBehaviour
{
    public static scoreManager Instance;
    public int score = 0;
    public TMP_Text scoreText;

    void Awake()
    {
        // 'this' mean scoreManager script
        // 'this' same as: transform.GetComponent<scoreManager>();
        Instance = this;
    }
    

    public void AddScore()
    {
        score = score + 1;
        scoreText.text = "Score: " + score;
    }
}
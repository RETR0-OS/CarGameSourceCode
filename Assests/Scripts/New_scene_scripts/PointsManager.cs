using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour
{
    public float scoreCount = 0;
    public float pointsPer10m = 1;
    public bool PlayerIsNotDead = true;
    public Text scoreText;
    public Vector3 lastPos; 
    public Transform Player; // drag the object to monitor here
    public Vector3 buffer_num;

    // Start is called before the first frame update
    void Start()
    {
        lastPos = Player.position; 
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerIsNotDead){ 
            if (Vector3.Distance(lastPos, Player.position)>10)
            { 
                scoreCount += pointsPer10m;
                lastPos = Player.position;
            }
            scoreText.text = scoreCount.ToString();
        }
    }
}

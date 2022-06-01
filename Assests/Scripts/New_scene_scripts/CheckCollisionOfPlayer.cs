using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollisionOfPlayer : MonoBehaviour
{
    public Transform Player;
    public NEWCarController Playercontrol;
    // Start is called before the first frame update

    // Update is called once per frame
    public void OnTriggerEnter(Collider Player) {
        Playercontrol.lives_left -= 1;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManageing : MonoBehaviour
{
    public GameObject playerDeadScreen;
    public GameObject playerUi;
    public GameObject Player;
    public GameObject StartScreen;
    private void Start() {
        Player.SetActive(false);
        playerUi.SetActive(true);
        playerDeadScreen.SetActive(false);
        StartScreen.SetActive(true);
    }
    // Start is called before the first frame update
    // Update is called once per frame
    public void PlayAgain()
    {
        SceneManager.LoadScene("Game");
    }

    public void StartGame(){
        StartScreen.SetActive(false);
        Player.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    GameObject player;

    [SerializeField]
    TMP_Text healthText;

    [SerializeField]
    GameObject loseText;
    [SerializeField]
    GameObject winText;
    [SerializeField]
    GameObject restartButton;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    public void restartLevel()
    {
        SceneManager.LoadScene("SampleScene");
    }
    // Update is called once per frame
    void Update()
    {
        if(player)
        {
            healthText.SetText(player.GetComponent<Killable>().health.ToString());
            if(player.GetComponent<PlayerMovement>().win)
            {
                winText.SetActive(true);
                restartButton.SetActive(true);
                Time.timeScale = 0;
            }
        }
        else
        {
            loseText.SetActive(true);
            restartButton.SetActive(true);
        }
    }
}

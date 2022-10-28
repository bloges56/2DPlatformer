using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
                Time.timeScale = 0;
            }
        }
        else
        {
            loseText.SetActive(true);
        }
    }
}

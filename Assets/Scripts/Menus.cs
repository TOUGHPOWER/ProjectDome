using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menus : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameMenu;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        gameMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            pauseMenu.SetActive(true);
            gameMenu.SetActive(false);
            Time.timeScale = 0;
        }
        
    }

    public void DisablePauseMenu()
    {
        pauseMenu.SetActive(false);
        gameMenu.SetActive(true);
        Time.timeScale = 1;
    }
}

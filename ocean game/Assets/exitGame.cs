using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitGame : MonoBehaviour
{
    public GameObject subMenu;
   

    public void openSub()
    {
        subMenu.SetActive(true);
    }

    public void closeSub()
    {
        subMenu.SetActive(false);
    }


    
    public void quitGame()
    {
        Application.Quit();
    }
}

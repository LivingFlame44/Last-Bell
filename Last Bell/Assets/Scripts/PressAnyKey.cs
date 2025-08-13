using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PressAnyKey : MonoBehaviour
{
    public GameObject panelToShow;
    public GameObject panelToHide;

    void Update()
    {
        if (Input.anyKeyDown) 
        {
            if (panelToShow != null)
                panelToShow.SetActive(true);

            if (panelToHide != null)
                panelToHide.SetActive(false);

            enabled = false;
        }
    }
}

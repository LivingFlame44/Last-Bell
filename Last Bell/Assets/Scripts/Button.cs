using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSwitcher : MonoBehaviour
{
    [Header("Panels to Show/Hide")]
    public GameObject panelToShow;
    public GameObject panelToHide;

    public void SwitchPanels()
    {
        if (panelToShow != null)
            panelToShow.SetActive(true);

        if (panelToHide != null)
            panelToHide.SetActive(false);
    }
}
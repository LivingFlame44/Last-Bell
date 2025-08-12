using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
{
    public GameObject interactText;
    public bool interacted;
    public UIManager uiManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interactText.SetActive(true);
            if (!interacted)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interacted = true;
                    WinGame();
                }
            }
        }
    }

    public void WinGame()
    {
        uiManager.Win();
    }
}

using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class Win : MonoBehaviour
{
    public GameObject win;
    public FirstPersonController firstPersonController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0f;
            firstPersonController.enabled = false;
            win.SetActive(true);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}

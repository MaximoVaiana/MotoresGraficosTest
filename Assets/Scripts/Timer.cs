using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private int seconds = 0;
    public TextMeshProUGUI timeText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(Tiempo), 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = "Tiempo: " + seconds.ToString();
    }

    void Tiempo()
    {
        seconds++;
    }
}

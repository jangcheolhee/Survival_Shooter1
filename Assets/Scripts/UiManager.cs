using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetScore(int score)
    {
        scoreText.text = $"Score :  {score}";
    }
}

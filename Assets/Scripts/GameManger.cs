using UnityEngine;

public class GameManger : MonoBehaviour
{
    public ZombieSpawner zombieSpawner;
    public PlayerShooter shooter;
    public PlayerMovement movement;
    public UiManager uiManager;
    public GameObject panel;

    private int score = 0;
    private bool isPause = false;

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseOnOff();
        }
    }
    public void AddScore(int score)
    {
        this.score = score;
        uiManager.SetScore(score);
    }
    public void GameExit()
    {
        Application.Quit();
    }
    public void PauseOnOff()
    {
        isPause = !isPause;
        if (isPause)
        {
            panel.SetActive(true);
            shooter.enabled = false;
            zombieSpawner.enabled = false;
            movement.enabled = false;
        }
        else
        {
            panel.SetActive(false);
            shooter.enabled = true;
            zombieSpawner.enabled = true;
            movement.enabled = true;
        }
    }
}

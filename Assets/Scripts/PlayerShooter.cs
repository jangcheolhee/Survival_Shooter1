using UnityEngine;

public class PlayerShooter : MonoBehaviour
{

    private PlayerInput playerInput;
    public Gun gun;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInput.Fire)
        {
            gun.Fire();
        }
    }
}

using UnityEngine;
using UnityEngine.Timeline;

public class PlayerInput : MonoBehaviour
{
    public float MoveV {  get; private set; }
    public float MoveH { get; private set; }
    public Vector3 Rotate {  get; private set; }
    public bool Fire{get; private set; }

    private void Update()
    {
        MoveV = Input.GetAxis("Vertical");
        MoveH = Input.GetAxis("Horizontal");
        Fire = Input.GetButton("Fire1");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Plane floor = new Plane(Vector3.up, Vector3.zero);
        float distance;
        if (floor.Raycast(ray, out distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            Rotate = hitPoint;
            
        }

    }
}

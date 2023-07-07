using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Vector2 tentacleAcceleration;
    [SerializeField]
    private float tentacleVelocityScale = 1f;

    public Tentacle tentaclePrefab;

    void OnEnable()
    {
        InputManager.MouseDrag += OnDrag;
    }

    void OnDisable()
    {
        InputManager.MouseDrag -= OnDrag;
    }

    void OnDrag(InputManager.MouseDragEvent ev)
    {
        Vector2 velocity = (ev.mouseDown - ev.mouseUp) * this.tentacleVelocityScale;
        Vector2 position = ev.mouseDown;
        position.y = this.transform.position.y;

        SpawnTentacle(position, velocity);
    }

    public void SpawnTentacle(Vector2 position, Vector2 velocity)
    {
        GameObject obj = Instantiate(this.tentaclePrefab.gameObject, (Vector3)position, Quaternion.identity);

        TentacleLaunch launch = obj.GetComponent<TentacleLaunch>();
        launch.velocity = velocity;
        launch.acceleration = this.tentacleAcceleration;
    }
}

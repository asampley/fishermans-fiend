using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Vector2 tentacleAcceleration;

    public Tentacle tentaclePrefab;

    private List<Tentacle> tentacles = new();

    void OnEnable()
    {
        InputManager.MouseDrag += OnDrag;
    }

    void OnDisable()
    {
        InputManager.MouseDrag -= OnDrag;
    }

    void Update()
    {
        // check if any tentacles have gone
        tentacles.RemoveAll(t => t == null);
    }

    void OnDrag(InputManager.MouseDragEvent ev)
    {
        if (GameManager.Instance.GameIsPaused) return;

        if (tentacles.Count >= GameManager.Instance.MaxTentacles) return;

        Vector2 velocity = Vector2.ClampMagnitude(
            (ev.mouseDown - ev.mouseUp) * GameManager.Instance.TentacleVelocityScale,
            GameManager.Instance.MaxTentacleLaunchStrength
            );

        SpawnTentacle(this.transform.position, velocity);
    }

    public void SpawnTentacle(Vector2 position, Vector2 velocity)
    {
        GameObject obj = Instantiate(this.tentaclePrefab.gameObject, (Vector3)position, Quaternion.identity);
        obj.transform.parent = GameManager.Instance.SpawnParent;

        Tentacle tentacle = obj.GetComponent<Tentacle>();

        TentacleLaunch launch = obj.GetComponent<TentacleLaunch>();
        launch.velocity = velocity;
        launch.acceleration = this.tentacleAcceleration;

        tentacles.Add(tentacle);
    }
}

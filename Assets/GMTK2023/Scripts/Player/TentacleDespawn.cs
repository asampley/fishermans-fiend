using UnityEngine;

public class TentacleDespawn : MonoBehaviour
{
    public float peakY;

    void FixedUpdate()
    {
        if (this.transform.localPosition.y + this.peakY < 0) {
            Destroy(this.gameObject);
        }
    }
}

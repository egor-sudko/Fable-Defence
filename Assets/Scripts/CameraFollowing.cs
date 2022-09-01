using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    [SerializeField]private Transform player;
    private Vector3 offset;

    [SerializeField]private float dividerValue = 1.6f;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(player.transform.position.x / dividerValue, player.transform.position.y + offset.y,
            player.transform.position.z + offset.z);
    }
}

using UnityEngine;

public class TrackHandBehavior : MonoBehaviour
{
    private Vector3 playerBoxPosition;

    public GameObject targetPalm;

    private Vector3 GetCurrentPosition()
    {
        return new Vector3(
            targetPalm.transform.position.x,
            targetPalm.transform.position.y,
            playerBoxPosition.z);
    }

    private Quaternion GetCurrentRotation()
    {
        return new Quaternion(
            0,
            0,
            1,
            targetPalm.transform.rotation.w);
    }

    public void Start()
    {
        playerBoxPosition = transform.parent.position;
    }

    public void Update()
    {
        var transform = GetComponent<Transform>();
        transform.position = GetCurrentPosition();
        //transform.rotation = GetCurrentRotation();
    }
}

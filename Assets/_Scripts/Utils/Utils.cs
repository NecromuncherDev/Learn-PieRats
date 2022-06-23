using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Vector3 ScreenToWorld2D(Camera camera, Vector3 position)
    {
        position.z = camera.nearClipPlane;
        return camera.ScreenToWorldPoint(position);
    }
}

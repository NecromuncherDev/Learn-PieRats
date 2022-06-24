using UnityEngine;

public class WarCamera : MonoBehaviour
{
    [SerializeField] private SmoothFollow cameraFollow;
    [SerializeField] private Transform playableShip;

    private Vector2 enemyShipPos;
    private Transform warFocus;
    //private WarManager warManager;


    private void Awake()
    {
        //warManager = WarManager.Instance;
    }

    private void OnEnable()
    {
        //warManager.OnWarStart += SetWarFocus;
        //warManager.OnWarEnd += SetNormalFocus;
    }

    private void OnDisable()
    {
        //warManager.OnWarStart -= SetWarFocus;
        //warManager.OnWarEnd -= SetNormalFocus;
    }
    private void SetWarFocus(Vector2 enemyShipPosition)
    {
        enemyShipPos = enemyShipPosition;
        warFocus.position = (playableShip.position + (Vector3)enemyShipPos) / 2;
        cameraFollow.target = warFocus;
    }

    private void SetNormalFocus()
    {
        cameraFollow.target = playableShip;
    }
}

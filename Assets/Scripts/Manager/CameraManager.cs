using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform Target;
    [SerializeField] private float LerpRate;
    public static CameraManager Instance;
    bool reinitialized;
    Vector3 targetVector;
    [HideInInspector] public Camera Camera;

    void Awake()
    {
        Instance = this;
        Camera = gameObject.GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (!reinitialized)
            targetVector = new Vector3(Target.position.x, Target.position.y, -10);
        FollowTarget();
        reinitialized = false;
    }

    public float GetLerpRate() { return LerpRate; }

    private void FollowTarget()
    {
        transform.position = Vector3.Lerp(transform.position, targetVector, Time.unscaledDeltaTime * LerpRate);
    }

    public void ConstantTarget(Vector2 target)
    {
        reinitialized = true;
        targetVector = new Vector3(target.x, target.y, -10);
    }

    public void Zoom(float zoomValue)
    {
        StartCoroutine(Zooming(zoomValue));
    }

    IEnumerator Zooming(float zoomValue)
    {
        float rate = 0.1f;
        if (Camera.orthographicSize > zoomValue)
            rate *= -1;
        int iterations = (int)((Camera.orthographicSize - zoomValue) / rate);
        for (int i = 0; i < iterations; i++)
        {
            Camera.orthographicSize += rate;
            yield return null;
        }
        Camera.orthographicSize = zoomValue;
    }
}

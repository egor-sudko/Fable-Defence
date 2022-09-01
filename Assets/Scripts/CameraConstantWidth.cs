using UnityEngine;

public class CameraConstantWidth : MonoBehaviour
{
    [SerializeField] private Vector2 DefaultResolution = new Vector2(720, 1280);
    [Range(0f, 1f)] public float WidthOrHeight = 0;

    private Camera componentCamera;

    private float targetAspect;

    private float initialFov;
    private float horizontalFov = 120f;

    private void Awake()
    {
        componentCamera = GetComponent<Camera>();

        targetAspect = DefaultResolution.x / DefaultResolution.y;

        initialFov = componentCamera.fieldOfView;
        horizontalFov = CalcVerticalFov(initialFov, 1 / targetAspect);
    }

    private void Start()
    {
        ChooseWightOrHeight();

        float constantWidthFov = CalcVerticalFov(horizontalFov, componentCamera.aspect);
        componentCamera.fieldOfView = Mathf.Lerp(constantWidthFov, initialFov, WidthOrHeight);

    }

    private float CalcVerticalFov(float hFovInDeg, float aspectRatio)
    {
        float hFovInRads = hFovInDeg * Mathf.Deg2Rad;

        float vFovInRads = 2 * Mathf.Atan(Mathf.Tan(hFovInRads / 2) / aspectRatio);

        return vFovInRads * Mathf.Rad2Deg;
    }

    private void ChooseWightOrHeight()
    {
        float currentAspect = (float)Screen.width / Screen.height;

        if (targetAspect < currentAspect)
        {
            WidthOrHeight = 1f;
        }
        else
        {
            WidthOrHeight = 0f;
        }
    }
}

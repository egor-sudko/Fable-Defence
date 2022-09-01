using UnityEngine;

public class GunMoving : MonoBehaviour
{
    [SerializeField] private float borderX;

    private float deltaX;
    private Camera _camera;
    private Transform _transform;
    
    private Vector3 touchPos;
    private float dist;

    private bool isMobile;

    private void Awake()
    {
        isMobile = Application.isMobilePlatform;
    }

    private void Start()
    {
        _camera = Camera.main;
        _transform = GetComponent<Transform>();

        Transform camTrans = _camera.transform;
        dist = Vector3.Dot(_transform.position - camTrans.position, camTrans.forward);
    }

    private void Update()
    {
        if (!isMobile)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 touchPosVector3 = Input.mousePosition;
                
                touchPosVector3.z = dist;

                touchPos = _camera.ScreenToWorldPoint(touchPosVector3);

                if (Input.GetMouseButtonDown(0))
                {
                    deltaX = touchPos.x - _transform.position.x;
                }

                Moving();
            }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                Vector3 touchPosVector3 = touch.position;

                touchPosVector3.z = dist;

                touchPos = _camera.ScreenToWorldPoint(touchPosVector3);

                if (touch.phase == TouchPhase.Began)
                {
                    deltaX = touchPos.x - _transform.position.x;
                }

                Moving();
            }
        }
    }

    private void Moving()
    {
        _transform.position = new Vector3(Mathf.Clamp(touchPos.x - deltaX, -borderX, borderX), _transform.position.y, _transform.position.z);

        if (_transform.position.x >= deltaX || _transform.position.x <= -deltaX)
        {
            deltaX = touchPos.x - _transform.position.x;
        }
    }
}

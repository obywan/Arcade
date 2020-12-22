using Assets.Scripts.Heplers;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private int leftScreenOffset = 0;
    [SerializeField] private int rightScreenOffset = 0;
    [SerializeField] private int topScreenOffset = 0;
    [SerializeField] private int bottomScreenOffset = 0;

    private PuckController puckController;
    private Vector3 worldMousePosition;
    private ProjectileLaunchHepler touchSpeedHelper;

    private void Start()
    {
        touchSpeedHelper = new ProjectileLaunchHepler(3);
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            MakeRaycast(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            OnMouseMoved(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnMouseRelease();
        }
#elif UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                MakeRaycast(Input.touches[0].position);
            }

            if (Input.touches[0].phase == TouchPhase.Moved || Input.touches[0].phase == TouchPhase.Stationary)
            {
                OnMouseMoved(Input.touches[0].position);
            }

            if (Input.touches[0].phase == TouchPhase.Ended)
            {
                OnMouseRelease();
            }
        }

#endif
    }

    private void OnMouseRelease()
    {
        puckController?.Launch(touchSpeedHelper.GetDirection(), touchSpeedHelper.GetAvarage());
        touchSpeedHelper.ClearPath();

        puckController = null;
    }

    private void OnMouseMoved(Vector2 mp)
    {
        if(CheckScreenOffset(mp))
        {
            OnMouseRelease();
            return;
        }

        if (!puckController)
        {
            MakeRaycast(mp);
            return;
        }

        worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        touchSpeedHelper.AddToPath(worldMousePosition);
        puckController?.Drag(worldMousePosition);
    }

    private void OnHit(Transform t)
    {        
        touchSpeedHelper.ClearPath();
        puckController = t.GetComponent<PuckController>();
        puckController.Grab();
    }

    private void MakeRaycast(Vector2 mp)
    {
        if (CheckScreenOffset(mp))
        {
            return;
        }

        RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(mp));
        if (rayHit)
        {
            //Debug.Log(rayHit.transform.name);
            if (rayHit.transform.CompareTag(PuckController.PUCK_TAG))
            {
                OnHit(rayHit.transform);
            }
        }
    }

    private bool CheckScreenOffset(Vector2 mp)
    {
        return mp.x < leftScreenOffset || mp.x > Screen.width - rightScreenOffset || mp.y > Screen.height - topScreenOffset || mp.y < bottomScreenOffset;
    }
}

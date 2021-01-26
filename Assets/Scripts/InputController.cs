using Assets.Scripts.Heplers;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private PuckController puckController;
    private Vector3 worldMousePosition;
    private ProjectileLaunchHepler touchSpeedHelper;

    private void Start()
    {
        touchSpeedHelper = new ProjectileLaunchHepler(3);
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            worldMousePosition = Camera.main.ScreenToWorldPoint(Input.touches[0].position);

            if (Input.touches[0].phase == TouchPhase.Began)
            {
                MakeRaycast(Input.touches[0].position);
            }

            if (Input.touches[0].phase == TouchPhase.Moved)
            {
                OnMouseMoved(Input.touches[0].position);
            }

            if (Input.touches[0].phase == TouchPhase.Ended)
            {
                OnMouseRelease();
            }
        }

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

        touchSpeedHelper.AddToPath(worldMousePosition);
        puckController.Drag(worldMousePosition);
    }

    private void OnHit(Transform t)
    {        
        touchSpeedHelper.ClearPath();
        puckController = t.GetComponent<PuckController>();
        puckController.Grab(worldMousePosition);
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
            if (rayHit.transform.CompareTag(PuckController.PUCK_TAG))
            {
                OnHit(rayHit.transform);
            }
        }
    }

    private bool CheckScreenOffset(Vector2 mp)
    {
        return mp.x < ScreenHepler.InputLimits.x || mp.x > ScreenHepler.InputLimits.width || mp.y < ScreenHepler.InputLimits.y || mp.y > ScreenHepler.InputLimits.height;
    }
}

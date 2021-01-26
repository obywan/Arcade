using UnityEngine;
using System.Collections;
using Assets.Scripts.Heplers;
using System.Collections.Generic;

public class PuckController : MonoBehaviour
{
    public const string PUCK_TAG = "Puck";

    public float launchBoots = 10f;
    public float maxSpeed = 10f;

    private Rigidbody2D rb2d;
    private bool free = true;
    private Vector2 targetPos;
    private Bounds currentBounds;

    private bool correctionNeeded = false;
    private Vector2 velocityCorrectionVector = Vector2.one;
    private List<ContactPoint2D> contactPoints = new List<ContactPoint2D>();
    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        currentBounds = GetComponent<Collider2D>().bounds;
    }

    private void FixedUpdate()
    {
        contactPoints.Clear();

        if(free)
        {
            RespectScreenBoundaries();
        }
        else
        {
            Move(targetPos);
        }

        StartCoroutine(ApplyBounce());
    }

    public void Grab(Vector2 p)
    {
        free = false;
        rb2d.velocity = Vector2.zero;
        targetPos = p;
    }

    public void Drag(Vector2 p)
    {
        free = false;
        targetPos = p;
    }

    public void Move(Vector2 p)
    {
        rb2d.MovePosition(p);
    }

    public void Launch(Vector2 direction, float force)
    {
        free = true;
        Vector2 vel = Vector2.ClampMagnitude(direction * force * launchBoots, maxSpeed);
        rb2d.velocity = vel;
    }

    /// <summary>
    /// Bounce off the obstacles
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        contactPoints.Add(collision.contacts[0]);
    }

    private IEnumerator ApplyBounce()
    {
        yield return waitForFixedUpdate;

        switch (contactPoints.Count)
        {
            case 0:
                break;
            default:
                ProcessContacts();
                break;
        }
    }

    private void ProcessContacts()
    {
        Vector2 result = Vector2.zero;

        for(int i = 0; i < contactPoints.Count; i++)
        {
            result += contactPoints[i].normal;
            DealDamage(i);
        }

        rb2d.velocity = Vector2.Reflect(rb2d.velocity, (result).normalized);
    }

    private void DealDamage(int index)
    {
        if (contactPoints[index].collider.gameObject.CompareTag(Brick.BRICK_TAG))
            contactPoints[index].collider.gameObject.GetComponent<Brick>()?.TakeDamage(1);
    }

    /// <summary>
    /// Bounce off the screen edges
    /// </summary>
    private void RespectScreenBoundaries()
    {
        velocityCorrectionVector = Vector2.one;
        if ((rb2d.velocity.x < float.Epsilon && transform.position.x < ScreenHepler.WorldLimits.x + currentBounds.size.x / 2) ||
            (rb2d.velocity.x > float.Epsilon && transform.position.x > ScreenHepler.WorldLimits.width - currentBounds.size.x / 2))
        {
            velocityCorrectionVector.x = -1;
            correctionNeeded = true;
        }
        if ((rb2d.velocity.y < float.Epsilon && transform.position.y < ScreenHepler.WorldLimits.y + currentBounds.size.y / 2) ||
            (rb2d.velocity.y > float.Epsilon && transform.position.y > ScreenHepler.WorldLimits.height - currentBounds.size.y / 2))
        {
            velocityCorrectionVector.y = -1;
            correctionNeeded = true;
        }

        if (correctionNeeded)
            rb2d.velocity *= velocityCorrectionVector;
    }
}

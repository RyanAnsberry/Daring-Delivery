using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryArrow : MonoBehaviour
{
    public Transform target;
    public float buffer;

    public Color farColor;
    public Color nearColor;
    public float maxDistance;

    private SpriteRenderer rend;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (target != null)
        {
            Vector2 difference = transform.position - target.position;
            float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle + buffer);
            rend.color = Color.Lerp(nearColor, farColor, DistanceToDestination());
        }
    }

    float DistanceToDestination()
    {
        return Mathf.Clamp01(Vector2.Distance(transform.position, target.position) / maxDistance);
    }
}

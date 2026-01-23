using UnityEngine;

public class PreventPlace : MonoBehaviour
{
    public string blockingTag = "Tower";
    public LayerMask blockingLayers;

    private Collider myCollider;

    private void Start()
    {
        myCollider = GetComponent<Collider>();

        if (myCollider == null)
        {
            Debug.LogError("PreventPlace requires a Collider.");
            return;
        }

        // Delay one physics frame so colliders are registered
        Invoke(nameof(CheckAndDestroy), 0f);
    }

    private void CheckAndDestroy()
    {
        Bounds bounds = myCollider.bounds;

        Collider[] hits = Physics.OverlapBox(
            bounds.center,
            bounds.extents,
            transform.rotation,
            blockingLayers
        );

        foreach (Collider hit in hits)
        {
            if (hit == myCollider)
                continue;

            if (hit.CompareTag(blockingTag))
            {
                Destroy(gameObject);
                return;
            }
        }
    }
}

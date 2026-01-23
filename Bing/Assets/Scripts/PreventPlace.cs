using UnityEngine;

public class PreventPlace : MonoBehaviour
{
    [Tooltip("Tag that blocks placement")]
    public string blockingTag = "Tower";

    [Tooltip("Layer mask for objects that can block placement (optional)")]
    public LayerMask blockingLayers;

    private Collider myCollider;

    private void Awake()
    {
        myCollider = GetComponent<Collider>();

        if (myCollider == null)
        {
            Debug.LogError("PreventPlace requires a Collider on the same GameObject.");
        }
    }

    /// <summary>
    /// Returns true if placement is allowed at the current position
    /// </summary>
    public bool CanPlace()
    {
        if (myCollider == null)
            return false;

        Bounds bounds = myCollider.bounds;

        Collider[] hits = Physics.OverlapBox(
            bounds.center,
            bounds.extents,
            transform.rotation,
            blockingLayers
        );

        foreach (Collider hit in hits)
        {
            // Ignore self
            if (hit == myCollider)
                continue;

            // Block placement if overlapping a Tower
            if (hit.CompareTag(blockingTag))
            {
                return false;
            }
        }

        return true;
    }
}

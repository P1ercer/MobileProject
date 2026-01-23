using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PreventPlace : MonoBehaviour
{
    [Tooltip("Tag of objects that block placement")]
    public string blockingTag = "Tower";

    private void Awake()
    {
        Collider col = GetComponent<Collider>();
        if (col == null)
        {
            Debug.LogError("PreventPlace requires a Collider!");
        }
        else
        {
            col.isTrigger = true; // Make it a trigger so it doesn’t physically block
        }
    }

    // Trigger detection
    private void OnTriggerEnter(Collider other)
    {
        // Only act on objects tagged as Tower
        if (other.CompareTag(blockingTag))
        {
            Debug.Log($"Tower {other.name} placed on NoPlacey zone!");

            // Refund the tower cost
            TowerController tc = other.GetComponent<TowerController>();
            if (tc != null)
            {
                int refund = tc.TowerPrice;
                Debug.Log($"Refunding {refund} gold to player");
                CurrencyManager.Instance.AddGold(refund);
            }

            // Destroy the tower immediately
            Destroy(other.gameObject);
        }
    }
}

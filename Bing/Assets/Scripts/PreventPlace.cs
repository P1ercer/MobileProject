using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class PreventPlace : MonoBehaviour
{

    private static TowerUIManager Instance;
    [Tooltip("Tag of objects that block placement")]
    public string blockingTag = "Tower";

    public BoxCollider2D col;


    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        col.isTrigger = true;
    }

    public bool IsBlocking(Vector2 position)
    {
        return col.OverlapPoint(position);
    }

    // Trigger detection
    private void OnTriggerEnter2D(Collider2D other)
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

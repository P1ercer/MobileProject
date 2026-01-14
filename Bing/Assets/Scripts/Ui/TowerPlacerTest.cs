using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacerTest : MonoBehaviour
{
    public LayerMask placementLayer;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (TowerSelectionUI.Instance.GetSelectedTowerPrefab() == null)
                return;

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0f, placementLayer);

            if (!TowerSelectionUI.Instance.HasSelection())
                return;

            Instantiate(
                TowerSelectionUI.Instance.GetSelectedTowerPrefab(),
                hit.point,
                Quaternion.identity
            );

        }
    }
}

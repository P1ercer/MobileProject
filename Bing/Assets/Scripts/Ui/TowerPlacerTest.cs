using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerPlacerTest : MonoBehaviour
{
    public LayerMask placementLayer;
    public bool requireGround = false;
    public bool clearSelectionAfterPlace = true;

    void Update()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        if (TowerSelectionUI.Instance == null)
        {
            Debug.LogError("TowerSelectionUI instance missing!");
            return;
        }

        if (!TowerSelectionUI.Instance.HasSelection())
        {
            Debug.Log("No tower selected");
            return;
        }

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        if (requireGround)
        {
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero, 0f, placementLayer);
            if (!hit)
            {
                Debug.Log("Clicked invalid placement area");
                return;
            }

            mouseWorldPos = hit.point;
            mouseWorldPos.z = 0f;
        }

        Instantiate(
            TowerSelectionUI.Instance.GetSelectedTowerPrefab(),
            mouseWorldPos,
            Quaternion.identity
        );

        if (clearSelectionAfterPlace)
            TowerSelectionUI.Instance.ClearSelection();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Camera Camera;
    [SerializeField] private RectTransform selectionBox;
    [SerializeField] private LayerMask unitLayers;
    [SerializeField] private LayerMask floorLayers;


    private float DragDelay = 0.1f;

    private float MouseDownTime;
    private Vector2 startMousePosition;

    //private int attackPossibleRadius = 15;
    //private int unitDistance;
    

    //private bool attack = false;

    private void Update()
    {
        HandleSelectionInputs();
        HandleOrderInputs();

        //les enemy doivent etre des navmesh agents de type "player"  ??
    }

    private void HandleOrderInputs()
    {
        //UseCapacity: click with M2
        //Attack: click on an enemy with M1
        //MoveTo: click the floor with M1

        if (Input.GetKeyUp(KeyCode.Mouse2) && SelectionManager.Instance.SelectedUnits.Count > 0)
        {
            foreach (SelectableUnit unit in SelectionManager.Instance.SelectedUnits)
            {
                unit.UseCapacity();
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1) && SelectionManager.Instance.SelectedUnits.Count > 0)
        {

            if (Physics.Raycast(Camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent<scriptEnemy>(out scriptEnemy enemyUnit))
                {
                    
                    foreach (SelectableUnit unit in SelectionManager.Instance.SelectedUnits)
                    {
                        StartCoroutine(unit.HandleAttack(enemyUnit));
                        //if there is no enemy around, we should stop the attack coroutine
                    }
                }
                else
                {
                    foreach (SelectableUnit unit in SelectionManager.Instance.SelectedUnits)
                    {
                        unit.MoveTo(hit.point);
                        //print("moving");
                    }
                }
            }
        }
    }


        private void HandleSelectionInputs()
        {
        //click with M0 to select one
        //hold M0 and drag the mouse to select many
        //hold shift and click with M0 to add or remove from selection
        //click with M0 to deselect all

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            selectionBox.sizeDelta = Vector2.zero;
            selectionBox.gameObject.SetActive(true);
            startMousePosition = Input.mousePosition;
            MouseDownTime = Time.time;
        }
        else if(Input.GetKey(KeyCode.Mouse0) && MouseDownTime + DragDelay < Time.time)
        {
            ResizeSelectionBox();
        }
        else if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            selectionBox.sizeDelta = Vector2.zero;
            selectionBox.gameObject.SetActive(false);

            if (Physics.Raycast(Camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, unitLayers) && hit.collider.TryGetComponent<SelectableUnit>(out SelectableUnit unit))
            {
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    if(SelectionManager.Instance.isSelected(unit))
                    {
                        SelectionManager.Instance.Deselect(unit);
                    }
                    else
                    {
                        SelectionManager.Instance.Select(unit);
                    }
                }
                else
                {
                    SelectionManager.Instance.DeselectAll();
                    SelectionManager.Instance.Select(unit);
                }
            }
            else if (MouseDownTime + DragDelay > Time.time)
            {
                SelectionManager.Instance.DeselectAll();
            }


            MouseDownTime = 0;
        }

       
    }

    private void ResizeSelectionBox()
    {
        float width = Input.mousePosition.x - startMousePosition.x;
        float height = Input.mousePosition.y - startMousePosition.y;

        selectionBox.anchoredPosition = startMousePosition + new Vector2(width / 2, height / 2);
        selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));

        Bounds bounds = new Bounds(selectionBox.anchoredPosition, selectionBox.sizeDelta);

        for (int i = 0; i < SelectionManager.Instance.AvailableUnits.Count; i++)
        {
            if (UnitIsInSelectionBox(Camera.WorldToScreenPoint(SelectionManager.Instance.AvailableUnits[i].transform.position), bounds)) 
            {
                SelectionManager.Instance.Select(SelectionManager.Instance.AvailableUnits[i]);
            }
            else
            {
                SelectionManager.Instance.Deselect(SelectionManager.Instance.AvailableUnits[i]);
            }
        }
    }

    private bool UnitIsInSelectionBox(Vector2 Position, Bounds Bounds)
    {
        return Position.x > Bounds.min.x && Position.x < Bounds.max.x && Position.y > Bounds.min.y && Position.y < Bounds.max.y;
    }
}

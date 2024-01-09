using System.Collections.Generic;
using UnityEngine;

public class playerInput : MonoBehaviour
{
    [SerializeField]
    private Camera Camera;
    [SerializeField]
    private RectTransform SelectionBox;
    [SerializeField]
    private LayerMask UnitLayers;
    [SerializeField]
    private LayerMask FloorLayers;
    [SerializeField]
    private float DragDelay = 0.1f;

    private float MouseDownTime;
    private string Run_Animation = "IsRunning";
    private string Attack_Animation = "IsAttacking";
    private Vector2 StartMousePosition;

    private float distance;

    

    private Animator anim;


    

    private HashSet<selectUnit> newlySelectedUnits = new HashSet<selectUnit>();
    private HashSet<selectUnit> deselectedUnits = new HashSet<selectUnit>();

    private void Update()
    {
        HandleSelectionInputs();
        HandleMovementInputs();
    }

    private void HandleMovementInputs()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1) && Selection.Instance.SelectedUnits.Count > 0)
        {
            if (Physics.Raycast(Camera.ScreenPointToRay(Input.mousePosition), out RaycastHit Hit, FloorLayers))
            {
                foreach (selectUnit unit in Selection.Instance.SelectedUnits)
                {
                    unit.MoveTo(Hit.point);
                    anim = unit.gameObject.GetComponent<Animator>();
                    anim.SetBool(Run_Animation,true);
                    
                }

               

                
            }
        }
    }

    private void HandleSelectionInputs()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SelectionBox.sizeDelta = Vector2.zero;
            SelectionBox.gameObject.SetActive(true);
            StartMousePosition = Input.mousePosition;
            MouseDownTime = Time.time;
        }
        else if (Input.GetKey(KeyCode.Mouse0) && MouseDownTime + DragDelay < Time.time)
        {
            ResizeSelectionBox();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            SelectionBox.sizeDelta = Vector2.zero;
            SelectionBox.gameObject.SetActive(false);

            foreach (selectUnit newUnit in newlySelectedUnits)
            {
                Selection.Instance.Select(newUnit);
            }
            foreach (selectUnit deselectedUnit in deselectedUnits)
            {
                Selection.Instance.Deselect(deselectedUnit);
            }

            newlySelectedUnits.Clear();
            deselectedUnits.Clear();

            if (Physics.Raycast(Camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, UnitLayers)
                && hit.collider.TryGetComponent<selectUnit>(out selectUnit unit))
            {
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    if (Selection.Instance.IsSelected(unit))
                    {
                        Selection.Instance.Deselect(unit);
                    }
                    else
                    {
                        Selection.Instance.Select(unit);
                    }
                }
                else
                {
                    Selection.Instance.DeselectAll();
                    Selection.Instance.Select(unit);
                }
            }
            else if (MouseDownTime + DragDelay > Time.time)
            {
                Selection.Instance.DeselectAll();
            }

            MouseDownTime = 0;
        }
    }

    private void ResizeSelectionBox()
    {
        float width = Input.mousePosition.x - StartMousePosition.x;
        float height = Input.mousePosition.y - StartMousePosition.y;

        SelectionBox.anchoredPosition = StartMousePosition + new Vector2(width / 2, height / 2);
        SelectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));

        Bounds bounds = new Bounds(SelectionBox.anchoredPosition, SelectionBox.sizeDelta);

        for (int i = 0; i < Selection.Instance.AvailableUnits.Count; i++)
        {
            if (UnitIsInSelectionBox(Camera.WorldToScreenPoint(Selection.Instance.AvailableUnits[i].transform.position), bounds))
            {
                if (!Selection.Instance.IsSelected(Selection.Instance.AvailableUnits[i]))
                {
                    newlySelectedUnits.Add(Selection.Instance.AvailableUnits[i]);
                }
                deselectedUnits.Remove(Selection.Instance.AvailableUnits[i]);
            }
            else
            {
                deselectedUnits.Add(Selection.Instance.AvailableUnits[i]);
                newlySelectedUnits.Remove(Selection.Instance.AvailableUnits[i]);
            }
        }
    }

    private bool UnitIsInSelectionBox(Vector2 Position, Bounds Bounds)
    {
        return Position.x > Bounds.min.x && Position.x < Bounds.max.x
            && Position.y > Bounds.min.y && Position.y < Bounds.max.y;
    }
}

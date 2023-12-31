using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Camera Camera;
    [SerializeField] private RectTransform selectionBox;
    [SerializeField] private LayerMask unitLayers;
    [SerializeField] private LayerMask floorLayers;

    public ControlledUnit controlledMagicien;
    public ControlledUnit controlledCavalier;
    //public ControlledUnit controlledSoignant;

    private float DragDelay = 0.1f;

    private float MouseDownTime;
    private Vector2 startMousePosition;

    private void Update()
    {
        HandleSelectionInputs();
        HandleMovementInputs();
        HandleAttackInputs();

        //qd on va instancier les player, il faut les renommer: PlayerMagicien et PlayerCavalier
    }


    private void HandleAttackInputs()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1) && SelectionManager.Instance.SelectedUnits.Count > 0)
        {
            if (Physics.Raycast(Camera.ScreenPointToRay(Input.mousePosition), out RaycastHit HitEnemy, unitLayers) && HitEnemy.collider.CompareTag("Enemy"))
            {
                print("enemy selectionné");
                //le navmesh s'effectue avec les agents "player". si les ennemis sont des agents de type "player" comme les players, ce raycast ne marche pas
                foreach (SelectableUnit unit in SelectionManager.Instance.SelectedUnits)
                {
                    //quand j'appuie sur un ennemy, mon player marche qd meme vers l'ennemi (la fct HandleMovementInputs s'execute), mais il faut que le player marche vers l'emmeni meme si il bouge 
                    unit.MoveTo(HitEnemy.collider.transform.position);
                    
                    if (unit.CompareTag("Magicien"))
                    {
                        controlledMagicien.Attack();
                    }
                    else if (unit.CompareTag("Cavalier"))
                    {
                        controlledCavalier.Attack();
                    }
                    
                }
            }
        }
    }
    private void HandleMovementInputs()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1) && SelectionManager.Instance.SelectedUnits.Count > 0)
        {
            if (Physics.Raycast(Camera.ScreenPointToRay(Input.mousePosition), out RaycastHit Hit, floorLayers))
            {
                foreach(SelectableUnit unit in SelectionManager.Instance.SelectedUnits)
                {
                    unit.MoveTo(Hit.point);
                    print("moving");
                }
            }
            
            
        }
    }
    private void HandleSelectionInputs()
    {
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

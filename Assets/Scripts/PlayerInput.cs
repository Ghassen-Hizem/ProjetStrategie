using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Camera Camera;
    [SerializeField] private RectTransform selectionBox;
    [SerializeField] private LayerMask unitLayers;
    [SerializeField] private LayerMask floorLayers;

    private int Layerunit;
    private int Layerfloor;

    public ControlledUnit controlledMagicien;
    public ControlledUnit controlledCavalier;
    //public ControlledUnit controlledSoignant;

    private float DragDelay = 0.1f;

    private float MouseDownTime;
    private Vector2 startMousePosition;

    /*
    private void Start()
    {
        Layerunit = LayerMask.GetMask("Units");
        Layerfloor = LayerMask.GetMask("Floor");
    }*/
    private void Update()
    {
        HandleSelectionInputs();
        HandleMovementInputs();
        HandleAttackInputs();
        //HandleOrderInputs();

        //tout les ennemis doivent appartenir à layer "Units" et doivent avoir le tag "Enemy" (ou alors changer mon code pour trouver les ennemis avec leurs scripts) , ils doivent etre des navmesh agents de type "player"
    }
    
    //Ce qu'il faut faire:
    //Quand on clique sur une position avec M1, on se deplace, sinon si on clique sur un enemy, on l'attaque. si il meurt, on attaque le plus proche dans notre zone d'attaque.si il n'y a aucun, ne rien faire
    //toutes les attaques doivent cibler un enemy
    

    /*
    private void HandleOrderInputs()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1) && SelectionManager.Instance.SelectedUnits.Count > 0)
        {
            if (Physics.Raycast(Camera.ScreenPointToRay(Input.mousePosition), out RaycastHit Hit, Layerfloor))
            {
                
                foreach (SelectableUnit unit in SelectionManager.Instance.SelectedUnits)
                {
                    unit.MoveTo(Hit.point);
                    print("moving");
                }
            }
            else if (Physics.Raycast(Camera.ScreenPointToRay(Input.mousePosition), out RaycastHit HitUnit, Layerunit) && HitUnit.collider.TryGetComponent<scriptTestEnemy>(out scriptTestEnemy Enemyunit))
            {
                print("enemy selected");
                
                foreach (SelectableUnit unit in SelectionManager.Instance.SelectedUnits)
                {
                    unit.Attack(Enemyunit);
                    print("attacking");
                }
            }
        }
    }
    */

    
    
    private void HandleAttackInputs()
    {
        //Attack: click with M2 after selecting some players
        //UseCapacity: hold C and click with M2

        if (Input.GetKey(KeyCode.C) && Input.GetKeyUp(KeyCode.Mouse2) && SelectionManager.Instance.SelectedUnits.Count > 0)
        { 
            foreach (SelectableUnit unit in SelectionManager.Instance.SelectedUnits)
            {
                unit.UseCapacity();
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse2) && SelectionManager.Instance.SelectedUnits.Count > 0)
        {
            foreach (SelectableUnit unit in SelectionManager.Instance.SelectedUnits)
            {
                unit.Attack();
            }
        }

    }

    
    private void HandleMovementInputs()
    {
        //click with M1 after selecting some players
        if (Input.GetKeyUp(KeyCode.Mouse1) && SelectionManager.Instance.SelectedUnits.Count > 0)
        {
            if (Physics.Raycast(Camera.ScreenPointToRay(Input.mousePosition), out RaycastHit Hit, floorLayers))
            {
                foreach(SelectableUnit unit in SelectionManager.Instance.SelectedUnits)
                {
                    unit.MoveTo(Hit.point);
                    //print("moving");
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

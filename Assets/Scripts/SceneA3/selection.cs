using System.Collections.Generic;

public class Selection
{
    private static Selection _instance;
    public static Selection Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Selection();
            }

            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }

    public HashSet<selectUnit> SelectedUnits = new HashSet<selectUnit>();
    public List<selectUnit> AvailableUnits = new List<selectUnit>();

    private Selection() { }

    public void Select(selectUnit Unit)
    {
        SelectedUnits.Add(Unit);
        Unit.OnSelected();
    }

    public void Deselect(selectUnit Unit)
    {
        Unit.OnDeselected();
        SelectedUnits.Remove(Unit);
    }

    public void DeselectAll()
    {
        foreach(selectUnit unit in SelectedUnits)
        {
            unit.OnDeselected();
        }
        SelectedUnits.Clear();
    }

    public bool IsSelected(selectUnit Unit)
    {
        return SelectedUnits.Contains(Unit);
    }
}
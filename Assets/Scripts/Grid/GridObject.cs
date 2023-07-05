using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private GridSystem gridSystem;
    private GridPosition gridPosition;
    private List<Unit> unitList;

    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
        this.unitList = new List<Unit>();
    }

    public void AddUnit(Unit unit)
    {
        unitList.Add(unit);
    }

    public List<Unit> GetUnits()
    {
        return unitList;
    }

    public void RemoveUnit(Unit unit)
    {
        unitList.Remove(unit);
    }

    public override string ToString()
    {
        string stringUnits = "";
        foreach(Unit currentUnit in unitList)
        {
            stringUnits += $"{currentUnit}\n";
        }
        return $"{gridPosition.ToString()} \n {stringUnits}";
    }

}

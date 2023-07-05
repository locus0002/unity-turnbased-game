using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    [SerializeField] private Transform gridDebugObjectPrefab;
    private GridSystem gridSystem;
    public static LevelGrid Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"There is more than one LevelGrid {transform} - {Instance}");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        gridSystem = new GridSystem(12, 12, 2f);
        gridSystem.CreateDebugObjects(gridDebugObjectPrefab);
    }

    public void AddUnitAtGridPosition(GridPosition gridPosition, Unit unit) 
    {
        GridObject currentGridObject = gridSystem.GetGridObject(gridPosition);
        if (currentGridObject != null) { currentGridObject.AddUnit(unit); }
    }
    public List<Unit> GetUnitListAtGridPosition(GridPosition gridPosition) 
    {
        GridObject currentGridObject = gridSystem.GetGridObject(gridPosition);
        if (currentGridObject != null) { return currentGridObject.GetUnits(); }
        return null;
    }
    public void ClearUnitAtGridPosition(GridPosition gridPosition, Unit unit) 
    {
        GridObject currentGridObject = gridSystem.GetGridObject(gridPosition);
        if (currentGridObject != null) { currentGridObject.RemoveUnit(unit); }
    }

    public void UnitMoveGridPosition(Unit unit, GridPosition fromPosition, GridPosition toPosition)
    {
        ClearUnitAtGridPosition(fromPosition, unit);
        AddUnitAtGridPosition(toPosition, unit);
    }

    public GridPosition GetGridPosition(Vector3 worldPosition) => gridSystem.GetGridPosition(worldPosition);
}

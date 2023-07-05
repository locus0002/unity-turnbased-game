using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * NOTE: This is a system, therefore, it is important that the script is executed
 * before others. 
 * We can set in the Project Settings > Script Execution Order > Drag and drop the system
 */
public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }
    public event EventHandler OnSelectedUnitChanged;
    enum MouseButton
    {
        Left = 0,
        Right = 1,
        Scroll = 2
    }
    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask mouseUnitLayerMask;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"There is more than one UnitActionSystem {transform} - {Instance}");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown((int)MouseButton.Left))
        {
            if (TryHandleUnitSelection()) { return; }
            selectedUnit?.Move(MouseWorld.GetPosition());
        }
    }

    private bool TryHandleUnitSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, mouseUnitLayerMask))
        {
            if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
            {
                SetSelectedUnit(unit);
                return true;
            }
        }
        return false;
    }

    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }

    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }
}

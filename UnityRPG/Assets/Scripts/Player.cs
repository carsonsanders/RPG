using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private InteractablePopup _popup;
    
    private CharacterController characterController;
    private IMover _mover;
    private Rotator _rotator;
    private Inventory _inventory;
    

    public Stats Stats { get; private set; }

    public Inventory GetInventory()
    {
        return _inventory;
    }

    public void popupBind(InteractablePopup popup)
    {
        _popup = popup;
        Debug.Log("player popup bound");
    }
    
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        _mover = new Mover(this);//new NavmeshMover(this); //Mover(this);
        _rotator = new Rotator(this);
        _inventory = GetComponent<Inventory>();

        PlayerInput.Instance.MoveModeTogglePressed += MoveModeTogglePressed;

        Stats = new Stats();
        Stats.Bind(_inventory);
        loadStats(GameStateMachine.Instance.GetStats()); //Gets the stats from character creation, stored in the gamestatemachine
    }

    private void loadStats(Dictionary<StatType, float> stats)
    {
        Stats.Add(StatType.INT, stats[StatType.INT]);
        Stats.Add(StatType.REF, stats[StatType.REF]);
        Stats.Add(StatType.TECH, stats[StatType.TECH]);
        Stats.Add(StatType.COOL, stats[StatType.COOL]);
        Stats.Add(StatType.ATTR, stats[StatType.ATTR]);
        Stats.Add(StatType.LUCK, stats[StatType.LUCK]);
        Stats.Add(StatType.BODY, stats[StatType.BODY]);
        Stats.Add(StatType.EMPATHY, stats[StatType.EMPATHY]);
    }

    private void MoveModeTogglePressed()
    {
        if (_mover is NavmeshMover)
            _mover = new Mover(this);
        else
            _mover = new NavmeshMover(this);
        
    }

    private void checkForInteractables()
    {
        RaycastHit[] _results = new RaycastHit[100];
        float _range = 1f;
        int _layerMask = LayerMask.GetMask("Default");
        
        Ray ray = Camera.main.ViewportPointToRay(Vector3.one / 2f); //ray in middle of camview
        int hits = Physics.RaycastNonAlloc(ray, _results, _range, _layerMask, QueryTriggerInteraction.Collide);

        RaycastHit nearest = new RaycastHit();
        double nearestDistance = double.MaxValue;
        for (int i = 0; i < hits; i++)
        {
            var distance = Vector3.Distance(transform.position, _results[i].point);
            if (distance < nearestDistance)
            {
                nearest = _results[i];
                nearestDistance = distance;
            }
            
        }
        if(nearest.transform != null)
        {
            var interactable = nearest.collider.GetComponent<IInteractable>();
            if (interactable.canInteract())
            {
                _popup.OpenMessagePanel(interactable.GetActionText());
                if (PlayerInput.Instance.GetKeyDown(KeyCode.F))
                {
                    interactable.Interact(this);
                }
            }
        }
        else
        {
            _popup.CloseMessagePanel();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Pause.Active)
            return;
        
        _mover.Tick();
        _rotator.Tick();
        checkForInteractables(); 
    }
}
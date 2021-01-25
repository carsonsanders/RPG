using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Entity))]
public class EntityAnimator : MonoBehaviour
{
    private Animator _animator;
    private Entity _entity;
    private static readonly int Die = Animator.StringToHash("Die");

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _entity = GetComponent<Entity>();
        _entity.OnDied += () => _animator.SetBool(Die, true);
    }


}

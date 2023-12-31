using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagicienInput : MonoBehaviour
{
    public ControlledUnit controlledUnit;

    public void Attack()
    {
        controlledUnit.Attack();
    }
}

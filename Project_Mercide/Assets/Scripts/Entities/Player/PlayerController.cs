using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntityController
{
    [SerializeField]
    private int playerIndex;
    public int PlayerIndex { get => playerIndex; set => playerIndex = value; }

    protected override void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            //Holster.Shoot();
        }
    }
}

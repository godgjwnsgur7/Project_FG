using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using FGDefine;

public class ActiveCharacter : Character
{
    public override void Idle(CharacterParam param = null)
    {
        base.Idle(param);
    }

    public override void Move(CharacterParam param)
    {
        base.Move(param);

        if (param == null) return;

        var moveParam = param as CharacterMoveParam;

        if (moveParam != null)
        {

        }
    }
}
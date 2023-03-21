using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialRoomConditionEvent : AbstractButtonEvent
{
    public RoomController roomController;

    public override void ExecuteEvent()
    {
        roomController.UpdateRoomConditions(this.gameObject);
    }
}

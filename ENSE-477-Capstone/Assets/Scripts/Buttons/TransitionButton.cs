using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ******************************************************************
// Purpose: uses everything in AbstractButton. Used as a base
//          class for buttons.
//
// Class Variables: none
// ******************************************************************
public class TransitionButton : AbstractButton
{
    public RoomController roomController;
    public Material green, parameterHighlight;
    private Renderer renderer;

    public override void OnPress()
    {
        renderer = GetComponent<Renderer>();

        transform.position = pressedPosition;
        affectedObject.ExecuteEvent();

        if (roomController.CheckRoomConditions())
        {
            renderer.material = green;
            initialMaterial = green;
        }

        else
        {
            foreach (bool condition in roomController.conditions)
            {
                if (!condition)
                {
                    GameObject parameter = roomController.roomParameters[roomController.conditions.IndexOf(condition)];
                    
                    if (parameter.GetComponent<AbstractButton>() != null)
                        parameter.GetComponent<AbstractButton>().ApplyHighlight(parameterHighlight);
                    
                    else if (parameter.GetComponent<AbstractGrabbable>() != null)
                        parameter.GetComponent<AbstractGrabbable>().ApplyHighlight(parameterHighlight);                    
                
                    else parameter.GetComponent<Renderer>().material = parameterHighlight;
                }
            }
        }
    }
}

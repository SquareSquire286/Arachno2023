using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleCreationController : AbstractButton
{
    public SpiderCreatorModel model;

    public override void OnPress()
    {
        transform.position = pressedPosition;

        if (model.GetRemainingSpiders() != 0)
            model.CreateSpider();

        // hard-coded special case for the Add One Spider button in Room 4
        if (affectedObject != null)
            affectedObject.ExecuteEvent();
    }

    public override void OnRelease()
    {
        transform.position = releasedPosition;
    }
}

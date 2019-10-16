using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface CollisionEvent : IEventSystemHandler
{
    void HandleCollision(CollisionHull2D a, CollisionHull2D b);
}

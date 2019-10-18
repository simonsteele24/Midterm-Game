using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface CollisionEvent : IEventSystemHandler

{
    // This function is an event that is broadcasted whenever a collision occurs
    void HandleCollision(CollisionHull2D a, CollisionHull2D b);
}

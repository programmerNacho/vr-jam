using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportNode : MonoBehaviour
{
    public Transform teleportTransform = null;
    public List<TeleportNode> connectedNodes = new List<TeleportNode>();


}

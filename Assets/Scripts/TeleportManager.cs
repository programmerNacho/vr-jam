using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    [SerializeField]
    private List<TeleportNode> nodes = new List<TeleportNode>();
    [SerializeField]
    private TeleportNode initialNodeWithPlayer = null;
    [SerializeField]
    private Camera eyesCamera = null;
    [SerializeField]
    private float angleThresholdSelect = 30;

    private Player player = null;
    private TeleportNode nodeWithPlayer = null;
    private TeleportNode selectedTeleportNode = null;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        TeleportPlayer(initialNodeWithPlayer);
    }

    private void OnEnable()
    {
        player.OnTeleportActionPressed.AddListener(PlayerTeleportActionPressed);
    }

    private void OnDisable()
    {
        player.OnTeleportActionPressed.RemoveListener(PlayerTeleportActionPressed);
    }

    private void TeleportPlayer(TeleportNode teleportNode)
    {
        player.transform.SetPositionAndRotation(teleportNode.teleportTransform.position, teleportNode.teleportTransform.rotation);
        nodeWithPlayer = teleportNode;
    }

    private void Update()
    {
        selectedTeleportNode = SelectTeleportNode();
    }

    private TeleportNode SelectTeleportNode()
    {
        if(nodeWithPlayer == null)
        {
            Debug.LogError("No nodeWithPlayer found.");
            return null;
        }

        if(nodeWithPlayer.connectedNodes.Count == 0)
        {
            Debug.LogWarning("The player is trapped in a TeleportNode.");
            return null;
        }

        TeleportNode teleportNodeSelected = null;
        float minAngle = float.MaxValue;
        Vector3 playerLookDirection = GetLookDirection();

        foreach (TeleportNode n in nodeWithPlayer.connectedNodes)
        {
            Vector3 playerToNodeDirection = n.transform.position - GetEyesCameraWorldPosition();
            playerToNodeDirection.Normalize();

            float angle = Vector3.Angle(playerLookDirection, playerToNodeDirection);

            if(angle < angleThresholdSelect && angle < minAngle)
            {
                teleportNodeSelected = n;
                minAngle = angle;
            }
        }

        return teleportNodeSelected;
    }

    private Vector3 GetLookDirection()
    {
        return eyesCamera.transform.forward;
    }

    private Vector3 GetEyesCameraWorldPosition()
    {
        return eyesCamera.transform.position;
    }

    private void PlayerTeleportActionPressed()
    {
        if(selectedTeleportNode == null)
        {
            Debug.LogWarning("Can't teleport. No selectedTeleportNode.");
            return;
        }

        TeleportPlayer(selectedTeleportNode);
    }
}

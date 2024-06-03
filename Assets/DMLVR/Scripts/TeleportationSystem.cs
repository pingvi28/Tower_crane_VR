using UnityEngine;

public class TeleportationSystem : MonoBehaviour
{
    private InteractiveTeleportAnchor currentInteractiveTeleportAnchor;

    public void Teleportation(Vector3 teleportationPoint, Vector3 cameraPosition)
    {
        var currentPosition = transform;
        var nextPosition = currentPosition.position + teleportationPoint -
                           cameraPosition;
        nextPosition.y = teleportationPoint.y;
        currentPosition.position = nextPosition;
    }

    public void Teleportation(InteractiveTeleportAnchor interactiveTeleportAnchor, Vector3 cameraPosition)
    {
        if (currentInteractiveTeleportAnchor != null)
        {
            EnablingCurrentInteractiveAnchor();
        }

        currentInteractiveTeleportAnchor = interactiveTeleportAnchor;
        DisablingCurrentInteractiveAnchor();

        var currentPosition = transform;
        var anchorPosition = interactiveTeleportAnchor.Anchor.position;
        var nextPosition = currentPosition.position + anchorPosition -
                           cameraPosition;
        nextPosition.y = anchorPosition.y;
        currentPosition.position = nextPosition;
    }

    private void EnablingCurrentInteractiveAnchor()
    {
        currentInteractiveTeleportAnchor.Enabling();
    }

    private void DisablingCurrentInteractiveAnchor()
    {
        currentInteractiveTeleportAnchor.Disabling();
    }
}
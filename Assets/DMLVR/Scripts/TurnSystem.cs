using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class TurnSystem : MonoBehaviour
{
    [SerializeField] private int angle;

    public Vector3 VRTurn(Vector3 playerCamera, Vector2 input)
    {
        var cardinal = CardinalUtility.GetNearestCardinal(input);
        switch (cardinal)
        {
            case Cardinal.South:
                transform.Rotate(0, 180f, 0);
                break;
            case Cardinal.East:
                transform.Rotate(0, angle, 0);
                break;
            case Cardinal.West:
                transform.Rotate(0, -angle, 0);
                break;
        }

        return new Vector3(playerCamera.x, transform.position.y, playerCamera.z);
    }
}
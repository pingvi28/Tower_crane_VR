using UnityEngine;

public class ResizableInteractiveTeleportAnchor : InteractiveTeleportAnchor
{
    [SerializeField] private float sizeScale;

    private void Awake()
    {
        hover.AddListener(IncreasingSize);
        dehover.AddListener(ReducingSize);
    }

    private void IncreasingSize()
    {
        var transform1 = transform;
        var localScale = transform1.localScale;
        localScale = new Vector3(localScale.x * sizeScale, localScale.y, localScale.z * sizeScale);
        transform1.localScale = localScale;
    }

    private void ReducingSize()
    {
        var transform1 = transform;
        var localScale = transform1.localScale;
        localScale = new Vector3(localScale.x / sizeScale, localScale.y, localScale.z / sizeScale);
        transform1.localScale = localScale;
    }
}
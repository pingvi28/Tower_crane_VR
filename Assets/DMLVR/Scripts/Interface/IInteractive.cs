using UnityEngine.Events;

public interface IInteractive
{
    public bool IsHover { get; }
    public void Hover(InteractiveController interactiveController);

    public void Dehover(InteractiveController interactiveController);

    public void SelectEntered(InteractiveController interactiveController);

    public void SelectExited(InteractiveController interactiveController);

    public void Activate(InteractiveController interactiveController);
}
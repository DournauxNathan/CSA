public interface IInteractable
{
    public string InteractionPrompt { get; }
    public bool isActive { get; set; }
    public float coolDown { get; set; }

    public bool Interact(bool isActive);
    public void Toogle();
    public void Activate();
    public void Deactivate();
    public void Timer();
}

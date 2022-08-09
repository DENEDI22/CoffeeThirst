public interface IInteractable
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns>If returns TRUE, this object will be deselected. If FALSE, object will NOT be deselected.</returns>
    public bool OnInteract();
    public void Select();
    public void Deselect();
}
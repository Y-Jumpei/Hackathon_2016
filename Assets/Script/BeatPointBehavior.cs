using UnityEngine;

public class BeatPointBehavior : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    public int activatedCount = 0;

    public Material activeMaterial;
    public Material inactiveMaterial;

    public void Pulse()
    {
        activatedCount = 10;
        Activate();
    }

    public void Activate()
    {
        meshRenderer.material = activeMaterial;
    }

    public void Inactivate()
    {
        meshRenderer.material = inactiveMaterial;
    }

    public void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = inactiveMaterial;
    }

    public void Update()
    {
        activatedCount--;
        if (activatedCount == 1)
        {
            activatedCount = 0;
            Inactivate();
        }
    }
}

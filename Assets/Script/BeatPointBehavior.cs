using UnityEngine;

public class BeatPointBehavior : MonoBehaviour
{
    private float scale = 0.3f;

    private MeshRenderer meshRenderer;

    public float defaultScale = 0.3f;
    public float pulsedScale = 0.8f;
    public int activatedCount = 0;

    public Material activeMaterial;
    public Material inactiveMaterial;

    public void Pulse()
    {
        scale = pulsedScale;
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
        //if (scale >= defaultScale)
        //{
        //    transform.localScale = new Vector3(scale, transform.localScale.y, scale);
        //    scale -= 0.05f;
        //}
    }
}

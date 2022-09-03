using UnityEngine;
using UnityEngine.Events;

public class Brick : MonoBehaviour
{
    public UnityEvent<int> onDestroyed;
    
    public int PointValue;

    void Start()
    {
        var renderer = GetComponentInChildren<Renderer>();

        MaterialPropertyBlock block = new MaterialPropertyBlock();
        switch (PointValue)
        {
            case 5 :
                block.SetColor("_BaseColor", new Color(0.2980392f, 0.2470588f, 0.5686275f));
                break;
            case 2:
                block.SetColor("_BaseColor", new Color(0.5686275f, 0.2705882f, 0.7137255f));
                break;
            case 1:
                block.SetColor("_BaseColor", new Color(0.7254902f, 0.345098f, 0.6470588f));
                break;
            default:
                block.SetColor("_BaseColor", new Color(1.0f, 0.3372549f, 0.4666667f));
                break;
        }
        renderer.SetPropertyBlock(block);
    }

    private void OnCollisionEnter(Collision other)
    {
        onDestroyed.Invoke(PointValue);
        
        //slight delay to be sure the ball have time to bounce
        Destroy(gameObject, 0.2f);
    }
}

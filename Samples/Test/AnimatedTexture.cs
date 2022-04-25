using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedTexture : MonoBehaviour
{
    [SerializeField]
    private Vector2 moveSpeed;

    [SerializeField]
    private Renderer rend;

    // Update is called once per frame
    void Update()
    {
        rend.material.SetTextureOffset(
            "_MainTex", 
            new Vector2(
                Time.time * moveSpeed.x, 
                Time.time * moveSpeed.y));
    }
}

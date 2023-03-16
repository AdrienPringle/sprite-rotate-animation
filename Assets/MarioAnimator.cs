using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum AnimationState
{
    Walk,
    RotateIn,
    RotateOut,
};


public class MarioAnimator : MonoBehaviour
{
    private Transform transformComponent;
    private Animator animatorComponent;
    private SpriteRenderer spriteRendererComponent;
    private AnimationState state;
    private Vector3 direction;
    private Vector3 oldDirection;

    public Sprite frontSprite;
    public Sprite backSprite;

    public Vector3 velocity;


    // Start is called before the first frame update
    void Awake()
    {
        animatorComponent = GetComponent<Animator>();
        transformComponent = GetComponent<Transform>();
        spriteRendererComponent = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        SetVelocity();

        Vector3 newDirection = new Vector3(Mathf.Sign(velocity.x), 0, Mathf.Sign(velocity.z));
        if (newDirection != oldDirection && newDirection != Vector3.zero)
        {
            animatorComponent.Play(AnimationState.RotateIn.ToString());
            direction = newDirection;
        }

        transformComponent.parent.position += Time.deltaTime * velocity;

    }

    public void OnRotateDone()
    {
        if (direction.x > 0)
            transformComponent.localScale = new Vector3(-1, 1, 1);
        else if (direction.x < 0)
            transformComponent.localScale = new Vector3(1, 1, 1);

        if (direction.z > 0)
            spriteRendererComponent.sprite = backSprite;
        else if (direction.z < 0)
            spriteRendererComponent.sprite = frontSprite;

        if (direction != Vector3.zero) oldDirection = direction;
    }

    private void SetVelocity(){
        //arbitrary code, could be anything
        
        var mousePos = Input.mousePosition;
        mousePos.x /= Screen.width;
        mousePos.y /= Screen.height;

        mousePos = mousePos * 2 - Vector3.one;

        velocity = new Vector3(mousePos.x, 0, mousePos.y);
    }
}

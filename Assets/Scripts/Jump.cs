using System;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private GameObject charSprite;
    [SerializeField] private float jumpStrength;
    [SerializeField] private float gravity = 2f;
    private bool grounded = true;
    private Vector3 startPos;
    private Vector3 velocity = Vector3.zero;
    private Animate _animate;

    private void Start()
    {
        startPos = charSprite.transform.localPosition;
        _animate = GetComponent<Animate>();
    }

    private void Update()
    {
        if (grounded) return;
        charSprite.transform.Translate(velocity * Time.deltaTime);

        velocity.y += gravity * -1 * Time.deltaTime;

        CheckGrounded();
    }

    public void DoJump()
    {
        if (!grounded) return;
        grounded = false;
        velocity = new Vector3(0, jumpStrength, 0);
        SetAnimations(true);
    }

    public void EndJump()
    {
        
    }

    private void CheckGrounded()
    {
        if (!(charSprite.transform.localPosition.y <= startPos.y)) return;
        grounded = true;
        charSprite.transform.localPosition = startPos;
        SetAnimations(false);
    }
    
    private void SetAnimations(bool isJumping)
    {
        if (object.ReferenceEquals(_animate, null)) return;
        
        _animate.SetSprite(isJumping ? "Jump" : "Idle");
    }
}
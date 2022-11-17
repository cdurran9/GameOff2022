using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;

public class Movement : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] TileBehaviors tileBehaviors;
    [SerializeField] float speed = 1f;
    [SerializeField] private float gizmoSize = 5;
    [SerializeField] private Jump jump;
    private Vector2 _moveIntent;
    private bool _hasJump;
    private Animate _animate;

    // Start is called before the first frame update
    void Start()
    {
        _hasJump = !object.ReferenceEquals(jump, null);
        _animate = GetComponent<Animate>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (_hasJump)
            {
                jump.DoJump();
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            if (_hasJump)
            {
                jump.EndJump();
            }
        }

        var pos = transform.position;

        _moveIntent = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * (speed * Time.deltaTime);

        SetAnimations();
        
        bool horizWalkable = GetIsWalkable(pos + new Vector3(_moveIntent.x, 0));
        bool vertWalkable = GetIsWalkable(pos + new Vector3(0, _moveIntent.y));
        transform.Translate(new Vector3(horizWalkable ? _moveIntent.x : 0, vertWalkable ? _moveIntent.y : 0), Space.Self);
    }

    bool GetIsWalkable(Vector3 worldPos)
    {
        var wtoc = tilemap.WorldToCell(worldPos);
        var tile = tilemap.GetTile(wtoc) as Tile;
       return tileBehaviors.IsWalkable(tile);
    }

    private void OnDrawGizmos()
    {
        Vector3 pos = transform.position;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(pos, new Vector3(_moveIntent.x, _moveIntent.y) * gizmoSize);
    }

    private void SetAnimations()
    {
        if (object.ReferenceEquals(_animate, null) || Mathf.Abs(_moveIntent.x) == 0) return;
        
        _animate.SetFlip(_moveIntent.x < 0);
    }
}

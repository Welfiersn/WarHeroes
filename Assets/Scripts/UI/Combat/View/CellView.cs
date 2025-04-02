using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
public class CellView : MonoBehaviour
{
    [SerializeField] private SpritesConfig _spritesConfig;
    [SerializeField] private SpriteRenderer _modelRenderer;
    [SerializeField] private SpriteRenderer _cellRender;

    private int _x;
    private int _y;

    public Action<int, int> OnClicked;
    public Action<int, int> OnMouseEntered;

    private void OnMouseDown()
    {
        OnClicked?.Invoke(_x, _y);
    }

    private void OnMouseEnter()
    {
        OnMouseEntered?.Invoke(_x, _y);
    }

    public void Init(int x, int y)
    {
        _cellRender = GetComponent<SpriteRenderer>();

        _x = x;
        _y = y;
    }

    public void ChangeModel(object sender, int id)
    {
        if (id == 999)
        {
            _modelRenderer.sprite = null;
            return;
        }

        _modelRenderer.sprite = _spritesConfig.Configs[id].Sprite;
    }

    public void Select(object sender, EventArgs eventArgs)
    {
        _cellRender.color = Color.gray;
    }

    public void Deselect(object sender, EventArgs eventArgs)
    {
        _cellRender.color = Color.white;
    }

    public void Flip(bool flipX)
    {
        _modelRenderer.flipX = flipX; 
    }
}

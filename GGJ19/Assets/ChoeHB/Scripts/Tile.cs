using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public static Tile[] east;
    public static Tile[,] tiles;

    public static void Initialize(int x, int y)
    {
        tiles = new Tile[x, y];
        east = new Tile[y];
    }

    public static void Enable() => Enable((x, y) => true);

    public static void Enable(Func<int, int, bool> enableChecker)
    {
        for (int x = 0; x < tiles.GetLength(0); x++)
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                bool isEnable = enableChecker(x, y);
                if (isEnable)
                    tiles[x, y].animator.Enable();
                else
                    tiles[x, y].animator.Disable();
            }

    }

    public static void Disable()
    {
        for (int x = 0; x < tiles.GetLength(0); x++)
            for (int y = 0; y < tiles.GetLength(1); y++)
                tiles[x, y].animator.Disable();
    }

    public enum State { Normal, Highlight, Warning }

    public static event Action<Tile> OnPointerEnter;
    public static event Action<Tile> OnPointerExit;
    public static event Action<Tile> OnPointerUp;
    public static event Action<Tile> OnPointerDown;

    private static Func<Tile, State> stateChecker;

    public static void SetStateGetter(Func<Tile, State> stateChecker)
    {
        if (Tile.stateChecker != null)
            throw new Exception("StateChecker가 이미 존재함");
        Tile.stateChecker = stateChecker;
    }

    public static void RemoveStateGetter()
    {
        stateChecker = null;
    }

    public float width;
    public float height;
    
    [SerializeField] TileAnimator m_animator;
    [SerializeField] float animatingTime = 0.3f;

    public TileAnimator animator => m_animator;

    public int x { get; private set; }
    public int y { get; private set; }

    public void SetPosition(int x, int y)
    {
        this.x = x;
        this.y = y;
        tiles[x, y] = this;

        int lastIndex = tiles.GetLength(0) - 1;

        if (x == lastIndex)
            east[y] = this;

        animator.Disable();
    }

    private void OnMouseExit()      => OnPointerExit?.Invoke(this);
    private void OnMouseEnter()     => OnPointerEnter?.Invoke(this);
    private void OnMouseUp()        => OnPointerUp?.Invoke(this);
    private void OnMouseDown()      => OnPointerDown?.Invoke(this);

    private void Update()
    {
        if (stateChecker == null)
        {
            animator.Normal();
            return;
        }

        switch (stateChecker(this))
        {
            case State.Normal:
                animator.Normal();
                break;

            case State.Highlight:
                animator.Highlight();
                break;
            case State.Warning:
                animator.Warning();
                break;
        }

    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressInt {

    public event Action OnUpdate;

    private int current_;
    public int current
    {
        get { return current_; }
        set {
            current_ = Mathf.Min(max, value);
            if (OnUpdate != null)
                OnUpdate();
        }
    }

    public int max { get; private set; }

    public bool isMax
    {
        get { return current == max; }
    }

    public ProgressInt(int max)
    {
        this.max = max;
    }

    public static ProgressInt operator +(ProgressInt progress, int value)
    {
        progress.current += value;
        return progress;
    }

    public static implicit operator float(ProgressInt progress)
    {
        if(progress.max <= progress.current)
            return 1;
        return progress.current / (float)progress.max;
    }
}

public class ProgressFloat
{
    public event Action OnUpdate;

    [SerializeField]
    private float current_;
    public float current
    {
        get { return current_; }
        set
        {
            current_ = Mathf.Min(max, value);
            if (OnUpdate != null)
                OnUpdate();
        }
    }

    private float max_;
    public float max
    {
        get { return max_; }
        set
        {
            max_ = value;
            current = current;
        }
    }

    public bool isMax
    {
        get { return current == max; }
    }

    public ProgressFloat(float max)
    {
        this.max = max;
    }

    public void Clear()
    {
        current = 0;
    }

    public static ProgressFloat  operator +(ProgressFloat progress, float value)
    {
        progress.current += value;
        return progress;
    }

    public static implicit operator float(ProgressFloat progress)
    {
        if (progress.max <= progress.current)
            return 1;
        return progress.current / progress.max;
    }
}


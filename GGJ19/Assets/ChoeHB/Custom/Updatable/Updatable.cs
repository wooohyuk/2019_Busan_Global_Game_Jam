using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0661
#pragma warning disable CS0660

public class Updatable<T>
{
    public event Action<T> OnAdd;       // 새로 추가된 값
    public event Action<T> OnUpdate;    // 신규 값
    public event Action<T, T> OnChange; // (기존 값, 신규 값)

    private T _value;
    [ShowInInspector]
    public T value
    {
        get { return _value; }
        set {
            if (_value.Equals(value))
                return;

            T oldValue = _value;
            T newValue = value;

            _value = value;

            OnAdd?.Invoke((dynamic)newValue - oldValue);
            OnUpdate?.Invoke(newValue);
            OnChange?.Invoke(oldValue, newValue);
        }
    }

    // OnUpdate +=로 하면 변경에 대한 이벤트는 바로 적용되어도
    // 1번은 초기화 해줘야 하기 때문에 이걸로 강제 이닛
    public void AddUpdateEvent(Action<T> OnUpdate)
    {
        this.OnUpdate += OnUpdate;
        OnUpdate?.Invoke(value);
    }

    public void RemoveUpdateEvent(Action<T> OnUpdate) => this.OnUpdate -= OnUpdate;

    public Updatable(T value = default(T))
    {
        _value = value;
    }

    public void Add(T t) => value += (dynamic)t;

    public static implicit operator T(Updatable<T> u) => u.value;

    public static Updatable<T> operator +(Updatable<T> u, T value)
    {
        u.value += (dynamic)value;
        return u;
    }
    public static Updatable<T> operator -(Updatable<T> u, T value)
    {
        u.value -= (dynamic)value;
        return u;
    }
    public static Updatable<T> operator ++(Updatable<T> u)
    {
        u.value = (dynamic)u.value + 1;
        return u;
    }
    public static Updatable<T> operator --(Updatable<T> u)
    {
        u.value = (dynamic)u.value - 1;
        return u;
    }

    public static bool operator ==(Updatable<T> uv, T value) => uv.value.Equals(value);
    public static bool operator !=(Updatable<T> uv, T value) => !uv.value.Equals(value);
                                                                 
    public static bool operator ==(T value, Updatable<T> uv) => uv.value.Equals(value);
    public static bool operator !=(T value, Updatable<T> uv) => !uv.value.Equals(value);

    public static bool operator <(Updatable<T> uv, T value) => (dynamic)uv.value < value;
    public static bool operator <(T value, Updatable<T> uv) => (dynamic)value < uv.value; 

    public static bool operator >(Updatable<T> uv, T value) => (dynamic)uv.value > value; 
    public static bool operator >(T value, Updatable<T> uv) => (dynamic)value > uv.value; 

    public static bool operator >=(Updatable<T> uv, T value) => (dynamic)uv.value >= value; 
    public static bool operator >=(T value, Updatable<T> uv) => (dynamic)value >= uv.value; 

    public static bool operator <=(Updatable<T> uv, T value) => (dynamic)uv.value <= value; 
    public static bool operator <=(T value, Updatable<T> uv) => (dynamic)value <= uv.value;

    public override string ToString() => value.ToString();
}


public class Int
{
    public event Action OnUpdate;
    private int value_;
    private int value
    {
        get { return value_; }
        set
        {
            value_ = value;
            if (OnUpdate != null)
                OnUpdate();
        }
    }

    public static implicit operator int(Int i)
    {
        return i.value;
    }

    public static Int operator +(Int i, int value)
    {
        i.value += value;
        return i;
    }

    public static Int operator -(Int i, int value)
    {
        i.value -= value;
        return i;
    }

    public static bool operator ==(Int i, int value) { return i.value.Equals(value); }
    public static bool operator ==(int value, Int i) { return i.value.Equals(value); }
    public static bool operator !=(Int i, int value) { return !i.value.Equals(value); }
    public static bool operator !=(int value, Int i) { return !i.value.Equals(value); }

    public static bool operator <(Int i, int value) { return i.value < value; }
    public static bool operator <(int value, Int i) { return value < i.value; }

    public static bool operator >(Int i, int value) { return i.value > value; }
    public static bool operator >(int value, Int i) { return value > i.value; }

    public static bool operator >=(Int i, int value) { return i.value >= value; }
    public static bool operator >=(int value, Int i) { return value >= i.value; }

    public static bool operator <=(Int i, int value) { return i.value <= value; }
    public static bool operator <=(int value, Int i) { return value <= i.value; }

    public override string ToString()
    {
        return value.ToString();
    }

}


public class Float
{
    public event Action OnUpdate;
    private float value_;
    private float value
    {
        get { return value_; }
        set {
            value_ = value;
            if (OnUpdate != null)
                OnUpdate();
        }
    }

    public static implicit operator float(Float f)
    {
        return f.value;
    }

    public static Float operator +(Float f, float value)
    {
        f.value += value;
        return f;
    }

    public static Float operator -(Float f, float value)
    {
        f.value -= value;
        return f;
    }

    public static bool operator ==(Float f, float value) { return f.value == value; }
    public static bool operator ==(float value, Float f) { return f.value == value; }
    public static bool operator !=(Float f, float value) { return f.value != value; }
    public static bool operator !=(float value, Float f) { return f.value != value; }

    public static bool operator <(Float f, float value) { return f.value < value; }
    public static bool operator <(float value, Float f) { return value < f.value; }

    public static bool operator >(Float f, float value) { return f.value > value; }
    public static bool operator >(float value, Float f) { return value > f.value; }

    public static bool operator >=(Float f, float value) { return f.value >= value; }
    public static bool operator >=(float value, Float f) { return value >= f.value; }

    public static bool operator <=(Float f, float value) { return f.value <= value; }
    public static bool operator <=(float value, Float f) { return value <= f.value; }

    public override string ToString()
    {
        return value.ToString();
    }

}


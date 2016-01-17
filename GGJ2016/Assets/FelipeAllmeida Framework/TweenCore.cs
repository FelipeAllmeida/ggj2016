using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum Ease
{
    LINEAR,
    QUAD_IN,
    QUAD_OUT,
    QUAD_IN_OUT,
    CUBIC_IN,
    CUBIC_OUT,
    CUBIC_IN_OUT,
    QUART_IN,
    QUART_OUT,
    QUART_IN_OUT,
    QUINT_IN,
    QUINT_OUT,
    QUINT_IN_OUT,
    SIN_IN,
    SIN_OUT,
    SIN_IN_OUT,
    EXP_IN,
    EXP_OUT,
    EXP_INT_OUT,
    CIRC_IN,
    CIRC_OUT,
    CIRC_IN_OUT,
}

public class TweenCore : MonoBehaviour
{

    #region Static Data

    private static GameObject _go;
    private static TweenCore _instance;
    public static TweenCore instance
    {
        get
        {
            if (_instance == null)
            {
                _go = new GameObject("TweenCore", typeof(TweenCore));
                DontDestroyOnLoad(_go);
                _instance = _go.GetComponent<TweenCore>();
            }
            return _instance;
        }
    }

    #endregion

    #region Private Data

    [SerializeField] private List<ATweenNodule> _nodulesList;
    [SerializeField] private List<ABezierCurve> _curvesList;

    #endregion

    void FixedUpdate()
    {
        if (_nodulesList != null)
        {
            List<ATweenNodule> __updatedNodulesList = new List<ATweenNodule>();

            for (int i = 0; i < _nodulesList.Count; i ++)
            {
                _nodulesList[i].AUpdate();
                if (!_nodulesList[i].finished && !_nodulesList[i].stopped)
                {
                    __updatedNodulesList.Add(_nodulesList[i]);
                }
            }
            _nodulesList.Clear();
            _nodulesList = __updatedNodulesList;
        }

        if (_curvesList != null)
        {
            List<ABezierCurve> __updatedCurvesList = new List<ABezierCurve>();
            for (int i = 0; i < _curvesList.Count; i++)
            {
                _curvesList[i].AUpdate();

                if (!_curvesList[i].finished && !_curvesList[i].stopped)
                {
                    __updatedCurvesList.Add(_curvesList[i]);
                }
            }
            _curvesList.Clear();
            _curvesList = __updatedCurvesList;
        }
    }

    public void AddNodule (ATweenNodule p_nodule)
    {
        if (_nodulesList == null)
        {
            _nodulesList = new List<ATweenNodule>();
        }
        _nodulesList.Add(p_nodule);
    }

    public void AddCurve (ABezierCurve p_curve)
    {
        if (_curvesList == null)
        {
            _curvesList = new List<ABezierCurve>();
        }
        _curvesList.Add(p_curve);
    }
}

public class ATweenNodule
{
    #region Private Data

    private float _start;
    private float _between;

    private float _duration;
    private int _timer;

    private Ease _ease;

    private bool _finished = false;
    private bool _stopped = false;

    private Action<float> _callback;

    #endregion

    #region Public Data

    public Action onFinished;

    public bool finished
    {
        get { return _finished; }
    }

    public bool stopped
    {
        get { return _stopped; }
    }

    #endregion
    public ATweenNodule (float p_startValue, float p_endValue, float p_duration, Ease p_ease, Action<float> p_callback)
    {
        _start = p_startValue;
        _between = p_endValue - _start;
        _duration = p_duration;
        _ease = p_ease;

        _callback = p_callback;
    }

    public virtual void AUpdate()
    {
        if (_timer >= (int)(_duration * 60))
        {
            if (onFinished != null)
            {
                onFinished();
            }
            _finished = true;
        }
        else
        {
            float __normalizedTime = (float)_timer / (60 * _duration);
            float __updateValue = BetweenValue(_start, _between, _duration, __normalizedTime);

            _callback(__updateValue);

            _timer += 1;
        }
    }
    public void Stop()
    {
        _stopped = true;
    }

    private float BetweenValue (float p_start, float p_between, float p_duration, float p_currentTime)
    {
        p_currentTime *= p_duration;

        switch (_ease)
        {
            case Ease.LINEAR:
                return p_start + p_between * p_currentTime / p_duration;

            case Ease.QUAD_IN:
                p_currentTime /= p_duration;
                return p_between * p_currentTime * p_currentTime + p_start;

            case Ease.QUAD_OUT:
                p_currentTime /= p_duration;
                return -p_between * p_currentTime * (p_currentTime - 2) + p_start;

            case Ease.QUAD_IN_OUT:
                p_currentTime /= p_duration / 2;
                if (p_currentTime < 1) return p_between / 2 * p_currentTime * p_currentTime + p_start;
                p_currentTime--;
                return -p_between / 2 * (p_currentTime * (p_currentTime - 2) - 1) + p_start;

            case Ease.CUBIC_IN:
                p_currentTime /= p_duration;
                return p_between * p_currentTime * p_currentTime * p_currentTime + p_start;

            case Ease.CUBIC_OUT:
                p_currentTime /= p_duration;
                p_currentTime--;
                return p_between * (p_currentTime * p_currentTime * p_currentTime + 1) + p_start;

            case Ease.CUBIC_IN_OUT:
                p_currentTime /= p_duration / 2;
                if (p_currentTime < 1) return p_between / 2 * p_currentTime * p_currentTime * p_currentTime + p_start;
                p_currentTime -= 2;
                return p_between / 2 * (p_currentTime * p_currentTime * p_currentTime + 2) + p_start;

            case Ease.QUART_IN:
                p_currentTime /= p_duration;
                return p_between * p_currentTime * p_currentTime * p_currentTime * p_currentTime + p_start;

            case Ease.QUART_OUT:
                p_currentTime /= p_duration;
                p_currentTime--;
                return -p_between * (p_currentTime * p_currentTime * p_currentTime * p_currentTime - 1) + p_start;

            case Ease.QUART_IN_OUT:
                p_currentTime /= p_duration / 2;
                if (p_currentTime < 1) return p_between / 2 * p_currentTime * p_currentTime * p_currentTime * p_currentTime + p_start;
                p_currentTime -= 2;
                return -p_between / 2 * (p_currentTime * p_currentTime * p_currentTime * p_currentTime - 2) + p_start;

            case Ease.QUINT_IN:
                p_currentTime /= p_duration;
                return p_between * p_currentTime * p_currentTime * p_currentTime * p_currentTime * p_currentTime + p_start;


            case Ease.QUINT_OUT:
                p_currentTime /= p_duration;
                p_currentTime--;
                return p_between * (p_currentTime * p_currentTime * p_currentTime * p_currentTime * p_currentTime + 1) + p_start;

            case Ease.QUINT_IN_OUT:
                p_currentTime /= p_duration / 2;
                if (p_currentTime < 1) return p_between / 2 * p_currentTime * p_currentTime * p_currentTime * p_currentTime * p_currentTime + p_start;
                p_currentTime -= 2;
                return p_between / 2 * (p_currentTime * p_currentTime * p_currentTime * p_currentTime * p_currentTime + 2) + p_start;

            case Ease.SIN_IN:
                return -p_between * Mathf.Cos(p_currentTime / p_duration * ((float)Mathf.PI / 2f)) + p_currentTime + p_start;

            case Ease.SIN_OUT:
                return p_between * Mathf.Sin(p_currentTime / p_duration * ((float)Math.PI / 2f)) + p_start;

            case Ease.SIN_IN_OUT:
                return -p_between / 2 * (Mathf.Cos((float)Math.PI * p_currentTime / p_duration) - 1f) + p_start;

            case Ease.EXP_IN:
                return p_between * Mathf.Pow(2, 10 * (p_currentTime / p_duration - 1)) + p_start;

            case Ease.EXP_OUT:
                return p_between * (-Mathf.Pow(2, -10 * p_currentTime / p_duration) + 1) + p_start;

            case Ease.EXP_INT_OUT:
                p_currentTime /= p_duration / 2;
                if (p_currentTime < 1) return p_between / 2 * Mathf.Pow(2, 10 * (p_currentTime - 1)) + p_start;
                p_currentTime--;
                return p_between / 2 * (-Mathf.Pow(2, -10 * p_currentTime) + 2) + p_start;

            case Ease.CIRC_IN:
                p_currentTime /= p_duration;
                return -p_between * (Mathf.Sqrt(1 - p_currentTime * p_currentTime) - 1) + p_start;

            case Ease.CIRC_OUT:
                p_currentTime /= p_duration;
                p_currentTime--;
                return p_between * Mathf.Sqrt(1 - p_currentTime * p_currentTime) + p_start;

            case Ease.CIRC_IN_OUT:
                p_currentTime /= p_duration / 2;
                if (p_currentTime < 1) return -p_between / 2 * (Mathf.Sqrt(1 - p_currentTime * p_currentTime) - 1) + p_start;
                p_currentTime -= 2;
                return p_between / 2 * (Mathf.Sqrt(1 - p_currentTime * p_currentTime) + 1) + p_start;

            default:
                return 0f;
        }
    }
}

public class ABezierCurve
{
    #region Private Data

    List<Vector3> _controlPoints;

    private float _duration;
    private int _timer = 0;

    private bool _finished = false;
    private bool _stopped = false;

    private Action<Vector3> _callback;

    #endregion

    #region Public Data

    public Action onFinished;

    public bool finished
    {
        get { return _finished; }
    }

    public bool stopped
    {
        get { return _stopped; }
    }

    #endregion

    public ABezierCurve (List<Vector3> p_controlPoints, float p_duration, Action<Vector3> p_callback)
    {
        _controlPoints = p_controlPoints;
        _duration = p_duration;
        _callback = p_callback;
    }

    public void AUpdate()
    {
        if (_timer >= (int)(_duration * 60))
        {
            if (onFinished != null)
            {
                onFinished();
            }
            _finished = true;
        }
        else
        {
            float __normalizedTime = (float)_timer / (60 * _duration);
            Vector3 __updatedPosition = PositionPointCalculator(__normalizedTime);
            _callback(__updatedPosition);
            _timer += 1;
        }
    }

    private Vector3 PositionPointCalculator(float p_normalizedTime)
    {
        Vector3 __positionPoint = Vector3.zero;
        int __totalControlPoints = _controlPoints.Count - 1;
        for (int i = 0; i < _controlPoints.Count; i ++)
        {
            int __binomial = Factorial(__totalControlPoints) / (Factorial(__totalControlPoints - i) * Factorial(i));
            float __oneMinusTime = Mathf.Pow(1f - p_normalizedTime, __totalControlPoints - i);
            float __timePow = Mathf.Pow(p_normalizedTime, i);

            __positionPoint += __binomial * __oneMinusTime * __timePow * _controlPoints[i];
        }
        return __positionPoint;
    }

    private int Factorial(int p_value)
    {
        if (p_value == 0)
            return 1;
        else
        {
            int __return = p_value;
            for (int i = p_value - 1; i > 0; i--)
            {
                __return *= i;
            }

            return __return;
        }
    }
}

public class ATween : MonoBehaviour
{
    public static ATweenNodule FloatTo(float p_startValue, float p_endValue, float p_duration, Ease p_ease, Action<float> p_callback)
    {
        ATweenNodule __newTween = new ATweenNodule(p_startValue, p_endValue, p_duration, p_ease, p_callback);

        TweenCore.instance.AddNodule(__newTween);

        return __newTween;
    }

    public static ATweenNodule Vector3To(Vector3 p_startValue, Vector3 p_endValue, float p_duration, Ease p_ease, Action<Vector3> p_callback)
    {
        ATweenNodule __newTween = FloatTo(0f, 1f, p_duration, p_ease, delegate (float p_value)
        {
            p_callback(Vector3.Lerp(p_startValue, p_endValue, p_value));
        });

        TweenCore.instance.AddNodule(__newTween);

        return __newTween;
    }

    public static ATweenNodule Vector2To(Vector2 p_startValue, Vector2 p_endValue, float p_duration, Ease p_ease, Action<Vector2> p_callback)
    {
        ATweenNodule __newTween = FloatTo(0f, 1f, p_duration, p_ease, delegate (float p_value)
        {
            p_callback(Vector2.Lerp(p_startValue, p_endValue, p_value));
        });

        TweenCore.instance.AddNodule(__newTween);

        return __newTween;
    }

    public static ATweenNodule QuaternionTo(Quaternion p_startValue, Quaternion p_endValue, float p_duration, Ease p_ease, Action<Quaternion> p_callback)
    {
        ATweenNodule __newTween = FloatTo(0f, 1f, p_duration, p_ease, delegate (float p_value)
        {
            p_callback(Quaternion.Lerp(p_startValue, p_endValue, p_value));
        });

        TweenCore.instance.AddNodule(__newTween);

        return __newTween;
    }

    public static ATweenNodule ColorTo(Color p_startValue, Color p_endValue, float p_duration, Ease p_ease, Action<Color> p_callback)
    {
        ATweenNodule __newTween = FloatTo(0f, 1f, p_duration, p_ease, delegate (float p_value)
        {
            p_callback(Color.Lerp(p_startValue, p_endValue, p_value));
        });

        TweenCore.instance.AddNodule(__newTween);

        return __newTween;
    }

    public static ABezierCurve BezierCurve(List<Vector3> p_controlPoints, float p_duration, Action<Vector3> p_callback)
    {
        ABezierCurve __newCurve = new ABezierCurve(p_controlPoints, p_duration, p_callback);

        TweenCore.instance.AddCurve(__newCurve);

        return __newCurve;
    }
}
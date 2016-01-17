using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TimerCore : MonoBehaviour
{
    #region Static Data

    private static GameObject _go;
    private static TimerCore _instance;
    public static TimerCore instance
    {
        get
        {
            if (_instance == null)
            {
                _go = new GameObject("TimerCore", typeof(TimerCore));
                DontDestroyOnLoad(_go);
                _instance = _go.GetComponent<TimerCore>();
            }
            return _instance;
        }
    }

    #endregion

    #region Private Data

    [SerializeField] private List<ATimerNodule> _nodulesList;

    #endregion

    void FixedUpdate()
    {
        List<ATimerNodule> __updateList = new List<ATimerNodule>();

        for (int i = 0; i < _nodulesList.Count; i ++)
        {
            _nodulesList[i].AUpdate();
            if (!_nodulesList[i].finished && !_nodulesList[i].stopped)
            {
                __updateList.Add(_nodulesList[i]);
            }
        }
        _nodulesList.Clear();
        _nodulesList = __updateList;
    }

    public void AddNodule (ATimerNodule p_nodule)
    {
        if (_nodulesList == null)
        {
            _nodulesList = new List<ATimerNodule>();
        }
        _nodulesList.Add(p_nodule);
    }
}

public class ATimerNodule
{
    #region Private Data

    private int _duration;
    private int _timer = 0;

    private Action _callback;

    private bool _finished;
    private bool _stopped = false;

    #endregion

    #region Public Data

    public bool finished
    {
        get { return _finished; }
    }

    public bool stopped
    {
        get { return _stopped; }
    }

    #endregion

    public ATimerNodule(float p_duration, Action p_callback)
    {
        _duration = (int)(p_duration * 60);
        _callback = p_callback;
    }

    public void AUpdate()
    {
        if (_timer >= _duration)
        {
            if (_callback != null)
            {
                _callback();
            }
            _finished = true;
        }
        else
        {
            _timer += 1;
        }
    }

    public void Stop()
    {
        _stopped = true;
    }
}

public class ATimer : MonoBehaviour
{
    public static ATimerNodule WaitSeconds(float p_duration, Action p_callback)
    {
        ATimerNodule __newTimer = new ATimerNodule(p_duration, p_callback);
        TimerCore.instance.AddNodule(__newTimer);
        return __newTimer;
    }
}

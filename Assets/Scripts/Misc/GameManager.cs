using System;
using System.Collections;
using System.Collections.Generic;
using Framework.Generics.Pattern.SingletonPattern;
using Misc;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private EventManager _xDialogueEventBus;
    public EventManager XDialogueEventBus { get => _xDialogueEventBus; }

    private void Start()
    {
        _xDialogueEventBus = new EventManager();
    }
}

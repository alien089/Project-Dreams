using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private Dictionary<string, List<Action<object[]>>> m_EventMap;

    private void Awake()
    {
        m_EventMap = new Dictionary<string, List<Action<object[]>>>();
    }

    /// <summary>
    /// Registra una nuova azione per un evento specifico. Restituisce null se uno dei parametri è incorretto.
    /// </summary>
    /// <param name="eventName">Nome dell'evento</param>
    /// <param name="action">Azione da eseguire</param>
    public void Register(string eventName, Action<object[]> action)
    {
        if (!CheckPrecondition(eventName, action))
            return;

        if (m_EventMap.ContainsKey(eventName))
            m_EventMap[eventName].Add(action);
        else
        {
            m_EventMap.Add(eventName, new List<Action<object[]>>());
            m_EventMap[eventName].Add(action);
        }
    }

    /// <summary>
    /// Deregistra un'azione per un evento specifico. Restituisce null se uno dei parametri è incorretto.
    /// </summary>
    /// <param name="eventName">Nome dell'evento</param>
    /// <param name="action">Azione da deregistrare</param>
    public void Unregister(string eventName, Action<object[]> action)
    {
        if (!CheckPrecondition(eventName, action))
            return;

        if (m_EventMap.ContainsKey(eventName))
            m_EventMap[eventName].Remove(action);
    }

    /// <summary>
    /// Scatena un evento specifico con tutti gli action correlati.
    /// </summary>
    /// <param name="eventName">Nome dell'evento</param>
    /// <param name="parameters">Parametri da passare agli action</param>
    public void TriggerEvent(string eventName, params object[] parameters)
    {
        if (m_EventMap.ContainsKey(eventName))
        {
            List<Action<object[]>> actions = m_EventMap[eventName];

            foreach (Action<object[]> action in actions)
                action.Invoke(parameters);
        }
    }

    private bool CheckPrecondition(string eventName, Action<object[]> action)
    {
        if (action == null) return false;
        if (string.IsNullOrEmpty(eventName.ToString())) return false;

        return true;
    }
}

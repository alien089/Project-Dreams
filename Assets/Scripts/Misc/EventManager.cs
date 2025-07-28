using System;
using System.Collections.Generic;

namespace Misc
{
    public class EventManager
    {
        private Dictionary<string, List<Action<object[]>>> _mEventMap = new Dictionary<string, List<Action<object[]>>>();

        /// <summary>
        /// Registra una nuova azione per un evento specifico. Restituisce null se uno dei parametri � incorretto.
        /// </summary>
        /// <param name="eventName">Nome dell'evento</param>
        /// <param name="action">Azione da eseguire</param>
        public void Register(string eventName, Action<object[]> action)
        {
            if (!CheckPrecondition(eventName, action))
                return;

            if (_mEventMap.ContainsKey(eventName))
                _mEventMap[eventName].Add(action);
            else
            {
                _mEventMap.Add(eventName, new List<Action<object[]>>());
                _mEventMap[eventName].Add(action);
            }
        }

        /// <summary>
        /// Deregistra un'azione per un evento specifico. Restituisce null se uno dei parametri � incorretto.
        /// </summary>
        /// <param name="eventName">Nome dell'evento</param>
        /// <param name="action">Azione da deregistrare</param>
        public void Unregister(string eventName, Action<object[]> action)
        {
            if (!CheckPrecondition(eventName, action))
                return;

            if (_mEventMap.ContainsKey(eventName))
                _mEventMap[eventName].Remove(action);
        }

        /// <summary>
        /// Scatena un evento specifico con tutti gli action correlati.
        /// </summary>
        /// <param name="eventName">Nome dell'evento</param>
        /// <param name="parameters">Parametri da passare agli action</param>
        public void TriggerEvent(string eventName, params object[] parameters)
        {
            if (_mEventMap.ContainsKey(eventName))
            {
                List<Action<object[]>> actions = _mEventMap[eventName];

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
}

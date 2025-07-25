namespace Framework.Generics.Pattern.StatePattern
{
    using UnityEngine;

    /// <summary>
    /// Basic state template 
    /// </summary>
    /// <typeparam name="TStateIDType">The type of the state ID</typeparam>
    public class State<TStateIDType>
    {
        public TStateIDType StateID;
        protected StatesMachine<TStateIDType> m_stateMachine;

        public State(TStateIDType stateID, StatesMachine<TStateIDType> stateMachine = null)
        {
            StateID = stateID;
            m_stateMachine = stateMachine;
        }

        /// <summary>
        /// Activities done on change state to this
        /// </summary>
        public virtual void OnEnter()
        {
            

        }

        /// <summary>
        /// Activities done while this state is running
        /// </summary>
        public virtual void OnUpdate()
        {
           
        }

        /// <summary>
        /// Activities done on change state to another
        /// </summary>
        public virtual void OnExit()
        {
            
        }
    }
}
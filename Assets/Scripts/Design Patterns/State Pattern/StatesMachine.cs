namespace Framework.Generics.Pattern.StatePattern
{
    using System.Collections.Generic;

    /// <summary>
    /// State manager to handle basic states
    /// </summary>
    /// <typeparam name="TStateIDType">The type of the state base ID</typeparam>
    public abstract class StatesMachine<TStateIDType>
    {
        public Dictionary<TStateIDType, State<TStateIDType>> StatesList;
        public State<TStateIDType> CurrentState;
        public State<TStateIDType> PreviousState;

        public StatesMachine()
        {
            StatesList = null;
            CurrentState = null;
            PreviousState = null;

            InitStatesManager();
        }

        /// <summary>
        /// Initialization states
        /// </summary>
        protected virtual void InitStatesManager()
        {
            if (StatesList == null) 
                StatesList = new Dictionary<TStateIDType, State<TStateIDType>>();

            InitStates();
        }

        /// <summary>
        /// Loads all the states in the dictionary 
        /// </summary>
        protected abstract void InitStates();

        /// <summary>
        /// Changes the current state to the passed state type (only if the current state is not already the passed state type)
        /// </summary>
        /// <param name="stateIDType"></param>
        public void ChangeState(TStateIDType stateIDType)
        {
            if (CurrentState == StatesList[stateIDType]) 
                return;

            PreviousState = CurrentState;
            CurrentState.OnExit();
            CurrentState = StatesList[stateIDType];
            CurrentState.OnEnter();
        }
    }
}
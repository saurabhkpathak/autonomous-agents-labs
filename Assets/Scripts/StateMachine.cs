public class StateMachine<T>
{
    private T owner;

    // This holds the current state for the state machine
    private State<T> currentState = null;
    public State<T> CurrentState
    {
        get { return currentState; }
        set { currentState = value; }
    }

    // The agent's previous state is needed to implement state blips
    private State<T> previousState = null;
    public State<T> PreviousState
    {
        get { return previousState; }
        set { previousState = value; }
    }

    // The agent's global state is always executed, if it exists
    private State<T> globalState = null;
    public State<T> GlobalState
    {
        get { return globalState; }
        set { globalState = value; }
    }

    // What a lovely constructor
    public StateMachine(T agent)
    {
        owner = agent;
    }

    // This is called by the Agent whenever the Game invokes the Agent's Update() method
    public void Update()
    {
        if (globalState != null)
        {
            globalState.Execute(owner);
        }
        if (currentState != null)
        {
            currentState.Execute(owner);
        }
    }

    // This method attempts to deliver a message first via the global state, and if that fails
    // via the current state
    public bool HandleMessage(Telegram telegram)
    {
        if (globalState != null)
        {
            if (globalState.OnMesssage(owner, telegram))
            {
                return true;
            }

        }
        if (currentState != null)
        {
            if (currentState.OnMesssage(owner, telegram))
            {
                return true;
            }
        }
        return false;
    }

    // Handle sense event
    public bool HandleSenseEvent(Sense sense)
    {
        if (globalState != null)
        {
            if (globalState.OnSenseEvent(owner, sense))
            {
                return true;
            }

        }
        if (currentState != null)
        {
            if (currentState.OnSenseEvent(owner, sense))
            {
                return true;
            }
        }
        return false;
    }

    // Switch to a new state and save the old one, so we can revert to it later if it's a state blip
    public void ChangeState(State<T> newState)
    {
        previousState = currentState;
        currentState.Exit(owner);
        currentState = newState;
        currentState.Enter(owner);
    }

    // Invoked when a state blip is finished
    public void RevertToPreviousState()
    {
        ChangeState(previousState);
    }

    // Checks whether the machine is in a given state
    public bool IsInState(State<T> state)
    {
        return (state.GetType().Equals(currentState.GetType()));
    }

    public void Awake()
    {
        this.currentState = null;
        this.previousState = null;
        this.globalState = null;
    }

    public void Init(T agent, State<T> startState)
    {
        this.owner = agent;
        this.currentState = startState;
    }
}

using UnityEngine;

abstract public class Agent : MonoBehaviour
{

    private static int agents = 0;
    public static int AgentsCount
    {
        get { return agents; }
    }

    // Every agent has a numerical id that is set when it is created
    private int id;
    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    // Agents can die now
    private bool isDead;
    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }

    // The agent keeps track of its own position
    private Vector2 position;
    public Vector2 CurrentPosition
    {
        get { return position; }
        set { position = value; }
    }


    // It also knows how much gold it's carrying
    protected int goldCarrying;
    public int GoldCarrying
    {
        get { return goldCarrying; }
        set { goldCarrying = value; }
    }

    abstract public void Update();
    abstract public bool HandleMessage(Telegram telegram);
    abstract public bool HandleSenseEvent(Sense sense);
}
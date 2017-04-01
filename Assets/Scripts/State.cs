using System.Collections.Generic;
using UnityEngine;

abstract public class State <T> {

	abstract public void Enter (T agent);
	abstract public void Execute (T agent);
	abstract public void Exit (T agent);
    abstract public bool OnMesssage(T agent, Telegram telegram);
    abstract public bool OnSenseEvent(T agent, Sense sense);
}

abstract public class TravelToTarget<T> : State<T>
{
	protected static AStar pathFinder = new AStar();
	protected List<Node> path;

	protected Vector2 targetPosition;
	protected State<T> targetState;
}
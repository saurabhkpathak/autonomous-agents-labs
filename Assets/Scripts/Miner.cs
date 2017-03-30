using UnityEngine;

public class Miner : Agent
{
        public int MaxNuggets = 3;
        public int ThirstLevel = 5;
        public int ComfortLevel = 5;
        public int TirednessThreshold = 5;

        // Here is the StateMachine that the Miner uses to drive the agent's behaviour
        private StateMachine<Miner> stateMachine;
        public StateMachine<Miner> StateMachine
        {
            get { return stateMachine; }
            set { stateMachine = value; }
        }

        private int wifeId;
        public int WifeId
        {
            get { return wifeId; }
            set { wifeId = value; }
        }

        private int moneyInBank;
        public int MoneyInBank
        {
            get { return moneyInBank; }
            set { moneyInBank = value; }
        }

        private int howThirsty;
        public int HowThirsty
        {
            get { return howThirsty; }
            set { howThirsty = value; }
        }

        private int howFatigued;
        public int HowFatigued
        {
            get { return howFatigued; }
            set { howFatigued = value; }
        }

        public Miner()
        {
            stateMachine = new StateMachine<Miner>(this);
            //stateMachine.CurrentState = new GoHomeAndSleepTillRested();
            //stateMachine.GlobalState = new MinerGlobalState();
            wifeId = this.Id + 1;  // hack hack

            //Location = Location.shack;
        }

        // This method is invoked by the Game object as a result of XNA updates 
        public override void Update()
        {
            //if (Location >= 0)
            //{
            //    howThirsty += 1;
            //}
            StateMachine.Update();
        }

        // This method is invoked when the agent receives a message
        public override bool HandleMessage(Telegram telegram)
        {
            return stateMachine.HandleMessage(telegram);
        }

        // This method is invoked when the agent senses
        public override bool HandleSenseEvent(Sense sense)
        {
            return stateMachine.HandleSenseEvent(sense);
        }

        // This method checks whether the agent's pockets are full or not, depending on the predefined level
        public bool PocketsFull()
        {
            if (goldCarrying >= MaxNuggets)
                return true;
            else
                return false;
        }

        // This method checks whether the agent is thirsty or not, depending on the predefined level
        public bool Thirsty()
        {
            if (howThirsty >= ThirstLevel)
                return true;
            else
                return false;
        }

        // This method checks whether the agent is fatigued or not, depending on the predefined level
        public bool Fatigued()
        {
            if (howFatigued >= TirednessThreshold)
                return true;
            else
                return false;
        }

        // This method checks whether the agent feels rich enough, depending on the predefined level
        public bool Rich()
        {
            if (moneyInBank >= ComfortLevel)
                return true;
            else
                return false;
        }
    }
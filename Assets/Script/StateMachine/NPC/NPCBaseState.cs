public abstract class NPCBaseState : State
{
    NPCStateMachine nPCStateMachine;
    public NPCBaseState(NPCStateMachine nPCStateMachine)
    {
        this.nPCStateMachine = nPCStateMachine;
    }


    protected void Move()
    {

    }
}

using UnityEngine;
using UnityEngine.AI;

public enum CustomerState
{
    GoToMarket,
    WaitForDelivery,
    Return,
}

public class Customer : MonoBehaviour
{
    public CustomerState currentState;
    public Transform carryTrans;
    public NavMeshAgent agent;
    public Animator anim;
    public int marketSlot { get; private set; }
    public ParticleSystem payEff;

    ICustomerState[] states = new ICustomerState[]
    {
        new CustomerGoToMarket(),
        new CustomerWaitForDelivery(),
        new CustomerReturn(),
    };

    public void Init(int marketSlot)
    {
        this.marketSlot = marketSlot;

        this.currentState = CustomerState.GoToMarket;
        states[(int)this.currentState].OnEnter(this);
    }

    public void SwitchState(CustomerState newState)
    {
        if (currentState != newState)
        {
            states[(int)currentState].OnExit(this);
            this.currentState = newState;
            states[(int)currentState].OnEnter(this);
        }
    }

    void Update()
    {
        states[(int)this.currentState].OnUpdate(this);
    }

}

public interface ICustomerState
{
    public void OnEnter(Customer owner);
    public void OnUpdate(Customer owner);
    public void OnExit(Customer owner);
}

public class CustomerGoToMarket : ICustomerState
{
    public void OnEnter(Customer owner)
    {
        owner.anim.Play("Move");
        owner.agent.SetDestination(GameManager.Instance.market.customerSlots[owner.marketSlot].position);
        GameManager.Instance.market.currentCustomers[owner.marketSlot] = owner;
    }

    public void OnExit(Customer owner)
    {
    }

    public void OnUpdate(Customer owner)
    {

        if (Vector3.Distance(owner.transform.position, GameManager.Instance.market.customerSlots[owner.marketSlot].position) < 0.1f)
        {
            owner.SwitchState(CustomerState.WaitForDelivery);
        }
    }
}

public class CustomerWaitForDelivery : ICustomerState
{
    public void OnEnter(Customer owner)
    {
        owner.anim.Play("Idle");
        owner.agent.isStopped = true;
        owner.transform.LookAt(new Vector3(GameManager.Instance.market.deliverySlots[owner.marketSlot].position.x, owner.transform.position.y, GameManager.Instance.market.deliverySlots[owner.marketSlot].position.z));
    }

    public void OnExit(Customer owner)
    {
        owner.agent.isStopped = false;
        owner.payEff.Play();
    }

    public void OnUpdate(Customer owner)
    {
        if (owner.carryTrans.childCount == GameConstant.maxProductCarries)
        {
            owner.SwitchState(CustomerState.Return);
        }
    }
}

public class CustomerReturn : ICustomerState
{
    public void OnEnter(Customer owner)
    {
        owner.anim.Play("CarryMove");
        owner.agent.SetDestination(GameManager.Instance.market.customerEnd.position);
        GameManager.Instance.market.currentCustomers[owner.marketSlot] = null;
    }

    public void OnExit(Customer owner)
    {
    }

    public void OnUpdate(Customer owner)
    {
        if (Vector3.Distance(owner.transform.position, GameManager.Instance.market.customerEnd.position) < 0.5f)
        {
            GameManager.Instance.activeCustomers.Remove(owner);
            foreach (Transform carry in owner.carryTrans)
            {
                GameObject.Destroy(carry.gameObject);
            }
            owner.Recycle();
        }
    }
}
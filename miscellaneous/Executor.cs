using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Executor : MonoBehaviour
{
    [SerializeField] private bool active = true;

    public void SetActive(bool value)
    {
        active = value;
    }

    [System.Serializable]
    private class UnityEventNamedDelayed
    {
        public string name = "";
        public float delay = 0f;
        public UnityEvent Event = new UnityEvent();
    }

    [Space]
    [SerializeField] private UnityEventNamedDelayed[] events = new UnityEventNamedDelayed[0];

    private void EventInvoke(int index)
    {
        if (events[index].delay == 0f) events[index].Event.Invoke();
        else StartCoroutine(EventInvokeCoroutine(index));
    }

    private IEnumerator EventInvokeCoroutine(int index)
    {
        yield return new WaitForSeconds(events[index].delay);

        events[index].Event.Invoke();
    }

    public void Execute(int index = 0)
    {
        if (!active) return;

        if (index < 0 || index > events.Length - 1)
        {
            Debug.LogWarning("Event index " + index + " is out of field in " + gameObject.name + " performer");
            return;
        }

        EventInvoke(index);
    }

    public void Execute(string name)
    {
        if (!active) return;

        for (int i = 0; i < events.Length; i++)
            if (events[i].name.Equals(name))
            {
                EventInvoke(i);
                return;
            }

        Debug.LogWarning("There are no events named \"" + name + "\" in " + gameObject.name + " performer");
    }
}
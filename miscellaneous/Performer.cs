using UnityEngine;
using UnityEngine.Events;

public class Performer : MonoBehaviour
{
    [System.Serializable]
    private class UnityEventNamed
    {
        public string name = "";
        public UnityEvent Event = new UnityEvent();
    }

    [SerializeField] private UnityEventNamed[] events = new UnityEventNamed[1];
    
    public void EventInvoke(int index = 0)
    {
        if (index < 0 || index > events.Length - 1)
        {
            Debug.LogWarning("Event index " + index + " is out of field in " + gameObject.name + " performer");
            return;
        }

        events[index].Event.Invoke();
    }

    public void EventInvoke(string name)
    {
        for (int i = 0; i < events.Length; i++)
            if (events[i].name.Equals(name))
            {
                events[i].Event.Invoke();
                return;
            }

        Debug.LogWarning("There are no events named \"" + name + "\" in " + gameObject.name + " performer");
    }
}

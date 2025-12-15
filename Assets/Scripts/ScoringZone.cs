using UnityEngine;
using UnityEngine.EventSystems;

public class ScoringZone : MonoBehaviour
{
    public EventTrigger.TriggerEvent scoreTrigger;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BallScript _ball = collision.gameObject.GetComponent<BallScript>();

        if(_ball != null)
        {
            BaseEventData eventData = new BaseEventData(EventSystem.current);
            scoreTrigger.Invoke(eventData);
        }
    }
}

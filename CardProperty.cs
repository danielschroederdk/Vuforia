using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class CardProperty : MonoBehaviour
{
    public InputField InputField;

// Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        StateManager sm = TrackerManager.Instance.GetStateManager();
        IEnumerable<TrackableBehaviour> trackableCards = sm.GetActiveTrackableBehaviours();
        
            foreach (TrackableBehaviour card in trackableCards)
            {
                InputField.text = card.TrackableName;
            }
            
    }
}

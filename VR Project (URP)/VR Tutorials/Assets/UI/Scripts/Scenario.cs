using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ESAVR
{
    public class Scenario : MonoBehaviour
    {
        [SerializeField] string _scenarioName;
        [SerializeField] List<GameObject> _elements;

        private void Start()
        {
            StateManager.Instance.RegisterScenario(_scenarioName, _elements);
        }

        public void Select()
        {
            StateManager.Instance.ToggleScenario(_scenarioName);
        }
    }
}

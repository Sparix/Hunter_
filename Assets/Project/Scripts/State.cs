using System;
using Project.Scripts.Behaviours;
using UnityEngine;

namespace Project.Scripts {
    public enum StateType {
        Wandering,
        Seeking,
        Fleeing
    }
    
    [Serializable]
    public class State {
        [SerializeField] private DesiredVelocityProvider velocityProvider;
        [SerializeField] private StateType stateType;
        [SerializeField] private float velocityLimit;
        public float VelocityLimit => velocityLimit;
        public StateType StateType => stateType;
        public DesiredVelocityProvider VelocityProvider => velocityProvider;
        
    }
}
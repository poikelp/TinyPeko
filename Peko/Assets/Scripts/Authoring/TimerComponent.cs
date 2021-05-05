using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace Peko.Authoring
{
    public class TimerComponent : MonoBehaviour, IConvertGameObjectToEntity
    {
        public GameObject[] numbers;
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData<Timer>(entity, new Timer
            {
                Value = 60
            });

            var numberBuffer = dstManager.AddBuffer<NumberPlace>(entity);
            if(numbers != null)
            {
                foreach(var n in numbers)
                {
                    var numberEntity = conversionSystem.GetPrimaryEntity(n);
                    numberBuffer.Add(new NumberPlace
                    {
                        Value = numberEntity
                    });    
                }    
            }
        }
    }

    [UpdateInGroup(typeof(GameObjectDeclareReferencedObjectsGroup))]
    class DeclareTimerComponentReference : GameObjectConversionSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((CounterComponent mgr) =>
            {
                if(mgr.numbers != null)
                {
                    foreach(var s in mgr.numbers)
                    {
                        DeclareReferencedAsset(s);
                    }
                }                
            });
        }
    }
}

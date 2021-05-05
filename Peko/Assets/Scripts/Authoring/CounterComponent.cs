using UnityEngine;
using Unity.Entities;

namespace Peko.Authoring
{
    public class CounterComponent : MonoBehaviour, IConvertGameObjectToEntity
    {
        public GameObject[] numbers;
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData<Counter>(entity, new Counter
            {
                Value = 0
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
    class DeclareCounterComponentReference : GameObjectConversionSystem
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
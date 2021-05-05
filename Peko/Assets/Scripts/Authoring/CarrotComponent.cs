using UnityEngine;
using Unity.Entities;

namespace Peko.Authoring
{
    public class CarrotComponent : MonoBehaviour, IConvertGameObjectToEntity
    {
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponent<CarrotTag>(entity);
        }
    }
}

using UnityEngine;
using Unity.Entities;

namespace Peko.Authoring
{
    public class BackgroundComponent : MonoBehaviour, IConvertGameObjectToEntity
    {
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponent<BGTag>(entity);
        }
    }
}
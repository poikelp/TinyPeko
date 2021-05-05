using Unity.Entities;
using Unity.Tiny;

namespace Peko
{
    public class DisplayCountSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var counterEntity = GetSingletonEntity<Counter>();
            var counter = GetComponent<Counter>(counterEntity);

            var buffer = GetBuffer<NumberPlace>(counterEntity);

            var numberEntity = GetSingletonEntity<NumberTag>();
            var number = GetBuffer<NumberSpriteBuffer>(numberEntity);

            var first = buffer[0].Value;
            var second = buffer[1].Value;
            var third = buffer[2].Value;
            
            int firstNum = counter.Value % 10;
            int secondNum = counter.Value % 100 / 10;
            int thirdNum = counter.Value % 1000 / 100;

            var firstRenderer = GetComponent<SpriteRenderer>(first);
            var secondRenderer = GetComponent<SpriteRenderer>(second);
            var thirdRenderer = GetComponent<SpriteRenderer>(third);

            firstRenderer.Sprite = number[firstNum].Sprite;
            secondRenderer.Sprite = number[secondNum].Sprite;
            thirdRenderer.Sprite = number[thirdNum].Sprite;

            EntityManager.SetComponentData<SpriteRenderer>(first, firstRenderer);
            EntityManager.SetComponentData<SpriteRenderer>(second, secondRenderer);
            EntityManager.SetComponentData<SpriteRenderer>(third, thirdRenderer);
        }
    }
}

using NUnit.Framework;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Entities.Tests;
using Platformer.Components;

namespace Platformer.Tests {
    [TestFixture]
    [Category("Unity ECS Tests")]
    public class PlayerMoveSystemTests : ECSTestsFixture{
        [Test]
        public void When_MoveSpeedEqualsZero_Then_EntityDoesNotMove() {
            var entity = m_Manager.CreateEntity(
                typeof(Position),
                typeof(PlayerInput),
                typeof(MoveSpeed)
            );

            m_Manager.SetComponentData(entity, new Position { Value = new float3(0, 0, 0) });
            m_Manager.SetComponentData(entity, new PlayerInput { Move = new float3(1, 1, 0) });
            m_Manager.SetComponentData(entity, new MoveSpeed { Value = 0.0f });

            World.CreateManager<PlayerMoveSystem>().Update();

            // assert
            Assert.AreEqual(new float3(0, 0, 0), m_Manager.GetComponentData<Position>(entity).Value);
        }

        [Test]
        public void When_MoveSpeedMoreThanZero_Then_EntityMove() {
            var entity = m_Manager.CreateEntity(
                typeof(Position),
                typeof(PlayerInput),
                typeof(MoveSpeed)
            );

            m_Manager.SetComponentData(entity, new Position { Value = new float3(0, 0, 0) });
            m_Manager.SetComponentData(entity, new PlayerInput { Move = new float3(1, 1, 0) });
            m_Manager.SetComponentData(entity, new MoveSpeed { Value = 15.0f });

            World.CreateManager<PlayerMoveSystem>().Update();

            // assert
            Assert.AreNotEqual(new float3(0, 0, 0), m_Manager.GetComponentData<Position>(entity).Value);
        }

        [Test]
        public void When_Input_X_AxisMoreThanZero_Then_XShouldMoreThanZero() {
            var entity = m_Manager.CreateEntity(
                typeof(Position),
                typeof(PlayerInput),
                typeof(MoveSpeed)
            );

            m_Manager.SetComponentData(entity, new Position {Value = new float3(0, 0, 0)});
            m_Manager.SetComponentData(entity, new PlayerInput {Move = new float3(1, 0, 0)});
            m_Manager.SetComponentData(entity, new MoveSpeed {Value = 15.0f});

            World.CreateManager<PlayerMoveSystem>().Update();

            // assert
            Assert.IsTrue(m_Manager.GetComponentData<Position>(entity).Value.x > 0);
        }

        [Test]
        public void When_Input_X_AxisLessThanZero_Then_XShouldLessThanZero() {
            var entity = m_Manager.CreateEntity(
                typeof(Position),
                typeof(PlayerInput),
                typeof(MoveSpeed)
            );

            m_Manager.SetComponentData(entity, new Position {Value = new float3(0, 0, 0)});
            m_Manager.SetComponentData(entity, new PlayerInput {Move = new float3(-1, 0, 0)});
            m_Manager.SetComponentData(entity, new MoveSpeed {Value = 15.0f});

            World.CreateManager<PlayerMoveSystem>().Update();

            // assert
            Assert.IsTrue(m_Manager.GetComponentData<Position>(entity).Value.x < 0);
        }
    }

}
using Unity.Entities;

public struct OtherPlayerTag : IComponentData {}

public class OtherPlayerTagComponent : ComponentDataWrapper<OtherPlayerTag> {}

using Unity.Entities;

public struct OtherPlayerTag : IComponentData {
    public int ID;
}

public class OtherPlayerTagComponent : ComponentDataWrapper<OtherPlayerTag> {}

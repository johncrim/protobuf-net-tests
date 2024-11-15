using System.Collections.Immutable;
using DeepEqual.Syntax;
using ProtoBuf;

namespace ProtobufNetTests;

public class ImmutableArraySerializationTests
{

    [Fact]
    public void ImmutableArrayContainer_RoundTrips()
    {
        var container = new ImmutableArrayContainer()
        {
            Array = ["foo", "bar"]
        };
        var deserialized = SerializeAndDeserialize(container);

        container.ShouldDeepEqual(deserialized);
    }

    [Fact]
    public void NullableImmutableArrayContainer_RoundTrips()
    {
        var container = new NullableImmutableArrayContainer()
        {
            NullableArray = ["foo", "bar"]
        };
        var deserialized = SerializeAndDeserialize(container);

        container.ShouldDeepEqual(deserialized);
    }

    private static T SerializeAndDeserialize<T>(T target)
    {
        using var stream = new MemoryStream();
        Serializer.Serialize(stream, target);
        stream.Position = 0;
        return Serializer.Deserialize<T>(stream);
    }
}

[ProtoContract(SkipConstructor = true)]
public class ImmutableArrayContainer
{
    [ProtoMember(1)]
    public ImmutableArray<string> Array;
}

[ProtoContract(SkipConstructor = true)]
public class NullableImmutableArrayContainer
{
    [ProtoMember(1)]
    public ImmutableArray<string>? NullableArray;

}
using Mapster;
using MapsterMapper;
using PaySpace.Calculator.Borders.Extensions;

namespace PaySpace.Calculator.Tests.Utils;

internal static class MapperSingleton
{
    private static readonly Mapper _instance;

    static MapperSingleton()
    {
        if (_instance != null)
            return;

        _instance = new Mapper();
        TypeAdapterConfig.GlobalSettings.Scan(typeof(ServiceCollectionExtensions).Assembly);
    }

    public static Mapper Instance() => _instance;
}

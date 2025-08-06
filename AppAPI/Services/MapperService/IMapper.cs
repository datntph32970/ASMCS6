namespace AppAPI.Services.MapperService
{
    public interface IMapper
    {
        TDestination Map<TSource, TDestination>(TSource source);
        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
        IEnumerable<TDestination> MapList<TSource, TDestination>(IEnumerable<TSource> sourceList);

    }
}

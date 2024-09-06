namespace Mapper;

public interface IMapper<T, U>
{
    T Map(U model);
    U Map(T model);
}
using AutoMapper;
using LibraryApp.Common.Extensions;
namespace LibraryApp.Common.Helpers
{
    public static class MapperHelper
    {
        private static IMapper _staticMapper;
        static MapperHelper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.IgnoreUnmapped();
            });
            _staticMapper = config.CreateMapper();
        }
        public static T MapFrom<T>(object entity)
        {
            return _staticMapper.Map<T>(entity);
        }
    }
}

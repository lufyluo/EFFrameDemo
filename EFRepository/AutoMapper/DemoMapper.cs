using System.Collections.Generic;
using AutoMapper;

namespace EFRepository.AutoMapper
{
    public static class DemoMapper
    {
        /// <summary>  
        ///  单个对象映射  
        /// </summary>  
        public static TDestination MapTo<TSource, TDestination>(TSource source)
        {
            if (source == null) return default(TDestination);
            Mapper.Initialize(x => x.CreateMap(typeof(TSource), typeof(TDestination)).IgnoreAllPropertiesWithAnInaccessibleSetter());
            return Mapper.Map<TDestination>(source);
        }

        /// <summary>  
        /// 集合列表类型映射  
        /// </summary>  
        public static List<TDestination> MapToList<TSource, TDestination>(this IEnumerable<TSource> source)
        {
            if (source == null) return default(List<TDestination>);
            Mapper.Initialize(x => x.CreateMap(typeof(TSource), typeof(TDestination)));
            return Mapper.Map<List<TDestination>>(source);
        }

        /// <summary>  
        /// 类型映射  
        /// </summary>  
        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
            where TSource : class
            where TDestination : class
        {
            if (source == null) return destination;
            Mapper.Initialize(x => x.CreateMap(typeof(TSource), typeof(TDestination)));
            return Mapper.Map(source, destination);
        }
    }
}

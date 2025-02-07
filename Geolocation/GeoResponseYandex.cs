using System;
//Десерилизация в объекты
namespace Geolocation
{
    class GeoResponseYandex
    {
        public ResponseDataYandex Response { get; set; }
    }

    class ResponseDataYandex
    {
        public GeoObjectCollectionYandex GeoObjectCollection { get; set; }
    }

    class GeoObjectCollectionYandex
    {
        public FeatureMemberYandex[] FeatureMember { get; set; }
    }

    class FeatureMemberYandex
    {
        public GeoObjectYandex GeoObject { get; set; }
    }

    class GeoObjectYandex
    {
        public PointYandex Point { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
    }
    class PointYandex
    {
        public string Pos { get; set; }
    }

}

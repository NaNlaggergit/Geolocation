using System;
//Десерилизация в объекты
namespace Geolocation
{
    class GeoResponseGis
    {
        public ResultGis Result { get; set; }
        public Meta Meta { get; set; }
    }
    class Meta
    {
        public int Code {  get; set; }
    }

    class ResultGis
    {
        public ItemsGis[] Items { get; set; }
    }
    class ItemsGis
    {
        public string Full_name { get; set; }
        public PointGis Point { get; set; }
    }
    class PointGis
    {
        public float Lat { get; set; }
        public float Lon { get; set; }
    }

}

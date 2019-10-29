using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace Flowers_web.Models
{
    [Serializable]
  public  class RouteXml
    {
        [Serializable]
        [XmlRoot(ElementName = "start_location")]
        public class start_location
        {
            [XmlElement(ElementName = "lat")]
            public string Lat { get; set; }
            [XmlElement(ElementName = "lng")]
            public string Lng { get; set; }
        }
        [Serializable]
        [XmlRoot(ElementName = "end_location")]
        public class End_location
        {
            [XmlElement(ElementName = "lat")]
            public string Lat { get; set; }
            [XmlElement(ElementName = "lng")]
            public string Lng { get; set; }
        }
        [Serializable]
        [XmlRoot(ElementName = "polyline")]
        public class Polyline
        {
            [XmlElement(ElementName = "points")]
            public string Points { get; set; }
        }
        [Serializable]
        [XmlRoot(ElementName = "duration")]
        public class Duration
        {
            [XmlElement(ElementName = "value")]
            public string Value { get; set; }
            [XmlElement(ElementName = "text")]
            public string Text { get; set; }
        }
        [Serializable]
        [XmlRoot(ElementName = "distance")]
        public class Distance
        {
            [XmlElement(ElementName = "value")]
            public string Value { get; set; }
            [XmlElement(ElementName = "text")]
            public string Text { get; set; }
        }
        [Serializable]
        [XmlRoot(ElementName = "step")]
        public class Step
        {
            [XmlElement(ElementName = "travel_mode")]
            public string Travel_mode { get; set; }
            [XmlElement(ElementName = "start_location")]
            public start_location Start_location { get; set; }
            [XmlElement(ElementName = "end_location")]
            public End_location End_location { get; set; }
            [XmlElement(ElementName = "polyline")]
            public Polyline Polyline { get; set; }
            [XmlElement(ElementName = "duration")]
            public Duration Duration { get; set; }
            [XmlElement(ElementName = "html_instructions")]
            public string Html_instructions { get; set; }
            [XmlElement(ElementName = "distance")]
            public Distance Distance { get; set; }
            [XmlElement(ElementName = "maneuver")]
            public string Maneuver { get; set; }
        }
        [Serializable]
        [XmlRoot(ElementName = "leg")]
        public class Leg
        {
            [XmlElement(ElementName = "step")]
            public List<Step> Step { get; set; }
            [XmlElement(ElementName = "duration")]
            public Duration Duration { get; set; }
            [XmlElement(ElementName = "distance")]
            public Distance Distance { get; set; }
            [XmlElement(ElementName = "start_location")]
            public start_location Start_location { get; set; }
            [XmlElement(ElementName = "end_location")]
            public End_location End_location { get; set; }
            [XmlElement(ElementName = "start_address")]
            public string Start_address { get; set; }
            [XmlElement(ElementName = "end_address")]
            public string End_address { get; set; }
        }
        [Serializable]
        [XmlRoot(ElementName = "overview_polyline")]
        public class Overview_polyline
        {
            [XmlElement(ElementName = "points")]
            public string Points { get; set; }
        }
        [Serializable]
        [XmlRoot(ElementName = "southwest")]
        public class Southwest
        {
            [XmlElement(ElementName = "lat")]
            public string Lat { get; set; }
            [XmlElement(ElementName = "lng")]
            public string Lng { get; set; }
        }
        [Serializable]
        [XmlRoot(ElementName = "northeast")]
        public class Northeast
        {
            [XmlElement(ElementName = "lat")]
            public string Lat { get; set; }
            [XmlElement(ElementName = "lng")]
            public string Lng { get; set; }
        }
        [Serializable]
        [XmlRoot(ElementName = "bounds")]
        public class Bounds
        {
            [XmlElement(ElementName = "southwest")]
            public Southwest Southwest { get; set; }
            [XmlElement(ElementName = "northeast")]
            public Northeast Northeast { get; set; }
        }
        [Serializable]
        [XmlRoot(ElementName = "route")]
        public class Route
        {
            [XmlElement(ElementName = "summary")]
            public string Summary { get; set; }
            [XmlElement(ElementName = "leg")]
            public List<Leg> Leg { get; set; }
            [XmlElement(ElementName = "copyrights")]
            public string Copyrights { get; set; }
            [XmlElement(ElementName = "overview_polyline")]
            public Overview_polyline Overview_polyline { get; set; }
            [XmlElement(ElementName = "waypoint_index")]
            public List<string> Waypoint_index { get; set; }
            [XmlElement(ElementName = "bounds")]
            public Bounds Bounds { get; set; }
        }
        [Serializable]
        [XmlRoot(ElementName = "geocoded_waypoint")]
        public class Geocoded_waypoint
        {
            [XmlElement(ElementName = "geocoder_status")]
            public string Geocoder_status { get; set; }
            [XmlElement(ElementName = "type")]
            public string Type { get; set; }
            [XmlElement(ElementName = "place_id")]
            public string Place_id { get; set; }
        }
        [Serializable]
        [XmlRoot(ElementName = "DirectionsResponse")]
        public class DirectionsResponse
        {
            [XmlElement(ElementName = "status")]
            public string Status { get; set; }
            [XmlElement(ElementName = "route")]
            public Route Route { get; set; }
            [XmlElement(ElementName = "geocoded_waypoint")]
            public List<Geocoded_waypoint> Geocoded_waypoint { get; set; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer.Dtos.Import
{
    [XmlType("Car")]
    public class ImportCarDto
    {
        [XmlElement("make")]
        public string Make { get; set; }

        [XmlElement("model")]
        public string Model { get; set; }

        [XmlElement("TraveledDistance")]
        public int TravelledDistance { get; set; }

        [XmlArray("parts")]
        public List<CarPartsDto> CarParts { get; set; }
    }

    [XmlType("partId")]
    public class CarPartsDto
    {
        [XmlAttribute("id")]
        public int PartId { get; set; }
    }
}

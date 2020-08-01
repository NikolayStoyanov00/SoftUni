using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Cinema.DataProcessor.ImportDto
{
    [XmlType("Customer")]
    public class ImportCustomerDto
    {
        [XmlElement("FirstName")]
        [StringLength(20, MinimumLength = 3)]
        [Required]
        public string FirstName { get; set; }

        [XmlElement("LastName")]
        [StringLength(20, MinimumLength = 3)]
        [Required]
        public string LastName { get; set; }

        [XmlElement("Age")]
        [Range(12, 110)]
        [Required]
        public int Age { get; set; }

        [XmlElement("Balance")]
        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        [Required]
        public decimal Balance { get; set; }

        [XmlArray("Tickets")]
        public ImportTicketDto[] Tickets { get; set; }
    }

    [XmlType("Ticket")]
    public class ImportTicketDto
    {
        [Required]
        [XmlElement("ProjectionId")]
        public int ProjectionId { get; set; }

        [XmlElement("Price")]
        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        [Required]
        public decimal Price { get; set; }
    }
}

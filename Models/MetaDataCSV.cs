using System.ComponentModel.DataAnnotations;

namespace uploadPOC.Models
{
    public class MetaDataCSV
    {
        public int Id { get; set; }
        public string fileName { get; set; }
        public string CollectionName { get; set; }
        public string CollectionSet { get; set; }
        public string CollectionDataType { get; set; }
        public string Comments { get; set; }

    }
}

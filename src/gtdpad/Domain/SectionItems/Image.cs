using System;

namespace gtdpad.Domain
{
    public class Image
    {
        public Image(
            Guid id,
            string fileExtension)
        {
            ID = id;
            FileExtension = fileExtension;
        }

        public Guid ID { get; set; }

        public string FileExtension { get; }

        public Image With(string fileExtension = default) =>
            new Image(ID, fileExtension ?? FileExtension);
    }
}

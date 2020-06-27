using System;

namespace gtdpad.Domain
{
    public class Image
    {
        public Image(
            Guid id,
            string fileExtension,
            int order)
        {
            ID = id;
            FileExtension = fileExtension;
            Order = order;
        }

        public Guid ID { get; set; }

        public string FileExtension { get; }

        public int Order { get; set; }

        public Image With(string fileExtension = default, int? order = default) =>
            new Image(ID, fileExtension ?? FileExtension, order ?? Order);
    }
}

using System;
using gtdpad.Domain;

namespace gtdpad.Dto
{
    public class ImageBlockDto : DtoBase<ImageBlockDto>
    {
        public Guid Page { get; set; }

        public string Title { get; set; }

        public int Order { get; set; }

        public static ImageBlockDto FromImageBlock(ImageBlock imageBlock) =>
            imageBlock is null
                ? null
                : new ImageBlockDto {
                    ID = imageBlock.ID,
                    Page = imageBlock.Page,
                    Title = imageBlock.Title,
                    Order = imageBlock.Order
                };
    }
}

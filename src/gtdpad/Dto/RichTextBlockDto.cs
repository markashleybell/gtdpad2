using System;
using gtdpad.Domain;

namespace gtdpad.Dto
{
    public class RichTextBlockDto : DtoBase<RichTextBlockDto>
    {
        public Guid Page { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public int Order { get; set; }

        public static RichTextBlockDto FromRichTextBlock(RichTextBlock richTextBlock) =>
            richTextBlock is null
                ? null
                : new RichTextBlockDto {
                    ID = richTextBlock.ID,
                    Page = richTextBlock.Page,
                    Title = richTextBlock.Title,
                    Order = richTextBlock.Order
                };
    }
}

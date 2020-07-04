using System;
using gtdpad.Domain;

namespace gtdpad.Dto
{
    public class PageDto : DtoBase<PageDto>
    {
        public string Title { get; set; }

        public string Slug { get; set; }

        public int Order { get; set; }

        public static PageDto FromPage(Page page) =>
            page is null
                ? null
                : new PageDto {
                    ID = page.ID,
                    Title = page.Title,
                    Slug = page.Slug,
                    Order = page.Order
                };
    }
}

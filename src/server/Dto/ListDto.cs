using System;
using gtdpad.Domain;

namespace gtdpad.Dto
{
    public class ListDto : DtoBase<ListDto>
    {
        public Guid Page { get; set; }

        public string Title { get; set; }

        public int Order { get; set; }

        public static ListDto FromList(List list) =>
            list is null
                ? null
                : new ListDto {
                    ID = list.ID,
                    Page = list.Page,
                    Title = list.Title,
                    Order = list.Order
                };
    }
}

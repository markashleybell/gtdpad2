using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using gtdpad.Domain;

namespace gtdpad.Services
{
    public interface IRepository
    {
        /* Queries */

        Task<IEnumerable<Page>> GetAllPages(Guid ownerID);

        /* Commands */

        Task<Page> GetPage(Guid id);

        Task PersistPage(Page page);

        Task DeletePage(Guid id);

        Task<List> GetList(Guid id);

        Task PersistList(List list, Guid pageID);

        Task DeleteList(Guid id);

        Task<RichTextBlock> GetRichTextBlock(Guid id);

        Task PersistRichTextBlock(RichTextBlock richTextBlock, Guid pageID);

        Task DeleteRichTextBlock(Guid id);

        Task<ImageBlock> GetImageBlock(Guid id);

        Task PersistImageBlock(ImageBlock imageBlock, Guid pageID);

        Task DeleteImageBlock(Guid id);

        Task<Image> GetImage(Guid id);

        Task PersistImage(Image image, Guid imageBlockID);

        Task DeleteImage(Guid id);

        Task<ListItem> GetListItem(Guid id);

        Task PersistListItem(ListItem listItem, Guid listID);

        Task DeleteListItem(Guid id);

        Task<User> FindUserByEmail(string email);
    }
}

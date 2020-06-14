using System;
using System.Threading.Tasks;
using gtdpad.Domain;

namespace gtdpad.Services
{
    public interface IRepository
    {
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

        Task<User> FindUserByEmail(string email);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using gtdpad.Domain;
using gtdpad.Services;
using gtdpad.Support;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using g = gtdpad.Domain;

namespace test
{
    public class IntegrationTests : IntegrationTestFixtureBase
    {
        private readonly Settings _testSettings = new Settings {
            ConnectionStrings = new Dictionary<string, string>()
        };

        private readonly User _testUser = new User(
            id: new Guid("df77778f-2ef3-49af-a1a8-b1f064891ef5"),
            email: "testuser@gtdpad.com",
            password: "!gtdpad-test2020"
        );

        public IntegrationTests() =>
            _testSettings.ConnectionStrings.Add("Main", _cfg.GetConnectionString("Test"));

        [Test]
        public async Task Page()
        {
            var repository = new SqlServerRepository(_testSettings);

            var newPage = new Page(
                id: Guid.NewGuid(),
                owner: _testUser.ID,
                title: "PAGE",
                slug: "page"
            );

            await repository.PersistPage(newPage);

            var page = await repository.GetPage(newPage.ID);

            Assert.AreEqual(newPage.ID, page.ID);
            Assert.AreEqual(newPage.Title, page.Title);
            Assert.AreEqual(newPage.Slug, page.Slug);

            var updatedPage = page.With(title: "PAGE UPDATED", slug: "page-updated");

            await repository.PersistPage(updatedPage);

            var pageAfterUpdate = await repository.GetPage(updatedPage.ID);

            Assert.AreEqual(pageAfterUpdate.ID, page.ID);
            Assert.AreEqual(pageAfterUpdate.Title, updatedPage.Title);
            Assert.AreEqual(pageAfterUpdate.Slug, updatedPage.Slug);

            await repository.DeletePage(page.ID);

            var pageAfterDelete = await repository.GetPage(page.ID);

            Assert.AreEqual(default(Page), pageAfterDelete);
        }

        [Test]
        public async Task RichTextBlock()
        {
            var repository = new SqlServerRepository(_testSettings);

            var page = new Page(
                id: Guid.NewGuid(),
                owner: _testUser.ID,
                title: "PAGE",
                slug: "page"
            );

            await repository.PersistPage(page);

            var newRichTextBlock = new RichTextBlock(
                id: Guid.NewGuid(),
                owner: _testUser.ID,
                title: "RICH TEXT BLOCK",
                text: "<p>Rich Text Block Body.</p>"
            );

            await repository.PersistRichTextBlock(newRichTextBlock, page.ID);

            var richTextBlock = await repository.GetRichTextBlock(newRichTextBlock.ID);

            Assert.AreEqual(newRichTextBlock.ID, richTextBlock.ID);
            Assert.AreEqual(newRichTextBlock.Title, richTextBlock.Title);
            Assert.AreEqual(newRichTextBlock.Text, richTextBlock.Text);

            var updatedRichTextBlock = richTextBlock.With(
                title: "RICH TEXT BLOCK UPDATED",
                text: "<p>Rich Text Block Body Updated.</p>"
            );

            await repository.PersistRichTextBlock(updatedRichTextBlock, page.ID);

            var richTextBlockAfterUpdate = await repository.GetRichTextBlock(updatedRichTextBlock.ID);

            Assert.AreEqual(richTextBlockAfterUpdate.ID, richTextBlock.ID);
            Assert.AreEqual(richTextBlockAfterUpdate.Title, updatedRichTextBlock.Title);
            Assert.AreEqual(richTextBlockAfterUpdate.Text, updatedRichTextBlock.Text);

            await repository.DeleteRichTextBlock(richTextBlock.ID);

            var richTextBlockAfterDelete = await repository.GetRichTextBlock(richTextBlock.ID);

            Assert.AreEqual(default(RichTextBlock), richTextBlockAfterDelete);
        }

        [Test]
        public async Task ImageBlock()
        {
            var repository = new SqlServerRepository(_testSettings);

            var page = new Page(
                id: Guid.NewGuid(),
                owner: _testUser.ID,
                title: "PAGE",
                slug: "page"
            );

            await repository.PersistPage(page);

            var newImageBlock = new ImageBlock(
                id: Guid.NewGuid(),
                owner: _testUser.ID,
                title: "IMAGE BLOCK"
            );

            await repository.PersistImageBlock(newImageBlock, page.ID);

            var imageBlock = await repository.GetImageBlock(newImageBlock.ID);

            Assert.AreEqual(newImageBlock.ID, imageBlock.ID);
            Assert.AreEqual(newImageBlock.Title, imageBlock.Title);

            var updatedImageBlock = imageBlock.With(
                title: "IMAGE BLOCK UPDATED"
            );

            await repository.PersistImageBlock(updatedImageBlock, page.ID);

            var imageBlockAfterUpdate = await repository.GetImageBlock(updatedImageBlock.ID);

            Assert.AreEqual(imageBlockAfterUpdate.ID, imageBlock.ID);
            Assert.AreEqual(imageBlockAfterUpdate.Title, updatedImageBlock.Title);

            await repository.DeleteImageBlock(imageBlock.ID);

            var imageBlockAfterDelete = await repository.GetImageBlock(imageBlock.ID);

            Assert.AreEqual(default(ImageBlock), imageBlockAfterDelete);
        }

        [Test]
        public async Task List()
        {
            var repository = new SqlServerRepository(_testSettings);

            var page = new Page(
                id: Guid.NewGuid(),
                owner: _testUser.ID,
                title: "PAGE",
                slug: "page"
            );

            await repository.PersistPage(page);

            var newList = new g.List(
                id: Guid.NewGuid(),
                owner: _testUser.ID,
                title: "LIST"
            );

            await repository.PersistList(newList, page.ID);

            var list = await repository.GetList(newList.ID);

            Assert.AreEqual(newList.ID, list.ID);
            Assert.AreEqual(newList.Title, list.Title);

            var updatedList = list.With(
                title: "LIST UPDATED"
            );

            await repository.PersistList(updatedList, page.ID);

            var listAfterUpdate = await repository.GetList(updatedList.ID);

            Assert.AreEqual(listAfterUpdate.ID, list.ID);
            Assert.AreEqual(listAfterUpdate.Title, updatedList.Title);

            await repository.DeleteList(list.ID);

            var listAfterDelete = await repository.GetList(list.ID);

            Assert.AreEqual(default(g.List), listAfterDelete);
        }
    }
}

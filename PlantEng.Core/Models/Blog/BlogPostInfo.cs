using System;

namespace PlantEng.Models.Blog
{
    public class BlogPostInfo
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int SystemCategoryId { get; set; }
        public string SystemCategoryName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int ViewCount { get; set; }
        public string Tags { get; set; }
        public DateTime CreateDateTime { get; set; }

        /// <summary>
        /// 扩展信息
        /// </summary>
        public string Url { get; set; }
        /*
            CREATE TABLE BlogPost(
	Id INT NOT NULL IDENTITY(1,1),
	UserId INT NOT NULL DEFAULT(0),
	Title NVARCHAR(200) NOT NULL DEFAULT(''),
	Content NVARCHAR(MAX) NOT NULL DEFAULT(''),
	ViewCount INT NOT NULL DEFAULT (0),
	CreateDateTime DATETIME NOT NULL DEFAULT(GETDATE()),
	PRIMARY KEY(Id)
)
         * ALTER TABLE BlogPosts ADD IsDeleted BIT NOT NULL DEFAULT(0)
         * ALTER TABLE BlogPosts ADD SystemCategoryId INT NOT NULL DEFAULT(0);
         */
    }
}

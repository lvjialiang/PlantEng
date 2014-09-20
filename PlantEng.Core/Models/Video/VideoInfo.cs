using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlantEng.Models.Video
{
    public class VideoInfo
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Remark { get; set; }
        public string VideoUrl { get; set; }
        public string ImageUrl { get; set; }
        public int ViewCount { get; set; }
        public int PlayCount { get; set; }
        public bool IsTop { get; set; }
        public bool IsDeleted { get; set; }
        public string Tags { get; set; }
        public DateTime PublishDateTime { get; set; }
        public DateTime CreateDateTime { get; set; }
        public VideoInfo() {
            PublishDateTime = CreateDateTime = DateTime.Now;
        }
        /*
         
CREATE TABLE Videos(
	Id INT NOT NULL IDENTITY(1,1),
	CategoryId INT NOT NULL DEFAULT(0),
	Title NVARCHAR(100) NOT NULL DEFAULT(''),
	Remark NVARCHAR(MAX) NOT NULL DEFAULT(''),
	VideoUrl NVARCHAR(255) NOT NULL DEFAULT(''),
	ImageUrl NVARCHAR(255) NOT NULL DEFAULT(''),
	ViewCount INT NOT NULL DEFAULT(0),
	PlayCount INT  NOT NULL DEFAULT(0),
	IsTop BIT NOT NULL DEFAULT(0),
	Tags NVARCHAR(MAX) NOT NULL DEFAULT(''),
	PublishDateTime DATETIME NOT NULL DEFAULT(GETDATE()),
	CreateDateTime DATETIME NOT NULL DEFAULT(GETDATE()),
	PRIMARY KEY(Id)
)
         * ALTER TABLE Videos ADD IsDeleted BIT NOT NULL DEFAULT(0)
         */
    }
}

using System;

namespace PlantEng.Models
{
    /// <summary>
    /// Eletter订阅
    /// </summary>
    public class EletterSubscribeInfo
    {
        /*
         * CREATE TABLE EletterSubscribes(
	Id INT NOT NULL IDENTITY(1,1),
	Email VARCHAR(50) NOT NULL DEFAULT(''),
	[Subject] NVARCHAR(100) NOT NULL DEFAULT(''),
	CreateDateTime DATETIME NOT NULL DEFAULT(GETDATE()),
	PRIMARY KEY(Id)
)
         */
        public int Id { get; set; }
        public string Email { get; set; }
        /// <summary>
        /// 订阅主题
        /// </summary>
        public string Subject { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}

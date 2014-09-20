using System;

namespace PlantEng.Models.Advertisment
{
    /// <summary>
    /// 广告位
    /// </summary>
    public class AdPositionInfo
    {
        public int Id { get; set; }
        /// <summary>
        /// 广告位名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 广告位宽
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 广告位高
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// 广告位说明
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 引用广告位URL
        /// </summary>
        public string DeliveryUrl { get; set; }
        public DateTime CreateDateTime { get; set; }
        /*
         CREATE TABLE [dbo].[AdPosition](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Width] [int] NOT NULL,
	[Height] [int] NOT NULL,
	[Remark] [nvarchar](max) NOT NULL,
	[DeliveryUrl] [nvarchar](max) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[AdPosition] ADD  DEFAULT ('') FOR [Name]
GO

ALTER TABLE [dbo].[AdPosition] ADD  DEFAULT ((0)) FOR [Width]
GO

ALTER TABLE [dbo].[AdPosition] ADD  DEFAULT ((0)) FOR [Height]
GO

ALTER TABLE [dbo].[AdPosition] ADD  DEFAULT ('') FOR [Remark]
GO

ALTER TABLE [dbo].[AdPosition] ADD  DEFAULT ('') FOR [DeliveryUrl]
GO

ALTER TABLE [dbo].[AdPosition] ADD  CONSTRAINT [DF_AdPosition_CreateDateTime]  DEFAULT (getdate()) FOR [CreateDateTime]
GO
         */
    }
}

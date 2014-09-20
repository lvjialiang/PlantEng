using System;

namespace PlantEng.Models.Advertisment
{
    /// <summary>
    /// 广告信息
    /// </summary>
    public class AdDeliveryInfo
    {
        public int Id { get; set; }
        /// <summary>
        /// 所属广告位
        /// </summary>
        public int AdPositionId { get; set; }
        /// <summary>
        /// 广告位名称-扩展字段
        /// </summary>
        public string AdPositionName { get; set; }
        /// <summary>
        /// 广告名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 广告状态
        /// </summary>
        public AdStatus Status { get; set; }
        /// <summary>
        /// 广告开始时间
        /// </summary>
        public string BeginTime { get; set; }
        /// <summary>
        /// 广告结束时间
        /// </summary>
        public string EndTime { get; set; }
        public DateTime CreateDateTime { get; set; }
        public AdDeliveryInfo() {
            AdPositionId = 0;
            Id = 0;
        }
        /*
         CREATE TABLE [dbo].[AdDelivery](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AdPositionId] [int] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Status] [smallint] NOT NULL,
	[BeginTime] [varchar](14) NOT NULL,
	[EndTime] [varchar](14) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[AdDelivery] ADD  DEFAULT ((0)) FOR [AdPositionId]
GO

ALTER TABLE [dbo].[AdDelivery] ADD  DEFAULT ('') FOR [Name]
GO

ALTER TABLE [dbo].[AdDelivery] ADD  DEFAULT ((1)) FOR [Status]
GO

ALTER TABLE [dbo].[AdDelivery] ADD  DEFAULT ('') FOR [BeginTime]
GO

ALTER TABLE [dbo].[AdDelivery] ADD  DEFAULT ('99990101240000') FOR [EndTime]
GO

ALTER TABLE [dbo].[AdDelivery] ADD  CONSTRAINT [DF_AdDelivery_CreateDateTime]  DEFAULT (getdate()) FOR [CreateDateTime]
GO
         */
    }
}

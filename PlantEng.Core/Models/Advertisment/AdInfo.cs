using System;

namespace PlantEng.Models.Advertisment
{
    /// <summary>
    /// 广告材料信息
    /// </summary>
    public class AdInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 广告材料类型
        /// </summary>
        public AdType Type { get; set; }
        public string ClickUrl { get; set; }
        public int TargetWindow { get; set; }
        public string Words { get; set; }
        public string AdCode { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public DateTime CreateDateTime { get; set; }
        /*

CREATE TABLE [dbo].[Ads](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DeliveryId] [int] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Type] [smallint] NOT NULL,
	[ClickUrl] [varchar](255) NOT NULL,
	[TargetWindow] [smallint] NOT NULL,
	[Words] [nvarchar](max) NOT NULL,
	[AdCode] [nvarchar](max) NOT NULL,
	[Width] [int] NOT NULL,
	[Height] [int] NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Ads] ADD  DEFAULT ((0)) FOR [DeliveryId]
GO

ALTER TABLE [dbo].[Ads] ADD  DEFAULT ('') FOR [Name]
GO

ALTER TABLE [dbo].[Ads] ADD  DEFAULT ((1)) FOR [Type]
GO

ALTER TABLE [dbo].[Ads] ADD  DEFAULT ('') FOR [ClickUrl]
GO

ALTER TABLE [dbo].[Ads] ADD  DEFAULT ((0)) FOR [TargetWindow]
GO

ALTER TABLE [dbo].[Ads] ADD  DEFAULT ('') FOR [Words]
GO

ALTER TABLE [dbo].[Ads] ADD  DEFAULT ('') FOR [AdCode]
GO

ALTER TABLE [dbo].[Ads] ADD  DEFAULT ((0)) FOR [Width]
GO

ALTER TABLE [dbo].[Ads] ADD  DEFAULT ((0)) FOR [Height]
GO

ALTER TABLE [dbo].[Ads] ADD  CONSTRAINT [DF_Ads_CreateDateTime]  DEFAULT (getdate()) FOR [CreateDateTime]
GO



         */
    }
}

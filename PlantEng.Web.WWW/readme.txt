2012-12-14
     ALTER TABLE Members ADD Company NVARCHAR(255) NOT NULL DEFAULT('')
     CREATE TABLE [dbo].[Company2Category](
	[CompanyId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_Company2Category] PRIMARY KEY CLUSTERED 
(
	[CompanyId] ASC,
	[CategoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Company2Category] ADD  CONSTRAINT [DF_Company2Category_CompanyId]  DEFAULT ((0)) FOR [CompanyId]
GO

ALTER TABLE [dbo].[Company2Category] ADD  CONSTRAINT [DF_Company2Category_CategoryId]  DEFAULT ((0)) FOR [CategoryId]
GO
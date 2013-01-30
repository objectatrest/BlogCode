USE [ScratchPad]
GO

/****** Object:  Table [dbo].[Person]    Script Date: 01/29/2013 19:43:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Person]') AND type in (N'U'))
DROP TABLE [dbo].[Person]
GO

USE [ScratchPad]
GO

/****** Object:  Table [dbo].[Person]    Script Date: 01/29/2013 19:43:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Person](
	[Firstname] [varchar](50) NULL,
	[Lastname] [varchar](50) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


insert into [dbo].[Person] values("Jim", "Bob")
insert into [dbo].[Person] values("Joe", "Bob")
insert into [dbo].[Person] values("Joe", "John")

--Group and count
select Lastname, count(*)
from Person
group by Lastname

--Group and count. Only include items that occur more than once.
select Lastname, count(*)
from Person
group by Lastname
having count(*) > 1
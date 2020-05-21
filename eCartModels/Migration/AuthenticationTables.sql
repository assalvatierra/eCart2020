
    CREATE TABLE [dbo].[Users](  
        [Id] [int] IDENTITY(1,1) NOT NULL,  
        [Username] [nvarchar](50) NULL,  
        [Password] [nvarchar](50) NULL,  
    PRIMARY KEY CLUSTERED   
    (  
        [Id] ASC  
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]  
    ) ON [PRIMARY]  
    GO  

    
    CREATE TABLE [dbo].[Roles](  
        [Id] [int] IDENTITY(1,1) NOT NULL,  
        [RoleName] [nvarchar](50) NULL,  
    PRIMARY KEY CLUSTERED   
    (  
        [Id] ASC  
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]  
    ) ON [PRIMARY]  
    GO  

    
    CREATE TABLE [dbo].[UserRolesMapping](  
        [Id] [int] IDENTITY(1,1) NOT NULL,  
        [UserId] [int] NULL,  
        [RoleId] [int] NULL,  
    PRIMARY KEY CLUSTERED   
    (  
        [Id] ASC  
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]  
    ) ON [PRIMARY]  
    GO  
      
    ALTER TABLE [dbo].[UserRolesMapping]  WITH CHECK ADD FOREIGN KEY([RoleId])  
    REFERENCES [dbo].[Roles] ([Id])  
    GO  
      
    ALTER TABLE [dbo].[UserRolesMapping]  WITH CHECK ADD FOREIGN KEY([RoleId])  
    REFERENCES [dbo].[Roles] ([Id])  
    GO  
      
    ALTER TABLE [dbo].[UserRolesMapping]  WITH CHECK ADD FOREIGN KEY([UserId])  
    REFERENCES [dbo].[Users] ([Id])  
    GO  


-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/13/2020 15:41:35
-- Generated from EDMX file: C:\Users\VILLOSA\source\repos\eCart2020\eCartModels\ecartdb.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ecartdb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserStatusUserDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserDetails] DROP CONSTRAINT [FK_UserStatusUserDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_MasterCityUserDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserDetails] DROP CONSTRAINT [FK_MasterCityUserDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_UserDetailCartDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CartDetails] DROP CONSTRAINT [FK_UserDetailCartDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_MasterAreaUserDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserDetails] DROP CONSTRAINT [FK_MasterAreaUserDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_StoreStatusStoreDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StoreDetails] DROP CONSTRAINT [FK_StoreStatusStoreDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_StoreCategoryStoreDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StoreDetails] DROP CONSTRAINT [FK_StoreCategoryStoreDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_MasterCityStoreDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StoreDetails] DROP CONSTRAINT [FK_MasterCityStoreDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_StoreDetailStoreItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StoreItems] DROP CONSTRAINT [FK_StoreDetailStoreItem];
GO
IF OBJECT_ID(N'[dbo].[FK_StoreDetailCartDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CartDetails] DROP CONSTRAINT [FK_StoreDetailCartDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_MasterAreaStoreDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StoreDetails] DROP CONSTRAINT [FK_MasterAreaStoreDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_StoreDetailStorePickupPoint]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StorePickupPoints] DROP CONSTRAINT [FK_StoreDetailStorePickupPoint];
GO
IF OBJECT_ID(N'[dbo].[FK_StoreDetailStorePickupPartner]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StorePickupPartners] DROP CONSTRAINT [FK_StoreDetailStorePickupPartner];
GO
IF OBJECT_ID(N'[dbo].[FK_StoreDetailStoreImage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StoreImages] DROP CONSTRAINT [FK_StoreDetailStoreImage];
GO
IF OBJECT_ID(N'[dbo].[FK_StoreDetailStorePayment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StorePayments] DROP CONSTRAINT [FK_StoreDetailStorePayment];
GO
IF OBJECT_ID(N'[dbo].[FK_StoreDetailStoreQue]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StoreKiosks] DROP CONSTRAINT [FK_StoreDetailStoreQue];
GO
IF OBJECT_ID(N'[dbo].[FK_MasterCityMasterBarangay]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MasterAreas] DROP CONSTRAINT [FK_MasterCityMasterBarangay];
GO
IF OBJECT_ID(N'[dbo].[FK_ItemMasterItemMasterCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ItemMasterCategories] DROP CONSTRAINT [FK_ItemMasterItemMasterCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_ItemMasterStoreItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StoreItems] DROP CONSTRAINT [FK_ItemMasterStoreItem];
GO
IF OBJECT_ID(N'[dbo].[FK_ItemMasterItemImage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ItemImages] DROP CONSTRAINT [FK_ItemMasterItemImage];
GO
IF OBJECT_ID(N'[dbo].[FK_ItemCategoryStoreItemCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ItemMasterCategories] DROP CONSTRAINT [FK_ItemCategoryStoreItemCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_ItemCatGroupItemCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ItemCategories] DROP CONSTRAINT [FK_ItemCatGroupItemCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_CartDetailCartItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CartItems] DROP CONSTRAINT [FK_CartDetailCartItem];
GO
IF OBJECT_ID(N'[dbo].[FK_StoreItemCartItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CartItems] DROP CONSTRAINT [FK_StoreItemCartItem];
GO
IF OBJECT_ID(N'[dbo].[FK_CartItemStatusCartItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CartItems] DROP CONSTRAINT [FK_CartItemStatusCartItem];
GO
IF OBJECT_ID(N'[dbo].[FK_CartStatusCartDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CartDetails] DROP CONSTRAINT [FK_CartStatusCartDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_CartStatusCartHistory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CartHistories] DROP CONSTRAINT [FK_CartStatusCartHistory];
GO
IF OBJECT_ID(N'[dbo].[FK_StorePickupPointStorePickupPartner]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StorePickupPartners] DROP CONSTRAINT [FK_StorePickupPointStorePickupPartner];
GO
IF OBJECT_ID(N'[dbo].[FK_StorePickupStatusStorePickupPoint]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StorePickupPoints] DROP CONSTRAINT [FK_StorePickupStatusStorePickupPoint];
GO
IF OBJECT_ID(N'[dbo].[FK_CartDetailCartDelivery]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CartDeliveries] DROP CONSTRAINT [FK_CartDetailCartDelivery];
GO
IF OBJECT_ID(N'[dbo].[FK_RiderDetailCartDelivery]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CartDeliveries] DROP CONSTRAINT [FK_RiderDetailCartDelivery];
GO
IF OBJECT_ID(N'[dbo].[FK_CartDeliveryCartDeliveryActivity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CartActivities] DROP CONSTRAINT [FK_CartDeliveryCartDeliveryActivity];
GO
IF OBJECT_ID(N'[dbo].[FK_RiderStatusRiderDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RiderDetails] DROP CONSTRAINT [FK_RiderStatusRiderDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_MasterCityRiderDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RiderDetails] DROP CONSTRAINT [FK_MasterCityRiderDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_CartActivityTypeCartActivity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CartActivities] DROP CONSTRAINT [FK_CartActivityTypeCartActivity];
GO
IF OBJECT_ID(N'[dbo].[FK_StoreImgTypeStoreImage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StoreImages] DROP CONSTRAINT [FK_StoreImgTypeStoreImage];
GO
IF OBJECT_ID(N'[dbo].[FK_StorePaymentTypeStorePayment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StorePayments] DROP CONSTRAINT [FK_StorePaymentTypeStorePayment];
GO
IF OBJECT_ID(N'[dbo].[FK_StorePaymentStatusStorePayment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StorePayments] DROP CONSTRAINT [FK_StorePaymentStatusStorePayment];
GO
IF OBJECT_ID(N'[dbo].[FK_CartDetailPaymentDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PaymentDetails] DROP CONSTRAINT [FK_CartDetailPaymentDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_PaymentReceiverPaymentDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PaymentDetails] DROP CONSTRAINT [FK_PaymentReceiverPaymentDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_PaymentStatusPaymentDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PaymentDetails] DROP CONSTRAINT [FK_PaymentStatusPaymentDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_PaymentPartyPaymentDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PaymentDetails] DROP CONSTRAINT [FK_PaymentPartyPaymentDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_RiderDetailRiderCashDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RiderCashDetails] DROP CONSTRAINT [FK_RiderDetailRiderCashDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_RiderCashPartyRiderCashDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RiderCashDetails] DROP CONSTRAINT [FK_RiderCashPartyRiderCashDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_CartDetailRiderCashDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RiderCashDetails] DROP CONSTRAINT [FK_CartDetailRiderCashDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_CartDetailStoreQueOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StoreKioskOrders] DROP CONSTRAINT [FK_CartDetailStoreQueOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_StoreKioskStoreKioskOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StoreKioskOrders] DROP CONSTRAINT [FK_StoreKioskStoreKioskOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_StorePickupPointCartDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CartDetails] DROP CONSTRAINT [FK_StorePickupPointCartDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_CartDetailCartHistory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CartHistories] DROP CONSTRAINT [FK_CartDetailCartHistory];
GO
IF OBJECT_ID(N'[dbo].[FK_CartReleaseCartDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CartReleases] DROP CONSTRAINT [FK_CartReleaseCartDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_CartReleaseUserDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CartReleases] DROP CONSTRAINT [FK_CartReleaseUserDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_StorePickupPointCartRelease]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CartReleases] DROP CONSTRAINT [FK_StorePickupPointCartRelease];
GO
IF OBJECT_ID(N'[dbo].[FK_UserDetailUserApplication]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserApplications] DROP CONSTRAINT [FK_UserDetailUserApplication];
GO
IF OBJECT_ID(N'[dbo].[FK_UserApplicationStatusUserApplication]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserApplications] DROP CONSTRAINT [FK_UserApplicationStatusUserApplication];
GO
IF OBJECT_ID(N'[dbo].[FK_UserApplicationTypeUserApplication]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserApplications] DROP CONSTRAINT [FK_UserApplicationTypeUserApplication];
GO
IF OBJECT_ID(N'[dbo].[FK_StoreDetailStoreUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StoreUsers] DROP CONSTRAINT [FK_StoreDetailStoreUser];
GO
IF OBJECT_ID(N'[dbo].[FK_StoreUserTypeStoreUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StoreUsers] DROP CONSTRAINT [FK_StoreUserTypeStoreUser];
GO
IF OBJECT_ID(N'[dbo].[FK_UserDetailStoreUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StoreUsers] DROP CONSTRAINT [FK_UserDetailStoreUser];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UserDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserDetails];
GO
IF OBJECT_ID(N'[dbo].[StoreDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StoreDetails];
GO
IF OBJECT_ID(N'[dbo].[UserStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserStatus];
GO
IF OBJECT_ID(N'[dbo].[StoreStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StoreStatus];
GO
IF OBJECT_ID(N'[dbo].[StoreCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StoreCategories];
GO
IF OBJECT_ID(N'[dbo].[MasterCities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MasterCities];
GO
IF OBJECT_ID(N'[dbo].[MasterAreas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MasterAreas];
GO
IF OBJECT_ID(N'[dbo].[ItemMasters]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ItemMasters];
GO
IF OBJECT_ID(N'[dbo].[ItemCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ItemCategories];
GO
IF OBJECT_ID(N'[dbo].[ItemMasterCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ItemMasterCategories];
GO
IF OBJECT_ID(N'[dbo].[StoreItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StoreItems];
GO
IF OBJECT_ID(N'[dbo].[CartDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CartDetails];
GO
IF OBJECT_ID(N'[dbo].[CartItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CartItems];
GO
IF OBJECT_ID(N'[dbo].[CartStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CartStatus];
GO
IF OBJECT_ID(N'[dbo].[CartHistories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CartHistories];
GO
IF OBJECT_ID(N'[dbo].[CartItemStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CartItemStatus];
GO
IF OBJECT_ID(N'[dbo].[ItemCatGroups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ItemCatGroups];
GO
IF OBJECT_ID(N'[dbo].[StorePickupPoints]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StorePickupPoints];
GO
IF OBJECT_ID(N'[dbo].[StorePickupPartners]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StorePickupPartners];
GO
IF OBJECT_ID(N'[dbo].[StorePickupStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StorePickupStatus];
GO
IF OBJECT_ID(N'[dbo].[CartDeliveries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CartDeliveries];
GO
IF OBJECT_ID(N'[dbo].[RiderDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RiderDetails];
GO
IF OBJECT_ID(N'[dbo].[RiderStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RiderStatus];
GO
IF OBJECT_ID(N'[dbo].[CartActivities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CartActivities];
GO
IF OBJECT_ID(N'[dbo].[CartActivityTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CartActivityTypes];
GO
IF OBJECT_ID(N'[dbo].[StoreImages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StoreImages];
GO
IF OBJECT_ID(N'[dbo].[ItemImages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ItemImages];
GO
IF OBJECT_ID(N'[dbo].[StoreImgTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StoreImgTypes];
GO
IF OBJECT_ID(N'[dbo].[StorePayments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StorePayments];
GO
IF OBJECT_ID(N'[dbo].[StorePaymentTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StorePaymentTypes];
GO
IF OBJECT_ID(N'[dbo].[StorePaymentStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StorePaymentStatus];
GO
IF OBJECT_ID(N'[dbo].[PaymentDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PaymentDetails];
GO
IF OBJECT_ID(N'[dbo].[PaymentReceivers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PaymentReceivers];
GO
IF OBJECT_ID(N'[dbo].[PaymentStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PaymentStatus];
GO
IF OBJECT_ID(N'[dbo].[RiderCashDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RiderCashDetails];
GO
IF OBJECT_ID(N'[dbo].[RiderCashParties]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RiderCashParties];
GO
IF OBJECT_ID(N'[dbo].[PaymentParties]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PaymentParties];
GO
IF OBJECT_ID(N'[dbo].[StoreKiosks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StoreKiosks];
GO
IF OBJECT_ID(N'[dbo].[StoreKioskOrders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StoreKioskOrders];
GO
IF OBJECT_ID(N'[dbo].[CartReleases]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CartReleases];
GO
IF OBJECT_ID(N'[dbo].[UserApplicationStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserApplicationStatus];
GO
IF OBJECT_ID(N'[dbo].[UserApplicationTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserApplicationTypes];
GO
IF OBJECT_ID(N'[dbo].[UserApplications]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserApplications];
GO
IF OBJECT_ID(N'[dbo].[StoreUserTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StoreUserTypes];
GO
IF OBJECT_ID(N'[dbo].[StoreUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StoreUsers];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserDetails'
CREATE TABLE [dbo].[UserDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(50)  NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Address] nvarchar(150)  NULL,
    [Email] nvarchar(30)  NULL,
    [Mobile] nvarchar(20)  NOT NULL,
    [Remarks] nvarchar(50)  NULL,
    [UserStatusId] int  NOT NULL,
    [MasterCityId] int  NOT NULL,
    [MasterAreaId] int  NOT NULL
);
GO

-- Creating table 'StoreDetails'
CREATE TABLE [dbo].[StoreDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LoginId] nvarchar(50)  NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Address] nvarchar(150)  NOT NULL,
    [Remarks] nvarchar(80)  NULL,
    [StoreStatusId] int  NOT NULL,
    [StoreCategoryId] int  NOT NULL,
    [MasterCityId] int  NOT NULL,
    [MasterAreaId] int  NOT NULL
);
GO

-- Creating table 'UserStatus'
CREATE TABLE [dbo].[UserStatus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(15)  NOT NULL
);
GO

-- Creating table 'StoreStatus'
CREATE TABLE [dbo].[StoreStatus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(10)  NOT NULL
);
GO

-- Creating table 'StoreCategories'
CREATE TABLE [dbo].[StoreCategories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(20)  NOT NULL,
    [SortOrder] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'MasterCities'
CREATE TABLE [dbo].[MasterCities] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(30)  NOT NULL
);
GO

-- Creating table 'MasterAreas'
CREATE TABLE [dbo].[MasterAreas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(30)  NOT NULL,
    [MasterCityId] int  NOT NULL
);
GO

-- Creating table 'ItemMasters'
CREATE TABLE [dbo].[ItemMasters] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(80)  NOT NULL
);
GO

-- Creating table 'ItemCategories'
CREATE TABLE [dbo].[ItemCategories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(20)  NOT NULL,
    [SortOrder] int  NOT NULL,
    [ItemCatGroupId] int  NOT NULL
);
GO

-- Creating table 'ItemMasterCategories'
CREATE TABLE [dbo].[ItemMasterCategories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ItemCategoryId] int  NOT NULL,
    [ItemMasterId] int  NOT NULL
);
GO

-- Creating table 'StoreItems'
CREATE TABLE [dbo].[StoreItems] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ItemMasterId] int  NOT NULL,
    [StoreDetailId] int  NOT NULL,
    [UnitPrice] decimal(18,0)  NOT NULL
);
GO

-- Creating table 'CartDetails'
CREATE TABLE [dbo].[CartDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserDetailId] int  NOT NULL,
    [StoreDetailId] int  NOT NULL,
    [CartStatusId] int  NOT NULL,
    [DtPickup] datetime  NOT NULL,
    [DeliveryType] nvarchar(10)  NOT NULL,
    [StorePickupPointId] int  NOT NULL
);
GO

-- Creating table 'CartItems'
CREATE TABLE [dbo].[CartItems] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CartDetailId] int  NOT NULL,
    [StoreItemId] int  NOT NULL,
    [ItemQty] decimal(18,0)  NOT NULL,
    [ItemOrder] nvarchar(max)  NOT NULL,
    [CartItemStatusId] int  NOT NULL,
    [Remarks1] nvarchar(80)  NULL,
    [Remarks2] nvarchar(80)  NULL
);
GO

-- Creating table 'CartStatus'
CREATE TABLE [dbo].[CartStatus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(10)  NOT NULL
);
GO

-- Creating table 'CartHistories'
CREATE TABLE [dbo].[CartHistories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CartStatusId] int  NOT NULL,
    [UserId] nvarchar(40)  NOT NULL,
    [dtStatus] datetime  NOT NULL,
    [CartDetailId] int  NOT NULL
);
GO

-- Creating table 'CartItemStatus'
CREATE TABLE [dbo].[CartItemStatus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(10)  NOT NULL
);
GO

-- Creating table 'ItemCatGroups'
CREATE TABLE [dbo].[ItemCatGroups] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(20)  NOT NULL,
    [SortOrder] int  NOT NULL
);
GO

-- Creating table 'StorePickupPoints'
CREATE TABLE [dbo].[StorePickupPoints] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StoreDetailId] int  NOT NULL,
    [Address] nvarchar(250)  NOT NULL,
    [Remarks] nvarchar(150)  NULL,
    [StorePickupStatusId] int  NOT NULL
);
GO

-- Creating table 'StorePickupPartners'
CREATE TABLE [dbo].[StorePickupPartners] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StorePickupPointId] int  NOT NULL,
    [StoreDetailId] int  NOT NULL
);
GO

-- Creating table 'StorePickupStatus'
CREATE TABLE [dbo].[StorePickupStatus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(10)  NOT NULL
);
GO

-- Creating table 'CartDeliveries'
CREATE TABLE [dbo].[CartDeliveries] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CartDetailId] int  NOT NULL,
    [dtDelivery] datetime  NOT NULL,
    [Address] nvarchar(250)  NOT NULL,
    [Remarks] nvarchar(180)  NULL,
    [RiderDetailId] int  NOT NULL
);
GO

-- Creating table 'RiderDetails'
CREATE TABLE [dbo].[RiderDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(50)  NOT NULL,
    [Name] nvarchar(150)  NOT NULL,
    [Address] nvarchar(180)  NOT NULL,
    [Mobile] nvarchar(20)  NOT NULL,
    [Remarks] nvarchar(180)  NULL,
    [RiderStatusId] int  NOT NULL,
    [MasterCityId] int  NOT NULL,
    [Mobile2] nvarchar(20)  NULL
);
GO

-- Creating table 'RiderStatus'
CREATE TABLE [dbo].[RiderStatus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(10)  NOT NULL
);
GO

-- Creating table 'CartActivities'
CREATE TABLE [dbo].[CartActivities] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CartDeliveryId] int  NOT NULL,
    [dtActivity] datetime  NOT NULL,
    [CartActivityTypeId] int  NOT NULL
);
GO

-- Creating table 'CartActivityTypes'
CREATE TABLE [dbo].[CartActivityTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(20)  NOT NULL,
    [SortOrder] int  NOT NULL
);
GO

-- Creating table 'StoreImages'
CREATE TABLE [dbo].[StoreImages] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StoreDetailId] int  NOT NULL,
    [ImageUrl] nvarchar(250)  NOT NULL,
    [StoreImgTypeId] int  NOT NULL
);
GO

-- Creating table 'ItemImages'
CREATE TABLE [dbo].[ItemImages] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ItemMasterId] int  NOT NULL,
    [ImageUrl] nvarchar(250)  NOT NULL
);
GO

-- Creating table 'StoreImgTypes'
CREATE TABLE [dbo].[StoreImgTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(30)  NOT NULL
);
GO

-- Creating table 'StorePayments'
CREATE TABLE [dbo].[StorePayments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StoreDetailId] int  NOT NULL,
    [dtPayment] datetime  NOT NULL,
    [Amount] decimal(18,0)  NOT NULL,
    [StorePaymentTypeId] int  NOT NULL,
    [Remarks] nvarchar(150)  NULL,
    [dtPosted] datetime  NULL,
    [StorePaymentStatusId] int  NOT NULL
);
GO

-- Creating table 'StorePaymentTypes'
CREATE TABLE [dbo].[StorePaymentTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(150)  NOT NULL,
    [Remarks] nvarchar(200)  NULL
);
GO

-- Creating table 'StorePaymentStatus'
CREATE TABLE [dbo].[StorePaymentStatus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(20)  NOT NULL
);
GO

-- Creating table 'PaymentDetails'
CREATE TABLE [dbo].[PaymentDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CartDetailId] int  NOT NULL,
    [Amount] decimal(18,0)  NOT NULL,
    [dtPayment] datetime  NOT NULL,
    [PaymentReceiverId] int  NOT NULL,
    [ReceiverInfo] nvarchar(30)  NULL,
    [PaymentPartyId] int  NOT NULL,
    [PartyInfo] nvarchar(30)  NULL,
    [PaymentStatusId] int  NOT NULL
);
GO

-- Creating table 'PaymentReceivers'
CREATE TABLE [dbo].[PaymentReceivers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'PaymentStatus'
CREATE TABLE [dbo].[PaymentStatus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(10)  NOT NULL
);
GO

-- Creating table 'RiderCashDetails'
CREATE TABLE [dbo].[RiderCashDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RiderDetailId] int  NOT NULL,
    [DtCash] datetime  NOT NULL,
    [Amount] decimal(18,0)  NOT NULL,
    [RiderCashPartyId] int  NOT NULL,
    [CartDetailId] int  NOT NULL
);
GO

-- Creating table 'RiderCashParties'
CREATE TABLE [dbo].[RiderCashParties] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(30)  NOT NULL,
    [Operation] nvarchar(10)  NOT NULL
);
GO

-- Creating table 'PaymentParties'
CREATE TABLE [dbo].[PaymentParties] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(20)  NOT NULL
);
GO

-- Creating table 'StoreKiosks'
CREATE TABLE [dbo].[StoreKiosks] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StoreDetailId] int  NOT NULL,
    [KioskName] nvarchar(20)  NOT NULL,
    [SettingId] smallint  NULL
);
GO

-- Creating table 'StoreKioskOrders'
CREATE TABLE [dbo].[StoreKioskOrders] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Customer] nvarchar(30)  NULL,
    [DtOrder] datetime  NOT NULL,
    [CartDetailId] int  NOT NULL,
    [StoreKioskId] int  NOT NULL
);
GO

-- Creating table 'CartReleases'
CREATE TABLE [dbo].[CartReleases] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DtRelease] datetime  NOT NULL,
    [ReleaseBy] nvarchar(30)  NULL,
    [LoginId] nvarchar(50)  NOT NULL,
    [CartDetailId] int  NOT NULL,
    [UserDetailId] int  NOT NULL,
    [StorePickupPointId] int  NOT NULL
);
GO

-- Creating table 'UserApplicationStatus'
CREATE TABLE [dbo].[UserApplicationStatus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'UserApplicationTypes'
CREATE TABLE [dbo].[UserApplicationTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'UserApplications'
CREATE TABLE [dbo].[UserApplications] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DtApplied] datetime  NOT NULL,
    [Email] nvarchar(30)  NOT NULL,
    [Mobile] nchar(20)  NOT NULL,
    [UserDetailId] int  NOT NULL,
    [UserApplicationStatusId] int  NOT NULL,
    [UserApplicationTypeId] int  NOT NULL
);
GO

-- Creating table 'StoreUserTypes'
CREATE TABLE [dbo].[StoreUserTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'StoreUsers'
CREATE TABLE [dbo].[StoreUsers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StoreDetailId] int  NOT NULL,
    [StoreUserTypeId] int  NOT NULL,
    [UserDetailId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'UserDetails'
ALTER TABLE [dbo].[UserDetails]
ADD CONSTRAINT [PK_UserDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StoreDetails'
ALTER TABLE [dbo].[StoreDetails]
ADD CONSTRAINT [PK_StoreDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserStatus'
ALTER TABLE [dbo].[UserStatus]
ADD CONSTRAINT [PK_UserStatus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StoreStatus'
ALTER TABLE [dbo].[StoreStatus]
ADD CONSTRAINT [PK_StoreStatus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StoreCategories'
ALTER TABLE [dbo].[StoreCategories]
ADD CONSTRAINT [PK_StoreCategories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MasterCities'
ALTER TABLE [dbo].[MasterCities]
ADD CONSTRAINT [PK_MasterCities]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MasterAreas'
ALTER TABLE [dbo].[MasterAreas]
ADD CONSTRAINT [PK_MasterAreas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ItemMasters'
ALTER TABLE [dbo].[ItemMasters]
ADD CONSTRAINT [PK_ItemMasters]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ItemCategories'
ALTER TABLE [dbo].[ItemCategories]
ADD CONSTRAINT [PK_ItemCategories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ItemMasterCategories'
ALTER TABLE [dbo].[ItemMasterCategories]
ADD CONSTRAINT [PK_ItemMasterCategories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StoreItems'
ALTER TABLE [dbo].[StoreItems]
ADD CONSTRAINT [PK_StoreItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CartDetails'
ALTER TABLE [dbo].[CartDetails]
ADD CONSTRAINT [PK_CartDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CartItems'
ALTER TABLE [dbo].[CartItems]
ADD CONSTRAINT [PK_CartItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CartStatus'
ALTER TABLE [dbo].[CartStatus]
ADD CONSTRAINT [PK_CartStatus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CartHistories'
ALTER TABLE [dbo].[CartHistories]
ADD CONSTRAINT [PK_CartHistories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CartItemStatus'
ALTER TABLE [dbo].[CartItemStatus]
ADD CONSTRAINT [PK_CartItemStatus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ItemCatGroups'
ALTER TABLE [dbo].[ItemCatGroups]
ADD CONSTRAINT [PK_ItemCatGroups]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StorePickupPoints'
ALTER TABLE [dbo].[StorePickupPoints]
ADD CONSTRAINT [PK_StorePickupPoints]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StorePickupPartners'
ALTER TABLE [dbo].[StorePickupPartners]
ADD CONSTRAINT [PK_StorePickupPartners]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StorePickupStatus'
ALTER TABLE [dbo].[StorePickupStatus]
ADD CONSTRAINT [PK_StorePickupStatus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CartDeliveries'
ALTER TABLE [dbo].[CartDeliveries]
ADD CONSTRAINT [PK_CartDeliveries]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RiderDetails'
ALTER TABLE [dbo].[RiderDetails]
ADD CONSTRAINT [PK_RiderDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RiderStatus'
ALTER TABLE [dbo].[RiderStatus]
ADD CONSTRAINT [PK_RiderStatus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CartActivities'
ALTER TABLE [dbo].[CartActivities]
ADD CONSTRAINT [PK_CartActivities]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CartActivityTypes'
ALTER TABLE [dbo].[CartActivityTypes]
ADD CONSTRAINT [PK_CartActivityTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StoreImages'
ALTER TABLE [dbo].[StoreImages]
ADD CONSTRAINT [PK_StoreImages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ItemImages'
ALTER TABLE [dbo].[ItemImages]
ADD CONSTRAINT [PK_ItemImages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StoreImgTypes'
ALTER TABLE [dbo].[StoreImgTypes]
ADD CONSTRAINT [PK_StoreImgTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StorePayments'
ALTER TABLE [dbo].[StorePayments]
ADD CONSTRAINT [PK_StorePayments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StorePaymentTypes'
ALTER TABLE [dbo].[StorePaymentTypes]
ADD CONSTRAINT [PK_StorePaymentTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StorePaymentStatus'
ALTER TABLE [dbo].[StorePaymentStatus]
ADD CONSTRAINT [PK_StorePaymentStatus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PaymentDetails'
ALTER TABLE [dbo].[PaymentDetails]
ADD CONSTRAINT [PK_PaymentDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PaymentReceivers'
ALTER TABLE [dbo].[PaymentReceivers]
ADD CONSTRAINT [PK_PaymentReceivers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PaymentStatus'
ALTER TABLE [dbo].[PaymentStatus]
ADD CONSTRAINT [PK_PaymentStatus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RiderCashDetails'
ALTER TABLE [dbo].[RiderCashDetails]
ADD CONSTRAINT [PK_RiderCashDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RiderCashParties'
ALTER TABLE [dbo].[RiderCashParties]
ADD CONSTRAINT [PK_RiderCashParties]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PaymentParties'
ALTER TABLE [dbo].[PaymentParties]
ADD CONSTRAINT [PK_PaymentParties]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StoreKiosks'
ALTER TABLE [dbo].[StoreKiosks]
ADD CONSTRAINT [PK_StoreKiosks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StoreKioskOrders'
ALTER TABLE [dbo].[StoreKioskOrders]
ADD CONSTRAINT [PK_StoreKioskOrders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CartReleases'
ALTER TABLE [dbo].[CartReleases]
ADD CONSTRAINT [PK_CartReleases]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserApplicationStatus'
ALTER TABLE [dbo].[UserApplicationStatus]
ADD CONSTRAINT [PK_UserApplicationStatus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserApplicationTypes'
ALTER TABLE [dbo].[UserApplicationTypes]
ADD CONSTRAINT [PK_UserApplicationTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserApplications'
ALTER TABLE [dbo].[UserApplications]
ADD CONSTRAINT [PK_UserApplications]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StoreUserTypes'
ALTER TABLE [dbo].[StoreUserTypes]
ADD CONSTRAINT [PK_StoreUserTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StoreUsers'
ALTER TABLE [dbo].[StoreUsers]
ADD CONSTRAINT [PK_StoreUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserStatusId] in table 'UserDetails'
ALTER TABLE [dbo].[UserDetails]
ADD CONSTRAINT [FK_UserStatusUserDetail]
    FOREIGN KEY ([UserStatusId])
    REFERENCES [dbo].[UserStatus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserStatusUserDetail'
CREATE INDEX [IX_FK_UserStatusUserDetail]
ON [dbo].[UserDetails]
    ([UserStatusId]);
GO

-- Creating foreign key on [MasterCityId] in table 'UserDetails'
ALTER TABLE [dbo].[UserDetails]
ADD CONSTRAINT [FK_MasterCityUserDetail]
    FOREIGN KEY ([MasterCityId])
    REFERENCES [dbo].[MasterCities]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MasterCityUserDetail'
CREATE INDEX [IX_FK_MasterCityUserDetail]
ON [dbo].[UserDetails]
    ([MasterCityId]);
GO

-- Creating foreign key on [UserDetailId] in table 'CartDetails'
ALTER TABLE [dbo].[CartDetails]
ADD CONSTRAINT [FK_UserDetailCartDetail]
    FOREIGN KEY ([UserDetailId])
    REFERENCES [dbo].[UserDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserDetailCartDetail'
CREATE INDEX [IX_FK_UserDetailCartDetail]
ON [dbo].[CartDetails]
    ([UserDetailId]);
GO

-- Creating foreign key on [MasterAreaId] in table 'UserDetails'
ALTER TABLE [dbo].[UserDetails]
ADD CONSTRAINT [FK_MasterAreaUserDetail]
    FOREIGN KEY ([MasterAreaId])
    REFERENCES [dbo].[MasterAreas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MasterAreaUserDetail'
CREATE INDEX [IX_FK_MasterAreaUserDetail]
ON [dbo].[UserDetails]
    ([MasterAreaId]);
GO

-- Creating foreign key on [StoreStatusId] in table 'StoreDetails'
ALTER TABLE [dbo].[StoreDetails]
ADD CONSTRAINT [FK_StoreStatusStoreDetail]
    FOREIGN KEY ([StoreStatusId])
    REFERENCES [dbo].[StoreStatus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StoreStatusStoreDetail'
CREATE INDEX [IX_FK_StoreStatusStoreDetail]
ON [dbo].[StoreDetails]
    ([StoreStatusId]);
GO

-- Creating foreign key on [StoreCategoryId] in table 'StoreDetails'
ALTER TABLE [dbo].[StoreDetails]
ADD CONSTRAINT [FK_StoreCategoryStoreDetail]
    FOREIGN KEY ([StoreCategoryId])
    REFERENCES [dbo].[StoreCategories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StoreCategoryStoreDetail'
CREATE INDEX [IX_FK_StoreCategoryStoreDetail]
ON [dbo].[StoreDetails]
    ([StoreCategoryId]);
GO

-- Creating foreign key on [MasterCityId] in table 'StoreDetails'
ALTER TABLE [dbo].[StoreDetails]
ADD CONSTRAINT [FK_MasterCityStoreDetail]
    FOREIGN KEY ([MasterCityId])
    REFERENCES [dbo].[MasterCities]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MasterCityStoreDetail'
CREATE INDEX [IX_FK_MasterCityStoreDetail]
ON [dbo].[StoreDetails]
    ([MasterCityId]);
GO

-- Creating foreign key on [StoreDetailId] in table 'StoreItems'
ALTER TABLE [dbo].[StoreItems]
ADD CONSTRAINT [FK_StoreDetailStoreItem]
    FOREIGN KEY ([StoreDetailId])
    REFERENCES [dbo].[StoreDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StoreDetailStoreItem'
CREATE INDEX [IX_FK_StoreDetailStoreItem]
ON [dbo].[StoreItems]
    ([StoreDetailId]);
GO

-- Creating foreign key on [StoreDetailId] in table 'CartDetails'
ALTER TABLE [dbo].[CartDetails]
ADD CONSTRAINT [FK_StoreDetailCartDetail]
    FOREIGN KEY ([StoreDetailId])
    REFERENCES [dbo].[StoreDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StoreDetailCartDetail'
CREATE INDEX [IX_FK_StoreDetailCartDetail]
ON [dbo].[CartDetails]
    ([StoreDetailId]);
GO

-- Creating foreign key on [MasterAreaId] in table 'StoreDetails'
ALTER TABLE [dbo].[StoreDetails]
ADD CONSTRAINT [FK_MasterAreaStoreDetail]
    FOREIGN KEY ([MasterAreaId])
    REFERENCES [dbo].[MasterAreas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MasterAreaStoreDetail'
CREATE INDEX [IX_FK_MasterAreaStoreDetail]
ON [dbo].[StoreDetails]
    ([MasterAreaId]);
GO

-- Creating foreign key on [StoreDetailId] in table 'StorePickupPoints'
ALTER TABLE [dbo].[StorePickupPoints]
ADD CONSTRAINT [FK_StoreDetailStorePickupPoint]
    FOREIGN KEY ([StoreDetailId])
    REFERENCES [dbo].[StoreDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StoreDetailStorePickupPoint'
CREATE INDEX [IX_FK_StoreDetailStorePickupPoint]
ON [dbo].[StorePickupPoints]
    ([StoreDetailId]);
GO

-- Creating foreign key on [StoreDetailId] in table 'StorePickupPartners'
ALTER TABLE [dbo].[StorePickupPartners]
ADD CONSTRAINT [FK_StoreDetailStorePickupPartner]
    FOREIGN KEY ([StoreDetailId])
    REFERENCES [dbo].[StoreDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StoreDetailStorePickupPartner'
CREATE INDEX [IX_FK_StoreDetailStorePickupPartner]
ON [dbo].[StorePickupPartners]
    ([StoreDetailId]);
GO

-- Creating foreign key on [StoreDetailId] in table 'StoreImages'
ALTER TABLE [dbo].[StoreImages]
ADD CONSTRAINT [FK_StoreDetailStoreImage]
    FOREIGN KEY ([StoreDetailId])
    REFERENCES [dbo].[StoreDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StoreDetailStoreImage'
CREATE INDEX [IX_FK_StoreDetailStoreImage]
ON [dbo].[StoreImages]
    ([StoreDetailId]);
GO

-- Creating foreign key on [StoreDetailId] in table 'StorePayments'
ALTER TABLE [dbo].[StorePayments]
ADD CONSTRAINT [FK_StoreDetailStorePayment]
    FOREIGN KEY ([StoreDetailId])
    REFERENCES [dbo].[StoreDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StoreDetailStorePayment'
CREATE INDEX [IX_FK_StoreDetailStorePayment]
ON [dbo].[StorePayments]
    ([StoreDetailId]);
GO

-- Creating foreign key on [StoreDetailId] in table 'StoreKiosks'
ALTER TABLE [dbo].[StoreKiosks]
ADD CONSTRAINT [FK_StoreDetailStoreQue]
    FOREIGN KEY ([StoreDetailId])
    REFERENCES [dbo].[StoreDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StoreDetailStoreQue'
CREATE INDEX [IX_FK_StoreDetailStoreQue]
ON [dbo].[StoreKiosks]
    ([StoreDetailId]);
GO

-- Creating foreign key on [MasterCityId] in table 'MasterAreas'
ALTER TABLE [dbo].[MasterAreas]
ADD CONSTRAINT [FK_MasterCityMasterBarangay]
    FOREIGN KEY ([MasterCityId])
    REFERENCES [dbo].[MasterCities]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MasterCityMasterBarangay'
CREATE INDEX [IX_FK_MasterCityMasterBarangay]
ON [dbo].[MasterAreas]
    ([MasterCityId]);
GO

-- Creating foreign key on [ItemMasterId] in table 'ItemMasterCategories'
ALTER TABLE [dbo].[ItemMasterCategories]
ADD CONSTRAINT [FK_ItemMasterItemMasterCategory]
    FOREIGN KEY ([ItemMasterId])
    REFERENCES [dbo].[ItemMasters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ItemMasterItemMasterCategory'
CREATE INDEX [IX_FK_ItemMasterItemMasterCategory]
ON [dbo].[ItemMasterCategories]
    ([ItemMasterId]);
GO

-- Creating foreign key on [ItemMasterId] in table 'StoreItems'
ALTER TABLE [dbo].[StoreItems]
ADD CONSTRAINT [FK_ItemMasterStoreItem]
    FOREIGN KEY ([ItemMasterId])
    REFERENCES [dbo].[ItemMasters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ItemMasterStoreItem'
CREATE INDEX [IX_FK_ItemMasterStoreItem]
ON [dbo].[StoreItems]
    ([ItemMasterId]);
GO

-- Creating foreign key on [ItemMasterId] in table 'ItemImages'
ALTER TABLE [dbo].[ItemImages]
ADD CONSTRAINT [FK_ItemMasterItemImage]
    FOREIGN KEY ([ItemMasterId])
    REFERENCES [dbo].[ItemMasters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ItemMasterItemImage'
CREATE INDEX [IX_FK_ItemMasterItemImage]
ON [dbo].[ItemImages]
    ([ItemMasterId]);
GO

-- Creating foreign key on [ItemCategoryId] in table 'ItemMasterCategories'
ALTER TABLE [dbo].[ItemMasterCategories]
ADD CONSTRAINT [FK_ItemCategoryStoreItemCategory]
    FOREIGN KEY ([ItemCategoryId])
    REFERENCES [dbo].[ItemCategories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ItemCategoryStoreItemCategory'
CREATE INDEX [IX_FK_ItemCategoryStoreItemCategory]
ON [dbo].[ItemMasterCategories]
    ([ItemCategoryId]);
GO

-- Creating foreign key on [ItemCatGroupId] in table 'ItemCategories'
ALTER TABLE [dbo].[ItemCategories]
ADD CONSTRAINT [FK_ItemCatGroupItemCategory]
    FOREIGN KEY ([ItemCatGroupId])
    REFERENCES [dbo].[ItemCatGroups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ItemCatGroupItemCategory'
CREATE INDEX [IX_FK_ItemCatGroupItemCategory]
ON [dbo].[ItemCategories]
    ([ItemCatGroupId]);
GO

-- Creating foreign key on [CartDetailId] in table 'CartItems'
ALTER TABLE [dbo].[CartItems]
ADD CONSTRAINT [FK_CartDetailCartItem]
    FOREIGN KEY ([CartDetailId])
    REFERENCES [dbo].[CartDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CartDetailCartItem'
CREATE INDEX [IX_FK_CartDetailCartItem]
ON [dbo].[CartItems]
    ([CartDetailId]);
GO

-- Creating foreign key on [StoreItemId] in table 'CartItems'
ALTER TABLE [dbo].[CartItems]
ADD CONSTRAINT [FK_StoreItemCartItem]
    FOREIGN KEY ([StoreItemId])
    REFERENCES [dbo].[StoreItems]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StoreItemCartItem'
CREATE INDEX [IX_FK_StoreItemCartItem]
ON [dbo].[CartItems]
    ([StoreItemId]);
GO

-- Creating foreign key on [CartItemStatusId] in table 'CartItems'
ALTER TABLE [dbo].[CartItems]
ADD CONSTRAINT [FK_CartItemStatusCartItem]
    FOREIGN KEY ([CartItemStatusId])
    REFERENCES [dbo].[CartItemStatus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CartItemStatusCartItem'
CREATE INDEX [IX_FK_CartItemStatusCartItem]
ON [dbo].[CartItems]
    ([CartItemStatusId]);
GO

-- Creating foreign key on [CartStatusId] in table 'CartDetails'
ALTER TABLE [dbo].[CartDetails]
ADD CONSTRAINT [FK_CartStatusCartDetail]
    FOREIGN KEY ([CartStatusId])
    REFERENCES [dbo].[CartStatus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CartStatusCartDetail'
CREATE INDEX [IX_FK_CartStatusCartDetail]
ON [dbo].[CartDetails]
    ([CartStatusId]);
GO

-- Creating foreign key on [CartStatusId] in table 'CartHistories'
ALTER TABLE [dbo].[CartHistories]
ADD CONSTRAINT [FK_CartStatusCartHistory]
    FOREIGN KEY ([CartStatusId])
    REFERENCES [dbo].[CartStatus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CartStatusCartHistory'
CREATE INDEX [IX_FK_CartStatusCartHistory]
ON [dbo].[CartHistories]
    ([CartStatusId]);
GO

-- Creating foreign key on [StorePickupPointId] in table 'StorePickupPartners'
ALTER TABLE [dbo].[StorePickupPartners]
ADD CONSTRAINT [FK_StorePickupPointStorePickupPartner]
    FOREIGN KEY ([StorePickupPointId])
    REFERENCES [dbo].[StorePickupPoints]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StorePickupPointStorePickupPartner'
CREATE INDEX [IX_FK_StorePickupPointStorePickupPartner]
ON [dbo].[StorePickupPartners]
    ([StorePickupPointId]);
GO

-- Creating foreign key on [StorePickupStatusId] in table 'StorePickupPoints'
ALTER TABLE [dbo].[StorePickupPoints]
ADD CONSTRAINT [FK_StorePickupStatusStorePickupPoint]
    FOREIGN KEY ([StorePickupStatusId])
    REFERENCES [dbo].[StorePickupStatus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StorePickupStatusStorePickupPoint'
CREATE INDEX [IX_FK_StorePickupStatusStorePickupPoint]
ON [dbo].[StorePickupPoints]
    ([StorePickupStatusId]);
GO

-- Creating foreign key on [CartDetailId] in table 'CartDeliveries'
ALTER TABLE [dbo].[CartDeliveries]
ADD CONSTRAINT [FK_CartDetailCartDelivery]
    FOREIGN KEY ([CartDetailId])
    REFERENCES [dbo].[CartDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CartDetailCartDelivery'
CREATE INDEX [IX_FK_CartDetailCartDelivery]
ON [dbo].[CartDeliveries]
    ([CartDetailId]);
GO

-- Creating foreign key on [RiderDetailId] in table 'CartDeliveries'
ALTER TABLE [dbo].[CartDeliveries]
ADD CONSTRAINT [FK_RiderDetailCartDelivery]
    FOREIGN KEY ([RiderDetailId])
    REFERENCES [dbo].[RiderDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RiderDetailCartDelivery'
CREATE INDEX [IX_FK_RiderDetailCartDelivery]
ON [dbo].[CartDeliveries]
    ([RiderDetailId]);
GO

-- Creating foreign key on [CartDeliveryId] in table 'CartActivities'
ALTER TABLE [dbo].[CartActivities]
ADD CONSTRAINT [FK_CartDeliveryCartDeliveryActivity]
    FOREIGN KEY ([CartDeliveryId])
    REFERENCES [dbo].[CartDeliveries]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CartDeliveryCartDeliveryActivity'
CREATE INDEX [IX_FK_CartDeliveryCartDeliveryActivity]
ON [dbo].[CartActivities]
    ([CartDeliveryId]);
GO

-- Creating foreign key on [RiderStatusId] in table 'RiderDetails'
ALTER TABLE [dbo].[RiderDetails]
ADD CONSTRAINT [FK_RiderStatusRiderDetail]
    FOREIGN KEY ([RiderStatusId])
    REFERENCES [dbo].[RiderStatus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RiderStatusRiderDetail'
CREATE INDEX [IX_FK_RiderStatusRiderDetail]
ON [dbo].[RiderDetails]
    ([RiderStatusId]);
GO

-- Creating foreign key on [MasterCityId] in table 'RiderDetails'
ALTER TABLE [dbo].[RiderDetails]
ADD CONSTRAINT [FK_MasterCityRiderDetail]
    FOREIGN KEY ([MasterCityId])
    REFERENCES [dbo].[MasterCities]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MasterCityRiderDetail'
CREATE INDEX [IX_FK_MasterCityRiderDetail]
ON [dbo].[RiderDetails]
    ([MasterCityId]);
GO

-- Creating foreign key on [CartActivityTypeId] in table 'CartActivities'
ALTER TABLE [dbo].[CartActivities]
ADD CONSTRAINT [FK_CartActivityTypeCartActivity]
    FOREIGN KEY ([CartActivityTypeId])
    REFERENCES [dbo].[CartActivityTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CartActivityTypeCartActivity'
CREATE INDEX [IX_FK_CartActivityTypeCartActivity]
ON [dbo].[CartActivities]
    ([CartActivityTypeId]);
GO

-- Creating foreign key on [StoreImgTypeId] in table 'StoreImages'
ALTER TABLE [dbo].[StoreImages]
ADD CONSTRAINT [FK_StoreImgTypeStoreImage]
    FOREIGN KEY ([StoreImgTypeId])
    REFERENCES [dbo].[StoreImgTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StoreImgTypeStoreImage'
CREATE INDEX [IX_FK_StoreImgTypeStoreImage]
ON [dbo].[StoreImages]
    ([StoreImgTypeId]);
GO

-- Creating foreign key on [StorePaymentTypeId] in table 'StorePayments'
ALTER TABLE [dbo].[StorePayments]
ADD CONSTRAINT [FK_StorePaymentTypeStorePayment]
    FOREIGN KEY ([StorePaymentTypeId])
    REFERENCES [dbo].[StorePaymentTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StorePaymentTypeStorePayment'
CREATE INDEX [IX_FK_StorePaymentTypeStorePayment]
ON [dbo].[StorePayments]
    ([StorePaymentTypeId]);
GO

-- Creating foreign key on [StorePaymentStatusId] in table 'StorePayments'
ALTER TABLE [dbo].[StorePayments]
ADD CONSTRAINT [FK_StorePaymentStatusStorePayment]
    FOREIGN KEY ([StorePaymentStatusId])
    REFERENCES [dbo].[StorePaymentStatus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StorePaymentStatusStorePayment'
CREATE INDEX [IX_FK_StorePaymentStatusStorePayment]
ON [dbo].[StorePayments]
    ([StorePaymentStatusId]);
GO

-- Creating foreign key on [CartDetailId] in table 'PaymentDetails'
ALTER TABLE [dbo].[PaymentDetails]
ADD CONSTRAINT [FK_CartDetailPaymentDetail]
    FOREIGN KEY ([CartDetailId])
    REFERENCES [dbo].[CartDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CartDetailPaymentDetail'
CREATE INDEX [IX_FK_CartDetailPaymentDetail]
ON [dbo].[PaymentDetails]
    ([CartDetailId]);
GO

-- Creating foreign key on [PaymentReceiverId] in table 'PaymentDetails'
ALTER TABLE [dbo].[PaymentDetails]
ADD CONSTRAINT [FK_PaymentReceiverPaymentDetail]
    FOREIGN KEY ([PaymentReceiverId])
    REFERENCES [dbo].[PaymentReceivers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PaymentReceiverPaymentDetail'
CREATE INDEX [IX_FK_PaymentReceiverPaymentDetail]
ON [dbo].[PaymentDetails]
    ([PaymentReceiverId]);
GO

-- Creating foreign key on [PaymentStatusId] in table 'PaymentDetails'
ALTER TABLE [dbo].[PaymentDetails]
ADD CONSTRAINT [FK_PaymentStatusPaymentDetail]
    FOREIGN KEY ([PaymentStatusId])
    REFERENCES [dbo].[PaymentStatus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PaymentStatusPaymentDetail'
CREATE INDEX [IX_FK_PaymentStatusPaymentDetail]
ON [dbo].[PaymentDetails]
    ([PaymentStatusId]);
GO

-- Creating foreign key on [PaymentPartyId] in table 'PaymentDetails'
ALTER TABLE [dbo].[PaymentDetails]
ADD CONSTRAINT [FK_PaymentPartyPaymentDetail]
    FOREIGN KEY ([PaymentPartyId])
    REFERENCES [dbo].[PaymentParties]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PaymentPartyPaymentDetail'
CREATE INDEX [IX_FK_PaymentPartyPaymentDetail]
ON [dbo].[PaymentDetails]
    ([PaymentPartyId]);
GO

-- Creating foreign key on [RiderDetailId] in table 'RiderCashDetails'
ALTER TABLE [dbo].[RiderCashDetails]
ADD CONSTRAINT [FK_RiderDetailRiderCashDetail]
    FOREIGN KEY ([RiderDetailId])
    REFERENCES [dbo].[RiderDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RiderDetailRiderCashDetail'
CREATE INDEX [IX_FK_RiderDetailRiderCashDetail]
ON [dbo].[RiderCashDetails]
    ([RiderDetailId]);
GO

-- Creating foreign key on [RiderCashPartyId] in table 'RiderCashDetails'
ALTER TABLE [dbo].[RiderCashDetails]
ADD CONSTRAINT [FK_RiderCashPartyRiderCashDetail]
    FOREIGN KEY ([RiderCashPartyId])
    REFERENCES [dbo].[RiderCashParties]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RiderCashPartyRiderCashDetail'
CREATE INDEX [IX_FK_RiderCashPartyRiderCashDetail]
ON [dbo].[RiderCashDetails]
    ([RiderCashPartyId]);
GO

-- Creating foreign key on [CartDetailId] in table 'RiderCashDetails'
ALTER TABLE [dbo].[RiderCashDetails]
ADD CONSTRAINT [FK_CartDetailRiderCashDetail]
    FOREIGN KEY ([CartDetailId])
    REFERENCES [dbo].[CartDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CartDetailRiderCashDetail'
CREATE INDEX [IX_FK_CartDetailRiderCashDetail]
ON [dbo].[RiderCashDetails]
    ([CartDetailId]);
GO

-- Creating foreign key on [CartDetailId] in table 'StoreKioskOrders'
ALTER TABLE [dbo].[StoreKioskOrders]
ADD CONSTRAINT [FK_CartDetailStoreQueOrder]
    FOREIGN KEY ([CartDetailId])
    REFERENCES [dbo].[CartDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CartDetailStoreQueOrder'
CREATE INDEX [IX_FK_CartDetailStoreQueOrder]
ON [dbo].[StoreKioskOrders]
    ([CartDetailId]);
GO

-- Creating foreign key on [StoreKioskId] in table 'StoreKioskOrders'
ALTER TABLE [dbo].[StoreKioskOrders]
ADD CONSTRAINT [FK_StoreKioskStoreKioskOrder]
    FOREIGN KEY ([StoreKioskId])
    REFERENCES [dbo].[StoreKiosks]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StoreKioskStoreKioskOrder'
CREATE INDEX [IX_FK_StoreKioskStoreKioskOrder]
ON [dbo].[StoreKioskOrders]
    ([StoreKioskId]);
GO

-- Creating foreign key on [StorePickupPointId] in table 'CartDetails'
ALTER TABLE [dbo].[CartDetails]
ADD CONSTRAINT [FK_StorePickupPointCartDetail]
    FOREIGN KEY ([StorePickupPointId])
    REFERENCES [dbo].[StorePickupPoints]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StorePickupPointCartDetail'
CREATE INDEX [IX_FK_StorePickupPointCartDetail]
ON [dbo].[CartDetails]
    ([StorePickupPointId]);
GO

-- Creating foreign key on [CartDetailId] in table 'CartHistories'
ALTER TABLE [dbo].[CartHistories]
ADD CONSTRAINT [FK_CartDetailCartHistory]
    FOREIGN KEY ([CartDetailId])
    REFERENCES [dbo].[CartDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CartDetailCartHistory'
CREATE INDEX [IX_FK_CartDetailCartHistory]
ON [dbo].[CartHistories]
    ([CartDetailId]);
GO

-- Creating foreign key on [CartDetailId] in table 'CartReleases'
ALTER TABLE [dbo].[CartReleases]
ADD CONSTRAINT [FK_CartReleaseCartDetail]
    FOREIGN KEY ([CartDetailId])
    REFERENCES [dbo].[CartDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CartReleaseCartDetail'
CREATE INDEX [IX_FK_CartReleaseCartDetail]
ON [dbo].[CartReleases]
    ([CartDetailId]);
GO

-- Creating foreign key on [UserDetailId] in table 'CartReleases'
ALTER TABLE [dbo].[CartReleases]
ADD CONSTRAINT [FK_CartReleaseUserDetail]
    FOREIGN KEY ([UserDetailId])
    REFERENCES [dbo].[UserDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CartReleaseUserDetail'
CREATE INDEX [IX_FK_CartReleaseUserDetail]
ON [dbo].[CartReleases]
    ([UserDetailId]);
GO

-- Creating foreign key on [StorePickupPointId] in table 'CartReleases'
ALTER TABLE [dbo].[CartReleases]
ADD CONSTRAINT [FK_StorePickupPointCartRelease]
    FOREIGN KEY ([StorePickupPointId])
    REFERENCES [dbo].[StorePickupPoints]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StorePickupPointCartRelease'
CREATE INDEX [IX_FK_StorePickupPointCartRelease]
ON [dbo].[CartReleases]
    ([StorePickupPointId]);
GO

-- Creating foreign key on [UserDetailId] in table 'UserApplications'
ALTER TABLE [dbo].[UserApplications]
ADD CONSTRAINT [FK_UserDetailUserApplication]
    FOREIGN KEY ([UserDetailId])
    REFERENCES [dbo].[UserDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserDetailUserApplication'
CREATE INDEX [IX_FK_UserDetailUserApplication]
ON [dbo].[UserApplications]
    ([UserDetailId]);
GO

-- Creating foreign key on [UserApplicationStatusId] in table 'UserApplications'
ALTER TABLE [dbo].[UserApplications]
ADD CONSTRAINT [FK_UserApplicationStatusUserApplication]
    FOREIGN KEY ([UserApplicationStatusId])
    REFERENCES [dbo].[UserApplicationStatus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserApplicationStatusUserApplication'
CREATE INDEX [IX_FK_UserApplicationStatusUserApplication]
ON [dbo].[UserApplications]
    ([UserApplicationStatusId]);
GO

-- Creating foreign key on [UserApplicationTypeId] in table 'UserApplications'
ALTER TABLE [dbo].[UserApplications]
ADD CONSTRAINT [FK_UserApplicationTypeUserApplication]
    FOREIGN KEY ([UserApplicationTypeId])
    REFERENCES [dbo].[UserApplicationTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserApplicationTypeUserApplication'
CREATE INDEX [IX_FK_UserApplicationTypeUserApplication]
ON [dbo].[UserApplications]
    ([UserApplicationTypeId]);
GO

-- Creating foreign key on [StoreDetailId] in table 'StoreUsers'
ALTER TABLE [dbo].[StoreUsers]
ADD CONSTRAINT [FK_StoreDetailStoreUser]
    FOREIGN KEY ([StoreDetailId])
    REFERENCES [dbo].[StoreDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StoreDetailStoreUser'
CREATE INDEX [IX_FK_StoreDetailStoreUser]
ON [dbo].[StoreUsers]
    ([StoreDetailId]);
GO

-- Creating foreign key on [StoreUserTypeId] in table 'StoreUsers'
ALTER TABLE [dbo].[StoreUsers]
ADD CONSTRAINT [FK_StoreUserTypeStoreUser]
    FOREIGN KEY ([StoreUserTypeId])
    REFERENCES [dbo].[StoreUserTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StoreUserTypeStoreUser'
CREATE INDEX [IX_FK_StoreUserTypeStoreUser]
ON [dbo].[StoreUsers]
    ([StoreUserTypeId]);
GO

-- Creating foreign key on [UserDetailId] in table 'StoreUsers'
ALTER TABLE [dbo].[StoreUsers]
ADD CONSTRAINT [FK_UserDetailStoreUser]
    FOREIGN KEY ([UserDetailId])
    REFERENCES [dbo].[UserDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserDetailStoreUser'
CREATE INDEX [IX_FK_UserDetailStoreUser]
ON [dbo].[StoreUsers]
    ([UserDetailId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
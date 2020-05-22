-- sample data for testing

insert into UserDetails([UserId], [Name], [Address], [Email], [Mobile], [Remarks], [UserStatusId], [MasterCityId], [MasterAreaId] )
values ('97a5778d-4ef4-4a48-b920-eeac9aee9c50', 'Admin', 'Davao City', 'admin@gmail.com', '0912-345-6789', 'Admin User', 1, 1, 1);

insert into UserDetails([UserId], [Name], [Address], [Email], [Mobile], [Remarks], [UserStatusId], [MasterCityId], [MasterAreaId] )
values ('c33996c3-a2ff-4c6c-b330-61ce70931d9b', 'Shopper', 'Davao City', 'shopper@gmail.com', '0912-345-6789', 'Shopper User', 1, 1, 1);

-- create stores --
insert into StoreDetails([LoginId],[Name],[Address],[Remarks],[StoreStatusId],[StoreCategoryId],[MasterCityId],[MasterAreaId]) 
values	(6, 'NCCC Mall', 'Matina Crossing, Davao City','', 1, 3, 1, 1),
		(7, 'SM Mall', 'Ecoland, Davao City','', 1, 3, 1, 1),
		(8, 'Gaisano Mall', 'Sta Ana, Davao City','', 1, 3, 2, 1);

-- store images --
insert into StoreImages([StoreDetailId],[StoreImgTypeId],[ImageUrl]) values
(1,1,'https://gidci.com.ph/wp-content/uploads/2015/06/NCCC.jpg'),
(2,1,'https://upload.wikimedia.org/wikipedia/commons/7/76/SM_Supermalls_Logo.png'),
(3,1,'https://media.glassdoor.com/sqll/656853/gaisano-mall-squarelogo-1471522333300.png')
;

-- store pickup locations --
insert into StorePickupPoints([StoreDetailId],[Address],[Remarks],[StorePickupStatusId])
values  (1,'Matina Crossing, Davao City','NA',1),
		(2,'Ecoland, Davao City','NA',1),
		(3,'Sta Ana, Davao City','NA',1),
		--second address --
		(1,'Buhangin, Davao City','NA',1),	
		(2,'Lanang, Davao City','NA',1),	
		(3,'Toril, Davao City','NA',1);

insert into StorePickupPartners([StorePickupPointId],[StoreDetailId])
values	(1,1),(2,2),(3,3),
		(4,1),(5,2),(6,3);

-- item Master --
insert into ItemMasters([Name])
values	('Loaf Bread'), ('Rice 1 kilo'), ('Sardines'), ('Chicken'), ('Beef'), ('Pork'), ('CornBeef Canned'), ('Century Tuna'), ('Powerdered Milk'), ('Coca-Cola 1 Liter'),
		('Colgate TootPaste'),('Safguard Soap'),('Clear Shampoo'),('Head & Shoulders'),('Cooking Oil');

insert into ItemMasterCategories([ItemCategoryId],[ItemMasterId])
values (14, 1),(14, 2),(3, 3),(14,4),(14,5),(14,6),(1,7),(4,8),(14,9),(5,10);

-- create items for store --
insert into StoreItems([ItemMasterId], [StoreDetailId], [UnitPrice]) 
values	( 1, 1, 15.00 ), ( 2, 1, 52.00 ), ( 3, 1, 18.00 ), ( 4, 1, 120.00 ),( 5, 1, 155.00 ), ( 7, 1, 140.00 ), ( 8, 1, 18.00 ), ( 9, 1, 25.00 ), ( 10, 1, 45.00 ), ( 11, 1, 75.00 ),
		( 1, 2, 15.75 ), ( 2, 2, 50.25 ), ( 3, 2, 18.25 ), ( 4, 2, 125.00 ),( 5, 2, 165.00 ), 
		( 7, 3, 13.00 ), ( 8, 3, 19.75 ), ( 9, 3, 27.25 ), ( 10, 3, 42.25 ), ( 11, 3, 72.50 );

-- item images --
insert into ItemImages([ItemMasterID],[ImageUrl]) values 
(11, 'https://zumia.co.zw/wp-content/uploads/2019/08/colgate.jpg'),
(10, 'https://redbottle.com.au/wp-content/uploads/2018/05/Coca-Cola-1_25L.jpg'),
(9,	'https://www.homeshop.ph/image/cache/catalog/Products/Dairy/Milk-Powder/Bear-Brand-Powdered-Milk-With-Iron-700g-500x500-product_popup.png'),
(8, 'https://www.freshdealites.com/var/images/product/300.300/Grocery/Canned%20Seafood/Grocery_Century-Tuna-Flakes-Hot-and-Spicy-420g.jpg'),
(7, 'https://shop.smmarkets.ph/pub/media/catalog/product/cache/0f831c1845fc143d00d6d1ebc49f446a/1/0/10006940.png'),
(5, 'https://thumbs.dreamstime.com/b/packaged-t-bone-steak-15165979.jpg'),
(4, 'https://pngimage.net/wp-content/uploads/2018/06/packed-chicken-meat-png-2.png'),
(3, 'https://cdn.shopify.com/s/files/1/0147/9445/7136/products/555_Sardines_In_Tomato_Sauce_Plain_155g__66573.1536919367_grande.jpg'),
(2, 'https://cdn.pixabay.com/photo/2016/02/29/05/46/brown-rice-1228099_960_720.jpg'),
(1, 'https://cdn.shopify.com/s/files/1/2889/2216/products/Gardenia_High_Fiber_Whole_Wheat_Loaf_Bread_600g_800x.png')
;


--- Create Sample Cart --
insert into CartDetails([UserDetailId], [StoreDetailId], [CartStatusId], [StorePickupPointId], [DtPickup], [DeliveryType])
values (1, 1, 2, 1, '05/03/2020 5:00 PM', 'Pickup');
insert into CartDetails([UserDetailId], [StoreDetailId], [CartStatusId], [StorePickupPointId], [DtPickup], [DeliveryType])
values (1, 1, 2, 1, '05/03/2020 3:00 PM', 'Pickup');

--- Create Cart Items ---
insert into CartItems([CartDetailId],[StoreItemId],[ItemQty],[ItemOrder],[CartItemStatusId],[Remarks1],[Remarks2])
values (1, 7, 1, '1', 1, '', ''), (1, 8, 2, '1', 1, '', '');
insert into CartItems([CartDetailId],[StoreItemId],[ItemQty],[ItemOrder],[CartItemStatusId],[Remarks1],[Remarks2])
values (2, 7, 2, '1', 1, '', ''), (1, 8, 2, '1', 1, '', '');


insert into PaymentDetails(Amount,CartDetailId,dtPayment,PaymentPartyId,PartyInfo,PaymentReceiverId,ReceiverInfo,PaymentStatusId)
values (120, 1, '05/02/2020', 1, 'party info', 1, 'receiver info', 1 );


--Rider sample --
insert into RiderDetails([UserId],[Name],[Address],[Mobile],[Remarks],[RiderStatusId],[MasterCityId],[Mobile2] )
values ('5','John Doe', 'Davao City', '0912-345-6789', 'None', 1, 1, '0998-765-4321');

-- Store Payment --
insert into StorePayments([StoreDetailId],[dtPayment],[Amount],[StorePaymentTypeId],[Remarks],[dtPosted],[StorePaymentStatusId]) values
(1,'05/09/2020 3:00 PM', '2500', 1, 'Pending test', '05/09/2020 3:45 PM', 1),
(2,'05/09/2020 3:00 PM', '2500', 1, 'Accepted test', '05/09/2020 3:45 PM', 2);


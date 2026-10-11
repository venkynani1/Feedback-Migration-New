use[HelixMaster]
insert into M_COUNTRY Values('LTU','Lithuania','1','36135',GETDATE())


Insert into  m_master Values ('C','LTU','Lithuania','1','36135',GETDATE())


use[VISA]

Insert into M_CITY_MASTER (S_Country_Code,S_CIty_Description,S_CITY_NAME)
Values
('LTU','Kaunas(KUN)','Kaunas'),
('LTU','Palanga(PLQ)','Palanga'),
('LTU','Siauliai(SQQ)','Siauliai'),
('LTU','Vilnius(VNO)','Vilnius'),
('LTU','Barysiai(HLJ)','Barysiai'),
('LTU','Klaipeda(KLI)','Klaipeda'),
('LTU','Panevezys(PNV)','Panevezys')



use[Travel]

Insert into M_CITY_MASTER (S_Country_Code,S_CIty_Description,S_CITY_NAME)
Values
('LTU','Kaunas(KUN)','Kaunas'),
('LTU','Palanga(PLQ)','Palanga'),
('LTU','Siauliai(SQQ)','Siauliai'),
('LTU','Vilnius(VNO)','Vilnius'),
('LTU','Barysiai(HLJ)','Barysiai'),
('LTU','Klaipeda(KLI)','Klaipeda'),
('LTU','Panevezys(PNV)','Panevezys')



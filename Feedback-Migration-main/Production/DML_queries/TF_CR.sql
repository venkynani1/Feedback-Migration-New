
BEGIN TRAN
Alter table Feedback add  R5 int
COMMIT


BEGIN TRAN
update Feedback set R5 ='0'
COMMIT

BEGIN TRAN

Insert into L1_QUESTIONS Values ('Training Administration','21','1')
Insert into L1_QUESTIONS Values ('Training Design & Methodology','22','2')
Insert into L1_QUESTIONS Values ('Trainer Delivery','23','3')
   
COMMIT
 select distinct pg.CA_Name as CName,
 --((select id from customer_account where customer_account_name=pg.CA_Name)+',') as CNId, 
 
 (select COUNT(*) from CSS_Triggering where PG='G' and projectgroup in 
  (select distinct Project_group_id from PG_Project  where
   Project_group_id = pg.project_group_id 
  
  --- Customer_Name=pg.Ca_Name
   
    and  1=1  )) as RSent, 
   
    (select COUNT(*) from Survey_Details where PType='PG' and signature='Completed' 
    and project_id in (select distinct project_id  from Project where Customer_Name=pg.Ca_Name and  1=1  )) as RReceived,
    
      (select SUM(convert(numeric(18),case when rating=1 then 7 when rating=2 then 6 when rating=3 then 5
       when rating=4 then 4  when rating=5 then 3 end)) from survey_rating  
       where question_name='Your Overall Experience with Hexaware' 
       and survey_id in (select survey_id from Survey_Details  where PType='P' 
       and signature='Completed' and project_id in (select distinct project_id from Project 
        where Customer_Name=pg.Ca_Name and  1=1  ))) as AvgScore  from project_group pg 
        
         join PG_customer_preference cp on cp.pg_id=pg.project_group_id 
         join PG_Css_milestone cm on cm.pg_id=pg.project_group_id  
         join PG_Project PGP on PGP.project_group_id=pg.project_group_id 
          join PG_General_Info gi on gi.pg_id=pg.project_group_id  
          join PG_Representative_details rd on rd.pg_id=pg.project_group_id 
           join Survey_Details sd on sd.project_id=pg.project_group_id 
            join CSS_Triggering A on A.projectgroup=pg.project_group_id 
            
           and sd.PType='PG'
           
            and sd.signature='Completed' and sd.user_id=rd.PG_Representative_Id 
            and sd.milestone_id=cm.PG_CSSTM_Id  where 1=1  and pg.ca_name in 
            (select distinct CustomerName from wsemdb.wsem.dbo.v_hex_employee_project_access
              where projectstatus='Active' and employeecode='30391') and 1=1  and
               (select top 1 trigger_date from CSS_Triggering where pg='G' and projectgroup=pg.Project_Group_Id 
               and representative_id=rd.pg_representative_id and milestone_id=cm.pg_csstm_id 
               order by CSS_Triggering_Id desc) between CONVERT(DATETIME,  '01/01/2017', 103)
                and CONVERT(DATETIME,  '01/01/2018', 103)  
                
                
        
        select * from CSS_Triggering where PG='G'
   and Milestone_id =18    
   
   
   
   select * from survey_rating where survey_id ='862'
   
   Delete from survey_rating where id  in('10813','10814','10816','10819','10817')
  
   
   
   
  
   
   
    
         
                
             
             
         
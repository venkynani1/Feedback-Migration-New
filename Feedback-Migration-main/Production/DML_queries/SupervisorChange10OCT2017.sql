SELECT Distinct LTRIM(RTRIM(EmpAss.S_EMPLOYEE_ID)) As S_EMPLOYEE_ID,    

   Ltrim(Rtrim(Exch.S_EMPLOYEE_FNAME)) +'  '+Ltrim(Rtrim(Exch.S_EMPLOYEE_LNAME))+ '('+rtrim(EmpAss.S_EMPLOYEE_ID) +')' As S_EMPLOYEE_NAME,    

  EmpAss.S_ROLE_ID , prj.S_PA_PROJECT_ID,prj.S_PROJECT_DESCRIPTION,prj.s_client_id,prj.s_fin_project_id,EmpAss.D_EFFECTIVE_START_DATE,EmpAss.D_EFFECTIVE_END_DATE  

  from dbo.M_EMPLOYEE_EXCHANGE Exch,dbo.M_EMPLOYEE_ASSIGNMENT EmpAss,   

  dbo.M_Project_Register prj  

  where Exch.S_EMPLOYEE_ID = EmpAss.S_EMPLOYEE_ID    

  AND  Exch.s_employee_status_id = 'A'    

  AND  Empass.S_Project_Id='PRJ-4182'  

  and Empass.s_role_id in('PM')  

  
  order by EmpAss.D_EFFECTIVE_END_DATE desc

  --AND GETDATE() BETWEEN EmpAss.D_EFFECTIVE_START_DATE AND isnull(EmpAss.D_EFFECTIVE_END_DATE,getdate())
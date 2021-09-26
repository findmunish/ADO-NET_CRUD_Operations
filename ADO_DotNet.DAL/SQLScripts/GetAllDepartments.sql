-- /****** Object:  Database:  [AdoDotNetEmpDeptDb]   ******/

Create procedure usp_getDepartments        
as        
Begin        
    select *        
    from Departments     
    order by Id   
End